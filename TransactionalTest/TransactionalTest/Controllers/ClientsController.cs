using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

namespace TransactionalTest.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClientsController : Controller
    {
        private IClientRepository _clientRepository;
        private ICompareServices _compareServices;
        private ILogger _logger;

        public ClientsController(IClientRepository clientRepository, ICompareServices compareServices, ILogger logger)
        {
            _clientRepository = clientRepository;
            _compareServices = compareServices;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var res = await _clientRepository.GetClientsAsync();
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(Guid Id)
        {
            var res = await _clientRepository.GetClientByIdAsync(Id);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            var res = await _clientRepository.CreateClientAsync(client);
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAsync(Client client, Guid Id)
        {
            if (Id != client.Id) BadRequest("Error en relacion");
            var clientDB = await _clientRepository.GetClientByIdAsync(Id);
            if (clientDB == null) return NotFound();
            // update
            if (_compareServices.CompareClient(client, clientDB)) BadRequest("Cliente sin cambios");
            _clientRepository.UpdateClientById(client);
            return Ok(client);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var client = await _clientRepository.GetClientByIdAsync(Id);
            if (client == null) return NotFound();
            // delete
            _clientRepository.DeleteClientById(client);
            return Ok(Id);
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> PatchClient(Guid id, [FromBody] JsonPatchDocument clientDocument)
        {
            var updatedClient = await _clientRepository.UpdateClientPatchAsync(id, clientDocument);
            if (updatedClient == null)
            {
                return NotFound();
            }
            return Ok(updatedClient);
        }
    }
}
