using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
