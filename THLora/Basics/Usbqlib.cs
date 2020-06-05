using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
/*
	*	Include File QLIB.CS  Version: 2.3.121
	* 	Required constants _M_X64 for 64-Bit Systems
	* 	and _M_IX86 for 32-Bit Systems 
*/
namespace Usbqlib
{

    [StructLayout(LayoutKind.Sequential)]
    public class CARDDATAS
    {
        public int SizeOf;					/*	Groesse der Struktur		*/
        public uint CardID;					/*	Karte-ID		 	*/
        public uint BusType;				/*	Bustype (ISA, PCI, VLB, USB)	*/
        public uint Features;				/*	Kartenausstattung		*/
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;					/*	Kartenname			*/
        public uint IOHandle;				/*	Basisadresse der Karte		*/
        public uint IrqChannel;				/*	IRQ-Kanal			*/
        public uint DMAChannel;				/*	DMA-Kanal			*/
        public uint Module;					/*	Modul-Nummer			*/
        public uint NumOfADChannels;		/*	Anzahl der A/D-Kanaele		*/
        public uint NumOfDAChannels;		/*	Anzahl der D/A-Kanaele		*/
        public uint NumOfDIChannels;		/*  Anzahl der DI-Kanaele		*/
        public uint NumOfDOChannels;		/*	Anzahl der DO-Kanaele		*/
        public uint NumOfDXChannels;		/*	Anzahl der DX-Kanaele		*/
        public uint IOBase1;				/*	I/O-Adresse 1			*/
        public uint IOBase2;				/*	I/O-Adresse 2			*/
        public uint IOBase3;				/*	I/O-Adresse 3			*/
        public uint IOBase4;				/*	I/O-Adresse 4			*/
        public uint IOBase5;				/*	I/O-Adresse 5			*/
        public uint IOBase6;				/*	I/O-Adresse 6			*/
        public uint IOBase7;				/*	I/O-Adresse 7			*/
        public uint IOBase8;				/*	I/O-Adresse 8			*/
        public uint IRQList;				/*  IRQs				*/
        public uint IRQ2List;				/*	IRQs				*/
        public uint DMAList;    			/*  DMAs				*/
        public uint IOSize;					/*	Groesse des belegten IO-Raums	*/
        public uint VendorCode;				/* 	Hersteller			*/
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ulReserved;			/* 	Reservierter Speicher		*/
    };

    /*
    **	qlib32 interface class
    */

    public class qlib32
    {

#if _M_X64
                public const string _qlibdll_ = "qlib64.dll";
#else
        public const string _qlibdll_ = "qlib32.dll";
#endif


        //	Debugging constants 		

        public const uint DBG_NONE = 0;	 /* nichts debuggen		*/
        public const uint DBG_NULL_PTR = 1;	 /* NULL-Pointer pruefen		*/
        public const uint DBG_FUNC_DEFINED = 2;	 /* gueltige Funktionen pruefen	*/
        public const uint DBG_VALID_CARD_ID = 4;	 /* gueltige Karten-ID pruefen	*/
        public const uint DBG_MAKE_LOG = 8;	 /* Log-File erzeugen            */
        public const uint DBG_WINDOW_LOG = 16;  /* Output to Logger Window      */
        public const uint DBG_FULL_DEBUG = 0xF;	 /* alles pruefen und aufzeichnen*/

        /*
        **	allgemeine Konstanten
        */

        public const uint BUS_ISA = 1;
        public const uint BUS_PCI = 2;
        public const uint BUS_VLB = 3;
        public const uint BUS_PAR = 4;
        public const uint BUS_USB = 5;
        public const uint BUS_UNKNOWN = 6;

        public const uint FEATURE_AD = 0x00000001;    			/*  Karte besitzt AD-Kanaele      */
        public const uint FEATURE_DA = 0x00000002;     			/*  Karte besitzt DA-Kanaele      */
        public const uint FEATURE_DI = 0x00000004;     			/*  Karte besitzt Digitaleingaenge*/
        public const uint FEATURE_DO = 0x00000008;     			/*  Karte besitzt Digitalausgaenge*/
        public const uint FEATURE_DX = 0x00000010;     			/*  Karte besitzt beliebige DI/Os */
        public const uint FEATURE_WD = 0x00000020;     			/*  Karte besitzt Watchdog        */
        public const uint FEATURE_CT = 0x00000040;     			/*  Karte besitzt Counter/Timer   */
        public const uint FEATURE_8253 = 0x00000080;            		/*  Karte besitzt 8253/54         */
        public const uint FEATURE_8255 = 0x00000100;					/*  Karte besitzt 8255            */
        public const uint FEATURE_WD_ST = 0x00000200;					/*	Karte besitzt Watchdog Status Register */
        public const uint FEATURE_READSTRING = 0x00000400;					/*  Karte kann String lesen */
        public const uint FEATURE_WRITESTRING = 0x00000800;					/*  Karte kann String schreiben */

