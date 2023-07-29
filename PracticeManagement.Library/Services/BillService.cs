using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Models;
using PracticeManagement.Library.Utilities;

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

        private List<BillDTO> listOfBills;

        private BillService()
        {
            var response = new WebRequestHandler()
                .Get($"/Bill/GetBills")
                .Result;
            listOfBills = JsonConvert
                .DeserializeObject<List<BillDTO>>(response)
                ?? new List<BillDTO>();
        }

        public void RefreshBills()
        {
            var response = new WebRequestHandler()
                .Get($"/Bill/GetBills")
                .Result;
            listOfBills = JsonConvert
                .DeserializeObject<List<BillDTO>>(response)
                ?? new List<BillDTO>();

        }
        public List<BillDTO> ListOfBills
        {
            get
            {
                return listOfBills;
            }
        }
        public BillDTO? Get(int id) => ListOfBills.FirstOrDefault(e => e.Id == id);

        public void AddOrUpdate(BillDTO? bill)
        {
            var response = new WebRequestHandler().Post("/Bill", bill).Result;
            var myUpdatedBill = JsonConvert.DeserializeObject<BillDTO>(response);
            if (myUpdatedBill != null)
            {
                var existingBill = listOfBills.FirstOrDefault(c => c.Id == myUpdatedBill.Id);
                if (existingBill == null)
                {
                    listOfBills.Add(myUpdatedBill);
                }
                else
                {
                    var index = listOfBills.IndexOf(existingBill);
                    listOfBills.RemoveAt(index);
                    listOfBills.Insert(index, myUpdatedBill);
                }
            }
            RefreshBills();
        }

       

        public void Delete(int id)
        {
            var response = new WebRequestHandler().Delete($"/Bill/Delete/{id}").Result;
            var billToRemove = Get(id);
            if (billToRemove != null)
            {
                ListOfBills.Remove(billToRemove);
            }
        }

        public void Read() => listOfBills.ForEach(Console.WriteLine);
    }
}
