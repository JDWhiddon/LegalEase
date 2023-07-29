using Newtonsoft.Json;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var response = new WebRequestHandler()
                        .Get($"/Project/GetProjects")
                        .Result;
            listOfProjects = JsonConvert
                .DeserializeObject<List<ProjectDTO>>(response)
                ?? new List<ProjectDTO>();
        }

        List<ProjectDTO> listOfProjects;
        public void AddOrUpdate(ProjectDTO dto)
        {
            var response = new WebRequestHandler().Post("/Project", dto).Result;
            var myUpdatedProject = JsonConvert.DeserializeObject<ProjectDTO>(response);
            if (myUpdatedProject != null)
            {
                var existingProject = listOfProjects.FirstOrDefault(c => c.Id == myUpdatedProject.Id);
                if (existingProject == null)
                {
                    listOfProjects.Add(myUpdatedProject);
                }
                else
                {
                    var index = listOfProjects.IndexOf(existingProject);
                    listOfProjects.RemoveAt(index);
                    listOfProjects.Insert(index, myUpdatedProject);
                }
            }
            RefreshProjectList();
        }
        public void RefreshProjectList()
        {
            var response = new WebRequestHandler()
                        .Get($"/Project/GetProjects")
                        .Result;
            listOfProjects = JsonConvert
                .DeserializeObject<List<ProjectDTO>>(response)
                ?? new List<ProjectDTO>();
        }

        public void ExecuteToggleProjectStatus(ProjectDTO project)
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


        public List<ProjectDTO> Search(int clientId) => ListOfProjects.Where(s => s.ClientId == clientId).ToList();
        public ProjectDTO? Get(int id) => listOfProjects.FirstOrDefault(e => e.Id == id);

        public List<ProjectDTO> ListOfProjects
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
            var response = new WebRequestHandler().Delete($"/Project/Delete/{id}").Result;
            var projectToRemove = Get(id);
            if (projectToRemove != null)
            {
                ListOfProjects.Remove(projectToRemove);
            }
        }


        public void Read() => listOfProjects.ForEach(Console.WriteLine);
    }
}
