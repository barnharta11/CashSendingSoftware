using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;
using TenmoClient.Models;

namespace TenmoClient
{
    public class ConsoleService
    {
        //Print methods

        public void PrintUsers(List<API_User> users)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("--------------------------------------------");
            foreach (API_User user in users)
            {
                Console.WriteLine($"Username: {user.Username} UserID: {user.UserId}");
            }
        }

        public void PrintAccount(Account account)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Account Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($" Account Id: {account.AccountID}");
            Console.WriteLine($" User Id: {account.UserID}");
            Console.WriteLine($" Username: {account.Username}");
            Console.WriteLine($" Balance: {account.Balance}");

        }

        public void PrintTransfers(List<Transfer> transferTo, List<Transfer> transferFrom)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Transfers\nID\t\tFrom/To\t\tAmount");
            Console.WriteLine("--------------------------------------------");
            foreach (Transfer transfer in transferTo)
            {
                
                    Console.WriteLine($"{transfer.TransferID}\t\tTo: {transfer.AccountToA.Username}\t\t{transfer.Amount:C}");
            }
            foreach (Transfer transfer in transferFrom)
            {

                Console.WriteLine($"{transfer.TransferID}\t\tFrom: {transfer.AccountFromA.Username}\t\t{transfer.Amount:C}");
            }
        }

        public void PrintTransfer(Transfer transfer, Account accountFrom, Account accountTo)
        {
            
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Transfer Details");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($" Transfer Id: {transfer.TransferID}" );
            Console.WriteLine($" From: {accountFrom.Username}");
            Console.WriteLine($" To: {accountTo.Username}");
            Console.WriteLine($" Type: {transfer.TransferTypeDesc}");
            Console.WriteLine($" Status: {transfer.TransferStatusDesc}");
            Console.WriteLine($" Amount: {transfer.Amount:C}");

        }

        public void PrintSentTransfer(Transfer transfer, Account accountFrom, Account accountTo)
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Users\nID\t\tName");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"{transfer.AccountTo}\t\t{accountTo.Username}");
            Console.WriteLine($"{transfer.AccountFrom}\t\t{accountFrom.Username}");
            Console.WriteLine("---------");
            
        }


    }

}
