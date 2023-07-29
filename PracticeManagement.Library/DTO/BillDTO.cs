using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeManagement.Library.Models;

namespace PracticeManagement.Library.DTO
{
    public class BillDTO
    {
        public BillDTO() { }
        public BillDTO(Bill b)
        {
            this.Id = b.Id;
            this.ClientId = b.ClientId;
            this.ProjectId = b.ProjectId;
            this.TotalAmount = b.TotalAmount;
            this.DueDate = b.DueDate;
            this.Paid = b.Paid;
        }
        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public override string ToString()
        {
            return $"Total: ${Math.Round(TotalAmount, 2)} Project: {ProjectId} Due: {DueDate}";
        }

    }
}
