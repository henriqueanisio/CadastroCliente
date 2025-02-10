using AutoMapper;
using CadastroCliente.API.DTOs;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthKeyService _authKeyService;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IMapper mapper,
            IAuthKeyService authKeyService)
        {
            _logger = logger;
            _mapper = mapper;
            _authKeyService = authKeyService;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] AuthRequestDTO authRequestDTO)
        {
            try
            {
                var token = await _authKeyService.Authenticate(authRequestDTO.CompanyName, authRequestDTO.ApiKey);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { message = "API Key inválida" });

                return Ok(new { token });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar o cliente: {ex.Message}");
                return BadRequest($"Erro ao atualizar o cliente: {ex.Message}");
            }
        }

    }
}
