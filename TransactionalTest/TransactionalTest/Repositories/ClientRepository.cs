using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TransactionalTest.Interfaces;
using TransactionalTest.Models;

namespace TransactionalTest.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client?> GetClientByIdAsync(Guid id);
        Task<Client?> GetClientByIdentificationAsync(string identification);
        Task<Client?> GetClientByNameAsync(string name);
        Task<Client> CreateClientAsync(Client client);
        Task<bool> UpdateClientById(Client client);
        Task<Client> UpdateClientPatchAsync(Guid id, JsonPatchDocument clientDocument);
        Task<bool> DeleteClientById(Client client);
    }
    public class ClientRepository : IClientRepository
    {
        private IAppDBContext _context;

        public ClientRepository(IAppDBContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteClientById(Client client)
        {
            try
            {
                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Client?> GetClientByIdAsync(Guid id) 
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Client?> GetClientByIdentificationAsync(string identification)
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.Identification == identification);
        }

        public async Task<Client?> GetClientByNameAsync(string name)
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Client.ToListAsync();
        }

        public async Task<bool> UpdateClientById(Client client)
        {
            try
            {
                var entity = await _context.Client.FirstOrDefaultAsync(c => c.Id == client.Id);
                if (entity != null)
                {
                    entity.Address = client.Address;
                    entity.Age = client.Age;
                    entity.Gender = client.Gender;
                    entity.Name = client.Name;
                    entity.Password = client.Password;
                    entity.Phone = client.Phone;
                    entity.State = client.State;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public async Task<Client> UpdateClientPatchAsync(Guid id, JsonPatchDocument clientDocument)
        {
            var clientQuery = await GetClientByIdAsync(id);
            if (clientQuery == null)
            {
                return clientQuery;
            }
            clientDocument.ApplyTo(clientQuery);
            await _context.SaveChangesAsync();

            return clientQuery;
        }
    }
}
