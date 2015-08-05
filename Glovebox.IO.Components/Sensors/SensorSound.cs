using System;
using Glovebox.IoT.Base;
using Glovebox.IO.Components.Converters;
using UnitsNet;
using Glovebox.IoT.Telemetry;
using UnitsNet.Units;
using Glovebox.IO.Components.Drivers;

namespace Glovebox.IO.Components.Sensors {
    public class SensorSound : SensorBase {

        public override double Current {
            get {
                return SampleSound() * 2;
            }
        }

        Sound sound;
        const int numberOfSamples = 4;
        const int averagedOver = 4;
        const int midpoint = 512;
        double runningAverage = 0;          //the running average of calculated values
        double sample;

        public SensorSound(ADS1015 adc, int SampleRateMilliseconds, string name) : base("sound", "p", SensorMakerDen.ValuesPerSample.One, SampleRateMilliseconds, name) {
            sound = new Sound(adc, ADS1015.Channel.A2, ADS1015.Gain.Volt5, ElectricPotential.From(5, ElectricPotentialUnit.Volt));

            StartMeasuring();
        }



        protected override string GeoLocation() {
            return string.Empty;
        }

        protected override void Measure(double[] value) {
            value[0] = SampleSound();
        }

        protected override void SensorCleanup() {
            
        }

        private double SampleSound() {
            double sumOfSamples = 0;
            double averageReading; //the average of that loop of readings

            for (int i = 0; i < numberOfSamples; i++) {
                sample = sound.Measure();
                sumOfSamples += sample;
            }

            averageReading = sumOfSamples / numberOfSamples;     //calculate running average
            runningAverage = (((averagedOver - 1) * runningAverage) + averageReading) / averagedOver;

            return runningAverage;
        }

    }
}
