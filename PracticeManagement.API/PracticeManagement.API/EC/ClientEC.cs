using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    context.Clients.Add(new Client(dto));
                    context.SaveChanges();
                }
            }
            else
            {
                using (var context = new EfContextFactory().CreateDbContext(new string[0]))
                {
                    var client = context.Clients.FirstOrDefault(c => c.Id == dto.Id);
                    client.Name = dto.Name;
                    client.Notes = dto.Notes;
                    client.OpenDate = dto.OpenDate;
                    client.ClosedDate = dto.ClosedDate;
                    client.IsActive = dto.IsActive;
                    context.SaveChanges();
                }

            }
            return dto;
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
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                List<Client> result = context.Clients.ToList();
                return result
                    .Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()))
                    .Take(1000)
                    .Select(c => new ClientDTO(c));
            }
        }

        public ClientDTO? Delete(int id)
        {
            using (var context = new EfContextFactory().CreateDbContext(new string[0]))
            {
                var clientToDelete = context.Clients.FirstOrDefault(c => c.Id == id);
                if (clientToDelete != null)
                {
                    context.Clients.Remove(clientToDelete);
                    context.SaveChanges();
                }
            }
            return new ClientDTO();
        }
    }
}