﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.Library.Models
{
    public class Time
    {
        public Time() { }
        public Time(TimeDTO c) 
        {
            this.Date = c.Date;
            this.Narrative = c.Narrative;
            this.Hours = c.Hours;
            this.ProjectId = c.ProjectId;
            this.EmployeeId = c.EmployeeId;
            this.Id = c.Id;
            this.Billed = c.Billed;
        }
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public decimal Hours { get; set; }  
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public int Id { get; set; }
        public bool Billed { get; set; }

        public override string ToString()
        {
            return $"Employee: {EmployeeId}, Hours: {Hours}";
        }
    }
}
