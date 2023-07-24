using PracticeManagement.CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PracticeManagement.Library.Services
{
    public class ProjectService
    {
        private static ProjectService? instance;
        private static object _lock = new object();

        public static ProjectService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectService();
                    }
                }
                return instance;
            }
        }

        private ProjectService()
        {
            listOfProjects = new List<Project> {
                new Project {Id = 1, LongName = "PWD", ClientId = 1, IsActive = true }                
            };
        }

        List<Project> listOfProjects;
        public void AddOrUpdate(Project project)
        {
            if(project.Id == 0)
            {
                project.Id = LastId + 1;
                project.IsActive = true;
                listOfProjects.Add(project);
            }
        }

        public void ExecuteToggleProjectStatus(Project project)
        {
            if (project.IsActive == true)
            {
                project.IsActive = false;
            } 
            else
            {
                project.IsActive = true;
            }
        }


        public List<Project> Search(string query) => ListOfProjects.Where(s => s.LongName.ToUpper().Contains(query.ToUpper())).ToList();
        public Project? Get(int id) => listOfProjects.FirstOrDefault(e => e.Id == id);

        public List<Project> ListOfProjects
        {
            get
            {
                return listOfProjects;
            }
        }

        private int LastId
        {
            get
            {
                return ListOfProjects.Any() ? ListOfProjects.Select(c => c.Id).Max() : 1;
            }
        }
        public void Delete(int id)
        {
            var projectToRemove = Get(id);
            if (projectToRemove != null)
            {
                listOfProjects.Remove(projectToRemove);
            }
        }


        public void Read() => listOfProjects.ForEach(Console.WriteLine);
    }
}
