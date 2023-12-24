using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using FAMPro.Models;

namespace FAMPro.Pages.Account
{

    

    public class LoginModel : PageModel
    {

        private readonly FAMPro.Data.FAMProContext _context;

        public LoginModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Credential? Credential { get; set; }
        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            var a = _context.Credential.Where(x => x.UserName == Credential.UserName).Where(x => x.Password == Credential.Password).ToList();

            if (!ModelState.IsValid) return Page();

            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "IT"),
                    new Claim("Admin", "true")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/AccountManagement/Index");
            }

            if (a.Count >= 1)
            {

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, Credential.UserName),
                    new Claim("Department", "IT"),
                    new Claim("Employee", "true")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Assets/Index");
            }

            return Page();

        }

    }

}
