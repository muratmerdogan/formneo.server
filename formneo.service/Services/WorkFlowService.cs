using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository;
using formneo.repository.Repositories;
using formneo.repository.UnitOfWorks;
using static System.Formats.Asn1.AsnWriter;

namespace formneo.service.Services
{
    public class WorkFlowService : Service<WorkflowHead>, IWorkFlowService
    {

        private readonly IWorkflowRepository _workFlowRepository;
        private readonly IWorkFlowItemRepository _workFlowItemRepository;
        private readonly IApproveItemsRepository _approveItemsRepository;
        private readonly IFormItemsRepository _formItemsRepository;
        private readonly IFormInstanceRepository _formInstanceRepository;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkFlowService(IGenericRepository<WorkflowHead> repository, IUnitOfWork unitOfWork, IMapper mapper, IWorkflowRepository workFlowRepository, IWorkFlowItemRepository workFlowItemRepository, IApproveItemsRepository approveItemsRepository, IFormItemsRepository formItemsRepository, IFormInstanceRepository formInstanceRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;

           //var s=  workFlowItemRepository.GetAll();
 
            _workFlowRepository = workFlowRepository;
            _workFlowItemRepository = workFlowItemRepository;
            _approveItemsRepository = approveItemsRepository;
            _formItemsRepository = formItemsRepository;
            _formInstanceRepository = formInstanceRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<WorkflowHead> GetWorkFlowWitId(Guid guid)
        {

            var result = await _workFlowRepository.GetWorkFlowWitId(guid);
            return result;
        }

        public async Task<bool> UpdateWorkFlowAndRelations(WorkflowHead head, List<WorkflowItem> workflowItems, ApproveItems approveItems = null, FormItems formItem = null, FormInstance formInstance = null)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                // WorkflowHead'i güncelle - tracking sorununu önlemek için yeniden yükle ve property'leri kopyala
                if (head.Id != Guid.Empty)
                {
                    // Tracking edilmiş entity'yi yükle (RowVersion dahil, workflowItems Include ile)
                    // UnitOfWork üzerinden context'e eriş ve Include ile tracking ile yükle
                    var unitOfWork = _unitOfWork as formneo.repository.UnitOfWorks.UnitOfWork;
                    WorkflowHead existingHead;
                    if (unitOfWork != null)
                    {
                        // Context üzerinden tracking ile yükle (workflowItems Include ile)
                        existingHead = await unitOfWork._context.WorkflowHead
                            .Include(e => e.workflowItems)
                            .FirstOrDefaultAsync(x => x.Id == head.Id);
                    }
                    else
                    {
                        // Fallback: Normal GetByIdStringGuidAsync kullan
                        existingHead = await _workFlowRepository.GetByIdStringGuidAsync(head.Id);
                    }
                    if (existingHead == null)
                    {
                        throw new Exception($"WorkflowHead with id '{head.Id}' not found");
                    }
                    
                    // Sadece değişen property'leri kopyala (tracking edilmiş entity'ye)
                    // EF Core otomatik olarak değişiklikleri algılayacak, Update() çağrısına gerek yok
                    // Bu şekilde RowVersion concurrency token korunur ve concurrency hatası önlenir
                    existingHead.WorkflowName = head.WorkflowName;
                    existingHead.CurrentNodeId = head.CurrentNodeId;
                    existingHead.CurrentNodeName = head.CurrentNodeName;
                    existingHead.workFlowStatus = head.workFlowStatus;
                    existingHead.WorkFlowInfo = head.WorkFlowInfo;
                    existingHead.FormId = head.FormId;
                    existingHead.WorkFlowDefinationId = head.WorkFlowDefinationId;
                    existingHead.WorkFlowDefinationJson = head.WorkFlowDefinationJson;
                    
                    // Update() çağrısına GEREK YOK!
                    // existingHead tracking edilmiş durumda, property değişiklikleri otomatik algılanacak
                    // Update() çağrısı tüm property'leri Modified olarak işaretler ve RowVersion sorununa neden olur
                    
                    // WorkflowItems'ları da güncelle (tracking edilmiş items ile eşleştir)
                    if (existingHead.workflowItems != null && workflowItems != null)
                    {
                        foreach (var incomingItem in workflowItems)
                        {
                            // Tracking edilmiş WorkflowItem'ı bul
                            var existingItem = existingHead.workflowItems.FirstOrDefault(e => e.Id == incomingItem.Id);
                            if (existingItem != null)
                            {
                                // Property'leri kopyala (tracking edilmiş entity'ye)
                                existingItem.NodeName = incomingItem.NodeName;
                                existingItem.NodeType = incomingItem.NodeType;
                                existingItem.NodeDescription = incomingItem.NodeDescription;
                                existingItem.workFlowNodeStatus = incomingItem.workFlowNodeStatus;
                                // Update() çağrısına gerek yok, tracking otomatik algılar
                            }
                        }
                    }
                }
                else
                {
                    // Yeni WorkflowHead ekle
                    await _workFlowRepository.AddAsync(head);
                }

                // ApproveItems artık kullanılmıyor, bu kısım kaldırıldı
                // if (approveItems != null) { ... }

            if (formItem != null)
            {
                if (formItem.Id == Guid.Empty)
                {
                    await _formItemsRepository.AddAsync(formItem);
                }
                else
                {
                        // FormItem'ı güncelle - var olup olmadığını kontrol et
                        try
                        {
                            var existingFormItem = await _formItemsRepository.GetByIdStringGuidAsync(formItem.Id);
                            if (existingFormItem != null)
                            {
                                // Property'leri kopyala (tracking edilmiş entity'ye)
                                existingFormItem.FormDesign = formItem.FormDesign;
                                existingFormItem.FormId = formItem.FormId;
                                existingFormItem.FormUser = formItem.FormUser;
                                existingFormItem.FormUserNameSurname = formItem.FormUserNameSurname;
                                existingFormItem.FormData = formItem.FormData;
                                existingFormItem.FormDescription = formItem.FormDescription;
                                existingFormItem.FormUserMessage = formItem.FormUserMessage;
                                existingFormItem.FormTaskMessage = formItem.FormTaskMessage;
                                existingFormItem.FormItemStatus = formItem.FormItemStatus;
                                // Update() çağrısına gerek yok, tracking otomatik algılar
                            }
                            else
                            {
                                // Bulunamazsa yeni ekle
                                formItem.Id = Guid.Empty;
                                await _formItemsRepository.AddAsync(formItem);
                            }
                        }
                        catch
                        {
                            // Hata durumunda yeni ekle
                            formItem.Id = Guid.Empty;
                            await _formItemsRepository.AddAsync(formItem);
                        }
                }
            }

            if (formInstance != null)
            {
                if (formInstance.Id == Guid.Empty)
                {
                    await _formInstanceRepository.AddAsync(formInstance);
                }
                else
                {
                        // FormInstance'ı güncelle - var olup olmadığını kontrol et
                        try
                        {
                            var existingFormInstance = await _formInstanceRepository.GetByIdStringGuidAsync(formInstance.Id);
                            if (existingFormInstance != null)
                            {
                                // Property'leri kopyala (tracking edilmiş entity'ye)
                                existingFormInstance.WorkflowHeadId = formInstance.WorkflowHeadId;
                                existingFormInstance.FormId = formInstance.FormId;
                                existingFormInstance.FormDesign = formInstance.FormDesign;
                                existingFormInstance.FormData = formInstance.FormData;
                                existingFormInstance.UpdatedBy = formInstance.UpdatedBy;
                                existingFormInstance.UpdatedByNameSurname = formInstance.UpdatedByNameSurname;
                                existingFormInstance.UpdatedDate = formInstance.UpdatedDate;
                                // Update() çağrısına gerek yok, tracking otomatik algılar
                            }
                            else
                            {
                                // Bulunamazsa yeni ekle
                                formInstance.Id = Guid.Empty;
                                await _formInstanceRepository.AddAsync(formInstance);
                            }
                        }
                        catch
                        {
                            // Hata durumunda yeni ekle
                            formInstance.Id = Guid.Empty;
                            await _formInstanceRepository.AddAsync(formInstance);
                        }
                }
            }

            // WorkflowItems içindeki FormItems'ları kaydet
            foreach (var item in workflowItems)
            {
                if (item.formItems != null && item.formItems.Count > 0)
                {
                    foreach (var fi in item.formItems)
                    {
                        if (fi.Id == Guid.Empty)
                        {
                            await _formItemsRepository.AddAsync(fi);
                        }
                        else
                        {
                                // FormItem'ın var olduğunu kontrol et
                                var existingFi = await _formItemsRepository.GetByIdStringGuidAsync(fi.Id);
                                if (existingFi == null)
                                {
                                    // Eğer bulunamazsa yeni ekle
                                    await _formItemsRepository.AddAsync(fi);
                                }
                                else
                                {
                                    // Property'leri kopyala (tracking edilmiş entity'ye)
                                    existingFi.FormDesign = fi.FormDesign;
                                    existingFi.FormId = fi.FormId;
                                    existingFi.FormUser = fi.FormUser;
                                    existingFi.FormUserNameSurname = fi.FormUserNameSurname;
                                    existingFi.FormData = fi.FormData;
                                    existingFi.FormDescription = fi.FormDescription;
                                    existingFi.FormUserMessage = fi.FormUserMessage;
                                    existingFi.FormTaskMessage = fi.FormTaskMessage;
                                    existingFi.FormItemStatus = fi.FormItemStatus;
                                    // Update() çağrısına gerek yok, tracking otomatik algılar
                                }
                            }
                        }
                    }
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _unitOfWork.Rollback();
                throw new Exception($"Concurrency error: The data may have been modified or deleted by another process. {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }


    }
}
 