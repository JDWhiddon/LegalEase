using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.CLI.Models
{
    public class Project
    {
        public Project(ProjectDTO p)
        {
            this.Id = p.Id;
            this.LongName = p.LongName;
            this.ClientId = p.ClientId;
            this.IsActive = p.IsActive;
            this.OpenDate = p.OpenDate;
            this.ClosedDate = p.ClosedDate;
        }

        public Project()
        {
            LongName = string.Empty;
        }
        public int Id { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool? IsActive { get; set; }
        public string? ShortName { get; set; }
        public string? LongName { get; set; }
        public int ClientId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - {LongName} Active: {IsActive}";
        }
    }
}
