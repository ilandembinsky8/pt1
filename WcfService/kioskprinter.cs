using System;
using com.citizen.sdk;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace WcfService
{
    class kioskprinter
    {
        ESCPOSPrinter posPtr;

        List<roomOrder> iList;

        // Count
        int count;
        public kioskprinter(List<roomOrder> list)
        {
            // Constructor
            posPtr = new ESCPOSPrinter();
    
            iList = list;

        }
        private void connect(int type, String address)
        {
            /*if(type == ESCPOSConst.CMP_PORT_COM) { 
            // Set COMM Properties
            posPtr.SetCommProperties(ESCPOSConst.CMP_COM_BAUDRATE_9600, ESCPOSConst.CMP_COM_PARITY_NONE, ESCPOSConst.CMP_COM_HANDSHAKE_DTRDSR);
            }*/
            posPtr.SetCommProperties(ESCPOSConst.CMP_COM_BAUDRATE_9600, ESCPOSConst.CMP_COM_PARITY_NONE, ESCPOSConst.CMP_COM_HANDSHAKE_DTRDSR);

            // Connect
            int result = posPtr.Connect(type, address);
            if (ESCPOSConst.CMP_SUCCESS == result)
            {
                // Connect Success
                 Console.WriteLine("----------------Connect() : Success Citizen_POS_sample2 ");
            }
            else
            {
                // Connect Error
                Console.WriteLine("------------------Connect() Error : " + result.ToString()+ "Citizen_POS_sample2");
            }
        }

          public void print()
        {
            int result;
            String msg = "";

            

            // Printer Check
            result = posPtr.PrinterCheck();
            if (ESCPOSConst.CMP_SUCCESS != result)
            {
                Console.WriteLine("PrinterCheck() Error : " + result.ToString()+ "Citizen_POS_sample2");
                connect(ESCPOSConst.CMP_PORT_COM, "COM7:");
                //return;
            }

            // Get Status
            result = posPtr.Status();
            if (ESCPOSConst.CMP_STS_NORMAL != result)
            {
                Console.WriteLine("Status Error : " + result.ToString()+ "Citizen_POS_sample2");
                return;
            }

            // Character set
            result = posPtr.SetEncoding("Windows-1255");	// Latin-1
            //result = posPtr.SetEncoding( "Shift_JIS" );	// Japanese 日本語を印字する場合は、この行を有効にしてください.
            if (ESCPOSConst.CMP_SUCCESS != result)
            {
                Console.WriteLine("SetEncoding() Error : " + result.ToString()+ "Citizen_POS_sample2");
                return;
            }

            try
            {
                // Start Transaction ( Batch )
                result = posPtr.TransactionPrint(ESCPOSConst.CMP_TP_TRANSACTION);
                if (ESCPOSConst.CMP_SUCCESS != result)
                {
                    msg = "TransactionPrint() Error : " + result.ToString();
                    throw new Exception();
                }

 
                double amount;
                double total_amount = 0;
                double Tax = 0;
                double Total = 0;
                const double TAX_RATE = 0.08;

                int wordcount1 = 7;
                int wordcount2 = 14;
                int wordcount3 = 3;
                int wordcount4 = 8;
                int wordcount5 = 9;
                int wordcount6 = 7;

                wordcount1 = 6;
                wordcount2 = 40;
                wordcount3 = 4;
                wordcount4 = 12;
                wordcount5 = 14;
                wordcount6 = 10;



                // Create an instance
                // Connect printer
                //result = Connect(printer);

                if (result != ESCPOSConst.CMP_SUCCESS)
                {
                    return;
                }

                // Acquire the date and time
                DateTime dat = DateTime.Now;
                string strDATE = dat.ToString("yyyy-MM-dd HH:mm:ss");
                // Update of the serial number
                count = count + 1;
                // Start Transaction ( Batch )
                posPtr.TransactionPrint(ESCPOSConst.CMP_TP_TRANSACTION);

                // Print Text
                posPtr.PrintBitmap(@"C:\kiosk\cut\logo.PNG",
                     ESCPOSConst.CMP_BM_ASIS,
                     ESCPOSConst.CMP_ALIGNMENT_CENTER,
                     ESCPOSConst.CMP_BM_MODE_HT_DITHER | ESCPOSConst.CMP_BM_MODE_CMD_RASTER);

                posPtr.PrintText(Reverse("תודה רבה"),
                     ESCPOSConst.CMP_ALIGNMENT_CENTER, ESCPOSConst.CMP_FNT_BOLD,
                     ESCPOSConst.CMP_TXT_1WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT);

                posPtr.PrintText("\n" + strDATE + "\n\n",
                     ESCPOSConst.CMP_ALIGNMENT_CENTER, ESCPOSConst.CMP_FNT_DEFAULT,
                     ESCPOSConst.CMP_TXT_1WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT);

                foreach (var order in iList)
                {
                    posPtr.PrintPaddingText(order.Price.ToString("0.00"), ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_1WIDTH, wordcount1, ESCPOSConst.CMP_SIDE_LEFT);
                    posPtr.PrintPaddingText(order.Count.ToString("0"), ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_1WIDTH, wordcount3, ESCPOSConst.CMP_SIDE_LEFT);
                    posPtr.PrintPaddingText(Reverse(order.Name.ToString()), ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_1WIDTH, wordcount2, ESCPOSConst.CMP_SIDE_LEFT);

                    amount = order.Price;


                    posPtr.PrintNormal("\n");

                    total_amount += amount* order.Count;
                }

                // Tax = (double)(total_amount * TAX_RATE);

                //  Total = (double)(total_amount + Tax);
                Total = (double)(total_amount);
                posPtr.PrintNormal("\n");

                posPtr.PrintPaddingText(total_amount.ToString("0.00"),
                     ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_2WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT,
                     wordcount4, ESCPOSConst.CMP_SIDE_RIGHT);

               posPtr.PrintPaddingText(Reverse("סה\"כ"),
                    ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_1HEIGHT,
                    wordcount6, ESCPOSConst.CMP_SIDE_LEFT);

                /* posPtr.PrintNormal("\n");

                posPtr.PrintPaddingText("TAX",
                     ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_2WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT,
                     wordcount5, ESCPOSConst.CMP_SIDE_RIGHT);

                posPtr.PrintPaddingText(Tax.ToString("0.00"),
                    ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_1HEIGHT,
                    wordcount6 * 2, ESCPOSConst.CMP_SIDE_LEFT);

                posPtr.PrintNormal("\n");

                posPtr.PrintPaddingText("Total",
                     ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_2WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT,
                     wordcount5, ESCPOSConst.CMP_SIDE_RIGHT);

                posPtr.PrintPaddingText(Total.ToString("0.00"),
                    ESCPOSConst.CMP_FNT_DEFAULT, ESCPOSConst.CMP_TXT_2WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT,
                    wordcount6, ESCPOSConst.CMP_SIDE_LEFT);
                */
                posPtr.PrintNormal("\n\n");

                posPtr.PrintText("     " + String.Format("{0:00000}", count) + "    \n\n",
                     ESCPOSConst.CMP_ALIGNMENT_CENTER, ESCPOSConst.CMP_FNT_DEFAULT,
                     ESCPOSConst.CMP_TXT_1WIDTH | ESCPOSConst.CMP_TXT_1HEIGHT);

                // After feed the paper to the cutting position, partial cut.
                result = posPtr.CutPaper(ESCPOSConst.CMP_CUT_PARTIAL_PREFEED);

                if (ESCPOSConst.CMP_SUCCESS != result)
                {
                    msg = "CutPaper() Error : " + result.ToString();
                    throw new Exception();
                }

                // End Transaction ( Batch )
                result = posPtr.TransactionPrint(ESCPOSConst.CMP_TP_NORMAL);
                if (ESCPOSConst.CMP_SUCCESS != result)
                {
                    msg = "TransactionPrint() Error : " + result.ToString();
                    throw new Exception();
                }

                // Connect Success
                Console.WriteLine("Print : Success"+ "Citizen_POS_sample2");
            }
            catch
            {
                // Clear all buffered output data by TransactionPrint.
                posPtr.ClearOutput();

                // Print Error
                Console.WriteLine(msg+ "Citizen_POS_sample2");
            }

                Disconnect();

        }
        public string Reverse(string text)
        {
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
        private void Disconnect()
        {
            // Disconnect
            int result = posPtr.Disconnect();
            if (ESCPOSConst.CMP_SUCCESS == result)
            {
                // Disconnect Success
                Console.WriteLine("Disconnect() : Success Citizen_POS_sample2");
            }
            else
            {
                // Disconnect Error
                Console.WriteLine("Disconnect() Error : " + result.ToString()+ "Citizen_POS_sample2");
            }
        }
    }
}
