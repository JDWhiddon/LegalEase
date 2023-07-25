using Newtonsoft.Json;
using PracticeManagement.Library.DTO;
using PracticeManagement.Library.Utilities;

namespace PracticeManagement.Library.Services
{
    public class ClientService
    {
        private static ClientService? instance;
        private static object _lock = new object();
        private List<ClientDTO> listOfClients = new List<ClientDTO>();
        public List<ClientDTO> ListOfClients
        {
            get
            {
                return listOfClients ?? new List<ClientDTO>();
            }
        }

        public static ClientService Current
        {
            get
            {
                lock(_lock)
                {
                    if (instance == null)
                    {
                        instance = new ClientService(); 
                    }
                }
                return instance;
            }
        }

        private ClientService()
        {
            var response = new WebRequestHandler()
                .Get($"/Client/GetClients")
                .Result;
            listOfClients = JsonConvert
                .DeserializeObject<List<ClientDTO>>(response)
                ?? new List<ClientDTO>();

        }

        public List<ClientDTO> Search(string query) => ListOfClients.Where(s => s.Name.ToUpper().Contains(query.ToUpper())).ToList();
        public ClientDTO? Get(int id)
        {
            return ListOfClients.FirstOrDefault(c => c.Id == id);
        }

        public void AddOrUpdate(ClientDTO? dto)
        {
            var response = new WebRequestHandler().Post("/Client", dto).Result;
            var myUpdatedClient = JsonConvert.DeserializeObject<ClientDTO>(response);
            if (myUpdatedClient != null)
            {
                var existingClient = listOfClients.FirstOrDefault(c => c.Id == myUpdatedClient.Id);
                if (existingClient == null)
                {
                    listOfClients.Add(myUpdatedClient);
                } else
                {
                    var index = listOfClients.IndexOf(existingClient);
                    listOfClients.RemoveAt(index);
                    listOfClients.Insert(index, myUpdatedClient);
                }
            }
            RefreshClientList();
        }

        public void Delete(int id)
        {
            var response = new WebRequestHandler().Delete($"/Client/Delete/{id}").Result;
            var clientToRemove = Get(id);
            if (clientToRemove != null)
            {
                ListOfClients.Remove(clientToRemove);
            }
        }
        public void Read() => ListOfClients.ForEach(Console.WriteLine);

        public void RefreshClientList()
        {
            var response = new WebRequestHandler()
                .Get($"/Client/GetClients")
                .Result;
            listOfClients = JsonConvert
                .DeserializeObject<List<ClientDTO>>(response)
                ?? new List<ClientDTO>();
        }
    }
}
