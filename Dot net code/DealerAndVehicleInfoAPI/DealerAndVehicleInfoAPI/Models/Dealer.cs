namespace DealerAndVehicleInfoAPI.Models
{
    public class Dealer 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DealerToVehicle> DealerToVehicle { get; set; }
    }
}
