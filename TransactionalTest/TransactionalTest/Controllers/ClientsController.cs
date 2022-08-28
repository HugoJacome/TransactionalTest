using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TransactionalTest.Models;
using TransactionalTest.Models.Constans;
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

        public ClientsController(IClientRepository clientRepository, ICompareServices compareServices)
        {
            _clientRepository = clientRepository;
            _compareServices = compareServices;
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
        public async Task<IActionResult> Post(ClientRequest client)
        {

            var res = await _clientRepository.CreateClientAsync(new Client()
            {
                Address = client.Address,
                Age = client.Age,
                Gender = client.Gender,
                Id = new Guid(),
                Identification = client.Identification,
                Name = client.Name,
                Password = client.Password,
                Phone = client.Phone,
                State = client.State,
            });
            if (res == null) return NotFound();
            return Ok(res);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAsync(Client client, Guid Id)
        {
            if (Id != client.Id) return BadRequest("Error en relacion");
            var clientDB = await _clientRepository.GetClientByIdAsync(Id);
            if (clientDB == null) return NotFound();
            // update
            if (_compareServices.CompareClient(client, clientDB)) return BadRequest("Cliente sin cambios");
            bool result = await _clientRepository.UpdateClientById(client);
            if (!result) return BadRequest(ExceptionConstants.ERROR_UPDATE);
            return Ok(client);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var client = await _clientRepository.GetClientByIdAsync(Id);
            if (client == null) return NotFound();
            // delete
            bool result = await _clientRepository.DeleteClientById(client);
            if (!result) return BadRequest(ExceptionConstants.ERROR_DELETE);
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