        public const uint LIST_DEFAULT_VALUE = 0x80000000;		/*	Resourcenwert ist Defaultwert						*/
        public const uint LIST_RESOURCE_NOT_USED = 0x40000000;	/*	Resource wird nicht verwendet						*/
        public const uint LIST_AUTO_RESOURCE = 0x20000000;		/*	Resourcenwert wird auto. vergeben (PCI)	*/
        public const uint LIST_EOL = 0x00000000;				/*	Ende der Liste													*/

        public const uint VC_QIS = 2;			/*	Manufacturer-ID: QUANCOM Informationssysteme GmbH	*/
        public const uint VC_KOLTER = 3;      	/*	Manufacturer-ID: Kolter Electronic				*/

        //	Constants for the QAPIExtReadAD, QAPIExtConvertDWToVoltage functions

        public const uint MODE_BI_5V = 0;
        public const uint MODE_BI_10V = 1;
        public const uint MODE_BI_3V3 = 2;
        public const uint MODE_BI_2V5 = 6;
        public const uint MODE_BI_1V25 = 7;
        public const uint MODE_UNI_10V = 3;
        public const uint MODE_UNI_5V = 4;
        public const uint MODE_UNI_3V3 = 5;
        public const uint MODE_UNI_2V5 = 8;
        public const uint MODE_UNI_1V25 = 9;

        //  Stroeme

        public const uint MODE_0_TO_20MA = 100;
        public const uint MODE_4_TO_20MA = 101;

        //  Temperaturen

        public const uint MODE_0_TO_100_DEGREE = 200;

        //  Filter (darf mit zuvor genannten Werten ODER-verknuepft werden

        public const uint MODE_FILTER = 0x80000000;

        //  Rueckgabewert bei Fehler (falscher Modus-Wert)

        public const uint MODE_INVALID = 0x4d414a41;

        /*
        **  Error codes
        */

        public const uint QAPI_ERROR_INVALID_CHANNEL = 0xffffff00;   /*  Parameter-Wert channel unzulaessig  */
        public const uint QAPI_ERROR_AD_TIMEOUT = 0xffffff01;   /*  kein AD-Wert lesbar                 */

        /*
        **  Fehlermeldungen und Sprache ( mr. 17.04.2002 )
        */

        public const uint QAPI_MESSAGES_ON = 0;
        public const uint QAPI_MESSAGES_OFF = 1;


        /*
        **	Jobs
        */

        public const uint JOB_READ_8255 = 0;
        public const uint JOB_WRITE_8255 = 1;

        public const uint JOB_ENABLE_WATCHDOG = 2;
        public const uint JOB_DISABLE_WATCHDOG = 3;
        public const uint JOB_RETRIGGER_WATCHDOG = 4;
        public const uint JOB_STATUS_WATCHDOG = 72;
        public const uint JOB_RELAYON_WATCHDOG = 93;

        public const uint JOB_READ_8253 = 5;
        public const uint JOB_WRITE_8253 = 6;

        public const uint JOB_ENABLE_IRQ = 7;
        public const uint JOB_DISABLE_IRQ = 8;
        public const uint JOB_ENABLE_IRQ_A = 7;
        public const uint JOB_DISABLE_IRQ_A = 8;
        public const uint JOB_ENABLE_IRQ_B = 9;
        public const uint JOB_DISABLE_IRQ_B = 10;

        public const uint JOB_IOREAD_BYTE = 11;
        public const uint JOB_IOREAD_WORD = 12;
        public const uint JOB_IOREAD_LONG = 13;
        public const uint JOB_IOWRITE_BYTE = 14;
        public const uint JOB_IOWRITE_WORD = 15;
        public const uint JOB_IOWRITE_LONG = 16;

        public const uint JOB_RESET_IN_FFS = 17;
        public const uint JOB_READ_IN_FFS = 18;
        public const uint JOB_ENABLE_IN_FFS = 84;
        public const uint JOB_DISABLE_IN_FFS = 89;

        public const uint JOB_ENABLE_TIMEOUT = 19;
        public const uint JOB_DISABLE_TIMEOUT = 20;
        public const uint JOB_RESET_TIMEOUT_STATUS = 21;
        public const uint JOB_READ_TIMEOUT_STATUS = 22;
        public const uint JOB_SET_WATCHDOG_TIME = 24;

        public const uint JOB_READ_DIP_SWITCH = 85;
        public const uint JOB_SET_LED = 88;

        public const uint JOB_READ_BACK_RELAYS = 123;

        public const uint JOB_WDOG3_INITIALIZE = 23;
        public const uint JOB_WDOG3_SET_WATCHDOG_TIME = 24;
        public const uint JOB_WDOG3_SET_RELAIS_TIME = 25;
        public const uint JOB_WDOG3_SET_REPEAT_TIME = 26;
        public const uint JOB_WDOG3_RELAIS_INVERSION = 27;

