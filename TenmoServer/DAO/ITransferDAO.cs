using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        Transfer Create(Transfer newTransfer);
    
        Transfer Get(int transferID);
        List<Transfer> TransferTo(int userID);
        List<Transfer> TransferFrom(int userID);

    }
}
