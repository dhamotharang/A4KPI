﻿using A4KPI.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A4KPI.Models
{
    [Table("ToDoList")]
    public class ToDoList: IDateTracking
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string Remark { get; set; }
        public int ObjectiveId { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsReject { get; set; }
        public bool IsRelease { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedTime { get ; set ; }
        public DateTime? ModifiedTime { get ; set ; }
      
        [ForeignKey(nameof(ObjectiveId))]
        public virtual Objective Objective { get; set; }

    }
}
