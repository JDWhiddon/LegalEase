using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;

namespace PracticeManagement.API.EC
{
    public class TimeEC
    {
        public TimeDTO AddOrUpdate(TimeDTO dto)
        {
            if (dto.Id <= 0)
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    Time time = new Time(dto);
                    time.Date = DateTime.Today;
                    time.Billed = false;
                    context.Times.Add(time);
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    var time = context.Times.FirstOrDefault(c => c.Id == dto.Id);
                    time.Date = dto.Date;
                    time.Narrative = dto.Narrative;
                    time.Hours = dto.Hours;
                    time.ProjectId = dto.ProjectId;
                    time.EmployeeId = dto.EmployeeId;
                    time.Billed = dto.Billed;
                    context.SaveChanges();
                }

            }
            return dto;
        }

        public IEnumerable<TimeDTO> Search(string query = "")
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                List<Time> result = context.Times.ToList();
                return result
                    .Take(1000)
                    .Select(c => new TimeDTO(c));
            }
        }

        public TimeDTO? Delete(int id)
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                var timeToDelete = context.Times.FirstOrDefault(c => c.Id == id);
                if (timeToDelete != null)
                {
                    context.Times.Remove(timeToDelete);
                    context.SaveChanges();
                }
            }
            return new TimeDTO();
        }
    }
}