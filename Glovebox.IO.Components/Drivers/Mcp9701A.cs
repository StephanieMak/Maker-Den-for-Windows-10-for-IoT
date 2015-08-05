using UnitsNet;

namespace Glovebox.IO.Components.Drivers {
    /// <summary>
    /// Represents a connection to a MCP9701A temperature sensor.
    /// </summary>
    /// <remarks>See <see cref="http://www.microchip.com/wwwproducts/Devices.aspx?dDocName=en022289"/>,
    /// <see cref="http://ww1.microchip.com/downloads/en/DeviceDoc/20001942F.pdf"/>,
    /// <see cref="http://au.rs-online.com/web/p/temperature-humidity-sensors/7387051/"/>.
    /// </remarks>
    public class Mcp9701a
	{
		#region Constants

		const double TemperatureCoefficientMillivoltsPerDegreeC = 19.53;
		const double ZeroDegreesMillivoltOffset = 400d / TemperatureCoefficientMillivoltsPerDegreeC;
		const double CalibrationOffset = -6;

		#endregion

		#region Fields

		private readonly InputAnalogPin inputPin;
		private readonly ElectricPotential referenceVoltage;

		#endregion

		#region Instance Management

		/// <summary>
		/// Initializes a new instance of the <see cref="Mcp9701a"/> class.
		/// </summary>
		/// <param name="inputPin">The input pin.</param>
		/// <param name="referenceVoltage">The reference voltage.</param>
		public Mcp9701a (InputAnalogPin inputPin, ElectricPotential referenceVoltage)
		{
			this.inputPin = inputPin;
			this.referenceVoltage = referenceVoltage;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets the temperature.
		/// </summary>
		/// <returns>The temperature.</returns>
		public UnitsNet.Temperature GetTemperature ()
		{
            var r = (double)inputPin.Relative();

            var milliVolts = r * (uint)referenceVoltage.Millivolts;
			var centigrade = (double)(milliVolts / TemperatureCoefficientMillivoltsPerDegreeC - ZeroDegreesMillivoltOffset) + CalibrationOffset;

			return UnitsNet.Temperature.FromDegreesCelsius (centigrade);
		}

        #endregion
    }
}
