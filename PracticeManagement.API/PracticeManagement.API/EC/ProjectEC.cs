using System.Xml;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PracticeManagement.API.EC
{
    public class ProjectEC
    {
        public ProjectDTO AddOrUpdate(ProjectDTO dto)
        {
            if (dto.Id <= 0)
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    Project p = new Project(dto);
                    p.IsActive = true;
                    p.OpenDate = DateTime.Now;
                    context.Projects.Add(p);
                    context.SaveChanges();
                }
            }
            else
            {
                var project = new Project();
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    project = context.Projects.FirstOrDefault(c => c.Id == dto.Id);
                    project.OpenDate = dto.OpenDate;
                    project.ClosedDate = dto.ClosedDate;
                    project.IsActive = dto.IsActive;
                    project.ShortName = dto.ShortName;
                    project.LongName = dto.LongName;
                    project.ClientId = dto.ClientId;
                    context.SaveChanges();
                }
                return new ProjectDTO(project);
            }
            return dto;
        }


            //public ProjectDTO? Get(int id)
            //{
            //using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            //{
            //    List<Project> result = context.Projects.ToList();
            //    return result
            //        .Take(1000)
            //        .Select(c => new ProjectDTO(c));
            //}
            //}

        public IEnumerable<ProjectDTO> Search()
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                List<Project> result = context.Projects.ToList();
                return result
                    .Take(1000)
                    .Select(c => new ProjectDTO(c));
            }
        }

       public ProjectDTO? Delete(int id)
       {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                var projectToDelete = context.Projects.FirstOrDefault(c => c.Id == id);
                if (projectToDelete != null)
                {
                    context.Projects.Remove(projectToDelete);
                    context.SaveChanges();
                }
            }
            return new ProjectDTO();
       }
    }
}