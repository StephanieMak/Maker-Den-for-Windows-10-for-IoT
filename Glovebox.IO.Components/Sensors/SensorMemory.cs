using Glovebox.IoT.Base;
using Glovebox.IoT.Telemetry;
using Windows.System;


namespace Glovebox.IO.Components.Sensors {
    public class SensorMemory : SensorBase {

        public SensorMemory(int SampleRateMilliseconds, string name)
            : base("mem", "KiB", SensorMakerDen.ValuesPerSample.One, SampleRateMilliseconds, name) {

            StartMeasuring();
        }

        protected override void Measure(double[] value) {
            value[0] = UnitsNet.Information.FromBytes(MemoryManager.AppMemoryUsage).Kilobytes;
        }

        protected override string GeoLocation() {
            return string.Empty;
        }

        public override double Current {
            get { return UnitsNet.Information.FromBytes(MemoryManager.AppMemoryUsage).Kilobytes; }
        }

        protected override void SensorCleanup() {
        }
    }
}
