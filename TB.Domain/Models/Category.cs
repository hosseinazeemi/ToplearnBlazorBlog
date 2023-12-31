﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Enums;

namespace TB.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public StatusType Status { get; set; }
        public bool IsSpecial { get; set; }
        public List<Content>? Contents { get; set; }
    }
}
