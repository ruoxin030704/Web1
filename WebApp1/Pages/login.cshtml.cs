using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebApp1.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = null!;

        [BindProperty]
        public string Password { get; set; } = null!;

        public IActionResult OnPost()
        {
            if (IsValidLogin(Username, Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            return username == "ruoxin" && password == "111010576";
        }
    }
}
