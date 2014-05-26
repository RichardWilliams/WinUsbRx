// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInterfaceFactory.cs" company="None">
//   
// </copyright>
// <summary>
//   Defines the IDeviceInterfaceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.Factory
{
    using System;
    using UnsafeNative;

    /// <summary>
    /// The DeviceInterfaceFactory interface.
    /// </summary>
    internal interface IDeviceInterfaceFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device Information Set Handle.
        /// </param>
        /// <param name="deviceInterfaceData">
        /// The device Interface Data.
        /// </param>
        /// <returns>
        /// The <see cref="DeviceInterface"/>.
        /// </returns>
        DeviceInterface Create(IntPtr deviceInformationSetHandle, DeviceInterfaceData deviceInterfaceData);
    }
}