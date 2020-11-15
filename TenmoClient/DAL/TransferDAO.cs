using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TenmoClient.Models;

namespace TenmoClient.DAL
{
    public class TransferDAO : ITransferDAO
    {
        
        static protected RestClient client;
        public TransferDAO (string API_BASE_URL)
        {
            client = new RestClient(API_BASE_URL);
        }
        public Transfer CreateTransfer(Transfer newTransfer)
        {
            RestRequest request = new RestRequest($"transfer");
            request.AddJsonBody(newTransfer);
            IRestResponse<Transfer> response = client.Post<Transfer>(request);
            CheckResponse(response); 

            return response.Data;
        }
        public Transfer GetTransfer(int transferID)
        {
            RestRequest request = new RestRequest($"transfer/{transferID}");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);
            CheckResponse(response); 

            return response.Data;
        }
        public List<Transfer> TransferFrom(int userID)
        {
            RestRequest request = new RestRequest($"transfer/from/{userID}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
            CheckResponse(response); 

            return response.Data;
        }
        public List<Transfer> TransferTo(int userID)
        {
            RestRequest request = new RestRequest($"transfer/to/{userID}");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);
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
