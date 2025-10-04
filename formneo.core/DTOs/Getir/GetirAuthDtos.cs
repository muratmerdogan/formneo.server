namespace formneo.core.DTOs.Getir
{
    public class GetirAuthLoginRequest
    {
        public string appSecretKey { get; set; } = string.Empty;
        public string restaurantSecretKey { get; set; } = string.Empty;
    }

    public class GetirAuthLoginResponse
    {
        public string? restaurantId { get; set; }
        public string? token { get; set; }
    }
}


