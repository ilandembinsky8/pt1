//Copyright 2011 Jared Faris

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
namespace WcfService
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Xml.Linq;

    public class ScannerService : InterfaceService
    {
        private DataSet1TableAdapters.transactionTableAdapter report = new DataSet1TableAdapters.transactionTableAdapter();


        public string addtransaction(string type, string total)
        {
            int repsonse = report.InsertQuery(DateTime.Now, type, Convert.ToDecimal(total));
            if (repsonse == 1)
            {

           
            return JsonConvert.SerializeObject(report.GetInvoiceNo());
            }else
            {
                return "error";
            }
        }
         public string GroupTran(string from, string to)
        {
            string s = "";
            
            DataSet ds = new DataSet("TimeRanges");
            using (SqlConnection conn = new SqlConnection(@"Data Source=AHMAD-NAWAF\SQLEXPRESS;Initial Catalog=betaTekvah;Integrated Security=True"))
            {
                SqlCommand sqlComm = new SqlCommand("proc_GroupTransactionByDate", conn);
                sqlComm.Parameters.AddWithValue("@FromDate", from);
                sqlComm.Parameters.AddWithValue("@ToDate", to);
                sqlComm.Parameters.AddWithValue("@IsForCsv", 0);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }


            return JsonConvert.SerializeObject(ds);

        }

        public string SelectTran(string from, string to)
        {
            string s = "";

            DataSet ds = new DataSet("TimeRanges");
            using (SqlConnection conn = new SqlConnection(@"Data Source=AHMAD-NAWAF\SQLEXPRESS;Initial Catalog=betaTekvah;Integrated Security=True"))
            {
                SqlCommand sqlComm = new SqlCommand("proc_SelectTransaction", conn);
                sqlComm.Parameters.AddWithValue("@FromDate", from);
                sqlComm.Parameters.AddWithValue("@ToDate", to);
                sqlComm.Parameters.AddWithValue("@IsForCsv", 0);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }


            return JsonConvert.SerializeObject(ds);

        }

        public string startCaspit(string price)
        {
            string result = "";
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\kiosk\caspit\caspit.exe",
                    Arguments = price,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            p.Start();
            while (!p.StandardOutput.EndOfStream)
            {
                string line = p.StandardOutput.ReadLine();
                result += line;
                // do something with line
            }
            p.Close();


            return result;
        }


        public string getCash()
        {

           // PayLink manager = new PayLink();
               
            return PayLink.getinstance().readValue();

        }
        public string startSdkPaylink()
        {
            PayLink.getinstance().startPaylink();
            return "true";
        }
        
        public string finishSdkPaylink()
        {
            PayLink.getinstance().EndPaylink();
            PayLink.setInstance();
            return "true";
        }
        public string getRemainder(string value)
        {

            // PayLink manager = new PayLink();

             PayLink.getinstance().PayIt(value);
            PayLink.setInstance();
            return "true";

        }
        public string PrintOrder(string input, string number)
        {
            Console.WriteLine("InvoiceNum is :  " + number);

            List<roomOrder> rooms = new List<roomOrder>();
            JArray items = JArray.Parse(input);



                foreach (var result in items)
            {
                roomOrder order = new roomOrder();
                // this can be a string or null
                order.Name = (string)result["name"];
                order.Count = (Int32)result["count"];
                order.Price = (double)result["price"];
                rooms.Add(order);
            }
            Console.WriteLine("------------");
            
            kioskprinter printer = new kioskprinter(rooms);
            printer.print(number);

            return "true";
            }
        
    }
}