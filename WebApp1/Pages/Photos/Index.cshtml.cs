using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages.Photos
{
    [Authorize] // 添加這一行以限制訪問
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Photo> Photos { get; set; } = new List<Photo>();

        public async Task OnGetAsync()
        {
            Photos = await _context.Photos.ToListAsync();
        }
    }
}
