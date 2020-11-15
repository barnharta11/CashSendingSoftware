using MenuFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.DAL;
using TenmoClient.Data;
using TenmoClient.Models;

namespace TenmoClient.Views
{
    public class MainMenu : ConsoleMenu
    {
        private IAccountDAO accountDAO;
        private ITransferDAO transferDAO;
        private AuthService authService;
        private readonly ConsoleService consoleService;

        public MainMenu(IAccountDAO accountDAO, ITransferDAO transferDAO, AuthService authService, ConsoleService consoleService)
        {
            this.accountDAO = accountDAO;
            this.transferDAO = transferDAO;
            this.authService = authService;
            this.consoleService = consoleService;

            AddOption("View your account details", ViewAccount)
                .AddOption("View your past transfers", ViewTransfers)
                .AddOption("View a specific transfer", ViewTransfer)
                .AddOption("Send TE bucks", SendTEBucks)
                //.AddOption("Request TE bucks", RequestTEBucks)
                .AddOption("Log in as different user", Logout)
                .AddOption("Exit", Exit);
        }

        protected override void OnBeforeShow()
        {
            SetColor(ConsoleColor.Green);
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"TE Account Menu for User: {UserService.GetUserName()}");
            int userID = UserService.GetUserId();
            Account account = accountDAO.GetAccount(userID);
            Console.WriteLine($"Balance for {UserService.GetUserName()}: {account.Balance:C}");
            Console.WriteLine("------------------------------------------");
        }

        private MenuOptionResult ViewAccount()
        {
            int userID = UserService.GetUserId();
            Account account = accountDAO.GetAccount(userID);
            consoleService.PrintAccount(account);
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfers()
        {
            int userID = UserService.GetUserId();

            List<Transfer> transfersFrom = transferDAO.TransferFrom(userID);
            List<Transfer> transfersTo = transferDAO.TransferTo(userID);
            foreach (Transfer transfer in transfersFrom)
            {
                transfer.AccountFromA = authService.GetUser(transfer.AccountFrom);
                transfer.AccountToA = authService.GetUser(transfer.AccountTo);
            }
            foreach (Transfer transfer in transfersTo)
            {
                transfer.AccountFromA = authService.GetUser(transfer.AccountFrom);
                transfer.AccountToA = authService.GetUser(transfer.AccountTo);
            }
            consoleService.PrintTransfers(transfersFrom, transfersTo);



            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult ViewTransfer()
        {
            Transfer transfer = new Transfer();

            transfer.TransferID = GetInteger("Please type in Transfer ID:");
            if (transfer.TransferID > 5)
            {
                Transfer myTransfer = transferDAO.GetTransfer(transfer.TransferID);
                Account accountTo = accountDAO.GetAccount(myTransfer.AccountTo);
                Account accountFrom = accountDAO.GetAccount(myTransfer.AccountFrom);
                Console.ForegroundColor = ConsoleColor.Green;
                consoleService.PrintTransfer(myTransfer, accountFrom, accountTo);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid Transfer ID.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SendTEBucks()
        {
            List<API_User> list = authService.GetUsers();
            consoleService.PrintUsers(list);
            Transfer newTransfer = new Transfer();

            int userID = UserService.GetUserId();
            int recID = GetInteger("Enter User ID of recipient:");
            if (recID != userID)
            {
                Account userAccount = accountDAO.GetAccount(userID);
                Account recAccount = accountDAO.GetAccount(recID);
                decimal amount = GetInteger($"Enter amount to transfer to {recAccount.Username}:");

                if (amount <= userAccount.Balance)
                {
                    userAccount.Balance = userAccount.Balance - amount;
                    recAccount.Balance = recAccount.Balance + amount;

                    accountDAO.UpdateAccount(recID, recAccount);
                    accountDAO.UpdateAccount(userID, userAccount);

                    newTransfer.Amount = amount;
                    newTransfer.AccountTo = recID;
                    newTransfer.AccountFrom = userID;
                    newTransfer.TransferStatusID = 2;
                    newTransfer.TransferTypeID = 2;

                    Transfer transfer = transferDAO.CreateTransfer(newTransfer);
                    Console.ForegroundColor = ConsoleColor.Green;
                    consoleService.PrintSentTransfer(transfer, userAccount, recAccount);
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter an amount less than {userAccount.Balance:C}.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Can't send money to yourself.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        //private MenuOptionResult RequestTEBucks()
        //{
        //    Console.WriteLine("Not yet implemented!");
        //    return MenuOptionResult.WaitAfterMenuSelection;
        //}

        private MenuOptionResult Logout()
        {
            UserService.SetLogin(new API_User()); //wipe out previous login info
            return MenuOptionResult.CloseMenuAfterSelection;
        }

    }
}
