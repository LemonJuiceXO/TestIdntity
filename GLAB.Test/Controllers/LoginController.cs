using System.Data.SqlClient;
using System.Security.Claims;
using GLAB.Api.Accounts;
using GLAB.Api.Users;
using GLAB.APP.Members;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Guid = System.Guid;

namespace GLAB.Api.Controllers;

public class LoginController : Controller
{

    private readonly IAccount _accountService;

    private readonly IUserService _userService;
    private readonly IMemberService _memberService;
    
    public LoginController(IAccount accountService,IUserService userService)
    {
        _accountService = accountService;
        this._userService = userService;
        
      
    }
    
    // GET
    public IActionResult CreateLabo2()
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
    public async Task<IActionResult> Login(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            TempData["Error"] = "Empty Username Or Password ";
            
            return RedirectToAction("LoginPage");
            
        }
     
        
        
        var (Status,user) =  await _accountService.CheckCredentials(username, password);

     
        if (Status == IAccount.LoginStatus.CanLogin)
        {
            
         await _userService.getUserRoles(user.UserId);
            
            try
            {
               
                var Claims = new List<Claim>()
                
                {
                    
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserId)
                
                };
                
                var identity = new ClaimsIdentity(Claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
                
            }
         
          return  RedirectToAction("CreateLabo2");
        }
        else
        {
            TempData["Error"] =  checkStatus(Status);
            
            return RedirectToAction("LoginPage");
        }

        
    }

    private string checkStatus(IAccount.LoginStatus status)
    {
        if (status == IAccount.LoginStatus.UserNotExists || status == IAccount.LoginStatus.UserBlocked)
        {
            return "User blocked Or Doesn't Exist";
        }
        else
        {
            return "Wrong Credentials";
        }
        
    }
    

    public IActionResult Logout(string username)
    {
        HttpContext.SignOutAsync();
      return RedirectToAction("LoginPage");
      
    }
   
    
    
}