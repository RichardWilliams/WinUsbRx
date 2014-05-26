// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnsafeNativeMethodsWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUnsafeNativeMethodsWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UnsafeNativeMethods
{
    using System;

    /// <summary>
    /// The UnsafeNativeMethodsWrapper interface.
    /// </summary>
    internal interface IUnsafeNativeMethodsWrapper
    {
        /// <summary>
        /// The register device notification.
        /// </summary>
        /// <param name="recipient">
        /// The recipient.
        /// </param>
        /// <param name="notificationFilter">
        /// The notification filter.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, uint flags);

        /// <summary>
        /// The un register device notification.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        IntPtr UnRegisterDeviceNotification(IntPtr handle);
    }
}