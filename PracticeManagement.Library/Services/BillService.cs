using PracticeManagement.Library.Models;

namespace PracticeManagement.Library.Services
{
    public class BillService
    {
        private static BillService? instance;
        private static object _lock = new object();

        public static BillService Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new BillService();
                    }
                }
                return instance;
            }
        }

        private List<Bill> listOfBills;

        private BillService()
        {
            listOfBills = new List<Bill> {
                new Bill {Id = 1, ClientId = 1, ProjectId = 1, TotalAmount = 100M}
            };
        }

        public List<Bill> ListOfBills
        {
            get
            {
                return listOfBills;
            }
        }
        public Bill? Get(int id) => ListOfBills.FirstOrDefault(e => e.Id == id);

        public void AddOrUpdate(Bill? bill)
        {
            if (bill.Id == 0)
            {
                //add
                bill.Id = LastId + 1;
                listOfBills.Add(bill);
            }
        }

        private int LastId
        {
            get
            {
                return ListOfBills.Any() ? ListOfBills.Select(c => c.Id).Max() : 1;
            }
        }

        public void Delete(int id)
        {
            var billToRemove = Get(id);
            if (billToRemove != null)
            {
                listOfBills.Remove(billToRemove);
            }
        }

        public void Read() => listOfBills.ForEach(Console.WriteLine);
    }
}
