using System.ComponentModel.DataAnnotations;

namespace Api_Ecommerce.Data.Dtos
{
    public class Register
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
