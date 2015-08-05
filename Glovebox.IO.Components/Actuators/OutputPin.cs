using Glovebox.IoT.Base;
using Glovebox.IoT.Command;
using Windows.Devices.Gpio;

namespace Glovebox.IO.Components.Actuators {
    public class OutputPin: ActuatorBase {

        GpioController gpio = GpioController.GetDefault();
        GpioPin pin;

        public enum Actions {
            On,
            Off
        }

        public OutputPin(int pinNumber, string name) : base(name, "output") {
            pin = gpio.OpenPin((int)pinNumber, GpioSharingMode.Exclusive);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin.Write(GpioPinValue.Low);
        }

        protected override void ActuatorCleanup() {
            pin.Dispose();
        }

        public void Action(Actions action) {
            switch (action) {
                case Actions.On:
                    On();
                    break;
                case Actions.Off:
                    Off();
                    break;
                default:
                    break;
            }
        }

        public override void Action(IotAction action) {
            switch (action.cmd) {
                case "on":
                    On();
                    break;
                case "off":
                    Off();
                    break;
            }
        }

        public void On() {
            pin.Write(GpioPinValue.High);
        }

        public void Off() {
            pin.Write(GpioPinValue.Low);
        }
    }
}
