namespace DealerAndVehicleInfoAPI.Models
{
    public class DealerToVehicle
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DealerId { get; set; }

        public Vehicle Vehicle { get; set; }
       public Dealer Dealer { get; set; }
    }
   
}
