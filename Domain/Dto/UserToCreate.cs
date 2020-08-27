using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class UserToCreate
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} character.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "{0} must have {1} character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} character.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} character.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "{0} must must have {1} character.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public string Email { get; set; }
    }
}