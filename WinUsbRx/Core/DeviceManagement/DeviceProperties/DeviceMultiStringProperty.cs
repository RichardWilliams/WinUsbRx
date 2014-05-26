// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceMultiStringProperty.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The device multi string property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    /// <summary>
    /// The device multi string property.
    /// </summary>
    internal class DeviceMultiStringProperty : BaseDeviceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceMultiStringProperty"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public DeviceMultiStringProperty(byte[] data) : base(data)
        {
        }
    }
}