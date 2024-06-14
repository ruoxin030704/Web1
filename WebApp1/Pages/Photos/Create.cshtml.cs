using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WebApp1.Data;
using WebApp1.Models;
using Microsoft.AspNetCore.Hosting;

namespace WebApp1.Pages.Photos
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Photo Photo { get; set; } = new Photo();

        [BindProperty]
        public IFormFile Upload { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null)
            {
                var fileName = Path.GetFileName(Upload.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                // 確保目錄存在
                if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images")))
                {
                    Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Upload.CopyToAsync(stream);
                }

                Photo.ImageUrl = "/images/" + fileName;
            }

            _context.Photos.Add(Photo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
