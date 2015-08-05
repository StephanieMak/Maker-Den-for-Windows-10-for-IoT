using Glovebox.IO.Components;
using Glovebox.IO.Components.Actuators;

namespace Glovebox.IO.Components.Actuators
{
    public class Relay : OutputPin
    {

        /// <summary>
        /// Create a relay control
        /// </summary>
        /// <param name="pin">Explorer Pro HAT Output pin number</param>
        /// <param name="name">Unique identifying name for command and control</param>
        public Relay(ExplorerPins.Output pinNumber, string name)
            : base((int)pinNumber, name)
        {
        }

        /// <summary>
        /// Create a relay control
        /// </summary>
        /// <param name="pin">Raspberry Pi 2 pin number</param>
        /// <param name="name">Unique identifying name for command and control</param>
        public Relay(int pinNumber, string name)
            : base((int)pinNumber, name)
        {
        }
    }
}
