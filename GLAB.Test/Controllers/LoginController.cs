using GLAB.Api.Accounts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GLAB.Api.Controllers;

public class LoginController : Controller
{

    private readonly IAccount _accountService;

    public LoginController(IAccount accountService)
    {
        _accountService = accountService;
    }
    
    // GET
    public IActionResult MainPage()
    {
        return View();
    }

    //GET
    public IActionResult LoginPage()
    {
        if (TempData["Error"] != null)
        {

            string errormessage = TempData["Error"].ToString();
                ViewBag.Error = errormessage;
              
        }
        
        return View();
    }


    
  

    //POST
    public IActionResult Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            TempData["Error"] = "Username or password is empty.";
            
            return RedirectToAction("LoginPage");
        }
        
  
        
      var (Status,user) =  _accountService.CheckCredentials(username, password).Result;
   
       
        if (Status == IAccount.LoginStatus.CanLogin)
        {
            try
            {
                CookieOptions cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(15),
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                    ,IsEssential = true
                    ,Path = "/"
                };
          
    
                Response.Cookies.Append(user.UserId, "GLAB.Cookie", cookieOptions);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
          return  RedirectToAction("MainPage");
        }
        else
        {
            throw new Exception(Status.ToString());
        }

        
    }

    public IActionResult Logout(string username)
    {
       Response.Cookies.Delete("DungEater");
      return RedirectToAction("LoginPage");
    }
    
    
}