using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BoligWebApp.Helper;
using Newtonsoft.Json;
using Shared;

namespace BoligWebApp.Controllers
{
    public class PostController : Controller
    {
        HttpClientHelperApi _api = new();
        
        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(RegisterViewModel model)
        {

            HttpClient client = _api.Initial();
            var response = await client.PostAsJsonAsync("api/Auth/Register", model);

            return View(model);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var post = new RegisterViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/Posts/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                post = JsonConvert.DeserializeObject<RegisterViewModel> (result);
            }

            return View(post);

        }
        
    }
}

