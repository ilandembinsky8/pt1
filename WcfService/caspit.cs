using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WcfService
{
    class caspit
    {
       /* public int sts { get; private set; }

        public void connect() {
            CaspitPayment.RequestTreatment deal = new CaspitPayment.RequestTreatment();
            deal.IpAddress = "127.0.0.1";
            deal.Port = 6900;
            deal.Timeout = 60;  //in seconds
            deal.Target = 52;

string xmlRequest =" < Request > < Command > 003 </ Command >"
   +" < TerminalId > 0880264 </ TerminalId >"
   +" < TimeoutInSeconds > 30 </ TimeoutInSeconds >"
   + " < TermNo > 1 </ TermNo >"
    + "< RequestId > 20171127113313 </ RequestId > "
   + "< CheckShva > 0 </ CheckShva >"
 + "   < CheckSwitch > 0 </ CheckSwitch > "
 + "   < CheckCaspitHost > 0 </ CheckCaspitHost >"
  + "  < CheckTMS > 0 </ CheckTMS >"
    + "< CheckCertServer > 0 </ CheckCertServer >"
    + "< CheckReportServer > 0 </ CheckReportServer >"
+ "</ Request >";
            string xmlOutput = "";
            Console.WriteLine(xmlRequest);
            sts = deal.PerformingTransactionXml(xmlRequest, out xmlOutput);
            Console.WriteLine(xmlOutput);

        }*/
    }
}
