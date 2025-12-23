using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLayer.Core.Services;
using NLayer.Service.Services;
using System.Linq;
using System.Net.Mail;
using System.Net;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Operations;
using formneo.core.Services;
using formneo.service.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.Budget.SF;

namespace formneo.workflow
{
    public class WorkFlowExecute
    {
        WorkFlowParameters _parameters;
        public async Task<WorkflowHead> StartAsync(WorkFlowDto dto, WorkFlowParameters parameters, string payloadJson)
        {

            _parameters = parameters;


            
            List<WorkflowItem> workFlowItems = new List<WorkflowItem>();

            WorkflowHead head = new WorkflowHead();
            var workFlowDefination = await parameters._workFlowDefination.GetByIdGuidAsync(new Guid(dto.WorkFlowDefinationId.ToString()));

            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            };


            if (workFlowDefination == null)
            {
                throw new Exception($"WorkflowDefinition with id '{dto.WorkFlowDefinationId}' not found");
            }
            Workflow workflow = JsonConvert.DeserializeObject<Workflow>(workFlowDefination!.Data.Defination!, settings);

            workflow._parameters = parameters;

            HashSet<string> nodeIds = new HashSet<string>(workflow.Nodes.Select(node => node.Id));

            workflow.Edges = workflow.Edges.Where(edge => nodeIds.Contains(edge.Source) && nodeIds.Contains(edge.Target)).ToList();
            workflow._workFlowItems = new List<WorkflowItem>();

            head.WorkFlowInfo = dto.WorkFlowInfo;

