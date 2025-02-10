using AutoMapper;
using CadastroCliente.API.DTOs;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(ILogger<ClientController> logger,
            IClientService clientService,
            IMapper mapper)
        {
            _logger = logger;
            _clientService = clientService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = await _clientService.GetAllClients();

            if (clients == null)
                return NotFound();

            var clientResponseDTO = _mapper.Map<List<ClientResponseDTO>>(clients);

            return Ok(clientResponseDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientService.GetClientById(id);

            if (client == null)
                return NotFound();

            var clientResponseDTO = _mapper.Map<ClientResponseDTO>(client);

            return Ok(clientResponseDTO);
        }

        [HttpGet("{id}/addresses")]
        public async Task<IActionResult> Addresses(int id)
        {
            var client = await _clientService.GetClientById(id);

            if (!client.Adressess.Any())
                return NotFound(new { message = "Nenhum endereço encontrado para este cliente." });

            var addressResponseDTO = _mapper.Map<List<AddressResponseDTO>>(client.Adressess);

            return Ok(addressResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientContentDTO clientDTO)
        {
            try
            {
                var client = await _clientService.CreateClient(_mapper.Map<Client>(clientDTO));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar o cliente: {ex.Message}");
                return BadRequest($"Erro ao criar o cliente: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] ClientContentDTO clientDTO)
        {
            try
            {
                await _clientService.UpdateClient(_mapper.Map<Client>(clientDTO));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o cliente: {ex.Message}");
                return BadRequest($"Erro ao atualizar o cliente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clientService.DeleteClient(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar o cliente: {ex.Message}");
                return BadRequest($"Erro ao deletar o cliente: {ex.Message}");
            }
        }
    }
}
