namespace Glovebox.IO.Components.Actuators {

    // Explorer Hat Led support
    // http://shop.pimoroni.com/products/explorer-hat

    public class Led_Explorer : Led {
        public Led_Explorer(ExplorerPins.Led pinNumber, string name) : base((int)pinNumber, name) { }
    }
}
