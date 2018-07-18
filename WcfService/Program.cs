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
    using Newtonsoft.Json.Linq;
    using System;
    using System.ServiceModel.Web;

    class Program3
    {
      
        static void Main(string[] args)
        {
            using (var serviceHost = new WebServiceHost(typeof(ScannerService)))
             {


                //This is the magic line!
              //  serviceHost.Opening += new EventHandler(host_Opening);
                serviceHost.Open();
                 Console.WriteLine("WCF REST JSON service pt 1.1.5 is running...");
                Console.WriteLine("cut full - printer 17/06/2018");
                Console.WriteLine("USB 001  - printer 17/06//2018");
                Console.WriteLine("sqlexpress  - sql 18/07/2018");



                Console.ReadLine();

                 serviceHost.Close();
             }
        }
    }
}
