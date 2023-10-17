using System.ComponentModel.DataAnnotations;

namespace Api_Ecommerce.Data.Dtos
{
    public class Login
    {
        
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
