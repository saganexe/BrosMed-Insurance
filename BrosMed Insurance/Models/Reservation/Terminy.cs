using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrosMed_Insurance.Models.Reservation
{
    public class Terminy
    {
        [Key]
        public int TerminyId { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int UslugaId { get; set; }
        [ForeignKey("UslugaId")]
        public virtual Usluga Usluga { get; set; }

        [Required]
        public Godzina godzina { get; set; }

    }
}
