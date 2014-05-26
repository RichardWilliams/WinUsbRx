// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInformationElementFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The DeviceInformationElementFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.Factory
{
    using System;
    using UnsafeNative;

    /// <summary>
    /// The DeviceInformationElementFactory interface.
    /// </summary>
    internal interface IDeviceInformationElementFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <param name="deviceInfoData">
        /// The device info data.
        /// </param>
        /// <returns>
        /// The <see cref="DeviceInformationElement"/>.
        /// </returns>
        DeviceInformationElement Create(IntPtr deviceInformationSetHandle, DeviceInfoData deviceInfoData);
    }
}