using System;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;
using System.Threading.Tasks;

namespace Glovebox.IO.Components.Converters {

    // http://ww1.microchip.com/downloads/en/DeviceDoc/21294C.pdf
    public class MCP3002 : IDisposable {

        private const string SPI_CONTROLLER_NAME = "SPI0";  // For Raspberry Pi 2, use SPI0

        private const int SINGLE_ENDED_MODE = 0x60;
        private const int PSEUDO_DIFFERENTIAL_MODE = 0x00;
        private const int ChipMode = SINGLE_ENDED_MODE;

        private SpiDevice SpiMCP3200;
        private static object deviceLock = new object();

        private const decimal RANGE = 1024;  // 10 bit adc for relative calculation

        byte[] readBuffer = new byte[2]; /*this is defined to hold the output data*/
        byte[] SpiControlFrame = new byte[2] { 0x00, 0x00 }; // SPI Config and must be the same length and the readbuffer

        public enum ChipSelect {
            CS0 = 0,
            CS1 = 1,
        }

        public enum Channel {
            C0 = 0x8,
            C1 = 0x10,
        }

        public MCP3002(ChipSelect cs) {
            Task.Run(() => InitSPI(cs)).Wait();
        }

        private async Task InitSPI(ChipSelect cs) {
            try {
                var settings = new SpiConnectionSettings((int)cs);
                settings.ClockFrequency = 500000;// 10000000;
                settings.Mode = SpiMode.Mode0; //Mode3;

                string spiAqs = SpiDevice.GetDeviceSelector(SPI_CONTROLLER_NAME);
                var deviceInfo = await DeviceInformation.FindAllAsync(spiAqs);
                SpiMCP3200 = await SpiDevice.FromIdAsync(deviceInfo[0].Id, settings);
            }
            /* If initialization fails, display the exception and stop running */
            catch (Exception ex) {
                throw new Exception("SPI Initialization Failed", ex);
            }
        }

        public int ReadAbsolute(Channel cn) {
            lock (deviceLock) {
                SpiControlFrame[0] = (byte)((byte)ChipMode | (byte)cn);
                SpiMCP3200.TransferFullDuplex(SpiControlFrame, readBuffer);
                return convertToInt(readBuffer);
            }
        }

        public decimal ReadRelative(Channel cn) {
            return ReadAbsolute(cn) / RANGE;
        }

        public int convertToInt(byte[] data) {
            // MCP3002 is a 10 bit ADC
            return (data[0] & 0x03) << 8 | data[1];
        }

        public void Dispose() {
            SpiMCP3200.Dispose();
        }
    }
}
