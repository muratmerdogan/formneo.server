namespace formneo.core.DTOs
{
    public class WorkFlowDto
    {
        public string? WorkFlowId { get; set; }

        public string workFlowItemId { get; set; }

        public Guid WorkFlowDefinationId { get; set; }

        public string? UserName { get; set; }

        public string? NameAndSurname { get; set; }

        public string? NodeId { get; set; }

        public string? Input { get; set; }

        public string ApproverItemId { get; set; }

        public string WorkFlowInfo { get; set; }


        public string Note { get; set; }
        public string? NumberManDay { get; set; }
        /// <summary>
        /// Form üzerindeki butonun action kodu (örn: SAVE, APPROVE)
        /// </summary>
        public string? Action { get; set; }

    }
    public class WorkFlowContiuneApiDto
    {

        public string ApproveItem { get; set; }

        public string workFlowItemId { get; set; }

        public string? UserName { get; set; }

        /// <summary>
        /// Form üzerindeki butonun action kodu (örn: SAVE, APPROVE, REJECT)
        /// Artık Input ile "yes/no" mantığı yok, buton bazlı sistem kullanılıyor
        /// </summary>
        public string? Action { get; set; }
        
        /// <summary>
        /// Form verileri (JSON formatında) - payloadJson olarak kullanılır
        /// </summary>
        public string? FormData { get; set; }

        public string Note { get; set; }
        public string? NumberManDay { get; set; }

    }

    public class WorkFlowStartApiDto
    {
        public string WorkFlowInfo { get; set; }
        public string? DefinationId { get; set; }
        public string? UserName { get; set; }
        /// <summary>
        /// Form verileri (JSON formatında)
        /// </summary>
        public string? FormData { get; set; }
        /// <summary>
        /// Form üzerindeki butonun action kodu (örn: SAVE, APPROVE)
        /// </summary>
        public string? Action { get; set; }
        /// <summary>
        /// Kullanıcının workflow'da yazdığı mesaj/not
        /// </summary>
        public string? Note { get; set; }
    }
}
