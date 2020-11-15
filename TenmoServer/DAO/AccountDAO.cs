using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {
        private static List<Account> Accounts { get; set; }
        private readonly string connectionString;
        public AccountDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccount(int accountID)
        {
            Account account = new Account();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select accounts.*, users.username from accounts join users on users.user_id = accounts.user_id Where account_id = @AccountID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AccountID", accountID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    account.AccountID = Convert.ToInt32(reader["account_id"]);
                    account.UserID = Convert.ToInt32(reader["user_id"]);
                    account.Balance = Convert.ToDecimal(reader["balance"]);
                    account.Username= Convert.ToString(reader["username"]);
                }
                return account;
            }
        }

        public bool UpdateAccount(int accountID, Account account)
        {
            string sql = "Update accounts set balance = @Balance where account_id = @AccountID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@AccountID", accountID);
                    command.Parameters.AddWithValue("@Balance", account.Balance);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }
    }
}
