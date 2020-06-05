using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace THLora_Testbench
{
    public class FlashProException : ApplicationException
    {
        public FlashProException(string message) : base(message) { }
        public FlashProException(string message, Exception innerException) : base(message, innerException) { }
    };

    public class Flashpro430
    {
        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_OpenInstancesAndFPAs(string TheList);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_DLLTypeVer();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_OpenInstances(int number);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Set_FPA_index(byte fpa);

        [DllImport("MSP430FPA1", SetLastError = true)]
        internal static unsafe extern int F_Initialization();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_AutoProgram(byte fpa);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Memory_Erase(int number);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Memory_Blank_Check();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Memory_Write(int number);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Memory_Verify(int number);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_ConfigFileLoad(string TheFileName);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_ReadCodeFile(int Format, string TheFileName);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Open_Target_Device();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Close_Target_Device();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Power_Target(int OnOff);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Copy_Flash_to_Buffer(int StartAdr, int Size);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern byte F_Get_Byte_from_Buffer(int StartAdr);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Blow_Fuse();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_VerifyFuseOrPassword();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_Reset_Target();

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_SetConfig(int Index, long Data);

        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern int F_CloseInstances();


        [DllImport("MSP430FPA", SetLastError = true)]
        internal static unsafe extern long F_Get_FPA_SN(byte index);

        public void Open(string sConfigFile)
        {
            if (F_OpenInstancesAndFPAs("*# *") > 0)
            {
                int n = F_DLLTypeVer();
                F_Set_FPA_index(1);
                F_Initialization();
            }
            else
            {
                // Please check:
                // - Is the driver installed correctly?
                // - Are BOTH libraries (MSP430FPA.dll and MSP430FPA1.dll) in the search path installed?
                // - Check registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_10C4&PID_EA60
                throw new FlashProException("FlashPro device not found");
            }

            if (F_ConfigFileLoad(sConfigFile) != 1)
                throw new FlashProException(string.Format("Cannot load FlashPro configuration file ({0})", sConfigFile));
        }

        public void Close()
        {
            F_CloseInstances();
        }

        public void ReadCodeFile(int nFormat, string sFilename)
        {
            int rc = F_ReadCodeFile(nFormat, sFilename);
            if ((rc & 0x01) != 1)
                throw new FlashProException(string.Format("Cannot read firmware file ({0})", sFilename));
        }

        public void AutoProgram(byte mode = 0)
        {
            if (F_AutoProgram(mode) != 1)
                throw new FlashProException("Error while programming firmware");
        }

        public bool DoFlash(ref int nStep)
        {
            bool bContinue = true;
            switch (nStep)
            {
                // Initialization.
                case 1:
                    nStep++;
                    break;
                // Erase flash memory.
                // Restore retain data (including DCO constants) if enabled.
                case 2:

                    if (F_Memory_Erase(1) != 1)
                        throw new FlashProException("Flash error: Cannot erase memory. Wrong device? Check Radio or Mbus/Pulse version!");

                    nStep++;
                    break;
                // Confirm if memory has been erased.
                case 3:
                    if (F_Memory_Blank_Check() != 1)
                        throw new FlashProException("Flash error: Memory blank check not passed");
                    nStep++;
                    break;
                // Flash programming and verification.
                case 4:
                    if (F_Memory_Write(0) != 1)
                        throw new FlashProException("Flash error: Cannot write firmware");
                    nStep++;
                    break;
                // Verify
                case 5:
                    if (F_Memory_Verify(0) != 1)
                        throw new FlashProException("Flash error: Cannot verify firmware");
                    nStep++;
                    break;
                case 6:
                    // exit
                    nStep = 0;
                    bContinue = false;
                    break;
                default:
                    // wrong step id
                    bContinue = false;
                    break;
            }
            return bContinue;
        }

    }
}
