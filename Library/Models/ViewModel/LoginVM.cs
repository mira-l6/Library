using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is a required field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
