using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

// ADS1015 ADC on I2C Bus
// http://www.ti.com/product/ads1015

namespace Glovebox.IO.Components.Converters {
    public class ADS1015 : IDisposable {
        private static object deviceLock = new object();
        private I2cDevice I2CDevice;

        private const int I2C_ADDRESS = 0x48;
        private const string I2C_CONTROLLER_NAME = "I2C1";        /* For Raspberry Pi 2, use I2C1 */

        private const int REG_CONV = 0x00;
        private const int REG_CFG = 0x01;

        ushort config = 0;
        byte[] data = new byte[3];

        public enum SamplesPerSecond {
            SPS128, SPS250, SPS490, SPS920, SPS1600, SPS2400, SPS3300
        }

        private ushort[] SamplePerSecondMap = { 0x0000, 0x0020, 0x0040, 0x0060, 0x0080, 0x00A0, 0x00C0 };
        private ushort[] SamplesPerSecondRate = { 128, 250, 490, 920, 1600, 2400, 3300 };

        public enum Channel {
            A4 = 0x4000, A3 = 0x5000, A2 = 0x6000, A1 = 0x7000
        }

        //https://learn.adafruit.com/adafruit-4-channel-adc-breakouts/programming
        ushort[] programmableGainMap = { 0x0000, 0x0200, 0x0400, 0x0600, 0x0800, 0x0A00 };
        ushort[] programmableGain_Scaler = { 6144, 4096, 2048, 1024, 512, 256 };

        public enum Gain {
            Volt5 = 0,  //   PGA_6_144V = 0, //6144,  //0
            Volt33 = 1, //   PGA_4_096V = 1, //4096,  //1
            PGA_2_048V = 2, //2048,  //2
            PGA_1_024V = 3, //1024,  //3
            PGA_0_512V = 4, //512,   //4
            PGA_0_256V = 5, //256,   //5
        }

        public ADS1015() {
            Task.Run(() => InitI2CDevice()).Wait();
        }

        private async Task InitI2CDevice() {
            try {
                var settings = new I2cConnectionSettings(I2C_ADDRESS);
                settings.BusSpeed = I2cBusSpeed.StandardMode;

                string aqs = I2cDevice.GetDeviceSelector(I2C_CONTROLLER_NAME);  /* Find the selector string for the I2C bus controller                   */
                var dis = await DeviceInformation.FindAllAsync(aqs);            /* Find the I2C bus controller device with our selector string           */
                I2CDevice = await I2cDevice.FromIdAsync(dis[0].Id, settings);    /* Create an I2cDevice with our selected bus controller and I2C settings */
            }
            catch (Exception ex) {
                throw new Exception("I2C Initialization Failed", ex);
            }
        }

        public double GetMillivolts(Channel channel, Gain gain = Gain.Volt5, SamplesPerSecond sps = SamplesPerSecond.SPS1600) {
            lock (deviceLock) {

                byte[] result = new byte[2];

                // Set disable comparator and set "single shot" mode	
                config = 0x0003 | 0x8000; // | 0x100;
                config |= (ushort)SamplePerSecondMap[(int)sps];
                config |= (ushort)channel;
                config |= (ushort)programmableGainMap[(int)gain];

                data[0] = REG_CFG;
                data[1] = (byte)((config >> 8) & 0xFF);
                data[2] = (byte)(config & 0xFF);


                I2CDevice.Write(data);
                // delay in milliseconds
                //int delay = (1000.0 / SamplesPerSecondRate[(int)sps] + .1;
            //    int delay = 1;
                Task.Delay(TimeSpan.FromMilliseconds(.5)).Wait();

                I2CDevice.WriteRead(new byte[] { (byte)REG_CONV, 0x00 }, result);

                //var r = (((result[0] << 8) | result[1]) >> 4);

                //Debug.WriteLine(r.ToString());

                return (ushort)(((result[0] << 8) | result[1]) >> 4) * programmableGain_Scaler[(int)gain] / 2048;
            }
        }

        void IDisposable.Dispose() {
            I2CDevice.Dispose();
        }
    }
}
