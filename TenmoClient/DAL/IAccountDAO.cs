using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient.DAL
{
    public interface IAccountDAO
    {
        Account GetAccount(int accountID);
        Account UpdateAccount(int accountID, Account accountToUpdate);
    }
}
