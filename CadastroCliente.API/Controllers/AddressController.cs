using AutoMapper;
using CadastroCliente.API.DTOs;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(ILogger<AddressController> logger,
            IAddressService addressService,
            IMapper mapper)
        {
            _logger = logger;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var address = await _addressService.GetAllAddress();

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var address = await _addressService.GetAddressById(id);

            if (address == null)
                return NotFound();

            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressContentDTO addressDTO)
        {
            try
            {
                var address = await _addressService.CreateAddress(_mapper.Map<Address>(addressDTO));

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar o endereço: {ex.Message}");
                return BadRequest($"Erro ao criar o endereço: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] AddressContentDTO addressDTO)
        {
            try
            {
                await _addressService.UpdateAddress(_mapper.Map<Address>(addressDTO));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o endereço: {ex.Message}");
                return BadRequest($"Erro ao atualizar o endereço: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _addressService.DeleteAddress(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o endereço: {ex.Message}");
                return BadRequest($"Erro ao atualizar o endereço: {ex.Message}");
            }
        }
    }
}
