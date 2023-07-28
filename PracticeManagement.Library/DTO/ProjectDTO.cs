using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.CLI.Models;

namespace PracticeManagement.Library.DTO
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
            LongName = string.Empty;
        }
        public ProjectDTO(Project p)
        {
            this.Id = p.Id;
            this.LongName = p.LongName;
            this.ClientId = p.ClientId;
        }
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public bool IsActive { get; set; }
        public string? ShortName { get; set; }
        public string? LongName { get; set; }
        public int ClientId { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - {LongName} Active: {IsActive}";
        }
    }
}
