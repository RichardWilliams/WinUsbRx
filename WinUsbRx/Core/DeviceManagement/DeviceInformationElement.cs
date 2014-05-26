// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInformationElement.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The device information element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    using System;
    using System.Collections.Generic;
    using UnsafeNative;

    /// <summary>
    /// The device information element.
    /// </summary>
    internal class DeviceInformationElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInformationElement"/> class.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="deviceInformationSetHandle">
        /// The device Information Set Handle.
        /// </param>
        /// <param name="deviceInfoData">
        /// The device info data.
        /// </param>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe Native Methods Wrapper.
        /// </param>
        public DeviceInformationElement(Guid guid, IntPtr deviceInformationSetHandle, DeviceInfoData deviceInfoData, IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper)
        {
            DeviceInterfaces = unsafeNativeMethodsWrapper.GetDeviceInterfaces(deviceInformationSetHandle, deviceInfoData, guid);
        }

        /// <summary>
        /// Gets the device interfaces.
        /// </summary>
        public IEnumerable<DeviceInterface> DeviceInterfaces { get; private set; }
    }
}