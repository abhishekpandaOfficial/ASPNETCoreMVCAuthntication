using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using AuthProjectMVC6.Models;
using System.Security;
using System.Security.Claims;

namespace AuthProjectMVC6.Controllers
{
    public class AccessController : Controller
    {
      
        //-----------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin loginModel)
        {

            if(loginModel.Email=="user1@gmail.com" && loginModel.Password=="123")
            {
                //--------------------Claims ---------------------------

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
                    new Claim("Other Properties","Example Role")
                };
                //--------------------Claims End  ---------------------------

                //--------------------Claims Identity  ---------------------------

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.KeepLoggedIn
                };
                //--------------------Claims Identity End  ---------------------------

                //--------------------Sign In and Claims Principal ---------------------------
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                 new ClaimsPrincipal(claimsIdentity),properties);

                //--------------------Sign In and Claims Principal- END ---------------------------

                ViewData["ValidateMessage"] = " User found";

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = " User Not found";

            return View();
        }



        //------------------Login Action Method -----------------------------

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}
