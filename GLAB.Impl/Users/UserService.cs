using GLAB.Api.Users;
using GLAB.Domains.Models.Users;
using GLab.Impl.Services.Users;
using Users.Infra.Storages;

namespace GLAB.Implementation.Users;

public class UserService : IUserService

{

    private readonly IUserStorage userStorage;
    
    private PasswordHasher passwordHasher;

    public UserService(IUserStorage userStorage)
    {
        this.userStorage = userStorage;
        passwordHasher = new PasswordHasher();
    }

    public async Task<User?> GetUserById(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByUserName(string userName)
    {
       return   await userStorage.SelectUserByUserName(userName);
        
    }

    public async Task<string> GetUserPassword(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ValidatePassword(string userId, string userPassword)
    {
       string password= await userStorage.SelectUserPassword(userId);
       return passwordHasher.VerifyHashedPassword(password, userPassword);
    }
}