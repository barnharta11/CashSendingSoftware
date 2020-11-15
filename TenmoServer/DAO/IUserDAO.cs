using System.Collections.Generic;
using System.Reflection;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IUserDAO
    {
        User GetUser(string username);
        User GetUserByID(int userID);
        User AddUser(string username, string password);
        List<User> GetUsers();
    }
}
