// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceNullProperty.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The device null property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    /// <summary>
    /// The device null property.
    /// </summary>
    internal class DeviceNullProperty : BaseDeviceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceNullProperty"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DeviceNullProperty(byte[] data) : base(data)
        {
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return "(null)";
        }
    }
}