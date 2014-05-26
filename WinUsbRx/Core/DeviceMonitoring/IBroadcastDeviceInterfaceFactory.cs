// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBroadcastDeviceInterfaceFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IDeviceManagementStructureFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    /// <summary>
    /// The BroadcastDeviceInterfaceFactory interface.
    /// </summary>
    internal interface IBroadcastDeviceInterfaceFactory
    {
        /// <summary>
        /// The create broadcast device interface.
        /// </summary>
        /// <returns>
        /// The <see cref="BroadcastDeviceInterface"/>.
        /// </returns>
        BroadcastDeviceInterface CreateBroadcastDeviceInterface();
    }
}