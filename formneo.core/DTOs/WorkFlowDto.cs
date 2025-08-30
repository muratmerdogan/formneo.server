namespace vesa.core.DTOs
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

    }
    public class WorkFlowContiuneApiDto
    {

        public string ApproveItem { get; set; }

        public string workFlowItemId { get; set; }

        public string? UserName { get; set; }

        public string? Input { get; set; }

        public string Note { get; set; }
        public string? NumberManDay { get; set; }

    }

    public class WorkFlowStartApiDto
    {
        public string WorkFlowInfo { get; set; }
        public string? DefinationId { get; set; }
        public string? UserName { get; set; }
    }
}