        /* mr 20.10.2000 */

        public const uint JOB_DOWNLOAD = 28;

        /* mr 22.10.2000 Jobs für UNITIMER */

        public const uint JOB_UNITIMER_RELAIS1 = 29;
        public const uint JOB_UNITIMER_RELAIS2 = 30;
        public const uint JOB_UNITIMER_OUT0 = 31;
        public const uint JOB_UNITIMER_OUT1 = 32;
        public const uint JOB_UNITIMER_OUT2 = 33;
        public const uint JOB_UNITIMER_OUT3 = 34;
        public const uint JOB_UNITIMER_OUT4 = 35;
        public const uint JOB_UNITIMER_OUT5 = 36;
        public const uint JOB_UNITIMER_OUT6 = 37;
        public const uint JOB_UNITIMER_OUT7 = 38;
        public const uint JOB_UNITIMER_GETLCAREG = 39;
        public const uint JOB_UNITIMER_SETLCAREG = 40;
        public const uint JOB_UNITIMER_GETCNTA = 41;
        public const uint JOB_UNITIMER_GETCNTB = 42;
        public const uint JOB_UNITIMER_GETCNTAB = 43;
        public const uint JOB_UNITIMER_GETCTREG = 44;
        public const uint JOB_UNITIMER_SETCTREG = 45;
        public const uint JOB_UNITIMER_INITIALIZE = 46;
        public const uint JOB_UNITIMER_SETCNTMODE = 47;
        public const uint JOB_UNITIMER_UNIT7 = 48;

        /* mr 27.11.2000 Jobs für PAR48IO */

        public const uint JOB_PAR48IO_INPUT = 0;
        public const uint JOB_PAR48IO_OUTPUT = 1;

        public const uint JOB_PAR48IO_LATCH = 49;
        public const uint JOB_PAR48IO_READCNT = 50;
        public const uint JOB_PAR48IO_SETMODE = 51;
        public const uint JOB_PAR48IO_WRITE = 52;
        public const uint JOB_PAR48IO_READ = 54;

        public const uint JOB_PAR48IO_RESET_TO_0 = 58;
        public const uint JOB_PAR48IO_RESET_TO_1 = 59;
        public const uint JOB_PAR48IO_IOMODE0_7 = 60;
        public const uint JOB_PAR48IO_IOMODE00_07 = 60;
        public const uint JOB_PAR48IO_IOMODE8_15 = 61;
        public const uint JOB_PAR48IO_IOMODE08_15 = 61;
        public const uint JOB_PAR48IO_IOMODE16_23 = 62;
        public const uint JOB_PAR48IO_IOMODE24_31 = 63;
        public const uint JOB_PAR48IO_IOMODE32_39 = 64;
        public const uint JOB_PAR48IO_IOMODE40_47 = 65;

        /* mr 29.11.2000 Jobs für OPTOLCA */

        public const uint JOB_OPTOLCA_SETEXTRAMEMORY = 66;
        public const uint JOB_OPTOLCA_GETEXTRAMEMORY = 67;
        public const uint JOB_OPTOLCA_SM_START = 68;
        public const uint JOB_OPTOLCA_SM_STOP = 69;
        public const uint JOB_OPTOLCA_SM_SSR = 70;

        /* mr 22.04.2002 Jobs für GPIB, PCIGPIB und USBGPIB */

        public const uint JOB_REGW = 73;
        public const uint JOB_REGR = 74;
        public const uint JOB_READSRQ = 75;
        public const uint JOB_SERIALPOLL = 76;
        public const uint JOB_GTL = 77;
        public const uint JOB_GET = 78;
        public const uint JOB_SDC = 79;
        public const uint JOB_LLO = 80;
        public const uint JOB_DCL = 81;
        public const uint JOB_REN = 90;
        public const uint JOB_RESET = 91;
        public const uint JOB_TIMEOUT = 92;
        public const uint JOB_READ_TIMEOUT = 127;

        /*  Jobs für PAR2DA Modul mr 03.06.2002 */

        public const uint JOB_PAR2DA_ENABLE1 = 82;
        public const uint JOB_PAR2DA_ENABLE2 = 83;

        /*  Jobs für schnellen Memory Zugriff mr 20.10.2002  */

        public const uint JOB_FASTMEM_INIT = 86;
        public const uint JOB_FASTMEM_RELEASE = 87;

        /* mr 25.11.2002 Jobs für PCITTL32IO */

        public const uint JOB_PCITTL32_INPUT = 0;
        public const uint JOB_PCITTL32_OUTPUT = 1;

        public const uint JOB_PCITTL32_IOMODE0_7 = 60;
        public const uint JOB_PCITTL32_IOMODE00_07 = 60;
        public const uint JOB_PCITTL32_IOMODE8_15 = 61;
        public const uint JOB_PCITTL32_IOMODE08_15 = 61;
        public const uint JOB_PCITTL32_IOMODE16_23 = 62;
        public const uint JOB_PCITTL32_IOMODE24_31 = 63;
        public const uint JOB_PCITTL32_IOMODE32_39 = 64;
        public const uint JOB_PCITTL32_IOMODE40_47 = 65;

