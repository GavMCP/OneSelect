using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using SimpleJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Net;

namespace SandboxTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //string accountNumber = "225029688"; // Account Number in Junifer
            //var client = new RestClient("http://94.236.0.52:43002/rest/v1/accounts?number=" + accountNumber);
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("accept", "application/json");
            //string response = client.Execute(request).Content;
            //JArray results = (JArray)JObject.Parse(response)["results"];
            //var results0 = (from d in results select d).FirstOrDefault();
            //string dbID = (string)results0["id"];
            //Console.WriteLine(dbID);

            // ********************************************************************************************************************************************************

            //string dbID = "29455";   // Database ID in junifer
            //var client = new RestClient("http://94.236.0.52:43002/rest/v1/accounts/" + dbID + @"/paymentSchedulePeriods");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("accept", "application/json");
            //string response = client.Execute(request).Content;
            //JArray results = (JArray)JObject.Parse(response)["results"];
            //var results0 = (from d in results select d).LastOrDefault();
            //string paymentID = (string)results0["id"];
            //string dateOfpayment = (string)results0["frequencyAlignmentDt"];

            //Console.WriteLine(paymentID + " " + dateOfpayment);
            // *******************************************************************************************************************************************************

            // List of parameters for PUT REST request. USING JUNIFER UAT
            //URL Paramter ID

            // int DBidValue = 29455;
            ////int idValue = Convert.ToInt32(paymentSPID);
            //string PUTidValue = "39896";

            //// string toDtValue = "2018-10-19";
            //string toDtValue = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
            //string frmDtValue = "2018-01-06";
            //string frequencyAlignmentDtValue = "2018-10-19";
            //double amountValue = 0.01;

            //////// Execute the Request

            //var client = new RestClient("http://94.236.0.52:43002/rest/v1/paymentSchedulePeriods/" + PUTidValue);
            //var request = new RestRequest(Method.PUT);
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Content-Type", "application/json");

            //request.AddParameter("undefined", "{\r\n  \"account.id\":" + DBidValue + ",\r\n  \"fromDt\": \"" + frmDtValue + "\",\r\n  \"toDt\":\"" + toDtValue + "\",\r\n  \"frequency\": \"Monthly\",\r\n  \"frequencyMultiple\": 1,\r\n  \"frequencyAlignmentDt\": \"" + frequencyAlignmentDtValue + "\",\r\n  \"amount\":" + amountValue + "\r\n}", ParameterType.RequestBody);
            ////IRestResponse response = client.Execute(request);
            //try
            //{
            //    //client.Execute(request);
            //    IRestResponse response = client.Execute(request);
            //    HttpStatusCode statusCode = response.StatusCode;
            //    int numericStatusCode = (int)statusCode;
            //    if (numericStatusCode == 204)
            //    {
            //        Console.WriteLine("Status code is {0}", numericStatusCode);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error with PUT REQUEST. StatusCode = {0}", numericStatusCode);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error removing old Direct Debit details for this account: \r\nError: " + ex.Message,
            //        "Error Processing PUT REST Request", MessageBoxButtons.OK, MessageBoxIcon.Error,
            //        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //    return;
            //}

            // *******************************************************************************************************************************************************
            int idValue = 805;
            string frequencyAlignmentDtValue = "2018-10-19";
            string frmDtValue = "2018-10-08";
            double newDDamount = 195.25;
            var client = new RestClient("http://94.236.0.52:43002/rest/v1/paymentSchedulePeriods");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("undefined", "{\r\n  \"account.id\":" + idValue + ",\r\n  \"fromDt\": \"" + frmDtValue + "\",\r\n  \"frequency\": \"Monthly\",\r\n  \"frequencyMultiple\": 1,\r\n  \"frequencyAlignmentDt\": \"" + frequencyAlignmentDtValue + "\",\r\n  \"seasonalPaymentFl\": false,\r\n  \"amount\":"+newDDamount+" \r\n}", ParameterType.RequestBody);
            //request.AddParameter("undefined", "{\r\n  \"account.id\": 29455 ,\r\n  \"fromDt\": \"2018-10-08\",\r\n  \"frequency\": \"Monthly\",\r\n  \"frequencyMultiple\": 1,\r\n  \"frequencyAlignmentDt\": \"2018-10-19\",\r\n  \"seasonalPaymentFl\": false,\r\n  \"amount\": 195.00\r\n}", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            //IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 400)
            {
                Console.WriteLine("Error with POST. Status code is {0}", numericStatusCode);
            }
            else
            {
                Console.WriteLine("StatusCode = {0}", numericStatusCode);
            }
            // *********************************************************************************************************************************************************************




        }
    }
}
