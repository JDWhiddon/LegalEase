using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;

namespace PracticeManagement.API.EC
{
    public class BillEC
    {
        public BillDTO AddOrUpdate(BillDTO dto)
        {
            if (dto.Id <= 0)
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    Bill bill = new Bill();
                    bill.DueDate = DateTime.Today.AddDays(14);
                    context.Bills.Add(new Bill(dto));
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    var bill = context.Bills.FirstOrDefault(c => c.Id == dto.Id);
                    bill.Id = dto.Id;
                    bill.ClientId = dto.ClientId;
                    bill.ProjectId = dto.ProjectId;
                    bill.TotalAmount = dto.TotalAmount;
                    bill.DueDate = dto.DueDate;
                    bill.Paid = dto.Paid;
                    context.SaveChanges();
                }

            }
            return dto;
        }

        public IEnumerable<BillDTO> Search(string query = "")
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                List<Bill> result = context.Bills.ToList();
                return result
                    .Take(1000)
                    .Select(c => new BillDTO(c));
            }
        }

        public BillDTO? Delete(int id)
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                var billToDelete = context.Bills.FirstOrDefault(c => c.Id == id);
                if (billToDelete != null)
                {
                    context.Bills.Remove(billToDelete);
                    context.SaveChanges();
                }
            }
            return new BillDTO();
        }
    }
}