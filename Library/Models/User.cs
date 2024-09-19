using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class User 
    {
        [Key] 
        public int Id { get; set; }

        [DisplayName("First name: ")]
        [Required(ErrorMessage = "First name is a required field")]
        public string FirstName { get; set; }

        [DisplayName("Last name: ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 charecters", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage ="Make a stronger password!")]
        public string Password { get; set; }
    }
}
