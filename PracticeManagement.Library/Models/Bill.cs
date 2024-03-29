﻿
using PracticeManagement.Library.DTO;

namespace PracticeManagement.Library.Models
{
    public class Bill
    {
        public Bill() { }
        public Bill(BillDTO b)
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
            return $"Total: ${Math.Round(TotalAmount,2)} Project: {ProjectId} Due: {DueDate}";
        }

    }
}
