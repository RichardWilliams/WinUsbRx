// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInterface.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceInterface type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    using System;
    using UnsafeNative;

    /// <summary>
    /// The device interface.
    /// </summary>
    internal class DeviceInterface
    {
        /// <summary>
        /// The _device interface data.
        /// </summary>
        private readonly DeviceInterfaceData _deviceInterfaceData;

        /// <summary>
        /// The _device interface detail.
        /// </summary>
        private IDeviceInterfaceDetail _deviceInterfaceDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInterface"/> class.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device Information Set Handle.
        /// </param>
        /// <param name="deviceInterfaceData">
        /// The device interface data.
        /// </param>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe Native Methods Wrapper.
        /// </param>
        public DeviceInterface(IntPtr deviceInformationSetHandle, DeviceInterfaceData deviceInterfaceData, IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper)
        {
            _deviceInterfaceData = deviceInterfaceData;
            _deviceInterfaceDetail = unsafeNativeMethodsWrapper.GetDeviceInterfaceDetail(deviceInformationSetHandle, deviceInterfaceData);
        }
    }
}