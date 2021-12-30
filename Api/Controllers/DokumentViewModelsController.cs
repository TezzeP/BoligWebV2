using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Shared;
using System.IO;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentViewModelsController : ControllerBase
    {
        private readonly BoligWebDbContext _context;

        public DokumentViewModelsController(BoligWebDbContext context)
        {
            _context = context;
        }

        // GET: api/DokumentViewModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DokumentViewModel>>> GetDokumentViewModel()
        {
            return await _context.DokumentViewModel.ToListAsync();
        }

        // GET: api/DokumentViewModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DokumentViewModel>> GetDokumentViewModel(int id)
        {
            var dokumentViewModel = await _context.DokumentViewModel.FindAsync(id);

            if (dokumentViewModel == null)
            {
                return NotFound();
            }

            return dokumentViewModel;
        }

        // PUT: api/DokumentViewModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDokumentViewModel(int id, DokumentViewModel dokumentViewModel)
        {
            if (id != dokumentViewModel.DocumentId)
            {
                return BadRequest();
            }

            _context.Entry(dokumentViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DokumentViewModelExists(id))
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

        // POST: api/DokumentViewModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  IActionResult PostDokumentViewModel(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtensio
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);


                    var objdDokuments = new DokumentViewModel()
                    {
                        DocumentId = 0,
                        Name = newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objdDokuments.DataFiles = target.ToArray();
                    }

                    _context.DokumentViewModel.Add(objdDokuments);
                    _context.SaveChanges();

                }
                
            }
            return Ok();
        }

        // DELETE: api/DokumentViewModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDokumentViewModel(int id)
        {
            var dokumentViewModel = await _context.DokumentViewModel.FindAsync(id);
            if (dokumentViewModel == null)
            {
                return NotFound();
            }

            _context.DokumentViewModel.Remove(dokumentViewModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DokumentViewModelExists(int id)
        {
            return _context.DokumentViewModel.Any(e => e.DocumentId == id);
        }
    }
}
