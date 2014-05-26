// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceNotifications.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The interface describing the register and unregister for device notifications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using Handle;

    /// <summary>
    /// The DeviceManagement interface.
    /// </summary>
    internal interface IDeviceNotifications
    {
        /// <summary>
        /// The register for device notifications.
        /// </summary>
        /// <param name="windowHandleToReceiveNotifications">
        ///     The window handle to receive notifications.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        IProcessHandleResult Register(IntPtr windowHandleToReceiveNotifications);

        /// <summary>
        /// The un register for device notifications.
        /// </summary>
        /// <param name="handleFromRegistration">
        /// The handle from registration.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        IProcessHandleResult UnRegister(IntPtr handleFromRegistration);
    }
}