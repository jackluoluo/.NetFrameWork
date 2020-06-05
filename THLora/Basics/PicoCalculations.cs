using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THLora_Testbench
{
    public class PicoCalculations
    {
        #region Static Functions
        //Static Functions
        public static bool FillVoltageBuffer(int Samples, double TheVoltRange, short[] Buffer, out double[] VoltageBuffer, out string Fehlerstring)
        {
            Fehlerstring = string.Empty;
            VoltageBuffer = new double[Samples];
            try
            {
                for (int i = 0; i < Samples; i++)
                {
                    VoltageBuffer[i] = TransNorm(TheVoltRange, Buffer[i]);
                }
                return true;
            }
            catch (System.Exception Se)
            {
                Fehlerstring = Se.ToString();
                return false;
            }
        }

        public static Double TransNorm(double TheVoltRange, short Wert)
        {
            //Werte normieren. In der Range entspricht -32768 -x Volt, 32767 +x Volt
            double WDummy = (double)Wert;
            double WMin = -32768;
            double WMax = 32767;
            double dummy = -TheVoltRange + ((WDummy - WMin) / (WMax - WMin)) * (TheVoltRange * 2);
            return dummy;
        }

        public static Double TransNorm(double TheVoltRange, double Wert)
        {
            //Werte normieren. In der Range entspricht -32768 -x Volt, 32767 +x Volt           
            double WMin = -32768;
            double WMax = 32767;
            double dummy = -TheVoltRange + ((Wert - WMin) / (WMax - WMin)) * (TheVoltRange * 2);
            return dummy;
        }

        public static double GetAverage(double[] Buffer)
        {
            try
            {
                double Summe = 0;
                for (int i = 0; i < Buffer.Length; i++)
                {
                    Summe += Buffer[i];
                }
                return Summe / Buffer.Length;
            }
            catch
            {
                return 0;
            }
        }

        public static double GetVoltageRangeDoubleValue(PicoScope2204.PS2000Range TheRange)
        {
            switch (TheRange)
            {
                case PicoScope2204.PS2000Range.PS2000_20MV:
                    return 0.02;
                case PicoScope2204.PS2000Range.PS2000_50MV:
                    return 0.05;
                case PicoScope2204.PS2000Range.PS2000_100MV:
                    return 0.1;
                case PicoScope2204.PS2000Range.PS2000_200MV:
                    return 0.2;
                case PicoScope2204.PS2000Range.PS2000_500MV:
                    return 0.5;
                case PicoScope2204.PS2000Range.PS2000_1V:
                    return 1;
                case PicoScope2204.PS2000Range.PS2000_2V:
                    return 2;
                case PicoScope2204.PS2000Range.PS2000_5V:
                    return 5;
                case PicoScope2204.PS2000Range.PS2000_10V:
                    return 10;
                case PicoScope2204.PS2000Range.PS2000_20V:
                    return 20;
                default:
                    return 0;
            }
        }
        #endregion
    }
}
