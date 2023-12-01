using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB.Shared.Dto.Global
{
    public class SearchDto
    {
        [Required(ErrorMessage = "متن جستجو را وارد کنید")]
        public string? Text { get; set; }
    }
}
