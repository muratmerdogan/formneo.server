using System.Collections.Generic;

namespace formneo.core.DTOs.Getir
{
    public class GetirLocalizedName
    {
        public string? tr { get; set; }
        public string? en { get; set; }
    }

    public class GetirPaymentMethodItem
    {
        public string? id { get; set; }
        public GetirLocalizedName? name { get; set; }
        public string? icon { get; set; }
        public int paymentGroup { get; set; }
        public List<int> deliveryTypes { get; set; } = new List<int>();
        public int type { get; set; }
    }
}


