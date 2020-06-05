using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Usbqlib;

namespace THLora_Testbench
{
    public class UsbOptRel32 : ZR_RelayCard
    {
        #region Declarations
        private bool[] RelayIsOn;   //Storing 32 Relaies States--On/Off
        private uint[] m_Handle;    //Store USB Relay device Handler(is Only for valid device)
        List<string> MyCardList;    //store USB Relay Device information(like "USBOPTOREL32 Card 0")
        private bool CardListIsBuild; //Initilization Realy Card is OK?
        private int MaxInputs = 32;    //Max DI
        private int MaxOutputs = 32;   //Max DO
        private int MaxNumberOfCards = 32; //Support Max Cards nummber
        #endregion //Declarations

        #region Constructor
        public UsbOptRel32() 
        {
            this.MyCardList = new List<string>();
            this.RelayIsOn = new bool[this.MaxOutputs];
            this.CardListIsBuild = false;
        }
        #endregion

        #region Private functions
        //initialization Relay Cards
        private bool BuildCardList(out string ErrorString) //Build Cards list
        {
            CARDDATAS lpcd = new CARDDATAS();
            uint device;
            uint handle;
            uint result;
            string s;
            ErrorString = string.Empty;

            try
            {
                // Open the USBOPTREL32	device ,get all availabel device handle    
                this.MyCardList = new List<string>();
                for (device = 0; device < this.MaxNumberOfCards; device++)
                {
                    handle = qlib32.QAPIExtOpenCard(qlib32.USBOPTOREL32, device);

                    if (handle != 0)
                    {
                        // save handle 
                        m_Handle[device] = handle;
                        lpcd.SizeOf = Marshal.SizeOf(lpcd);
                        // return card name
                        unsafe
                        {
                            result = qlib32.QAPIExtGetCardInfoEx(qlib32.USBOPTOREL32, lpcd);
                        }
                        s = lpcd.Name + " Card " + device;
                        // add card name to list
                        MyCardList.Add(s);
                    }
                }

                // no card found ? display message and terminate !
                if (MyCardList.Count < 1)
                {
                    ErrorString = "Unable to open any USBOPTOREL32 Card. Did you install the drivers ?";
                    throw new System.ApplicationException(ErrorString);
                }
                return true;
            }
            catch (System.Exception Se)
            {
                ErrorString = Se.ToString();
                return false;
            }
        }
        //Relay On
        private bool WriteOutput(out string ErrorString)
        {
            uint nDevice;
            uint lines;
            uint bit;
            string s;
            ErrorString = string.Empty;
            try
            {
                if (!this.CardListIsBuild)
                {
                    ErrorString = "Card is not initialized!";
                    return false;
                }

                s = MyCardList[0]; //Get devide[0] of the device list 
                s = s.Substring(s.Length - 1, 1);//get card index,example:USBOPTOREL32 Card0==>"0"
                nDevice = UInt32.Parse(s);

                if (m_Handle[nDevice] != 0)
                {
                    lines = 0; bit = 1;
                    for (uint j = 0; j < this.MaxOutputs; j++)
                    {
                        if (this.RelayIsOn[j])
                        {
                            // bit is set
                            lines = lines | bit;
                        }
                        else
                        {
                            // bit is no set, do nothing
                        }

                        bit = bit << 1;
                    }
                    qlib32.QAPIExtWriteDO32(m_Handle[nDevice], 0, lines, 0);
                }
                else
                {
                    ErrorString = "Can't open USBOPTREL32 Card!";
                    throw new System.ApplicationException(ErrorString);
                }
                return true;
            }
            catch (System.Exception Se)
            {
                ErrorString = Se.ToString();
                return false;
            }

        }
        //Detect Input status
        private bool ReadInput(out uint Lines, out string ErrorString)
        {
            uint nDevice;
            Lines = 0;
            string s;
            ErrorString = string.Empty;
            try
            {
                if (!this.CardListIsBuild)
                {
                    ErrorString = "Card is not initialized!";
                    return false;
                }
                // list all cards of type USBOPTOREL32   		
                s = MyCardList[0]; //This case Only  one USBOPTOREL32 device
                //if more than One device ,must provide a function to choose 
                s = s.Substring(s.Length - 1, 1);
                nDevice = UInt32.Parse(s);

                if (m_Handle[nDevice] != 0)
                {
                    Lines = qlib32.QAPIExtReadDI32(m_Handle[nDevice], 0, 0);
                }
                else
                {
                    ErrorString = "Can't open USBOPTREL32 Card!";
                    throw new System.ApplicationException(ErrorString);
                }
                return true;
            }
            catch (System.Exception Se)
            {
                ErrorString = Se.ToString();
                return false;
            }

        }
        #endregion //Private functions

