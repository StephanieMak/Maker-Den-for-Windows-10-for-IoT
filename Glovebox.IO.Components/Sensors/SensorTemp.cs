using Glovebox.IO.Components.Converters;
using Glovebox.IO.Components.Drivers;
using Glovebox.IoT.Base;
using Glovebox.IoT.Telemetry;

namespace Glovebox.IO.Components.Sensors {
    public class SensorTemp : SensorBase {

        Mcp9700A temp;
        public SensorTemp(ADS1015 adc, int SampleRateMilliseconds, string name) : base("temp", "c", SensorMakerDen.ValuesPerSample.One, SampleRateMilliseconds, name) {
            temp = new Mcp9700A(adc, ADS1015.Channel.A4, ADS1015.Gain.Volt5);

            StartMeasuring();
        }

        public override double Current {
            get {
                return temp.Measure().DegreesCelsius;
            }
        }

        protected override string GeoLocation() {
            return string.Empty;
        }

        protected override void Measure(double[] value) {
            value[0] = temp.Measure().DegreesCelsius;
        }

        protected override void SensorCleanup() {
            temp.Dispose();
        }
    }
}
