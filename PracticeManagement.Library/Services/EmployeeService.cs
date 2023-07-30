using Newtonsoft.Json;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;
using PracticeManagement.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Library.Services
{
    public class EmployeeService
    {
        private static EmployeeService? instance;
        private static object _lock = new object();

        public static EmployeeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeService();
                    }
                }
                return instance;
            }
        }

        private List<EmployeeDTO> listOfEmployees;

        private EmployeeService()
        {
            var response = new WebRequestHandler()
                .Get($"/Employee/GetEmployees")
                .Result;
            listOfEmployees = JsonConvert
                .DeserializeObject<List<EmployeeDTO>>(response)
                ?? new List<EmployeeDTO>();
        }

        public List<EmployeeDTO> ListOfEmployees
        {
            get
            {
                return listOfEmployees;
            }
        }

        public List<EmployeeDTO> Search(string query) => ListOfEmployees.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).ToList();

        public EmployeeDTO? Get(int id) => listOfEmployees.FirstOrDefault(e => e.Id == id);

        public void AddOrUpdate(EmployeeDTO? employeeDTO)
        {
            var response = new WebRequestHandler().Post("/Employee", employeeDTO).Result;
            var myUpdatedEmployee = JsonConvert.DeserializeObject<EmployeeDTO>(response);
            if (myUpdatedEmployee != null)
            {
                var existingEmployee = listOfEmployees.FirstOrDefault(c => c.Id == myUpdatedEmployee.Id);
                if (existingEmployee == null)
                {
                    listOfEmployees.Add(myUpdatedEmployee);
                }
                else
                {
                    var index = listOfEmployees.IndexOf(existingEmployee);
                    listOfEmployees.RemoveAt(index);
                    listOfEmployees.Insert(index, myUpdatedEmployee);
                }
            }
            RefreshEmployeeList();
        }
        public void RefreshEmployeeList()
        {
            var response = new WebRequestHandler()
                .Get($"/Employee/GetEmployees")
                .Result;
            listOfEmployees = JsonConvert
                .DeserializeObject<List<EmployeeDTO>>(response)
                ?? new List<EmployeeDTO>();
        }
        private int LastId
        {
            get
            {
                return ListOfEmployees.Any() ? ListOfEmployees.Select(c => c.Id).Max() : 1;
            }
        }

        public void Delete(int id)
        {
            var response = new WebRequestHandler().Delete($"/Employee/Delete/{id}").Result;
            var employeeToRemove = Get(id);
            if (employeeToRemove != null)
            {
                ListOfEmployees.Remove(employeeToRemove);
            }
        }

        public void Read() => listOfEmployees.ForEach(Console.WriteLine);
    }
}
