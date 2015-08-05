using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glovebox.IO.Components {
    public class ExplorerPins {

        public enum Output : ushort {
            One = 6,
            Two = 12,
            Three = 13,
            Four = 16,
        }

        public enum Input : ushort {
            One = 23,
            Two = 22,
            Three = 24,
            Four = 25,
        }

        public enum Led : ushort {
            Three = 27,
            Four = 5,
        }

        public enum Spi : ushort {
            MOSI = 10,
            MISO = 9,
            SCK = 11,
            CS = 8
        }

        public enum Serial : ushort {
            TX = 14,
            RX = 15
        }

        public enum I2C : ushort {
            SDA = 2,
            SCL = 3
        }

        public enum Motor : ushort {
            TwoPlus = 21,
            TwoMinus = 26,
            OnePlus = 19,
            OneMinus = 20
        }
    }
}
