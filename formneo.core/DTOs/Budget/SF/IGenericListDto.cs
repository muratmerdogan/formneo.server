namespace formneo.core.DTOs.Budget.SF
{
    public abstract class IGenericListDto
    {
        public int Count { get; set; }

        public List<object> list { get; set; }
    }
}