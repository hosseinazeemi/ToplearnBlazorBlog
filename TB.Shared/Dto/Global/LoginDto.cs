using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Shared.Dto.Global
{
    public class LoginDto
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        public string Email { get; set; }
        [Required(ErrorMessage = "پسورد را وارد کنید")]
        public string Password { get; set; }
    }
}
