using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Shared;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostViewModelsController : ControllerBase
    {
        private readonly BoligWebDbContext _context;

        public BlogPostViewModelsController(BoligWebDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogPostViewModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostViewModel>>> GetBlogPostViewModel()
        {
            return await _context.BlogPostViewModel.ToListAsync();
        }

        // GET: api/BlogPostViewModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostViewModel>> GetBlogPostViewModel(int id)
        {
            var blogPostViewModel = await _context.BlogPostViewModel.FindAsync(id);

            if (blogPostViewModel == null)
            {
                return NotFound();
            }

            return blogPostViewModel;
        }

        // PUT: api/BlogPostViewModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPostViewModel(int id, BlogPostViewModel blogPostViewModel)
        {
            if (id != blogPostViewModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogPostViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostViewModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogPostViewModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPostViewModel>> PostBlogPostViewModel(BlogPostViewModel blogPostViewModel)
        {
            _context.BlogPostViewModel.Add(blogPostViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPostViewModel", new { id = blogPostViewModel.Id }, blogPostViewModel);
        }

        // DELETE: api/BlogPostViewModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPostViewModel(int id)
        {
            var blogPostViewModel = await _context.BlogPostViewModel.FindAsync(id);
            if (blogPostViewModel == null)
            {
                return NotFound();
            }

            _context.BlogPostViewModel.Remove(blogPostViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostViewModelExists(int id)
        {
            return _context.BlogPostViewModel.Any(e => e.Id == id);
        }
    }
}
