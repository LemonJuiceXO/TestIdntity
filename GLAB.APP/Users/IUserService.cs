using GLAB.Domains.Models.Users;

namespace GLAB.Api.Users;

public interface IUserService 
{
    Task<User?> GetUserById(string userId);
    
    Task<User> GetUserByUserName(string userName);

    Task<string> GetUserPassword(string userId);

    Task<bool> CreateUser(User user);

    Task<bool> ValidatePassword(string userId , string userPassword);
}