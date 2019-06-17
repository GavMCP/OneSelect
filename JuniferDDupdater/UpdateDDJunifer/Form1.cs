using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateDDJunifer
{
    // ****************************************************************
    // NAME: Junifer Direct Debit Updater.
    // VERSION: 1.1.0
    // AUTHOR: Gavin Challice
    // DATE: 04/10/2018
    // SUBJECT: A simple GUI program to allow a user to pick a CSV file that contains Junifer account numbers and new Direct Debit data.
    //          From this file the data several RESTful GET, PUT, and POST requests are made which will result in the account DDs being
    //          updated.
    // 
    // Update 1: 10/10/2018: Gavin C: Added functionality for a report creator. This can be run after an DD update has taken place which will
    //                                copy the txtMainText text to a text report stating success and failure.
    // ****************************************************************



    public partial class frmMain : Form
    {
        // USED ONLY FOR TEST AGAINST JUNIFER UAT. ****************************
        private string testURL = "http://94.236.0.52:43002/rest/v1/";

        private string liveURL = "http://146.177.7.108:43002/rest/v1/";
        // ********************************************************************
        // Name of the excel csv file containing the updates to take place.
        private string _CSVfileName;

        // Used for creating reports after an update has taken place.
        private StringBuilder sb = new StringBuilder();

        // Variables for APIs **************************************************************************************************
        // GET Method Call. Returns Database ID
        private string _AccountNumber;
        private string _dateOfMonthlyPayment;

        // Did the process complete successfully.
        bool? processComplete = null;
        
        public frmMain()
        {
            InitializeComponent();
        }
        
        // ********************************* EVENT HANDLERS ************************************************************

        /// <summary>
        /// Get the filename which contains the account and new DD details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCSVfile_Click(object sender, EventArgs e)
        {
            // Create new instance of FileOpenDialog and set properties.
            OpenFileDialog findFile = new OpenFileDialog
            {
                Filter = "CSV files(*.csv)| *.csv",
                RestoreDirectory = true
                
            };
            if (findFile.ShowDialog() == DialogResult.OK)
            {
                _CSVfileName = findFile.FileName;
                txtMainDisplay.Text = string.Concat(txtMainDisplay.Text, "CSV file picked: " + _CSVfileName + "\r\n\r\nClick 'Update' to begin DD Update.\r\n");
                txtMainDisplay.Update();
            }
        }//----------------------------------------------------------------------------------------------

        /// <summary>
        /// Start the process of updating Junifer with the new account details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateJunifer_Click(object sender, EventArgs e)
        {
            Task tskRunReport = Task.Run(() =>
            {
                StartUpdate();
            });
            
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Create a text report after an update has been run. This will contain all account numbers and what their DD amount should be.
        /// Will include Success and Failures.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save Junifer Update Results",
                Filter = "txt files(*.txt)| *.txt"
            };
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                if(saveFile.FileName!="")
                {
                    string saveFileName = saveFile.FileName;
                    File.WriteAllText(saveFileName, sb.ToString());
                }
            }
            else
            {
                MessageBox.Show("Enter a name for your save file.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }




        // *************************************** METHODS ****************************************************

        /// <summary>
        /// Run the process of extracting Account & DD info from CSV file. Creating REST services with Junifer API,
        /// remove current DD info and then update Junifer with new DD data.
        /// </summary>
        private void StartUpdate()
        {
            txtMainDisplay.Text = "Starting DD update...\r\n";
            // Make sure csv filename is not blank...1.
            if (_CSVfileName != null)
            {
                // Extract string data from CSV and convert DD data to Double data type.
                // Insert extracted data into Dict<string, double>.
                txtMainDisplay.Text = string.Concat(txtMainDisplay.Text,
                    "Extracting Account ID , Scheduled Payment ID, and DD data from CSV file.\r\n");
                txtMainDisplay.Update();
                ExtractDataFromCSV(_CSVfileName);
            }
            else
            {
                txtMainDisplay.Text =
                    string.Concat(txtMainDisplay.Text, "No CSV File selected...DD update process aborted.\r\n");
                MessageBox.Show("No CSV file selected.\r\nClick 'Select CSV File' to pick a file to process.\r\n", "Invalid Filename", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                
                return;
            }

        } // -------------------------------------------------------------------------------------------------------------------------

        
        /// <summary>
        /// Extract the new DD and account details from CSV file and place in Dict.
        /// </summary>
        /// <param name="csvFile"></param>
        private void ExtractDataFromCSV(string csvFile)
        {
            using (StreamReader sr = new StreamReader(csvFile))
            {
                int counter = 0;
                int recordCounter = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Split each column from the CSV file.      
                    string[] parts = line.Split(',');
                    // Convert DD string value into double data type.
                    double ddNewAmount = Convert.ToDouble(parts[1]);
                    string databaseID = "";
                    string scheduledPaymentID = "";
                   
                    // Extract Account number 225...... from CSV file.
                    _AccountNumber = parts[0];
                    // Get the databaseID for this account by passing in account Number.    GET request
                    databaseID =  GETdatabaseID(_AccountNumber);

                    if (databaseID != "error")
                    {
                        // Get latest/uptodate PaymentSchedulePaymentID for this account. GET request.
                        scheduledPaymentID = GETpaymentSchedulePaymentID(databaseID, _AccountNumber);
                        if (scheduledPaymentID != "error")
                        {
                            // Remove current DD details from account. Pass in Junifer databaseID, Account Number and PaymentSchedulePeriodid   PUT Request
                            string result = StopOldDDValue(databaseID, scheduledPaymentID, _AccountNumber);
                            if (result != "error")
                            {
                                // POST new DD value into Junifer
                                string postResult = POSTNewAmount(databaseID, ddNewAmount, _dateOfMonthlyPayment);
                                if (postResult != "error")
                                {
                                    // Increment counter for textbox main display, prepended to the beginning of each line.
                                    counter++;
                                    txtMainDisplay.Text =
                                        txtMainDisplay.Text + "\r\n" + counter + " Account No: " + _AccountNumber +
                                        " has been updated.";
                                    txtMainDisplay.Update();
                                    // Add to StringBuilder for Report creation once update is complete.
                                    sb.AppendLine(counter+ " Account No: " + _AccountNumber + " has been updated.        "+ parts[1]);
                                    // Increment record counter txtBox..
                                    recordCounter++;
                                    txtCounter.Text = ""+recordCounter+"";
                                    txtCounter.Update();
                                }
                                else
                                {
                                    
                                    counter++;
                                    txtMainDisplay.Text =
                                        txtMainDisplay.Text + "\r\n" + counter + " ERROR: Account No: " + _AccountNumber +
                                        " could not be updated with new DD details.";
                                    txtMainDisplay.Update();
                                    sb.AppendLine(counter + " ERROR: Account No: " + _AccountNumber + " could not be updated with new DD details.        "+ parts[1]);
                                    recordCounter++;
                                    txtCounter.Text = ""+recordCounter+"";
                                    continue;
                                }
                               
                                continue;           
                            }
                            else if(result == "fatal")
                            {
                                // Fatal non-recoverable error occurs.
                                return;
                            }
                            else
                            {
                                counter++;
                                txtMainDisplay.Text =
                                    txtMainDisplay.Text + "\r\n" + counter + " ERROR: Account No: " + _AccountNumber +
                                    ": Could not remove the current Scheduled D.D Payment for this account.";
                                txtMainDisplay.Update();
                                sb.AppendLine(counter + " ERROR: Account No: " + _AccountNumber +
                                    ": Could not remove the current Scheduled D.D Payment for this account.        "+parts[1]);
                                recordCounter++;
                                txtCounter.Text = ""+recordCounter+"";
                                continue;
                            }
                        }
                        else if(scheduledPaymentID == "fatal")
                        {
                            // If a fatal non-recoverable error occurs.
                            return;
                        }
                        else
                        {
                            // move on to next iteration of the loop.
                            counter++;
                            txtMainDisplay.Text =
                                txtMainDisplay.Text + "\r\n" + counter + " ERROR: Account No: " + _AccountNumber +
                                ": Could not get the present Scheduled Payment ID for this account.";
                            txtMainDisplay.Update();
                            sb.AppendLine(counter + " ERROR: Account No: " + _AccountNumber +
                                ": Could not get the present Scheduled Payment ID for this account.        "+parts[1]);
                            recordCounter++;
                            txtCounter.Text = ""+recordCounter+"";
                            continue;
                        }
                    }
                    else
                    {
                        counter++;
                        txtMainDisplay.Text =
                            txtMainDisplay.Text + "\r\n\r\n" + counter + " ERROR: Account No: " + _AccountNumber +
                            ": Could not get the database ID for this account.\r\nCheck that this account exists in Junifer.\r\n";
                        txtMainDisplay.Update();
                        sb.AppendLine(counter + " ERROR: Account No: " + _AccountNumber +
                            ": Could not get the database ID for this account.\r\nCheck that this account exists in Junifer.        "+parts[1]);
                        recordCounter++;
                        txtCounter.Text = ""+recordCounter+"";
                        // Move on to next iteration of While Loop.
                        continue;
                    }
                    
                }
                txtMainDisplay.Text = txtMainDisplay.Text + "\r\nDIRECT DEBIT UPDATE COMPLETE!";
                txtMainDisplay.Update();
              
            }
        } // ----------------------------------------------------------------------------------------------------------------------------


       
        /// <summary>
        /// Get the database ID for an account. From this we can get the Payments Scehduled ID currently setup in each account.
        /// </summary>
        /// <param name="accountNumber"></param>
        private string GETdatabaseID(string accountNumber)
        {
            string databaseId = "";

            var client = new RestClient("http://146.177.7.108:43002/rest/v1/accounts?number=" + accountNumber);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            
            var response = client.Execute(request).Content;      // Get the whole JSON results
            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode; // Get the StatusCode from the request.

            int numericStatusCode = (int)statusCode;
            if (numericStatusCode != 200)
            {
                txtMainDisplay.Text =
                    txtMainDisplay.Text = "\r\nCould not extract Database ID for account " + accountNumber;
                txtMainDisplay.Update();
                sb.AppendLine("Error: Could not extract Database ID for account: " + accountNumber );
                return "error";
            }
            else
            {
                    JArray results = (JArray)JObject.Parse(response)["results"];
                    var results0 = (from d in results select d).FirstOrDefault();
                    // Extract the ID field value from JSON string and return it.
                    return databaseId = (string)results0["id"];
                       
            }
        }


        /// <summary>
        /// Get the latest Scheduled Payment Period ID for this account.
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        private string GETpaymentSchedulePaymentID(string databaseId, string accountNumber)
        {
            string scheduledPaymentID = "";
            var client = new RestClient("http://146.177.7.108:43002/rest/v1/accounts/" + databaseId + @"/paymentSchedulePeriods");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            try
            {
                string response = client.Execute(request).Content;
                // Check status code reponse.
                IRestResponse restResponse = client.Execute(request);
                HttpStatusCode statusCode = restResponse.StatusCode; // Get the StatusCode from the request.
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode != 200)
                {
                    txtMainDisplay.Text =
                        txtMainDisplay.Text = "\r\nCould not extract Scheduled Payment ID for this account: " + accountNumber;
                    return "error";
                }
                else
                {
                    JArray results = (JArray)JObject.Parse(response)["results"];

                    // We want the latest payment details.
                    var results0 = (from d in results select d).LastOrDefault();
                    // Extract the current payment date each month from the current payment plan and use this in setup of the next payment plan.
                    _dateOfMonthlyPayment = (string)results0["frequencyAlignmentDt"];
                    return scheduledPaymentID = (string)results0["id"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal Error getting current Scheduled Payment ID from Junifer.\r\nError: " + ex.Message, "Critical GET RESTful Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                sb.AppendLine("Fatal Error getting current Scheduled Payment ID from Junifer.\r\nError: " + ex.Message + "\r\nInner Error: " + ex.InnerException + "\r\nStack Trace: " + ex.StackTrace);
                return "fatal";
            }
            
        }



        /// <summary>
        /// Create REST PUT to delete accounts current Direct Debit details.
        /// </summary>
        /// <param name="paymentSPID"></param>
        private string StopOldDDValue(string databaseID, string paymentSPID, string accountNumber)
        {

            int dbIDValue = Convert.ToInt32(databaseID);
            string toDtValue = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
            // frmDtValue is needed for JSON package but the value is not used.
            string frmDtValue = "2018-01-01";
            // defaultDDAmount is needed for the JSON package in PUT request but the value is not needed. 
            double defaultDDamount = 0.01;

            var client = new RestClient("http://146.177.7.108:43002/rest/v1/paymentSchedulePeriods/" + paymentSPID);
            var request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\r\n  \"account.id\":" + dbIDValue + ",\r\n  \"fromDt\": \"" + frmDtValue + "\",\r\n  \"toDt\": \"" + toDtValue + "\",\r\n  \"frequency\": \"Monthly\",\r\n  \"frequencyMultiple\": 1,\r\n  \"frequencyAlignmentDt\": \"" + _dateOfMonthlyPayment + "\",\r\n  \"amount\":" + defaultDDamount + "\r\n}", ParameterType.RequestBody);

            // Execute the Request
            try
            {
                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode == 204)
                {
                    return "";
                }
                else
                {
                    return "error";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fatal Error removing current Scheduled Payment Amount.\r\nError: " + ex.Message, "Critical PUT Update Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                // write to the report
                sb.AppendLine("Fatal Error removing current Scheduled Payment Amount.\r\nError: " + ex.Message + "\r\nInner Error: " + ex.InnerException + "\r\nStack Trace: " + ex.StackTrace);
                return "fatal";
            }
            
        }

       
        /// <summary>
        /// POST the new details into Junifer.
        /// </summary>
        /// <param name="databaseID"></param>
        /// <param name="newAmount"></param>
        /// <param name="paymentDate"></param>
        private string POSTNewAmount(string databaseID, double newDDamount, string paymentDate)
        {
            var client = new RestClient("http://146.177.7.108:43002/rest/v1/paymentSchedulePeriods");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            string frmDtValue = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));

            request.AddParameter("undefined", "{\r\n  \"account.id\":" + databaseID + ",\r\n  \"fromDt\": \"" + frmDtValue + "\",\r\n  \"frequency\": \"Monthly\",\r\n  \"frequencyMultiple\": 1,\r\n  \"frequencyAlignmentDt\": \"" + paymentDate + "\",\r\n  \"seasonalPaymentFl\": false,\r\n  \"amount\":"+newDDamount+" \r\n}", ParameterType.RequestBody);
           
            try
            {
                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                if (numericStatusCode != 201)
                {
                    MessageBox.Show("Error updating new DD amount. Error code: " + numericStatusCode, "POST Update Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return "error";

                }
                else
                {
                    // Success, do nothing.
                    return "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatal Error Updating new DD amount. Error: " + ex.Message, "Critical POST Update Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                // write to the report
                sb.AppendLine("Fatal Error Updating new DD amount. Error: " + ex.Message+"\r\nInner Error: "+ex.InnerException+"\r\nStack Trace: "+ex.StackTrace);
                return "fatal";
            }
        }

        
    }
}
