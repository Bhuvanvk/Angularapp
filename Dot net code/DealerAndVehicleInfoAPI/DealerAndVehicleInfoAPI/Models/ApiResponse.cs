namespace DealerAndVehicleInfoAPI.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DealerToVehicleInfo> Data { get; set; }
        public long StatusCode { get; set; }
        
}
}
