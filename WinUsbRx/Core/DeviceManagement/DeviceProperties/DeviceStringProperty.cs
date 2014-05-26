// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceStringProperty.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceStringProperty type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    /// <summary>
    /// The device string property.
    /// </summary>
    internal class DeviceStringProperty : BaseDeviceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceStringProperty"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DeviceStringProperty(byte[] data) : base(data)
        {
            StringData = System.Text.Encoding.Unicode.GetString(data, 0, data.Length - sizeof(char));
        }

        /// <summary>
        /// Gets or sets the string data.
        /// </summary>
        private string StringData { get; set; }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return StringData;
        }
    }
}