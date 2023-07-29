using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;

namespace PracticeManagement.API.EC
{
    public class EmployeeEC
    {
        public EmployeeDTO AddOrUpdate(EmployeeDTO dto)
        {
            if (dto.Id <= 0)
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    context.Employees.Add(new Employee(dto));
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    var employee = context.Employees.FirstOrDefault(c => c.Id == dto.Id);
                    employee.Name = dto.Name;
                    employee.Rate = dto.Rate;
                    context.SaveChanges();
                }
            }
            return dto;
        }

        public IEnumerable<EmployeeDTO> Search(string query = "")
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                List<Employee> result = context.Employees.ToList();
                return result
                    .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(c => new EmployeeDTO(c));
            }
            return new List<EmployeeDTO> { new EmployeeDTO() };
        }

        public EmployeeDTO? Delete(int id)
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                var employeeToRemove = context.Employees.FirstOrDefault(c => c.Id == id);
                if (employeeToRemove != null)
                {
                    context.Employees.Remove(employeeToRemove);
                    context.SaveChanges();
                }
            }
            return new EmployeeDTO();
        }
    }
}