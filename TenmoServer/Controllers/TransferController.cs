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
    public class TransferController : ControllerBase
    {

        private ITransferDAO transferDAO;

        public TransferController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }

        [HttpGet("to/{userID}")]
        public List<Transfer> TransferTo(int userID)
        {
            return transferDAO.TransferTo(userID);
        }

        [HttpGet("from/{userID}")]
        public List<Transfer> TransferFrom(int userID)
        {
            return transferDAO.TransferFrom(userID);
        }

        [HttpGet("{transferID}")]
        public ActionResult<Transfer> GetTransfer(int transferID)
        {
            Transfer transfer = transferDAO.Get(transferID);
            
            if (transfer == null)
            {
                return NotFound();
            }
            else
            {
                return transfer;
            }
        }

        [HttpPost()]
        public ActionResult<Transfer> CreateTransfer(Transfer transfer) 
        {
            Transfer newTransfer = transferDAO.Create(transfer);
            return Created($"transfer/{newTransfer.TransferID}", newTransfer);
        }

    }
}
