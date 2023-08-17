using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DealerAndVehicleInfoAPI.Models
{
    public class APIDataLogEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ClientIP { get; set; }
      
        public DateTime CreatedDate { get; set; }
        public string? Response { get; set; } // Nullable string to store the response
    }
}
