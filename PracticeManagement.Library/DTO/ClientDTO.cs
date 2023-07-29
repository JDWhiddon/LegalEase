using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.CLI.Models;

namespace PracticeManagement.Library.DTO
{
    public class ClientDTO
    {
        public ClientDTO()
        { 
            Name = string.Empty;
        }
        public ClientDTO(Client c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
        }
        public int Id { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string? IsActive { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }

        private List<Client>? clients { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name}";
        }
    }
}
