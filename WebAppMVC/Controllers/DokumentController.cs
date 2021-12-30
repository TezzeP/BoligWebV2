using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAppMVC.Helper;

namespace WebAppMVC.Controllers
{
    public class DokumentController : Controller
    {
        HttpClientHelperApi _api = new HttpClientHelperApi();

        public async Task<IActionResult> Index()
        {
            List<DokumentViewModel> dokument = new List<DokumentViewModel>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/DokumentViewModels");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject<List<DokumentViewModel>>(result);
            }

            return View(dokument);
        }




        public async Task<IActionResult> Details(int id, DokumentViewModel dokument)
        {

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/DokumentViewModels/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject<DokumentViewModel>(result);
            }

            return View(dokument);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            var dokument = new DokumentViewModel();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/DokumentViewModels/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dokument = JsonConvert.DeserializeObject<DokumentViewModel>(result);
            }

            return View(dokument);
        }

        //public async Task<IActionResult> Download(int? id)
        //{

        //    var client = new HttpClient();
        //    var response = await client.GetAsync(@"http://localhost:9000/api/file/GetFile?filename=myPackage.zip");

        //    using (var stream = await response.Content.ReadAsStreamAsync())
        //    {
        //        var fileInfo = new FileInfo("myPackage.zip");
        //        using (var fileStream = fileInfo.OpenWrite())
        //        {
        //            await stream.CopyToAsync(fileStream);
        //        }
        //    }
        //    return View();

        //}

    }


    
}
