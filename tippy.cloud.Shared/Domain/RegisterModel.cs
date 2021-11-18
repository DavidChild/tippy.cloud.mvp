using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace tippy.cloud.Shared.Domain
{
    public class RegisterModel
    {
       
        [Required]
        
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "the password only include the english letters or numbers  and start with english letters.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "two enter password is not equal.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Icon { get; set; } = "https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png";

       
        public string Captcha { get; set; }
    }

}

