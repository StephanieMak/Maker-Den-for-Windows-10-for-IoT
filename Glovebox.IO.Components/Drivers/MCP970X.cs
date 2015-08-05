using Glovebox.IO.Components.Converters;
using System;
using UnitsNet;

namespace Glovebox.IO.Components.Drivers {
    public class Mcp970X : IDisposable {

        private readonly ADS1015 adc;
        private readonly ADS1015.Channel channel;
        private readonly ADS1015.Gain gain;
        private readonly int CalibrationOffset;
        private readonly int zeroDegreeOffset;
        private readonly double millivoltsPerDegree;

        public Mcp970X(ADS1015 adc, ADS1015.Channel channel, ADS1015.Gain gain, int zeroDegreeOffset, double millivoltsPerDegree, int CalibrationOffset) {
            this.adc = adc;
            this.channel = channel;
            this.gain = gain;
            this.zeroDegreeOffset = zeroDegreeOffset;
            this.millivoltsPerDegree = millivoltsPerDegree;
            this.CalibrationOffset = CalibrationOffset;
        }

        public Temperature Measure() {
            var temp = GetTemperature(adc.GetMillivolts(channel, gain));
            return temp;
        }

        private UnitsNet.Temperature GetTemperature(double voltage) {
            var milliVolts = voltage;
            var centigrade = (double)((milliVolts - zeroDegreeOffset) / millivoltsPerDegree) + CalibrationOffset; // / TemperatureCoefficientMillivoltsPerDegreeC - ZeroDegreesMillivoltOffset) + CalibrationOffset;
            return UnitsNet.Temperature.FromDegreesCelsius(centigrade);
        }

        public void Dispose() {
        }
    }
}
