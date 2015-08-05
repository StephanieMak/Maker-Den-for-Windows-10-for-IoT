using Glovebox.IO.Components.Converters;

namespace Glovebox.IO.Components.Drivers {
    public class InputAnalogPin {
        public MCP3002 adc;
        public MCP3002.Channel channel;

        public InputAnalogPin(MCP3002 adc, MCP3002.Channel channel) {
            this.adc = adc;
            this.channel = channel;
        }

        public int Absolute() {
            return adc.ReadAbsolute(channel);
        }

        public decimal Relative() {
            return adc.ReadRelative(channel);
        }
    }
}
