using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;

namespace TenmoClient.Models
{
    public class Transfer
    {
        public int TransferID { get; set; }
        public int TransferTypeID { get; set; }
        public int TransferStatusID { get; set; }
        public int AccountFrom { get; set; }
        public int AccountTo { get; set; }
        public decimal Amount { get; set; }
        public string Username { get; set; }
        public int AccountID { get; set; }
        public string TransferTypeDesc { get; set; }
        public string TransferStatusDesc { get; set; }

        public API_User AccountFromA { get; set; }
        public API_User AccountToA { get; set; }
    }

}

