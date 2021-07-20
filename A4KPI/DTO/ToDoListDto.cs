﻿using A4KPI.Models;
using A4KPI.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A4KPI.DTO
{
   
    public class ToDoListDto 
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Remark { get; set; }
        public int? ProgressId { get; set; }
        public int ObjectiveId { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public string AccountGroupType{ get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsReject { get; set; }
        public bool IsRelease { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }

    }

    public class ToDoListByLevelL1L2Dto
    {

        public int Id { get; set; }
        public string Objective { get; set; }
        public string L0TargetList { get; set; }
        public List<string> L0ActionList { get; set; }
        public string Result1OfMonth { get; set; }
        public string Result2OfMonth { get; set; }
        public string Result3OfMonth { get; set; }
        public object Months { get; set; }
    }
    public class SelfScoreDto
    {

        public List<string> ObjectiveList { get; set; }
        public string ResultOfMonth { get; set; }
        public int Month { get; set; }
    }
    public class ImportExcelFHO
    {

        public string KPIObjective { get; set; }
        public  string UserList { get; set; }
    }
    public class L0Dto
    {
        public int Id { get; set; }
        public int TodolistId { get; set; }
        public string Topic { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public int HalfYearId { get; set; }
        public int QuarterPeriodTypeId { get; set; }
        public int Quarter { get; set; }
        public int PeriodTypeId { get; set; }
        public List<int> Settings { get; set; }
        public bool IsDisplayUploadResult { get; set; }
        public bool IsDisplaySelfScore { get; set; }
        public bool IsDisplayAction { get; set; }
        public bool IsReject { get; set; }
        public bool IsRelease { get; set; }
        public bool HasFunctionalLeader { get; set; }

    }
    public class FunctionalLeaderDto
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public int PeriodTypeId { get; set; }
        public List<int> Settings { get; set; }
        public bool IsDisplayKPIScore { get; set; }
        public bool IsDisplayAttitude { get; set; }
    }
    public class L1Dto
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public int PeriodTypeId { get; set; }
        public int HalfYearId { get; set; }
        public List<int> Settings { get; set; }
        public bool IsDisplayKPIScore { get; set; }
        public bool IsDisplayAttitude { get; set; }
        public bool HasFunctionalLeader { get; set; }
    }
    public class L2Dto
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public string FullName { get; set; }
        public int Period { get; set; }
        public int PeriodTypeId { get; set; }
        public int HalfYearId { get; set; }
        public List<int> Settings { get; set; }
        public bool IsDisplayKPIScore { get; set; }
        public bool IsDisplayAttitude { get; set; }
        public bool HasFunctionalLeader { get; set; }
    }
    public class GHRDto
    {
        public int Id { get; set; }
        public string Objective { get; set; }
        public DateTime DueDate { get; set; }
        public string Type { get; set; }
        public int Period { get; set; }
        public int PeriodTypeId { get; set; }
        public List<int> Settings { get; set; }
        public bool IsDisplayKPIScore { get; set; }
        public bool IsDisplayAttitude { get; set; }
    }
    public class Q1OrQ3Request
    {
        public int Period { get; set; }
        public int PeriodTypeId { get; set; }
        public int AccountId { get; set; }
    }

    public class Q1OrQ3Export
    {
        public string FullName { get; set; }
        public double L1Score { get; set; }
        public string L1Comment { get; set; }
        public double L2Score { get; set; }
        public string L2Comment { get; set; }
        public double GHRSmartScore { get; set; }
    }
}