        // Jobs für PCIEXT64 mr. 28.06.2004

        public const uint JOB_PCIEXT64_READ_TEMPERATURE = 94;
        public const uint JOB_PCIEXT64_ENABLE = 95;
        public const uint JOB_PCIEXT64_DISABLE = 96;
        public const uint JOB_PCIEXT64_CARD_DETECT_STATUS = 97;
        public const uint JOB_PCIEXT64_RESET_ACTIVE = 98;
        public const uint JOB_PCIEXT64_ACTIVE = 99;
        public const uint JOB_PCIEXT64_PCI_CONFIGSPACE = 100;

        // Jobs für PCIWDOG3 & 4 mr. 27.09.2004

        public const uint JOB_LOAD_WATCHDOG = 101;
        public const uint JOB_CLEAR_LOG = 102;
        public const uint JOB_READ_LOG = 103;
        public const uint JOB_TIME_SET = 104;
        public const uint JOB_TIME_GET = 105;
        public const uint JOB_SEND_SMS = 106;
        public const uint JOB_GOTO_IDLE = 107;
        public const uint JOB_EEPROM_WRITE = 108;
        public const uint JOB_READ_DEFAULT_TIMEOUT = 109;
        public const uint JOB_WRITE_DEFAULT_TIMEOUT = 110;
        public const uint JOB_LOCK_DEVICE = 111;
        public const uint JOB_UNLOCK_DEVICE = 112;
        public const uint JOB_GETSTATUS = 113;
        public const uint JOB_GETSTATUS_STRING = 114;
        public const uint JOB_SHUTDOWN = 115;
        public const uint JOB_GETVERSION = 116;
        public const uint JOB_IRQ_GET_DATA_RESULTCODE = 118;
        public const uint JOB_IRQ_GET_DATA_EMAILADDRESS = 119;
        public const uint JOB_IRQ_GET_DATA_EMAILTEXT = 120;
        public const uint JOB_GET_EMAILTEXT = 121;
        public const uint JOB_GET_EMAILADDRESS = 122;
        public const uint JOB_IRQ_GET_DATA_RESULTTYPE = 124;
        public const uint JOB_GET_SHUTDOWNUSERABORTTIME = 125;
        public const uint JOB_CANCEL_SHUTDOWN = 126;


        public const uint JOB_READ_FLASH_MEMORY_BYTE = 128;
        public const uint JOB_READ_FLASH_MEMORY_WORD = 129;
        public const uint JOB_GET_PHASETEXT = 130;
        public const uint JOB_READ_LOG_ENTRY = 131;
        public const uint JOB_GET_PHASE = 132;
        public const uint JOB_READ_WATCHDOG_TIMER = 133;
        public const uint JOB_READ_RELAY_TIMER = 134;
        public const uint JOB_READ_OPTOCOUPLER_INPUTS = 135;
        public const uint JOB_READ_SMS_STATUS = 136;
        public const uint JOB_READ_SMS_STATUS_STRING = 137;
        public const uint JOB_READ_TEMPERATURE_VALUES = 138;
        public const uint JOB_READ_VOLTAGE_VALUES = 139;


        /* Jobs für PCITTL64 mr. 23.05.2005 */

        public const uint JOB_PCITTL64_INPUT = 0;
        public const uint JOB_PCITTL64_OUTPUT = 1;

        public const uint JOB_PCITTL64_IOMODE0_7 = 140;
        public const uint JOB_PCITTL64_IOMODE8_15 = 141;
        public const uint JOB_PCITTL64_IOMODE16_23 = 142;
        public const uint JOB_PCITTL64_IOMODE24_31 = 143;
        public const uint JOB_PCITTL64_IOMODE32_39 = 144;
        public const uint JOB_PCITTL64_IOMODE40_47 = 145;
        public const uint JOB_PCITTL64_IOMODE48_55 = 146;
        public const uint JOB_PCITTL64_IOMODE56_63 = 147;

        // Jobs für USB-FLASH			

        public const uint JOB_USB_FLASH_DEVICE = 148;

        // Jobs für PCIWDOG3 mr. 25.10.2005

        public const uint JOB_SET_LOG_LEVEL = 149;
        public const uint JOB_GET_LOG_LEVEL = 150;

        // Jobs for USBAD8DAC2 mr. 18.11.2005

        public const uint JOB_USBAD8DAC2_IOMODE0_7 = 151;
        public const uint JOB_USBAD8DAC2_IOMODE8_15 = 152;
        public const uint JOB_USBAD8DAC2_IOMODE16_23 = 153;

        public const uint JOB_USBAD8DAC2_INPUT = 0;
        public const uint JOB_USBAD8DAC2_OUTPUT = 1;

