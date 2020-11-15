using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient.DAL
{
    public interface ITransferDAO
    {
        Transfer GetTransfer(int transferID);
        List<Transfer> TransferFrom(int userID);
        List<Transfer> TransferTo(int userID);
        Transfer CreateTransfer(Transfer newTransfer);
    }
}
