using PracticeManagement.CLI.Models;
using PracticeManagement.Library.Models;
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

        List<Time> listOfTimes;
        private TimeService()
        {
            listOfTimes = new List<Time> {
                new Time {Id = 1, Hours = 1, EmployeeId = 1, ProjectId = 1 }
            };
        }

        public void AddOrUpdate(Time time)
        {
            if (time.Id == 0)
            {
                time.Id = LastId + 1;
                listOfTimes.Add(time);
            }
        }

        public Time? Get(int id) => listOfTimes.FirstOrDefault(e => e.Id == id);

        public List<Time> ListOfTimes
        {
            get
            {
                return listOfTimes;
            }
        }

        private int LastId
        {
            get
            {
                return ListOfTimes.Any() ? ListOfTimes.Select(c => c.Id).Max() : 1;
            }
        }
        public void Delete(int id)
        {
            var timeToRemove = Get(id);
            if (timeToRemove != null)
            {
                listOfTimes.Remove(timeToRemove);
            }
        }

        public void Read() => listOfTimes.ForEach(Console.WriteLine);
    }
}
