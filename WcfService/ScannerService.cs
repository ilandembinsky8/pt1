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
    using System.Diagnostics;
    using System.Xml.Linq;

    public class ScannerService : InterfaceService
    {

    
           public string startCaspit(string price)
        {
            Process p = new Process();
            p.StartInfo.FileName = @"C:\kiosk\caspit\caspit.exe";
            p.StartInfo.Arguments = price;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = true;
            p.Start();
            p.WaitForExit();
  
            return "true";
        }

        public string getCash()
        {

           // PayLink manager = new PayLink();
               
            return PayLink.getinstance().readValue();

        }
        public string getRemainder(string value)
        {

            // PayLink manager = new PayLink();

             PayLink.getinstance().PayIt(value);
            PayLink.setInstance();
            return "true";

        }
        public string PrintOrder(string input)
        {
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
            printer.print();

            return "true";
            }
        
    }
}