using System.ComponentModel.DataAnnotations;

namespace BrosMed_Insurance.Models.Reservation
{
    public class Usluga
    {
        [Key]
        public int UslugaId { get; set; }

        [Required]
        public string Nazwa { get; set; }
        [Required]
        public string Cena { get; set; }


    }
}
