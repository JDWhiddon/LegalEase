﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.Library.Models
{
    public class Employee
    {
        public Employee() { }
        public Employee(EmployeeDTO e)
        {
            this.Id = e.Id;
            this.Name = e.Name;
            this.Rate = e.Rate;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Rate { get; set; }
        private List<Employee>? employees { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} {Name} Rate: {Rate}";
        }
    }
}
