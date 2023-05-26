using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Client.ViewModel
{
    public class ChangePasswordVM
    {
        public string NIK { get; set; } = null!;

        public string? Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
