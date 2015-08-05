using Glovebox.IO.Components.Converters;

namespace Glovebox.IO.Components.Drivers {
    class Mcp9701AE : Mcp970X {

        public Mcp9701AE(ADS1015 adc, ADS1015.Channel channel, ADS1015.Gain gain) : base(adc, channel, gain, 400, 20.5, -6) {
        }
    }
}
