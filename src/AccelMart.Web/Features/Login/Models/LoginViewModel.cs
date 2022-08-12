using System.ComponentModel.DataAnnotations;

namespace AccelMart.Web.Features.Login.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required!")]
        [EmailAddress(ErrorMessage ="Invalid email format detected")]
        [MinLength(10, ErrorMessage = "Email address is too short!")]
        public string? Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password is too short!")]
        public string? Password { get; set; }
    }
}
