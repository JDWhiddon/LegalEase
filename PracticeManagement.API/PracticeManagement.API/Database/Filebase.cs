using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using PracticeManagement.API.EC;
using PracticeManagement.CLI.Models;

namespace PracticeManagement.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _clientRoot;
        private string _projectRoot;
        private static Filebase? _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }
                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp";
            _clientRoot = $"{_root}\\Clients";
            _projectRoot = $"{_root}\\Projects";
            //TODO: Add support for employee, time, bills
        }

        private int LastClientId
        {
            get
            {
                return Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
            }
        }
        public Client AddOrUpdate(Client c)
        {
            //set up a new Id if one doesn't already exist
            if(c.Id <= 0)
            {
                c.Id = LastClientId + 1;
            }

            var path = $"{_clientRoot}\\{c.Id}.json";


            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(c));

            //using (var fw = new FileStream(path, FileMode.Create))
            //{
            //   // fw.Write(JsonConvert.SerializeObject(c));
            //}

                //return the item, which now has an id
                return c;
        }

        public List<Client> Clients
        {
            get
            {
                var root = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                foreach(var clientFile in root.GetFiles())
                {
                    var client = JsonConvert.DeserializeObject<Client>(File.ReadAllText(clientFile.FullName));
                    if(client != null)
                    {
                        _clients.Add(client);
                    }
                }
                return _clients;
            }
        }

        public bool Delete(string id)
        {
            var path = $"{_clientRoot}\\{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            return true;
        }
        public void Delete(Client client)
        {
            Delete(client.Id.ToString());
        }
    }



    // ------------------- FAKE MODEL FILES, REPLACE THESE WITH A REFERENCE TO YOUR MODELS -------- //
    public class Item
    {
        public string Id { get; set; }
    }

    public class ToDo : Item
    {

    }

    public class Appointment : Item
    {

    }
}
