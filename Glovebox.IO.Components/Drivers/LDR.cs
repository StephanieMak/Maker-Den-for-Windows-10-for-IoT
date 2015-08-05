﻿using Glovebox.IO.Components.Converters;

namespace Glovebox.IO.Components.Drivers {
    public class LDR {

        private readonly ADS1015 adc;
        private readonly ADS1015.Channel channel;
        private readonly ADS1015.Gain gain;
        const double CalibrationOffset = -2;
        private UnitsNet.ElectricPotential volts;

        public LDR(ADS1015 adc, ADS1015.Channel channel, ADS1015.Gain gain, UnitsNet.ElectricPotential volts) {
            this.adc = adc;
            this.channel = channel;
            this.gain = gain;
            this.volts = volts;
        }

        public double Measure() {
            var millivolts = adc.GetMillivolts(channel, gain);

            // convert to a percentage between zero and volts (eg 5000 millivolts)
            return (volts.Millivolts - millivolts) / volts.Millivolts * 100d;
        }
    }
}