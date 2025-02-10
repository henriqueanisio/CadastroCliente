using Microsoft.AspNetCore.Mvc;

namespace CadastroCliente.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var token = _contextAccessor.HttpContext.Session.GetString("JwtToken");

            return View((object)token);
        }  
        
    }
}
