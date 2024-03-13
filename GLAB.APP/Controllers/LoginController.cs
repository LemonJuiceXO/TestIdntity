using GLAB.Api.Accounts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace GLAB.Api.Controllers;

public class LoginController : Controller
{

    [Inject] private IAccount accountservice { get; }
    
    // GET
    public IActionResult MainPage()
    {
        return View();
    }

    public IActionResult Login(string username, string password)
    {
        var Status =  accountservice.CheckCredentials(username, password).Result;

        if (Status == IAccount.LoginStatus.CanLogin)
        {
            CookieOptions cookieOptions = new CookieOptions()
            {
            Expires = DateTime.Now.AddHours(15)
            ,SameSite = SameSiteMode.Strict
            ,Domain = "GLAB"
            };
            Response.Cookies.Append(new Guid().ToString(),"GLAB.Cookie",cookieOptions);
            
            RedirectToAction("MainPage");
        }
        else
        {
            throw new Exception(Status.ToString());
        }

        return null;
    }
    
    
}