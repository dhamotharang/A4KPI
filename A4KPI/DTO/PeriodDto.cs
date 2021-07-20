﻿using A4KPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A4KPI.DTO
{
  
    public class PeriodDto
    {
        public int Id { get; set; }
        public int PeriodTypeId { get; set; }
        public string Months { get; set; }
        public int Value { get; set; }
        public string Title { get; set; }
        public DateTime ReportTime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

    }
}
