using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared;
using System.Net.Http;
using System.Net.Http.Json;
using WebAppMVC.Helper;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebAppMVC.Controllers
{
    public class BlogPostController : Controller
    {
        HttpClientHelperApi _api = new();


        //Get api/BlogPostViewModels
        public async Task<IActionResult> Index()
        {
            List<BlogPostViewModel> posts = new List<BlogPostViewModel>();

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/BlogPostViewModels");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<BlogPostViewModel>>(result);
            }

            return View(posts);
        }

        // GET: api/BlogPostViewModels/{id}
        public async Task<IActionResult> Details(int? id, BlogPostViewModel posts)
        {
            
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/BlogPostViewModels/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<BlogPostViewModel>(result);
            }

            return View(posts);

        }

        // Post: /api/BlogPostViewModels
        public ActionResult CreateView()
        {
            return View();
        }

        // POST: BlogPostController/Create
        public async Task<IActionResult> Create(BlogPostViewModel post)
        {

            HttpClient client = _api.Initial();
            var postTask = await client.PostAsJsonAsync("/api/BlogPostViewModels", post);

            return View(post);
        }

        // GET: BlogPostController/Edit/5
        public async Task<IActionResult> EditView(int? id, BlogPostViewModel posts)
        {

            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/BlogPostViewModels/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<BlogPostViewModel>(result);
            }
            return View(posts);
        }

        // POST: BlogPostController/Edit/5
        public async Task<IActionResult> Edit(int? id, BlogPostViewModel posts)
        {
            
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.PutAsJsonAsync("api/BlogPostViewModels/" + id, posts);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<BlogPostViewModel>(result);
            }

            return View(posts);

        }

        // GET: BlogPostController/Delete/5
        public async Task<ActionResult> DeleteView(int id, BlogPostViewModel posts)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/BlogPostViewModels/" + id);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<BlogPostViewModel>(result);
            }

            return View(posts);
            
        }

        // Delete: BlogPostController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {            
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync("api/BlogPostViewModels/" + id);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();

        }
    }
}
