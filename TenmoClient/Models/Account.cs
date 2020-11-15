using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Models
{
    public class Account
    {
        public decimal Balance { get; set; }
        public int AccountID { get; set; }

        public string Username { get; set; }
        public int UserID { get; set; }

        
    }
}