            head.WorkFlowDefinationJson = workFlowDefination!.Data.Defination!;
            //Daha önce var devam et
            if (!String.IsNullOrEmpty(dto.WorkFlowId))
            {
                Guid g = new Guid(dto.WorkFlowId);
                head = await parameters.workFlowService.GetWorkFlowWitId(g);
                workflow._workFlowItems = head.workflowItems;
                var startNode = head.workflowItems.Where(e => e.Id == new Guid(dto.NodeId)).FirstOrDefault();

                if (startNode == null)
                {
                    throw new Exception($"WorkflowItem with id '{dto.NodeId}' not found");
                }

                workflow._HeadId = new Guid(dto.WorkFlowId);

                // Continue metoduna payloadJson'ı da geç (FormData için)
                // Artık Input yerine Action kullanılıyor (buton bazlı sistem)
                // Start mantığı ile aynı - var olan forma devam ediyor
                string actionToPass = dto.Action ?? "";
                workflow.Continue(startNode, startNode.NodeId, dto.UserName, actionToPass, head, null, payloadJson);

                Utils utils = new Utils();
                FormInstance formInstanceToSave = null;
                FormItems formItemToSave = null;
                
                // Sadece FormTaskNode için FormInstance ve FormItem işlemleri yapılır
                // ApproverNode (UserTask) için sadece WorkflowItem durumu yönetilir (ApproveItem artık kullanılmıyor)
                if (startNode.NodeType == "formTaskNode")
                {
                    // FormItems'ı bul ve kaydet (sadece FormTaskNode için)
                    WorkflowItem formTaskWorkflowItem = null;
                    foreach (var item in head.workflowItems)
                    {
                        // FormTaskNode kontrolü - sadece formTaskNode için FormInstance güncelle
                        if (item.NodeType == "formTaskNode" && item.formItems != null && item.formItems.Count > 0)
                        {
                            formItemToSave = item.formItems.FirstOrDefault();
                            formTaskWorkflowItem = item;
                            // Kullanıcı mesajını ekle
                            if (formItemToSave != null && dto.Note != null)
                            {
                                formItemToSave.FormUserMessage = dto.Note;
                            }
                            // Form verilerini kaydet: payloadJson (FormTaskNode'dan geliyorsa FormData gelir)
                            if (formItemToSave != null && !string.IsNullOrEmpty(payloadJson))
                            {
                                formItemToSave.FormData = payloadJson;
                            }
                            break;
                        }
                    }

                    // FormId'yi WorkflowHead'e set et (tek ana form için)
                    if (formItemToSave != null && formItemToSave.FormId.HasValue)
                    {
                        head.FormId = formItemToSave.FormId;
                    }

                    // FormInstance'ı güncelle veya oluştur (her zaman son/güncel form verisi)
                    // Sadece FormTaskNode tamamlandığında FormInstance güncellenir
                    if (formTaskWorkflowItem != null && formItemToSave != null && !string.IsNullOrEmpty(payloadJson))
                {
                    // FormDesign'i belirle: FormItem'dan veya FormId varsa Form tablosundan
                    string formDesign = formItemToSave.FormDesign;
                    
                    // Eğer FormDesign boşsa ve FormId varsa, Form tablosundan al
                    if (string.IsNullOrEmpty(formDesign) && formItemToSave.FormId.HasValue && parameters._formService != null)
                    {
                        try
                        {
                            var form = await parameters._formService.GetByIdStringGuidAsync(formItemToSave.FormId.Value);
                            if (form != null && !string.IsNullOrEmpty(form.FormDesign))
                            {
                                formDesign = form.FormDesign;
                                // FormItem'daki FormDesign'i de güncelle
                                formItemToSave.FormDesign = formDesign;
                            }
                        }
                        catch
                        {
                            // Form bulunamazsa devam et
                        }
                    }
                    
                    // Mevcut FormInstance'ı kontrol et
                    var existingFormInstanceQuery = parameters._formInstanceService.Where(e => e.WorkflowHeadId == head.Id);
                    var existingFormInstance = await existingFormInstanceQuery.FirstOrDefaultAsync();

                    string userNameSurname = utils.GetNameAndSurnameAsync(dto.UserName).ToString();

                    if (existingFormInstance != null)
                    {
                        // Mevcut FormInstance'ı güncelle
                        existingFormInstance.FormData = payloadJson; // FormData (payloadJson)
                        existingFormInstance.FormDesign = formDesign; // Güncellenmiş FormDesign'i kullan
                        existingFormInstance.FormId = formItemToSave.FormId;
                        existingFormInstance.UpdatedBy = dto.UserName;
                        existingFormInstance.UpdatedByNameSurname = userNameSurname;
                        existingFormInstance.UpdatedDate = DateTime.Now;
                        formInstanceToSave = existingFormInstance;
                    }
                    else
                    {
                        // Yeni FormInstance oluştur
                        formInstanceToSave = new FormInstance
                        {
                            WorkflowHeadId = head.Id,
                            FormId = formItemToSave.FormId,
                            FormDesign = formDesign, // Güncellenmiş FormDesign'i kullan
                            FormData = payloadJson, // FormData (payloadJson)
                            UpdatedBy = dto.UserName,
                            UpdatedByNameSurname = userNameSurname
                        };
                    }
                    }
                }
                // ApproverNode (UserTask) için sadece WorkflowItem durumu yönetilir (ApproveItem artık kullanılmıyor)

                // AlertNode kontrolü - Continue işleminden sonra yeni bir alertNode'a gelinirse rollback yapılacak
                // AlertNode sadece error ve warning için kullanılır, success ve info mesajları normal component'te gösterilir
                // AlertNode'a gelince işlem durdurulur ve rollback yapılır
                var pendingAlertNode = head.workflowItems.FirstOrDefault(item => 
                    item.NodeType == "alertNode" && item.workFlowNodeStatus == WorkflowStatus.Pending);
                
                // Eğer alertNode'a gelindi ise, rollback yap (işlemi geri al)
                if (pendingAlertNode != null)
                {
                    // Rollback: WorkflowHead ve WorkflowItem'ları kaydetme
                    // Alert bilgilerini response'da döndürmek için head'i işaretle
                    // Id'yi Empty yap = rollback flag (response builder bunu algılayacak)
                    head.Id = Guid.Empty; // Rollback flag
                    head.workFlowStatus = WorkflowStatus.Pending;
                    return head; // Alert bilgileriyle birlikte döndür, ama veritabanına kaydetme
                }

                // Continue metodunda head.workflowItems'i gönder (FormItems'ları kaydetmek için)
                // Sadece FormTaskNode için FormInstance ve FormItem kaydedilir
                var result = await parameters.workFlowService.UpdateWorkFlowAndRelations(head, head.workflowItems, null, formItemToSave, formInstanceToSave);

                if (result != null)
                {

                    if (head.workFlowStatus == WorkflowStatus.Completed)
                    {
                        string getUniqApproveId = head.UniqNumber.ToString();

                        SendMail(MailStatus.OnaySureciTamamlandı, head.CreateUser, "", getUniqApproveId);
                    }

                    // ApproveItem artık kullanılmıyor, mail gönderme işlemi kaldırıldı
                    // Mail gönderme işlemleri gerekirse WorkflowItem durumuna göre yapılabilir
                }
                return null;
            }
            else
            {

                head = new WorkflowHead();
                head.WorkflowName = workFlowDefination.Data.WorkflowName;
                head.CreateUser = dto.UserName;
                head.WorkFlowInfo = dto.WorkFlowInfo;
                head.WorkFlowDefinationId = dto.WorkFlowDefinationId;

                head.WorkFlowDefinationJson = workFlowDefination!.Data.Defination!;
                head.workFlowStatus = WorkflowStatus.InProgress;

                // Action'ı workflow'a geçir
                // NOT: Action Start metodunda set edilir ve formNode'a gelince kullanılır
                string actionToPass = dto.Action ?? "";
                workflow.Start(dto.UserName, payloadJson, actionToPass);

                head.workflowItems = workflow._workFlowItems;

                // FormTaskNode'dan FormId'yi al ve WorkflowHead'e set et (tek ana form için)
                if (head.workflowItems != null)
                {
                    var formTaskNodeItem = head.workflowItems.FirstOrDefault(item => 
                        item.NodeType == "formTaskNode" && item.formItems != null && item.formItems.Count > 0);
                    if (formTaskNodeItem != null && formTaskNodeItem.formItems != null)
                    {
                        var formItem = formTaskNodeItem.formItems.FirstOrDefault();
                        if (formItem != null && formItem.FormId.HasValue)
                        {
                            head.FormId = formItem.FormId;
                        }
                    }
                }

                // AlertNode kontrolü - AlertNode'a gelirse rollback yapılacak
                // AlertNode sadece error ve warning için kullanılır, success ve info mesajları normal component'te gösterilir
                // AlertNode'a gelince işlem durdurulur ve rollback yapılır
                    var pendingAlertNode = head.workflowItems.FirstOrDefault(item => 
                    item.NodeType == "alertNode" && item.workFlowNodeStatus == WorkflowStatus.Pending);
                
                // Eğer alertNode'a gelindi ise, rollback yap (işlemi geri al)
                if (pendingAlertNode != null)
                {
                    // Rollback: WorkflowHead ve WorkflowItem'ları kaydetme
                    // Alert bilgilerini response'da döndürmek için head'i işaretle
                    // Id'yi Empty yap = rollback flag (response builder bunu algılayacak)
                    head.Id = Guid.Empty; // Rollback flag
                    head.workFlowStatus = WorkflowStatus.Pending;
                    return head; // Alert bilgileriyle birlikte döndür, ama veritabanına kaydetme
                }

                var result = await parameters.workFlowService.AddAsync(head);

                // FormItems'ları kaydet (UpdateWorkFlowAndRelations ile - ApproveItems mantığı gibi)
                // FormTaskNode'a gelindiğinde FormItems oluşturuldu ve workFlowItem.formItems'e eklendi
                // UpdateWorkFlowAndRelations içinde FormItems'lar kaydedilecek
                
                // FormTaskNode için FormInstance oluştur (ilk kez oluşturulduğunda)
                FormItems formItemToSave = null;
                FormInstance formInstanceToSave = null;
                
                if (result != null && result.workflowItems != null)
                {
                    // FormTaskNode kontrolü - FormTaskNode'a gelindiğinde FormInstance oluştur
                    foreach (var item in result.workflowItems)
                    {
                        if (item.NodeType == "formTaskNode" && item.formItems != null && item.formItems.Count > 0)
                        {
                            formItemToSave = item.formItems.FirstOrDefault();
                            if (formItemToSave != null)
                            {
                                // FormDesign'i belirle: FormItem'dan veya FormId varsa Form tablosundan
                                string formDesign = formItemToSave.FormDesign;
                                
                                // Eğer FormDesign boşsa ve FormId varsa, Form tablosundan al
                                // Bu işlem async olduğu için Start metodunda yapılır (ExecuteFormTaskNode senkron)
                                if (string.IsNullOrEmpty(formDesign) && formItemToSave.FormId.HasValue && parameters._formService != null)
                                {
                                    try
                                    {
                                        var form = await parameters._formService.GetByIdStringGuidAsync(formItemToSave.FormId.Value);
                                        if (form != null && !string.IsNullOrEmpty(form.FormDesign))
                                        {
                                            formDesign = form.FormDesign;
                                            // FormItem'daki FormDesign'i de güncelle
                                            formItemToSave.FormDesign = formDesign;
                                        }
                                    }
                                    catch
                                    {
                                        // Form bulunamazsa devam et
                                    }
                                }
                                
                                // FormInstance oluştur (FormDesign boş olsa bile oluştur, sonra güncellenebilir)
                                Utils utils = new Utils();
                                string userNameSurname = utils.GetNameAndSurnameAsync(dto.UserName).ToString();
                                
                                // FormData her zaman gelir (formdan butonla action ve form verisi gelir)
                                // FormItem'a da FormData'yı kaydet
                                if (!string.IsNullOrEmpty(payloadJson))
                                {
                                    formItemToSave.FormData = payloadJson;
                                }
                                
                                // FormInstance oluştur (FormDesign boş olsa bile, sonra güncellenebilir)
                                formInstanceToSave = new FormInstance
                                {
                                    WorkflowHeadId = result.Id,
                                    FormId = formItemToSave.FormId,
                                    FormDesign = formDesign, // FormDesign varsa kullan, yoksa null olabilir
                                    FormData = payloadJson, // FormData her zaman gelir
                                    UpdatedBy = dto.UserName,
                                    UpdatedByNameSurname = userNameSurname
                                };
                            }
                            break;
                        }
                    }
                    
                    await parameters.workFlowService.UpdateWorkFlowAndRelations(result, result.workflowItems, null, formItemToSave, formInstanceToSave);
                }

                if (result != null)
                {
                    // ApproveItem artık kullanılmıyor, mail gönderme işlemi kaldırıldı
                    // Mail gönderme işlemleri gerekirse WorkflowItem durumuna göre yapılabilir
                    
                    // Eğer workflow execution sırasında alertNode'a gelip pending durumunda kaldıysa
                    // (ama error/warning değilse, sadece info ise)
                    if (pendingAlertNode != null)
                    {
                        var workflowJson = JObject.Parse(head.WorkFlowDefinationJson);
                        var nodes = workflowJson["nodes"] as JArray;
                        var alertNodeDef = nodes?.FirstOrDefault(n => n["id"]?.ToString() == pendingAlertNode.NodeId);
                        var alertType = alertNodeDef?["data"]?["type"]?.ToString()?.ToLower() ?? "info";
                        
                        // Info tipindeki alert'ler için rollback yapma, sadece pending olarak işaretle
                        if (alertType == "info" || alertType == "success")
                        {
                            result.workFlowStatus = WorkflowStatus.Pending;
                            result.CurrentNodeId = pendingAlertNode.NodeId;
                        }
                    }
                }

                    return result;
            }
        }

            
        // JSON verisini Workflow nesnesine dönüştüren metod
        private static Workflow ConvertJsonToWorkflow(string jsonData)
        {
            Workflow workflow = JsonConvert.DeserializeObject<Workflow>(jsonData.Replace(";", ""));
            return workflow;
        }

