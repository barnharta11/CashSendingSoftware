using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient.DAL
{
    public class AccountDAO : IAccountDAO
    {
        //private readonly static string API_BASE_URL = "https://localhost:44315/";

        static protected RestClient client;
        public AccountDAO(string API_BASE_URL)
        {
            client = new RestClient(API_BASE_URL);
        }

        public Account GetAccount(int accountID)
        {
            RestRequest request = new RestRequest($"account/{accountID}");
            IRestResponse<Account> response = client.Get<Account>(request);
            CheckResponse(response); 

            return response.Data;
        }

        public Account UpdateAccount(int accountID, Account accountToUpdate)
        {
            RestRequest request = new RestRequest($"account/{accountToUpdate.AccountID}");
            request.AddJsonBody(accountToUpdate);
            IRestResponse<Account> response = client.Put<Account>(request);
            CheckResponse(response); 

            return response.Data;
        }

        public static void CheckResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.");
            }

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authorization is required for this option. Please log in.");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new Exception("You do not have permission to perform the requested action");
                }

                throw new Exception($"Error occurred - received non-success response: {response.StatusCode} ({(int)response.StatusCode})");
            }
        }
    }
}
