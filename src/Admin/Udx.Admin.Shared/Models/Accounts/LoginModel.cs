using System.ComponentModel.DataAnnotations;

namespace Udx.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string MessageInfo { get; set; }
    }
}
