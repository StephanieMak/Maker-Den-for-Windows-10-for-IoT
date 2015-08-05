using Glovebox.IoT.Base;
using Glovebox.IoT.Command;
using Windows.System;

namespace Glovebox.IO.Components.Actuators {
    public class Sys : ActuatorBase {

        public enum Actions {
            Shutdown,
            Restart
        }


        /// <summary>
        /// Create a System control
        /// </summary>
        /// <param name="name">Unique identifying name for command and control</param>
        public Sys(string name)
            : base(name, "system") {
        }

        protected override void ActuatorCleanup() {
        }

        public void Action(Actions action) {
            switch (action) {
                case Actions.Shutdown:
                    Shutdown();
                    break;
                case Actions.Restart:
                    Restart();
                    break;
                default:
                    break;
            }
        }

        public override void Action(IotAction action) {
    //        if (action.identified) { return; }
            switch (action.cmd) {
                case "halt":
                    Shutdown();
                    break;
                case "reboot":
                    Restart();
                    break;
            }
        }

        public void Shutdown() {
            Windows.System.ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, new System.TimeSpan(0));
        }

        public void Restart() {
            Windows.System.ShutdownManager.BeginShutdown(ShutdownKind.Restart, new System.TimeSpan(0));
        }
    }
}
