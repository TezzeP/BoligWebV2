using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAppMVC.Helper;
using Newtonsoft.Json;
using Shared;

namespace WebAppMVC.Controllers
{
    public class AuthController : Controller
    {
        HttpClientHelperApi _api = new();

        public ActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            HttpClient client = _api.Initial();

            var response = await client.PostAsJsonAsync("api/Auth/Register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index" , "Home");

            }
            return BadRequest();        
        }

        public ActionResult LoginView()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            HttpClient client = _api.Initial();

            var response = await client.PostAsJsonAsync("api/Auth/Login", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", response);

            }
            return BadRequest();
        }

    }
}

