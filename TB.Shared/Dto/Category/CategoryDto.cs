﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Shared.Enums;

namespace TB.Shared.Dto.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "وارد کردن نام اجباری است")]
        public string Name { get; set; }
        public StatusType Status { get; set; }
        public bool IsSpecial { get; set; }
    }
}