        #region Method for implementing an abstract class
        //Initialization Realy Card
        public override bool InitializeCard(out string ErrorString)
        {
            this.CardListIsBuild = false;
            ErrorString = string.Empty;
            m_Handle = new uint[this.MaxNumberOfCards];

            // Clear QLIB Handles
            for (int i = 0; i < this.MaxNumberOfCards; i++)
            {
                m_Handle[i] = 0;
            }

            // Scan installed hardware
            if (!BuildCardList(out ErrorString))
            {
                return false;
            }

            this.CardListIsBuild = true;
            if (!this.RelieveRelays(out ErrorString))
            {
                return false;
            }

            for (int i = 0; i < this.MaxOutputs; i++)
            {
                RelayIsOn[i] = false;
            }

            return true;
        }

        public override List<string> GetCardList()
        {
            return this.MyCardList;
        }

        //Enable relay(1~32)
        public override bool RelayOn(int Number, out string ErrorString)
        {
            if ((Number < 1) || (Number > this.MaxOutputs))
            {
                ErrorString = "Wrong relay number (" + Number.ToString() + ") Valid numbers: from 1 to " + this.MaxOutputs.ToString() + "!";
                return false;
            }
            RelayIsOn[Number - 1] = true;
            if (!this.WriteOutput(out ErrorString))
            {
                return false;
            }
            return true;
        }

        //Disable relay
        public override bool RelayOff(int Number, out string ErrorString)
        {
            if ((Number < 1) || (Number > this.MaxOutputs))
            {
                ErrorString = "Wrong relay number (" + Number.ToString() + ") Valid numbers: from 1 to " + this.MaxOutputs.ToString() + "!";
                return false;
            }
            RelayIsOn[Number - 1] = false;
            if (!this.WriteOutput(out ErrorString))
            {
                return false;
            }
            return true;
        }

        //Relay Status
        public override bool GetRelayState(int Number, out RelayState State, out string ErrorString)
        {
            ErrorString = string.Empty;
            State = ZR_RelayCard.RelayState.Undef;
            if ((Number < 1) || (Number > this.MaxOutputs))
            {
                ErrorString = "Wrong relay number (" + Number.ToString() + ") Valid numbers: from 1 to " + this.MaxOutputs.ToString() + "!";
                return false;
            }

            if (!this.CardListIsBuild)
            {
                ErrorString = "Card is not initialized!";
                return false;
            }

            if (RelayIsOn[Number - 1])
            {
                State = ZR_RelayCard.RelayState.On;
            }
            else
            {
                State = ZR_RelayCard.RelayState.Off;
            }
            return true;
        }

        //All Relay Reset
        public override bool RelieveRelays(out string ErrorString)
        {
            ErrorString = string.Empty;
            for (int i = 0; i < this.MaxOutputs; i++)
            {
                RelayIsOn[i] = false;
            }
            if (!this.WriteOutput(out ErrorString))
            {
                return false;
            }
            return true;
        }

        //Input Status
        public override bool GetInputState(int Number, out ZR_RelayCard.InputState State, out string ErrorString)
        {
            ErrorString = string.Empty;
            uint Lines;
            State = ZR_RelayCard.InputState.Undef;
            if ((Number < 1) || (Number > this.MaxInputs))
            {
                ErrorString = "Wrong input number (" + Number.ToString() + ") Valid numbers: from 1 to " + this.MaxInputs.ToString() + "!";
                return false;
            }

            if (!this.CardListIsBuild)
            {
                ErrorString = "Card is not initialized!";
                return false;
            }

            if (!this.ReadInput(out Lines, out ErrorString))
            {
                return false;
            }

            uint dummy = 1;
            if (Number > 1)
            {
                dummy = dummy << (Number - 1);
            }


            if ((dummy & Lines) == dummy)
            {
                State = InputState.On;
            }
            else
            {
                State = InputState.Off;
            }
            return true;
        }

        public override bool GetInputStates(int Count, out List<InputState> States, out string ErrorString)
        {
            uint Lines;
            States = new List<InputState>();
            for (int i = 0; i < Count; i++)
            {
                InputState TheState = InputState.Undef;
                States.Add(TheState);
            }

            if ((Count < 1) || (Count > this.MaxInputs))
            {
                ErrorString = "Wrong number (" + Count.ToString() + ") Valid numbers: from 1 to " + this.MaxInputs.ToString() + "!";
                return false;
            }

            if (!this.CardListIsBuild)
            {
                ErrorString = "Card is not initialized!";
                return false;
            }

            if (!this.ReadInput(out Lines, out ErrorString))
            {
                return false;
            }

            for (int i = 0; i < Count; i++)
            {
                uint dummy = 1;
                dummy = dummy << i;
                if ((dummy & Lines) == dummy)
                {
                    States[i] = InputState.On;
                }
                else
                {
                    States[i] = InputState.Off;
                }
            }
            return true;
        }

        #endregion

    }
}
