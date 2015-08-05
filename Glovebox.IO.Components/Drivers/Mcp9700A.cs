using Glovebox.IO.Components.Converters;

namespace Glovebox.IO.Components.Drivers {
    public class Mcp9700A : Mcp970X {

        public Mcp9700A(ADS1015 adc, ADS1015.Channel channel, ADS1015.Gain gain) : base(adc, channel, gain, 530, 11, -2) {
        }
    }
}
