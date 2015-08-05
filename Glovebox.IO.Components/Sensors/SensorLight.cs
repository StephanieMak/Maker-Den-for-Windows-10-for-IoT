using Glovebox.IO.Components.Converters;
using Glovebox.IO.Components.Drivers;
using Glovebox.IoT.Base;
using Glovebox.IoT.Telemetry;
using UnitsNet;
using UnitsNet.Units;

namespace Glovebox.IO.Components.Sensors {
    public class SensorLight : SensorBase {

        LDR ldr;

        public SensorLight(ADS1015 adc, int SampleRateMilliseconds, string name) : base("light", "p", SensorMakerDen.ValuesPerSample.One, SampleRateMilliseconds, name) {
            ldr = new LDR(adc, ADS1015.Channel.A3, ADS1015.Gain.Volt5, ElectricPotential.From(5, ElectricPotentialUnit.Volt));

            StartMeasuring();
        }

        public override double Current {
            get {
                return ldr.Measure();
            }
        }

        protected override string GeoLocation() {
            return string.Empty;
        }

        protected override void Measure(double[] value) {
            value[0] = ldr.Measure();
        }

        protected override void SensorCleanup() {
        }
    }
}