        // JSON verisi
        private static void SendMail(MailStatus status, string UserName, string SendApproverSurname, string approveId, string text = "")
        {

            string senderEmail = "support@formneo.com";
            string senderPassword = "Sifre2634@!!";

            // E-posta alıcısının adresi
            string toEmail = "murat.merdogan@formneo.com";

            // E-posta başlığı
            string subject = "Onay Süreci Hakkında Bilgilendirme";

            // Dinamik veriler
            string kullaniciAdi = "";
            string numaralar = "12345, 67890";
            string onayDurumu = "";
            if (status == MailStatus.OnayınızaSunuldu)
            {

                kullaniciAdi = SendApproverSurname;
                onayDurumu = "Onayınıza Sunuldu"; // veya "Onaylandı" / "Reddedildi"
            }

            if (status == MailStatus.Reddedildi)
            {
                kullaniciAdi = UserName;
                onayDurumu = "Reddedildi"; // veya "Onaylandı" / "Reddedildi"
            }

            if (status == MailStatus.OnaySureciBasladi)
            {

                kullaniciAdi = UserName;
                onayDurumu = "Onay Süreci Başladı"; // veya "Onaylandı" / "Reddedildi"
            }

            if (status == MailStatus.OnaySureciTamamlandı)
            {
                kullaniciAdi = UserName;
                onayDurumu = "İş Akışı Süreci Tamamlandı"; // veya "Onaylandı" / "Reddedildi"
            }
            // HTML şablonu
            string body = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9; }}
        h1 {{ color: #0056b3; }}
        .details {{ background-color: #fff; padding: 15px; border-radius: 5px; border: 1px solid #ddd; }}
        .footer {{ margin-top: 20px; font-size: 12px; color: #777; }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Onay Süreci Bilgilendirmesi</h1>
        <p>Sayın <strong>{kullaniciAdi}</strong>,</p>
        <p>Aşağıda belirtilen sürece ait onay işlemi hakkında bilgilendirme:</p>

        <div class='details'>
            <p><strong>Onay ID:</strong> {approveId}</p>

            <p><strong>Durum:</strong> <span style='color:{(onayDurumu == "Onaylandı" ? "green" : "red")};'>{onayDurumu}</span></p>
            <p><strong>Süreç Detayı: {text}</strong></p>
  
        </div>

        <p>Gerekli aksiyonları zamanında almanızı rica ederiz.</p>
        <p style=""color:#0056b3;""><strong>formneo Destek Sistemi: https://support.formneo-tech.com/</strong></p>
        
        <div class='footer'>
            <p>Bu e-posta otomatik olarak oluşturulmuştur, lütfen yanıtlamayınız.</p>
            <p><strong>formneo</strong></p>
        </div>
    </div>
</body>
</html>";


            // SMTP (Simple Mail Transfer Protocol) ayarları
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            // E-posta oluştur
            MailMessage mail = new MailMessage(senderEmail, UserName, subject, body)
            {
                IsBodyHtml = true
            };

            //mail.To.Add(new MailAddress(UserName));

            try
            {
                // E-postayı gönder
                smtpClient.Send(mail);
                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderme sırasında bir hata oluştu: " + ex.Message);
            }

        }


        public enum MailStatus
        {
            Onaylandi = 1,
            Reddedildi = 2,
            OnayınızaSunuldu = 3,
            OnaySureciTamamlandı = 4,
            OnaySureciBasladi = 5
        }
    }
}