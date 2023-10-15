using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Dto.Global;
using TB.Shared.Enums;

namespace TB.Shared.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام را وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل را بدرستی وارد کنید")]
        public string Email { get; set; }
        public string? Phone { get; set; }
        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public StatusType Status { get; set; }
        public RoleType Role { get; set; }
        public FileDto? File { get; set; }
    }
}
