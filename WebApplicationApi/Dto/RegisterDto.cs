using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Displayname { get; set; }

        [Required]
      
        public string Email { get; set; }

        public string phonenumber { get; set; }
        public string Password { get; set; }

    }
}
