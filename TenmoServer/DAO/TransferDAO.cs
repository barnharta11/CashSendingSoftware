using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private static List<Transfer> Transfers { get; set; }
        private readonly string connectionString;
        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;

        }

        public Transfer Create(Transfer newTransfer)
        {
            string sql = "Insert into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) Values (@transfer_type_id, @transfer_status_id, @account_from, @account_to, @amount)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@transfer_type_id", newTransfer.TransferTypeID);
                    command.Parameters.AddWithValue("@transfer_status_id", newTransfer.TransferStatusID);
                    command.Parameters.AddWithValue("@account_from", newTransfer.AccountFrom);
                    command.Parameters.AddWithValue("@account_to", newTransfer.AccountTo);
                    command.Parameters.AddWithValue("@amount", newTransfer.Amount);

                    newTransfer.TransferID = Convert.ToInt32(command.ExecuteScalar());
                    return newTransfer;

                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Transfer Get(int transferID)
        {
            Transfer transfer = new Transfer();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select transfers.*, transfer_statuses.transfer_status_desc, transfer_types.transfer_type_desc from transfers join transfer_statuses on transfer_statuses.transfer_status_id = transfers.transfer_status_id join transfer_types on transfer_types.transfer_type_id = transfers.transfer_type_id Where transfer_id = @TransferID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@TransferID", transferID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    transfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                    transfer.TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]);
                    transfer.TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]);
                    transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
                    transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
                    transfer.Amount = Convert.ToDecimal(reader["amount"]);
                    transfer.TransferStatusDesc = Convert.ToString(reader["transfer_status_desc"]);
                    transfer.TransferTypeDesc = Convert.ToString(reader["transfer_type_desc"]);

                }
                return transfer;
            }

        }


        public List<Transfer> TransferTo(int userID)
        {
            List<Transfer> transfers = new List<Transfer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select transfers.*, accounts.account_id, users.username from transfers join accounts on accounts.account_id = transfers.account_to join users on users.user_id = accounts.user_id Where accounts.user_id = @UserID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserID", userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = new Transfer();
                    transfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                    transfer.TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]);
                    transfer.TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]);
                    transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
                    transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
                    transfer.Amount = Convert.ToInt32(reader["amount"]);
                    transfer.AccountID = Convert.ToInt32(reader["account_id"]);
                    transfer.Username = Convert.ToString(reader["username"]);
                    
                    transfers.Add(transfer);

                }
            }
            return transfers;


        }
        public List<Transfer> TransferFrom(int userID)
        {
            List<Transfer> transfers = new List<Transfer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select transfers.*, accounts.account_id, users.username from transfers join accounts on accounts.account_id = transfers.account_from join users on users.user_id = accounts.user_id Where accounts.user_id = @UserID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserID", userID);


                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = new Transfer();
                    transfer.TransferID = Convert.ToInt32(reader["transfer_id"]);
                    transfer.TransferTypeID = Convert.ToInt32(reader["transfer_type_id"]);
                    transfer.TransferStatusID = Convert.ToInt32(reader["transfer_status_id"]);
                    transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
                    transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
                    transfer.Amount = Convert.ToInt32(reader["amount"]);
                    transfer.AccountID = Convert.ToInt32(reader["account_id"]);
                    transfer.Username = Convert.ToString(reader["username"]);

                    transfers.Add(transfer);
                    
                }
            }
            return transfers;

        }
    }
}
