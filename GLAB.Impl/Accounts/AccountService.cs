using System.Data.SqlClient;
using GLAB.Api.Accounts;
using GLAB.Api.Users;
using GLAB.Domains.Models.Users;


namespace GLAB.Implementation.Accounts;

public class AccountService : IAccount
{
    private readonly IUserService userService;

    public AccountService(IUserService userService)
    {
        this.userService = userService;
        
    }

    public async Task<IAccount.LoginStatus> CheckCredentials(string username, string password)
    {
       
        
      var user= await userService.GetUserByUserName(username);

      if (user == null || user.State == UserState.Deleted)
          return IAccount.LoginStatus.UserNotExists;

      if (user.State == UserState.Bloqued)
          return IAccount.LoginStatus.UserBlocked;


      bool isPasswordCorrect = await userService.ValidatePassword(user.UserId, password);

      if (isPasswordCorrect)
      {
          return IAccount.LoginStatus.CanLogin;  
      }

      return IAccount.LoginStatus.WrongCredentials;
      
      
    }

    public async Task<bool> ChangePassword(string userid, string oldpassword, string newpassword)
    {
        throw new NotImplementedException();
    }
}