namespace DealerAndVehicleInfoAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

       public List<DealerToVehicle> DealerToVehicle { get; set; }
    }

}
