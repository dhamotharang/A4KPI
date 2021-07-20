﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A4KPI.Models
{
    public class SpecialScore
    {
        [Key]
        public int Id { get; set; }
        public double Point { get; set; }
    }
}
