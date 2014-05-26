// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnsafeNativeMethodsWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UnsafeNativeMethodsWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UnsafeNativeMethods
{
    using System;

    /// <summary>
    /// The unsafe native methods wrapper for device monitoring.
    /// </summary>
    internal class UnsafeNativeMethodsWrapper : IUnsafeNativeMethodsWrapper
    {
        /// <summary>
        /// The register device notification, this calls into the unsafe methods that are pinvoked.
        /// </summary>
        /// <param name="recipient">
        /// The h recipient.
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
        public IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, uint flags)
        {
            return UnsafeNativeMethods.RegisterDeviceNotification(recipient, notificationFilter, flags);
        }

        /// <summary>
        /// The un register device notification, this calls into the unsafe methods that are pinvoked.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        public IntPtr UnRegisterDeviceNotification(IntPtr handle)
        {
            return UnsafeNativeMethods.UnRegisterDeviceNotification(handle);
        }
    }
}