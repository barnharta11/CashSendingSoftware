namespace TenmoServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }

        //public User (int userID, string username, string passwordHash, string salt, string email)
        //{
        //    UserId = userID;
        //    Username = username;
        //    PasswordHash = passwordHash;
        //    Salt = salt;
        //    Email = email;
        //}
    }

    /// <summary>
    /// Model to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; }

        //public ReturnUser (int userId, string username, string token)
        //{
        //    UserId = userId;
        //    Username = username;
        //    Token = token;
        //}
    }

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //public LoginUser (string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}
    }
}
