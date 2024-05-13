using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrosMed_Insurance.Models.Reservation
{
    public class Godzina
    {
        [Key]
        public int GodzinaId { get; set; }
        public string godzinaVM { get; set; }

    }
}
