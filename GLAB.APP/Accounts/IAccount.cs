using GLAB.Domains.Models.Users;

namespace GLAB.Api.Accounts;

public interface IAccount
{
    
    Task<(LoginStatus,User)> CheckCredentials(string user,string password);


    Task<bool> ChangePassword(string userid, string oldpassword, string newpassword);
    
        
    public enum LoginStatus
    {
        CanLogin,
        UserBlocked,
        UserNotExists,
        WrongCredentials
    }
    
}