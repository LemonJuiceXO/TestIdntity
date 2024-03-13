using GLAB.Domains.Models.Users;

namespace Users.Infra.Storages;

public interface IUserStorage
{
    ValueTask<User?> SelectUserById(string userId);

    ValueTask<User> SelectUserByUserName(string userName);

    ValueTask<string> SelectUserPassword(string userId);

    ValueTask<bool> InsertUser(User user);

  
}