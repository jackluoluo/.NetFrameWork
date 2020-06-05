using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartupLib;
using ZR_ClassLibrary;
using ZR_WorkbenchLibrary;
using System.Windows.Forms;

namespace THLora_Testbench
{
    public  class TestParameters
    {
        #region Define Paramters
        public string OrderNr { get; set; }
        public string SapNr { get; set; }
        public string SerialNr { get; set; }
        public int TestCenterID { get; set; }
        public int TestBenchID { get; set; }
        public int TestCourseID { get; set; }

        private static Dictionary<string, string> mapOrderNr_SapNr = new Dictionary<string, string>();
        #endregion

        #region Get MeterInfoID
        private uint meterInfoId;
        public uint? MeterInfoID
        {
            get
            {
                if (meterInfoList == null)
                {
                    meterInfoList = GetMeterInfoList();
                }

                for (int j = 0; j < meterInfoList.Count; j++)
                {
                    if (meterInfoList[j].PPSArtikelNr == SapNr)
                    {
                        meterInfoId  = (uint)meterInfoList[j].MeterInfoID ;
                        return meterInfoId ;
                    }
                }
                return meterInfoId ;
            }

            set
            {
                meterInfoId = (uint)value;
            }
        }
        #endregion

        #region Get HardwareTypeID
        private uint hardWareTypeId;
        public uint? HardWareTypeID 
        {
            get
            {
                if (meterInfoList == null)
                {
                    meterInfoList = GetMeterInfoList();
                }

                for (int i = 0; i < meterInfoList.Count; i++)
                {
                    if (meterInfoList[i].PPSArtikelNr == SapNr)
                    {
                        hardWareTypeId = (uint)meterInfoList[i].HardwareTypeID;
                        return hardWareTypeId;
                    }
                }
                return hardWareTypeId;
            } 
           
            set
            {
                hardWareTypeId =(uint) value;
            }

        }
        #endregion

        #region Meter Info List
        private List<MeterInfo> meterInfoList;
        public List<MeterInfo> MeterInfoList
        {
            get
            {
                if(meterInfoList == null)
                {
                    meterInfoList = GetMeterInfoList();
                }
                if(meterInfoList.Where ( p => p.PPSArtikelNr == SapNr).Count() == 0)
                {
                    meterInfoList = GetMeterInfoList();
                }
                    
                return meterInfoList;
            }
        }
        #endregion

        #region Show Device Description
        private string deviceInfo;
        public string DeviceInfo
        {
            set 
            {
                deviceInfo = value;
            }
            get 
            {
                if (meterInfoList == null)
                {
                    meterInfoList = GetMeterInfoList();
                }

                for (int j = 0; j < meterInfoList.Count; j++)
                {
                    if (meterInfoList[j].PPSArtikelNr == SapNr)
                    {
                        deviceInfo = meterInfoList[j].Description;
                        return deviceInfo;
                    }                  
                }
                return deviceInfo;
            }

        }
        #endregion

        #region Check OrderNr is Valid or not
        public bool IsValidOrderNumber
        {
            get
            {
                return MeterInfoList != null;
            }
        }
        #endregion

