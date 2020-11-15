using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /***************
         * Implement these endpoints:
         * 
         *  /user/{id}       GET     - get specific user
         *  /user            GET     - get list of users
         *  /user            POST    - add user
         ***************/

        private IUserDAO userDAO;

        public UserController(IUserDAO userDAO)
        {
            
            this.userDAO = userDAO;
        }

        [HttpGet()]
        public List<User> ListUsers()
        {
            return userDAO.GetUsers();
        }

        [HttpGet("{userID}")]
        public ActionResult<User> GetUserByID(int userID)
        {
            User user = userDAO.GetUserByID(userID);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet()]
        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            User newUser = userDAO.AddUser(user.Username, user.PasswordHash);
            return Created($"/user/{newUser.UserId}", newUser);
        }
    }
}
