using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;
using WebApp1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp1.Pages.Photos
{
    public class SlideshowModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SlideshowModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Photo> Photos { get; set; }

        public async Task OnGetAsync()
        {
            Photos = await _context.Photos.ToListAsync();
        }
    }
}
