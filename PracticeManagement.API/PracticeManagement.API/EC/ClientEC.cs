using System.Xml;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.API.EC
{
    public class ClientEC
    {
        public ClientDTO AddOrUpdate(ClientDTO dto)
        {
            if (dto.Id <= 0)
            {
                var result = MsSqlContext.Current.Insert(new Client(dto));
                return new ClientDTO(result);
            }
            else
            {
                MsSqlContext.Current.Update(new Client(dto));
                return dto;
            }
        }


        public ClientDTO? Get(int id)
        {

            var result = MsSqlContext.Current.GetClient()
                .FirstOrDefault(x => x.Id == id)
                ?? new Client();

            return new ClientDTO(result);
        }

        public IEnumerable<ClientDTO> Search(string query = "")
        {
            List<Client> result = MsSqlContext.Current.GetClient();
            return result
                .Where(c => c.Name.ToUpper()
                .Contains(query.ToUpper()))
                .Take(1000)
                .Select(c => new ClientDTO(c));
        }

        public ClientDTO? Delete(int id)
        {
            MsSqlContext.Current.Delete(id);
            return new ClientDTO();
        }
    }
}