        // Jobs for TTL ports  mr. 17.01.2005 ( DDR = Data Direction Register )

        public const uint JOB_WRITE_DDR = 154;
        public const uint JOB_READ_DDR = 155;

        public const uint JOB_IOMODE0_7 = 156;
        public const uint JOB_IOMODE8_15 = 157;
        public const uint JOB_IOMODE16_23 = 158;
        public const uint JOB_IOMODE24_31 = 159;
        public const uint JOB_IOMODE32_39 = 160;
        public const uint JOB_IOMODE40_47 = 161;
        public const uint JOB_IOMODE48_55 = 162;
        public const uint JOB_IOMODE56_63 = 163;

        public const uint JOB_INPUT = 0;
        public const uint JOB_OUTPUT = 1;

        public const uint JOB_USB_SET_FLASH_MODE = 164;

        // Jobs for USBCPU mr 1.12.2008

        public const uint JOB_USBCPU_WRITEREG = 190;
        public const uint JOB_USBCPU_READREG = 191;
        public const uint JOB_USBCPU_WRITEREGWORD = 192;
        public const uint JOB_USBCPU_READREGWORD = 193;
        public const uint JOB_USBCPU_WRITEREGLONG = 194;
        public const uint JOB_USBCPU_READREGLONG = 195;

        // Jobs for USBCOUNTER mr 1.12.2008

        public const uint JOB_USBCOUNTER_LATCHCOUNTER = 196;
        public const uint JOB_USBCOUNTER_LATCHCOUNTERALL = 197;
        public const uint JOB_USBCOUNTER_CLEARCOUNTER = 198;
        public const uint JOB_USBCOUNTER_CLEARCOUNTERALL = 199;
        public const uint JOB_USBCOUNTER_SETMODE = 200;
        public const uint JOB_USBCOUNTER_SETCOMPAREVALUE = 201;
        public const uint JOB_USBCOUNTER_READCOUNTERLATCH = 202;
        public const uint JOB_USBCOUNTER_READLATCHDATAVALID = 203;
        public const uint JOB_USBCOUNTER_READINPUTCHANNELS = 204;
        public const uint JOB_USBCOUNTER_SETTTLMODE = 205;



        /*  nächster job 164L !             */

        public const uint JOB_INVALID = 0x4d414a41;


        //  List of error codes for function QAPIGetLastError()		

        public const uint ERROR_NONE = 0;	// no error
        public const uint ERROR_GETLASTERROR = 1;	// kernel returns special error ( for details see GetLastError() in MS SDK Documentation )
        public const uint ERROR_WSAGETLASTERROR = 2;	// winsock returns special error ( for details see WSAGetLastError() in MS SDK Documentation )
        public const uint ERROR_QLIB_INTERNAL = 3;	// not specified error
        public const uint ERROR_QLIB_BUFFER_TO_SMALL = 4;	// buffer overflow
        public const uint ERROR_QLIB_CONNECTION = 5;	// connection error
        public const uint ERROR_QLIB_CONNECTION_TIMEOUT = 6;	// no response from server
        public const uint ERROR_QLIB_CONNECTION_LOGIN_FAILED = 7;	// authentification failed ( username, password )
        public const uint ERROR_QLIB_CONNECTION_DISCONNECTED = 8;	// connection has been shutdown 
        public const uint ERROR_QLIB_ILLEGAL_PARAMETER = 9;	// illegal parameter passed to QAPIxxxx function
        public const uint ERROR_EXCEPTION = 10;	// exception ( check buffers and parameters )
        public const uint ERROR_LOADING_WINSOCK = 11;	// TCP/IP not installed ?
        public const uint ERROR_QLIB_CARDID_NOT_VALID = 12;	// illegal card id
        public const uint ERROR_QLIB_FUNCTION_NOT_SUPPORTED = 13;	// QAPIxxxx function not supported by this card
        public const uint ERROR_GPIB_TIMEOUT = 14;	// device is not responding
        public const uint ERROR_GPIB_ERR = 15;	// status register returns err flag
        public const uint ERROR_QLIB_UNABLE_TO_LOAD_QMULTI = 16;	// QMULTI32.DLL nicht im Suchpfad
        public const uint ERROR_QLIB_QMULTI_HAS_WRONG_VERSION = 17;	// remove installation and run setup again to install matching DLL's
        public const uint ERROR_QLIB_QMULTI_DIRECTIO = 18;	// load of dll failed -> no link / subsequent calls will fail
        public const uint ERROR_QLIB_FASTMEM_MAP_FAILED = 19;	// unable to get card memory pointer 
        public const uint ERROR_QLIB_FASTMEM_UNMAP_FAILED = 20;	// unable to release card memory pointer
        public const uint ERROR_QLIB_DEVICE_BUSY = 21;	// another thread has locked access to device ( semaphore )		
        public const uint ERROR_QLIB_DEVICE_NOT_PRESENT = 22;	// device removed, isa card not present ( register check )
        public const uint ERROR_QLIB_DEVICE_CLOSED = 23; // close on handle for card waiting for an IRQ
        public const uint ERROR_QLIB_IRQ_DISABLED = 24; // IRQ disabled for card waiting for an IRQ
        public const uint ERROR_QLIB_IRQ_ALREADY_ENABLED = 25; // IRQ has been enabled before this call
        public const uint ERROR_QLIB_IRQ_ALREADY_DISABLED = 26; // IRQ has been disabled before this call
        public const uint ERROR_QLIB_IRQ_NOT_AVAILABLE = 27; // IRQ not available for this card
        public const uint ERROR_QLIB_TIMEOUT = 28; // Timeout (i.e. waiting for data from onboard cpu)
        public const uint ERROR_QLIB_RESET_ERROR = 29; // Error, onboard cpu reset failed
        public const uint ERROR_QLIB_INVALID_DATA = 30; // Invalid Data received ( i.e. onboard cpu returns wrong date length)
        public const uint ERROR_INVALID_LOG_DATA = 31; // Invalid Log Entry ( PCIWDOG3, ... )
        public const uint ERROR_QLIB_FILE_NOT_FOUND = 32; // File not found
        public const uint ERROR_QLIB_FILE_HEX_FORMAT_REQUIRED = 33; // File must be in Hex-Format
        public const uint ERROR_QLIB_WRITE_FLASH_FAILED = 34; // Writing to the Flash Memory failed
        public const uint ERROR_QLIB_VERIFY_FLASH_FAILED = 35; // Flash Memory Verify failed
        public const uint ERROR_QLIB_UNABLE_INITIALIZE_DEVICE = 36; // Unable to Initialize the Device
        public const uint ERROR_QLIB_NOT_SUPPORTED_IN_REMOTE_MODE = 37;  // Function not supported in remote mode ( over a TCP/IP connection )
        public const uint ERROR_QLIB_COUNTER_OVERFLOW = 38; // Counter overflow
        public const uint ERROR_QLIB_SIGNAL_OUT_OF_RANGE = 39;  // AI line signal out of range


