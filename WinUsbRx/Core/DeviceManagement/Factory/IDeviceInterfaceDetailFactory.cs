// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInterfaceDetailFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IDeviceInterfaceDetailFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.Factory
{
    using System;

    /// <summary>
    /// The DeviceInterfaceDetailFactory interface.
    /// </summary>
    internal interface IDeviceInterfaceDetailFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device Information Set Handle.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="DeviceInterfaceDetail"/>.
        /// </returns>
        DeviceInterfaceDetail Create(IntPtr deviceInformationSetHandle, int size);

        /// <summary>
        /// The create null.
        /// </summary>
        /// <returns>
        /// The <see cref="NullDeviceInterfaceDetail"/>.
        /// </returns>
        NullDeviceInterfaceDetail CreateNull();
    }
}