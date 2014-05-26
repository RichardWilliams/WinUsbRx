// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInterfaceDetail.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IDeviceInterfaceDetail type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    /// <summary>
    /// The DeviceInterfaceDetail interface.
    /// </summary>
    internal interface IDeviceInterfaceDetail
    {
        /// <summary>
        /// Gets the device path.
        /// </summary>
        string DevicePath { get; }
    }
}