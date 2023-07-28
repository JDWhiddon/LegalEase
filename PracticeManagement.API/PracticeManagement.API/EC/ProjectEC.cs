using System.Xml;
using PracticeManagement.API.Database;
using PracticeManagement.CLI.Models;
using PracticeManagement.Library.DTO;

namespace PracticeManagement.API.EC
{
    public class ProjectEc
    {
        public ProjectDTO AddOrUpdate(ProjectDTO dto)
        {
            if (dto.Id <= 0)
            {
                var result = MsSqlContext.Current.InsertProject(new Project(dto));
                return new ProjectDTO(result);
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