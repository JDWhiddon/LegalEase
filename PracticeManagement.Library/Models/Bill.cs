
namespace PracticeManagement.Library.Models
{
    public class Bill
    {
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
