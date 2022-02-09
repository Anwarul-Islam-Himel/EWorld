using System.ComponentModel.DataAnnotations;

namespace E_World.RequestModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), MinLength(8), MaxLength(30)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required"), Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
