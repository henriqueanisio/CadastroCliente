using CadastroCliente.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CadastroCliente.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public AddressController(HttpClient httpClient,
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(_configuration["Api:Url"] + "address/" + id);

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Client");

            var json = await response.Content.ReadAsStringAsync();
            var client = JsonConvert.DeserializeObject<EditAddressViewModel>(json);

            return View(client);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync(_configuration["Api:Url"] + "address/" + id);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage);
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAddressViewModel addressViewModel)
        {
            if (!ModelState.IsValid)
                return View(addressViewModel);

            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(addressViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_configuration["Api:Url"] + "address", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage);

                return View(addressViewModel);
            }

            return RedirectToAction("Addresses", "Client", new { id = addressViewModel.ClientId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditAddressViewModel addressViewModel)
        {
            if (!ModelState.IsValid)
                return View(addressViewModel);

            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = new StringContent(JsonConvert.SerializeObject(addressViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_configuration["Api:Url"] + "address/" + addressViewModel.Id, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage);

                return View(addressViewModel);
            }

            return RedirectToAction("Addresses", "Client", new { id = addressViewModel.ClientId });
        }

        private string GetTokenAuthenticate()
        {
            var json = _contextAccessor.HttpContext.Session.GetString("JwtToken");

            if (string.IsNullOrEmpty(json))
                return string.Empty;

            // Deserializando o JSON
            var tokenObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Pegando o valor do "token"
            var token = tokenObject["token"];


            return token;
        }
    }

}
