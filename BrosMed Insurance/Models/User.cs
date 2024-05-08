using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BrosMed_Insurance.Models
{
    public class User : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? name { get; set; }
        public string? surname { get; set; }
        [Required]
        public string? address { get; set; }
        public string? pesel {  get; set; }

    }
}