        /*
        **  List of valid card id's
        */

        public const uint PAR8DA = 0;
        public const uint UNITIMER = 1;
        public const uint PAR12AD = 2;
        public const uint PDAC4 = 3;
        public const uint PAD12 = 4;
        public const uint PAD16 = 5;
        public const uint PAD12DAC4 = 6;
        public const uint PAD16DAC4 = 7;
        public const uint PUNIREL = 8;
        public const uint ADGVT12 = 9;
        public const uint ADGVT16 = 10;
        public const uint PAR16AD = 11;
        public const uint PREL8 = 12;
        public const uint PREL16 = 13;
        public const uint POPTOREL16 = 14;
        public const uint POPTO16IN = 15;
        public const uint PWDOG = 16;
        public const uint POPTOLCA = 17;
        public const uint WATCHDOG = 18;
        public const uint PTTL24IO = 19;
        public const uint PROTO1 = 20;
        public const uint PROTO2 = 21;
        public const uint PAR8R = 22;
        public const uint PAR8O = 23;
        public const uint PAR48IO = 24;
        public const uint PAR2DA = 25;
        public const uint DAC4 = 26;
        public const uint OPTORELTTL = 27;
        public const uint OPTOREL16 = 28;
        public const uint OPTOMOS = 29;
        public const uint OPTOLCALC = 30;
        public const uint OPTOLCA = 31;
        public const uint OPTO16IN = 32;
        public const uint DAC4UI = 33;
        public const uint DAC16BITDUAL = 34;
        public const uint ADI1 = 35;
        public const uint ADI2 = 36;
        public const uint AD12BIT = 37;
        public const uint C3X32BIT = 38;
        public const uint R220V = 39;
        public const uint REL16 = 40;
        public const uint REL8 = 41;
        public const uint REL8UM = 42;
        public const uint TIMER9 = 43;
        public const uint TIMER9LCA = 44;
        public const uint TTL24IO = 45;
        public const uint WATCHDOG3 = 46;
        public const uint MFB51 = 47;
        public const uint TAP14PCI = 48;
        public const uint TAP14ISA = 49;
        public const uint USBWDOG1 = 50;
        public const uint USBWDOG2 = 51;
        public const uint USBWDOG3 = 52;
        public const uint GPIB = 53;
        public const uint PCIGPIB = 54;
        public const uint USBGPIB = 55;
        public const uint PCITTL32 = 56;
        public const uint PCIOPTOREL16 = 57;
        public const uint PCIOPTO16IO = 58;
        public const uint PCIOPTO16IOLC = 59;
        public const uint PCIREL16 = 60;
        public const uint PCIPROTO = 61;
        public const uint USBOPTOREL16 = 62;
        public const uint USBOPTO16IO = 63;
        public const uint USBREL8 = 64;
        public const uint USBOPTO8 = 65;
        public const uint PCIAD16DAC4 = 66;
        public const uint USBREL8LC = 67;
        public const uint USBOPTO8LC = 68;
        public const uint LOGICANALYZER = 69;
        public const uint TASTMAUS1 = 70;
        public const uint PCIWDOG3 = 71;
        public const uint PCIWDOG4 = 72;
        public const uint USBOPTOREL32 = 73;
        public const uint USBOPTOIO32 = 74;
        public const uint PCIEXT64 = 75;
        public const uint PCITTL64 = 76;
        public const uint USBFLASH = 77;
        public const uint USBAD8LC = 78;
        public const uint USBOPTOIN64 = 79;
        public const uint USBOPTOOUT64 = 80;
        public const uint USBREL64 = 81;
        public const uint PCITTL64FIFO = 82;
        public const uint PCIDAC416 = 83;
        public const uint USBAD8DAC2 = 84;
        public const uint SEROPTOREL32 = 85;
        public const uint SERREL64 = 86;
        public const uint ETHOPTOREL32 = 87;
        public const uint ETHREL64 = 88;
        public const uint USBOPTOREL8 = 89;
        public const uint USBOPTOIO8 = 90;
        public const uint USBAD8DAC214 = 91;
        public const uint USBTTL24 = 92;
        public const uint ETHOPTO64IN = 93;
        public const uint ETHOPTO64OUT = 94;
        public const uint ETHOPTO32IO = 95;
        public const uint ETHOPTOREL16 = 96;
        public const uint ETHOPTO16IO = 97;
        public const uint ETHOPTOREL8 = 98;
        public const uint ETHOPTO8IO = 99;
        public const uint SEROPTO64IN = 100;
        public const uint SEROPTO64OUT = 101;
        public const uint SEROPTO32IO = 102;
        public const uint SEROPTOREL16 = 103;
        public const uint SEROPTO16IO = 104;
        public const uint SEROPTOREL8 = 105;
        public const uint SEROPTO8IO = 106;
        public const uint USBPROTO1 = 107;
        public const uint USBCAN1 = 108;
        public const uint USBSPIMON = 109;
        public const uint USBSPIANA = 110;
        public const uint USBCOUNTER1 = 111;
        public const uint PCIEXPWDOG1 = 16;
        public const uint PCIEXPWDOG2 = 16;
        public const uint PCIEXPTTL128 = 112;
        public const uint PCIEXPPROTO1 = 113;
        public const uint USBAD16DAC = 114;
        public const uint ETHAD8DAC2 = 115;
        public const uint ETHAD8DAC214 = 116;
        public const uint SERAD8DAC2 = 117;
        public const uint SERAD8DAC214 = 118;
        public const uint ETHTTL24 = 119;
        public const uint SERTTL24 = 120;
        public const uint BTOPTOREL16 = 121;
        public const uint BTOPTO16IO = 122;
        public const uint BTOPTOREL32 = 123;
        public const uint BTOPTO32IO = 124;
        public const uint BTOPTO64IN = 125;
        public const uint BTOPTO64OUT = 126;
        public const uint BTREL64 = 127;
        public const uint BTOPTOREL8 = 128;
        public const uint BTOPTO8IO = 129;
        public const uint BTAD8DAC2 = 130;
        public const uint BTAD8DAC214 = 131;
        public const uint BTTTL24 = 132;
        public const uint USBREL8UM = 133;
        public const uint USBREL32 = 134;
        public const uint USBIN32 = 135;
        public const uint USBOUT32 = 136;
        public const uint USBREL2 = 137;
        public const uint USBREL4 = 138;
        public const uint USBIN8 = 139;
        public const uint USBIN4 = 140;
        public const uint LASTCARD = 140;