        #region Check Serial Number is valid or not
        //***************************************************
        //Serial Number Define
        //Digit 1  2  3  4  5  6  7   8  9  10  11  12  13  14
        //      F  Z  R  I  F 0/2 3      0~9(must as even)
        //15289(T without LCD) 0  3   3~9
        //15290(T&H with LCD)  2  3   0~2
        //15291(T with LCD)    0  3   3~9
        //***************************************************
        public bool IsValidSerialNmber
        {
            get
            {
                long numPart;
                if (this.SerialNr.Length != 14)
                {
                    return false;
                }
                if (this.SerialNr.Substring(0, 5) != "FZRIF")
                {
                    return false;
                }
                if (!(this.SerialNr.Substring(5, 1) != "0" || this.SerialNr.Substring(5, 1) != "2"))
                {
                    return false;
                }
                if (this.SerialNr.Substring(6, 1) != "3")
                {
                    return false;
                }
                if (!long.TryParse(this.SerialNr.Substring(7, 7), out numPart))
                {
                    return false;
                }
                if (numPart % 2 != 0) //even number
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region Check serialNr,SapNr,OrderNr is OK or not
        public bool IsParasInit
        {
            get
            {
                if (string.IsNullOrEmpty(SerialNr )
                    || string.IsNullOrEmpty(OrderNr)
                    || string.IsNullOrEmpty(SapNr)
                    || TestCenterID == 0
                    || TestBenchID == 0
                    || TestCourseID ==0
                    )
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region Constructor
        public TestParameters ()
        { }
        #endregion

        #region Get SapNr By OrderNr
        public bool GetSapNrByOrderNr(string orderNr, out string ErrorString)
        {
            ErrorString = string.Empty;
            try
            {
                //check if the order number loaded 
                if (mapOrderNr_SapNr.ContainsKey(orderNr))
                {
                    SapNr = mapOrderNr_SapNr[orderNr];
                    return true;
                }

                bool connection;
                string sapNr = string.Empty;

                SAP_WebServiceFunctions TheWebServiceFunctions = new SAP_WebServiceFunctions(PlugInLoader.GmmConfiguration);
                SAP_WebServiceFunctions.SapLocation TheLocation = TheWebServiceFunctions.GetSapLocation();
                TheWebServiceFunctions.GetMaterialNumberFromOrderNumber(orderNr, out sapNr, out connection, out ErrorString);

                if (string.IsNullOrEmpty(sapNr))
                {
                    ErrorString = "order number not exist!";
                    return false;
                }

                bool sapIsValid = false;
                //Make suer get SapNr is 152889,152890,152891 
                //2016.6.14 add
                if (sapNr == "152889")
                {
                    sapIsValid = true;
                }
                if (sapNr != "152890")
                {
                    sapIsValid = true;
                }

                if (sapNr != "152891")
                {
                    sapIsValid = true;
                }

                //for Itlay,155088
                if (sapNr != "155088")
                {
                    sapIsValid = true;
                }

                if (!sapIsValid )
                {
                    MessageBox.Show("Input order number wrong!");
                    return false;
                }

                this.SapNr = sapNr;
   

                //check if the order number is correct
                //if according OrderNr can not get meterinfo,the OrderNr is Invalid.
                if (!IsValidOrderNumber)
                {
                    ErrorString = "Wrong order number!";
                    return false;
                }

                // if have get sapNr,check serialNr again
                if(! CheckSerialNrBySapNr (this.SapNr,this.SerialNr ))
                {
                    ErrorString = "Serial Number not match Sap Number!";
                    return false;
                }

                //add to map
                mapOrderNr_SapNr.Add(orderNr, sapNr);

                return true;
            }
            catch
            {
                ErrorString = "Order number not exist!";
                return false;
            }
        }
        #endregion

        #region According SapNr Check Serial Number 
        private bool  CheckSerialNrBySapNr(string sapNr,string serialnr)
        {
            bool checkResult=true ;
            switch (sapNr)
            {
                case "152889":
                case "152891":
                    if (serialnr.Substring(5,1) != "0" )
                    {
                        checkResult = false;
                    }
                    break;
      
                case "152890":
                case "155088":
                    if(serialnr.Substring (5,1) !="2")
                    {
                        checkResult = false;
                    }
                    break;
                default :
                    checkResult = true;
                    break;
            }
            return checkResult;
        }
        #endregion

        #region Get Meter Info
        private List<MeterInfo> GetMeterInfoList()
        {
            string hardwareName = "TH"; // Table "MeterInfo,HardwareType" ,name="TH"
            var list = MeterDatabase.LoadMeterInfoByHardwareName(hardwareName);
            if(list.Count >0)
            {
                return list;
            }
            return null;
        }
        #endregion

        #region WebserviceCoinfig
        public static void WebserviceCoinfig()
        {
            SAP_WebServiceConfiguration frmSAP_WebServiceConfiguration = new SAP_WebServiceConfiguration(PlugInLoader.GmmConfiguration);
            frmSAP_WebServiceConfiguration.ShowDialog();
        }
        #endregion
    }
}
