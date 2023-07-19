using PracticeManagement.CLI.Models;

namespace PracticeManagement.API.Database
{
    public static class FakeDatabase
    {
        public static List<Client> Clients = new List<Client>
        {
             new Client {Id = 1, Name = "John"},
             new Client {Id = 2, Name = "Doe"},
             new Client {Id = 3, Name = "Ron"}
        };
    }
}
