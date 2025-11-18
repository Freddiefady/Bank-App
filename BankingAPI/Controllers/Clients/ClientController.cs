using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankingAPI.DTOs;
using BankingAPI.Services;

namespace BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching clients", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientById(int id)
        {
            try
            {
                var client = await _clientService.GetClientById(id);

                if (client == null)
                {
                    return NotFound(new { message = "Client not found" });
                }

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching client", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] CreateClientDto clientDto)
        {
            try
            {
                var clientId = await _clientService.AddClient(clientDto);
                var client = await _clientService.GetClientById(clientId);

                return CreatedAtAction(nameof(GetClientById), new { id = clientId }, client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating client", error = ex.Message });
            }
        }
    }
}
