using AES;
using System;

namespace WcfService
{

    class PayLink
    {
        private int TotalAccepted;
        private int EscrowAtAccept;
        private static PayLink _instance = new PayLink();
        private MHE.Acceptor currentAcceptor;
       // private static PayLink _instance = new PayLink();
        private MHE mhe;

        public static PayLink getinstance()
        {
            if (null == _instance)
            {
                _instance = new PayLink();
            }
        
            return _instance;
        }
        public static void setInstance()
        {
            _instance=null;
        }
        private  PayLink()
        {
            Console.WriteLine("start");
            mhe = new MHE();

            int Ret = mhe.Open();
            if (Ret != 0)
            {
                Console.WriteLine("MHE Open error " + Ret.ToString());

            }
            else
            {
                

                // remember present current values
                TotalAccepted = mhe.CurrentValue;
                EscrowAtAccept = mhe.Escrow.Throughput;

                // initialse Escrow as enabled
                mhe.Escrow.Enabled = false;

                // set the curent acceptor to the first in the list
                if (mhe.Acceptors.Count > 0)
                {
                    currentAcceptor = mhe.Acceptors[1];
                    Console.WriteLine(mhe.Acceptors[0]);
                    Console.WriteLine(mhe.Acceptors[1]);
                    Console.WriteLine(mhe.Acceptors[2]);

                }


            }
        }

            private void UpdateControls()
            {
            UpdateUSBDriverStatus();
            UpdateMHEControls();
            UpdateEventControls();

             //   UpdateEventControls();

        }
        public void startPaylink()
        {
            mhe.Enabled = true;
        }
        public void EndPaylink()
        {
            mhe.Enabled = false;
        }
        public string readValue()
        {
            return String.Format("{0:0.00}", ((decimal)mhe.CurrentValue - this.TotalAccepted) / 100);
        }
        public void PayIt(string payoutText)
        {
            decimal payout;
            if (decimal.TryParse(payoutText, out payout))
            {
                mhe.PayOut((int)(payout * 100));
            }
        }
        private void UpdateMHEControls()
        {
            Console.WriteLine( "totall  "+String.Format("{0:0.00}", (decimal)mhe.CurrentValue / 100));
            Console.WriteLine( "current read "+String.Format("{0:0.00}", ((decimal)mhe.CurrentValue - this.TotalAccepted) / 100));

        //    this.TotalAmountPaidOutTextBox.Text = String.Format("{0:0.00}", (decimal)mhe.CurrentPaid / 100);

          //  this.PayStatusLabel.Text = mhe.PayStatus.ToString();

           // this.EscrowAmountTextBox.Text = String.Format("{0:0.00}", (decimal)(mhe.Escrow.Throughput - this.EscrowAtAccept) / 100);
        }
        private void UpdateUSBDriverStatus()
        {
            // show USB Status
            Console.WriteLine("USB Driver: " + mhe.USBDriverStatus.ToString());
        }
        public void UpdateEventControls()
        {
            // get any events that have happend since last time
            MHE.Event eventBlock = new AES.MHE.Event();
            MHE.Event.Code eventCode = mhe.NextEvent(eventBlock);
            while (eventCode != MHE.Event.Code.IMHEI_NULL)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                switch (eventCode)
                {
                    case MHE.Event.Code.IMHEI_APPLICATION_START:
                       Console.WriteLine("Application Start");
                        break;
                    case MHE.Event.Code.IMHEI_APPLICATION_EXIT:
                        Console.WriteLine("Application Exit");
                        break;
                    case MHE.Event.Code.IMHEI_INTERFACE_START:
                        Console.WriteLine("Interface Start");
                        break;
                    case MHE.Event.Code.IMHEI_OVERFLOW:
                        Console.WriteLine("Overflow");
                        break;
                    case MHE.Event.Code.IMHEI_NULL:
                        Console.WriteLine("Null");
                        break;
                }
                if (sb.Length == 0)
                {
                    // add the unit type
                    switch ((ushort)eventCode & MHE.Event.UNIT_TYPE_MASK)
                    {
                        case MHE.Event.COIN_DISPENSER_EVENT:
                            Console.WriteLine("Coin Dispenser: ");
                            break;
                        case MHE.Event.NOTE_DISPENSER_EVENT:
                            Console.WriteLine("Note Dispenser: ");
                            break;
                        case MHE.Event.COIN_EVENT:
                            Console.WriteLine("Coin Acceptor: ");
                            break;
                        case MHE.Event.NOTE_EVENT:
                            Console.WriteLine("Note Acceptor: ");
                            break;
                        default:
                            Console.WriteLine("Device: ");
                            break;
                    }

                    // add the fault or event depending on the fault bit
                    if (((ushort)eventCode & MHE.Event.FAULT_BIT) > 0)
                    {
                        switch ((ushort)eventCode & MHE.Event.EVENT_CODE_MASK)
                        {
                            case MHE.Event.NOW_OK:
                                Console.WriteLine("Now OK");
                                break;
                            case MHE.Event.REPORTED_FAULT:
                                Console.WriteLine("Unit Reported Fault");
                                break;
                            case MHE.Event.UNIT_TIMEOUT:
                                Console.WriteLine("Unit Timeout");
                                break;
                            case MHE.Event.UNIT_RESET:
                                Console.WriteLine("Unit Reset");
                                break;
                            case MHE.Event.SELF_TEST_REFUSED:
                                Console.WriteLine("Self Test refused");
                                break;
                            default:
                                Console.WriteLine("Fault");
                                break;
                        }
                    }
                    else
                    {
                        switch ((ushort)eventCode & MHE.Event.EVENT_CODE_MASK)
                        {
                            case MHE.Event.EVENT_OK:
                                Console.WriteLine("OK");
                                break;
                            case MHE.Event.EVENT_BUSY:
                                Console.WriteLine("Busy");
                                break;
                            case MHE.Event.REJECTED:
                                Console.WriteLine("Rejected");
                                break;
                            case MHE.Event.INHIBITED:
                                Console.WriteLine("Inhibited");
                                break;
                            case MHE.Event.MISREAD:
                                Console.WriteLine("Misread");
                                break;
                            case MHE.Event.FRAUD:
                                Console.WriteLine("Fraud");
                                break;
                            case MHE.Event.JAM:
                                Console.WriteLine("Jam");
                                break;
                            case MHE.Event.JAM_FIXED:
                                Console.WriteLine("Jam Fixed");
                                break;
                            case MHE.Event.RETURN:
                                Console.WriteLine("Return");
                                break;
                            case MHE.Event.OUTPUT_PROBLEM:
                                Console.WriteLine("Output Problem");
                                break;
                            case MHE.Event.OUTPUT_FIXED:
                                Console.WriteLine("Output Fixed");
                                break;
                            case MHE.Event.INTERNAL_PROBLEM:
                                Console.WriteLine("Internal Problem");
                                break;
                            case MHE.Event.UNKNOWN:
                                Console.WriteLine("Unknown");
                                break;
                            case MHE.Event.DISPENSE_UPDATE:
                                Console.WriteLine("Dispenser Update");
                                break;
                            default:
                                Console.WriteLine("Event");
                                break;
                        }
                    }
                }

                int len = sb.Length;
                int noSpaces = 50 - len;
                for (int i = 0; i < noSpaces; ++i)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine("| Raw Code: {0:x4}  Disp: {1}", eventBlock.RawEvent, eventBlock.DispenserEvent);

                // this.EventListBox.Items.Add(sb.ToString());
                eventCode = mhe.NextEvent(eventBlock);
            }
        }
        

    }
    }
