using System;
using System.Collections.Generic;
using TenmoClient.DAL;
using TenmoClient.Data;
using TenmoClient.Views;

namespace TenmoClient
{
    class Program
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        static void Main(string[] args)
        {
            IAccountDAO accountDAO = new AccountDAO (API_BASE_URL);
            ITransferDAO transferDAO = new TransferDAO(API_BASE_URL);
            ConsoleService consoleService = new ConsoleService();
            AuthService authService = new AuthService();

            new LoginRegisterMenu(authService, accountDAO, transferDAO, consoleService).Show();
            
            Console.WriteLine("\r\nThank you for using TEnmo!!!\r\n");
        }
    }
}
