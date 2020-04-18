using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRrgisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
         [StringLength(8, ErrorMessage = "The {0} must be at between 4 and 8 ", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

   
}