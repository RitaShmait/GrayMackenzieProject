using System.ComponentModel.DataAnnotations;

namespace ContactsMVCCore.Dtos
{
    public class AddContactInput
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers are allowed."),  StringLength(8, MinimumLength = 8, ErrorMessage = "Phone number must be minimum 8 digits.")]
        public string PhoneNumber { get; set; }
    }
}
