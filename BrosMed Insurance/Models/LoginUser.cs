using System.ComponentModel.DataAnnotations;

namespace BrosMed_Insurance.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Wpisz email.")]
        [Display(Name = "Email")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Wpisz hasło.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string? password { get; set; }
        [Display(Name = "Zapamiętać hasło?")]
        public bool rememberMe { get; set; }
    }
}
