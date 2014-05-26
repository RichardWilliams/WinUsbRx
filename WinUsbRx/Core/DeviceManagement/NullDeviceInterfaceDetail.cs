// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullDeviceInterfaceDetail.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the NullDeviceInterfaceDetail type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    /// <summary>
    /// The null device interface detail.
    /// </summary>
    internal class NullDeviceInterfaceDetail : IDeviceInterfaceDetail
    {
        /// <summary>
        /// Gets the device path.
        /// </summary>
        public string DevicePath
        {
            get { return "Unknown"; }
        }
    }
}