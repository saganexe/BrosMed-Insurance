using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BrosMed_Insurance.Models.Reservation
{
    public class Finalizacja
    {
        [Key]
        public int FinalizacjaId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int TerminyId { get; set; }
        [ForeignKey("TerminyId")]
        public virtual Terminy Terminy { get; set; }
    }
}
