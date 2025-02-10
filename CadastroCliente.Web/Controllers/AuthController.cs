using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CadastroCliente.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthController(IConfiguration configuration, 
            HttpClient httpClient, 
            IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
        }


        public async Task<IActionResult> Auth()
        {
            var payload = new
            {
                CompanyName = _configuration["Authenticate:CompanyName"],
                ApiKey = _configuration["Authenticate:Key"]
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var tokenResponse = await _httpClient.PostAsync(_configuration["Api:Url"] + "Authentication", jsonContent);

            if (tokenResponse.IsSuccessStatusCode)
            {
                var token = await tokenResponse.Content.ReadAsStringAsync();
                _contextAccessor.HttpContext.Session.SetString("JwtToken", token);
            }

                return RedirectToAction("Index", "Home");
        }
    }
}
