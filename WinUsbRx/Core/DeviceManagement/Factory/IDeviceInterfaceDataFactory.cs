// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInterfaceDataFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The DeviceInterfaceDataFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.Factory
{
    using UnsafeNative;

    /// <summary>
    /// The DeviceInterfaceDataFactory interface.
    /// </summary>
    internal interface IDeviceInterfaceDataFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="DeviceInterfaceData"/>.
        /// </returns>
        DeviceInterfaceData Create();
    }
}