        public const uint DEFAULTGPIB = 0x8000;

        [DllImport(_qlibdll_)]
        public static extern uint QAPIVersion(uint type);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIInitialize(uint para1, uint para2, uint para3, uint para4);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtSetDebugLevel(uint dbgval);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtEnableIRQ(uint cdl, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtNumOfCards();
        //[DllImport(_qlibdll_)]
        //LPCARDDATAS QAPICALLER QAPIExtGetCardInfo (uint cardnum);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtGetCardInfoEx(uint cardnum, [In, Out] CARDDATAS lpcd);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtReleaseCardInfo([In, Out] CARDDATAS lpcd);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtOpenCard(uint cardnum, uint devnum);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtCloseCard(uint cdl);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadAD(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtWriteDA(uint cdl, uint channel, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtLatchDA(uint cdl);
        [DllImport(_qlibdll_)]
        public static extern float QAPIExtConvertDWToVoltage(uint cdl, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtConvertVoltageToDW(uint cdl, float value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadDI1(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadDI8(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadDI16(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadDI32(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern UInt64 QAPIExtReadDI64(uint cdl, uint channel, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWriteDO1(uint cdl, uint channel, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWriteDO8(uint cdl, uint channel, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWriteDO16(uint cdl, uint channel, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWriteDO32(uint cdl, uint channel, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWriteDO64(uint cdl, uint channel, UInt64 value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWrite8255(uint cdl, uint chipnum, uint reg, uint value);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtRead8255(uint cdl, uint chipnum, uint reg);
        [DllImport(_qlibdll_)]
        public static extern void QAPIExtWrite8253(uint cdl, uint chipnum, uint reg, uint value);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtRead8253(uint cdl, uint chipnum, uint reg);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtWatchdog(uint cdl, uint job);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtSpecial(uint cdl, uint jobcode, uint para1, uint para2);
        [DllImport(_qlibdll_)]
        public static extern UInt64 QAPIExtSpecial64(uint cdl, uint jobcode, UInt64 para1, UInt64 para2);
        [DllImport(_qlibdll_, EntryPoint = "QAPIExtSpecial")]
        public static extern uint QAPIExtSpecialSP(uint cdl, uint jobcode, uint para1, ref uint para2);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadString(uint cdl, uint device, StringBuilder buffer, uint maxsize, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtWriteString(uint cdl, uint device, [MarshalAs(UnmanagedType.LPStr)]String buffer, uint maxsize, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtWriteStringEx(uint cdl, uint device, [MarshalAs(UnmanagedType.LPStr)]String buffer, uint maxsize, uint mode, uint bEnableREN);
        [DllImport(_qlibdll_)]
        public static extern uint QAPINumOfCards();
        //[DllImport(_qlibdll_)]
        //LPCARDDATAS QAPICALLER QAPIGetCardInfo (uint cardnum);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetCardInfoEx(uint cardnum, [In, Out] CARDDATAS lpcd);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetAD(uint cardnum, uint channel);
        [DllImport(_qlibdll_)]
        public static extern float QAPIConvertDWToVoltage(uint cardnum, uint value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIConvertVoltageToDW(uint cardnum, float value, uint mode);
        [DllImport(_qlibdll_)]
        public static extern void QAPIPutDA(uint cardnum, uint channel, uint value);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetDI(uint cardnum, uint channel);
        [DllImport(_qlibdll_)]
        public static extern void QAPIPutDO(uint cardnum, uint channel, uint value);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIRead8253(uint cardnum, uint reg);
        [DllImport(_qlibdll_)]
        public static extern void QAPIWrite8253(uint cardnum, uint reg, uint value);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIRead8255(uint cardnum, uint reg);
        [DllImport(_qlibdll_)]
        public static extern void QAPIWrite8255(uint cardnum, uint reg, uint value);
        [DllImport(_qlibdll_)]
        public static extern void QAPIWatchdogEnable();
        [DllImport(_qlibdll_)]
        public static extern void QAPIWatchdogDisable();
        [DllImport(_qlibdll_)]
        public static extern void QAPIWatchdogRetrigger();
        [DllImport(_qlibdll_)]
        public static extern void QAPIWatchdogLoad();
        [DllImport(_qlibdll_)]
        public static extern uint QAPIWatchdogStatus();
        [DllImport(_qlibdll_)]
        public static extern uint QAPISpecial(uint cardnum, uint jobcode, uint para1, uint para2);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIReadString(uint cardnum, uint device, StringBuilder buffer, uint maxsize, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIWriteString(uint cardnum, uint device, [MarshalAs(UnmanagedType.LPStr)]String buffer, uint maxsize, uint mode);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIWriteStringEx(uint cardnum, uint device, [MarshalAs(UnmanagedType.LPStr)]String buffer, uint maxsize, uint mode, uint bEnableREN);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIConnect([MarshalAs(UnmanagedType.LPStr)]String ip, uint port, [MarshalAs(UnmanagedType.LPStr)]String username, [MarshalAs(UnmanagedType.LPStr)]String password, uint timeout);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIDisconnect();
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetLastError();
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetLastErrorCode();
        //[DllImport(_qlibdll_)]
        //char*   QAPICALLER QAPIGetLastErrorString();
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetLastErrorStringEx(StringBuilder buffer, uint buffersize);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIWaitIRQ(uint cardnum, uint devnum);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIGetConnectionMode(uint nType, StringBuilder buffer, uint buffersize);
        [DllImport(_qlibdll_)]
        public static extern uint QAPISetupCounter(uint cardnum, uint counter, uint mode, uint reserved);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIResetCounter(uint cardnum, uint counter, uint reserved1, uint reserved2);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIReadCounter(uint cardnum, uint counter, uint mode, uint reserved2);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtSetupCounter(uint cdl, uint counter, uint mode, uint reserved);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtResetCounter(uint cdl, uint counter, uint reserved1, uint reserved2);
        [DllImport(_qlibdll_)]
        public static extern uint QAPIExtReadCounter(uint cdl, uint counter, uint mode, uint reserved2);


    }

}

