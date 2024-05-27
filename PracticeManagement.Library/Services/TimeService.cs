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
using System.Transactions;

namespace PracticeManagement.Library.Services
{
    public class TimeService
    {
        private static TimeService? instance;
        private static object _lock = new object();

        public static TimeService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new TimeService();
                    }
                }
                return instance;
            }
        }

        List<TimeDTO> listOfTimes;
        private TimeService()
        {
            var response = new WebRequestHandler()
                .Get($"/Time/GetTimes")
                .Result;
            listOfTimes = JsonConvert
                .DeserializeObject<List<TimeDTO>>(response)
                ?? new List<TimeDTO>();
        }

        public void AddOrUpdate(TimeDTO time)
        {
            if (EmployeeService.Current.Get(time.EmployeeId) != null)
            {
                var response = new WebRequestHandler().Post("/Time", time).Result;
                var myUpdatedTime = JsonConvert.DeserializeObject<TimeDTO>(response);
                if (myUpdatedTime != null)
                {
                    var existingTime = listOfTimes.FirstOrDefault(c => c.Id == myUpdatedTime.Id);
                    if (existingTime == null)
                    {
                        listOfTimes.Add(myUpdatedTime);
                    }
                    else
                    {
                        var index = listOfTimes.IndexOf(existingTime);
                        listOfTimes.RemoveAt(index);
                        listOfTimes.Insert(index, myUpdatedTime);
                    }
                }
            }
            RefreshTimeList();
        }
        public void RefreshTimeList()
        {
            var response = new WebRequestHandler()
                .Get($"/Time/GetTimes")
                .Result;
            listOfTimes = JsonConvert
                .DeserializeObject<List<TimeDTO>>(response)
                ?? new List<TimeDTO>();
        }

        public TimeDTO? Get(int id) => listOfTimes.FirstOrDefault(e => e.Id == id);

        public List<TimeDTO> ListOfTimes
        {
            get
            {
                return listOfTimes;
            }
        }

        public void Delete(int id)
        {
            var response = new WebRequestHandler().Delete($"/Time/Delete/{id}").Result;
            var timeToRemove = Get(id);
            if (timeToRemove != null)
            {
                ListOfTimes.Remove(timeToRemove);
            }
        }

        public void Read() => listOfTimes.ForEach(Console.WriteLine);
    }
}
