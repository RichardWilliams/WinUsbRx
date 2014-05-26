// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseDeviceProperty.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The base device property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    /// <summary>
    /// The base device property.
    /// </summary>
    internal abstract class BaseDeviceProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDeviceProperty"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        protected BaseDeviceProperty(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        protected byte[] Data { get; private set; }
    }
}