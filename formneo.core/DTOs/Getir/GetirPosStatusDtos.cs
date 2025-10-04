namespace formneo.core.DTOs.Getir
{
    public class GetirPosStatusResponse
    {
        public int PosStatus { get; set; }
        public int RestaurantSecretKeyStatus { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantId { get; set; }
        public string? RestaurantStatus { get; set; }
    }

    public class GetirPosStatusPostRequest
    {
        public string appSecretKey { get; set; } = string.Empty;
        public string restaurantSecretKey { get; set; } = string.Empty;
    }

    public class GetirSetPosStatusRequest
    {
        public int PosStatus { get; set; }
    }
}


