using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using WebApp1.Data;
using WebApp1.Models;
using Microsoft.AspNetCore.Hosting;

namespace WebApp1.Pages.Photos
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Photo Photo { get; set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Photo = await _context.Photos.FindAsync(id);

            if (Photo == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var photoToUpdate = await _context.Photos.FindAsync(id);

            if (photoToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null)
            {
                var fileName = Path.GetFileName(Upload.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images")))
                {
                    Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Upload.CopyToAsync(stream);
                }

                photoToUpdate.ImageUrl = "/images/" + fileName;
            }

            if (await TryUpdateModelAsync<Photo>(
                photoToUpdate,
                "photo",
                p => p.Title, p => p.Description, p => p.DateAdded))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
