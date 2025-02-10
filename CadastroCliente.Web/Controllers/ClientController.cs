using CadastroCliente.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace CadastroCliente.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public ClientController(HttpClient httpClient,
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(_configuration["Api:Url"] + "client");

            if (!response.IsSuccessStatusCode)
                return View(new List<CreateClientViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<List<IndexClientViewModel>>(json);

            return View(clients);
        }

        [HttpGet("client/{id}/addresses")]
        public async Task<IActionResult> Addresses(int id)
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(_configuration["Api:Url"] + "client/" + id + "/addresses");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao buscar endereços do cliente: {errorMessage}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<List<IndexAddressViewModel>>(json);

            return View(addresses);
        }

        [HttpGet("client/{id}/address/create")]
        public async Task<IActionResult> CreateAddress(int id)
        {
            var model = new CreateAddressViewModel { ClientId = id };
            return View("Views/Address/Create.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateClientViewModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(_configuration["Api:Url"] + "client/" + id);

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            var json = await response.Content.ReadAsStringAsync();
            var client = JsonConvert.DeserializeObject<EditClientViewModel>(json);

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync(_configuration["Api:Url"] + "client/" + id);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao remover cliente: {errorMessage}");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
                return View(clientViewModel);

            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (clientViewModel.LogoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    clientViewModel.LogoFile.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    clientViewModel.Logo = Convert.ToBase64String(fileBytes);
                }
            }

            var addresses = new List<IndexAddressViewModel>();

            addresses.Add(new IndexAddressViewModel()
            {
                Street = clientViewModel.Street,
                City = clientViewModel.City,
                Complement = clientViewModel.Complement,
                Neighborhood = clientViewModel.Neighborhood,
                Number = clientViewModel.Number,
                State = clientViewModel.State
            });

            clientViewModel.Adressess = addresses;

            var jsonContent = new StringContent(JsonConvert.SerializeObject(clientViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_configuration["Api:Url"] + "client", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao cadastrar cliente: {errorMessage}");

                return View(clientViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
                return View(clientViewModel);

            var token = GetTokenAuthenticate();

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Home");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (clientViewModel.LogoFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    clientViewModel.LogoFile.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    clientViewModel.Logo = Convert.ToBase64String(fileBytes);
                }
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(clientViewModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_configuration["Api:Url"] + "client/" + clientViewModel.Id, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Erro ao atualizar cliente: {errorMessage}");

                return View(clientViewModel);
            }

            return RedirectToAction("Index");
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
