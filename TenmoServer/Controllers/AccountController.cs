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
    public class AccountController : ControllerBase
    {
        /***************
         * Implement these endpoints:
         * 
         *  /account/{id}       GET     - user account balance
         *  /account/{id}       PUT     - Update account balance
         
         ***************/

        private IAccountDAO accountDAO;
        

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }

        
        [HttpGet("{id}")]
        public Account GetAccount(int id)
        {
            return accountDAO.GetAccount(id);
        }


        [HttpPut("{accountID}")]

        public ActionResult<Account> UpdateAccount(int accountID, Account account)
        {
            if (accountDAO.UpdateAccount(accountID, account))
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }
        }
    }
    
}
