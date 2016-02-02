using System.ComponentModel.DataAnnotations;

namespace Sabio.Web.Models.Requests.UsersData
{
    public class UserInfoAddRequest 
    {
        [Required]
        [StringLength(20,MinimumLength = 2, ErrorMessage = "FirstName Must Contain 5-20 Characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "LastName Must Contain 5-20 Characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username Must Contain 5-20 Characters")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 15, ErrorMessage = "Bio must contain at least 15 characters")]
        public string Bio { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Phone number must have atleast 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Gender { get; set; }

    }
}