using FisOkuma.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FisOkuma.Common
{
    public static class RemoteApi
    {

        public static ReceiptStr GetFisFromApi(byte[] imageArray, string fileName)
        {
            var client = new RestClient("https://coden-dev.azurewebsites.net/api/");

            var request = new RestRequest("SaveFileToBlobStorage", Method.POST);
            request.AddQueryParameter("code", "FKfPkeYWaahi3Rb0FI8wkyepzgEIDE9GqXsPOnYsFn/mLkXADUSmww==");
            request.AddFileBytes("file", imageArray, fileName);

            IRestResponse<SaveReceiptOperationResult> response = client.Execute<SaveReceiptOperationResult>(request);


            return response.Data.Receipt;
        }
    }
}
