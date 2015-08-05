using Glovebox.Adafruit.Mini8x8Matrix;
using Glovebox.IO.Components.Actuators;
using Glovebox.IO.Components.Converters;
using Glovebox.IO.Components.Sensors;

namespace Glovebox.IO.Components {
    public class ExplorerHat {

        protected ADS1015 adc;
        protected Led_Explorer[] explorerLEDs;
        public Led ExplorerLedRed { get { return explorerLEDs[0]; } }
        public Led ExplorerLedGreen { get { return explorerLEDs[1]; } }

        public ExplorerHat(bool hat) {
            if (hat) {
                adc = new ADS1015();

                explorerLEDs = new Led_Explorer[] {
                    new Led_Explorer(ExplorerPins.Led.Three, "led3"),
                    new Led_Explorer(ExplorerPins.Led.Four, "led4")
                };

                foreach (var led in explorerLEDs) {
                    led.Off();
                }
            }
        }
    }
}
