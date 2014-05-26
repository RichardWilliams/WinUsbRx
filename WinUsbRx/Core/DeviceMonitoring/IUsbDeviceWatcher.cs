// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDeviceWatcher.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The UsbDeviceWatcher interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using Handle;
    using UsbDeviceNotifications;

    /// <summary>
    /// The UsbDeviceWatcher interface.
    /// </summary>
    internal interface IUsbDeviceWatcher
    {
        /// <summary>
        /// Gets the guid of the device to register for notifications.
        /// </summary>
        Guid DeviceGuid { get; }

        /// <summary>
        /// Gets the handle used when we have registered for notifications on a device.
        /// </summary>
        CreatedHandle RegisteredCreatedHandle { get; }

        /// <summary>
        /// The assign handle.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        void AssignHandle(IntPtr handle);

        /// <summary>
        /// The release handle.
        /// </summary>
        void ReleaseHandle();

        /// <summary>
        /// This will start the process of watching for devices.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        IObservable<IUsbDeviceNotification> AllDeviceNotifications();

        /// <summary>
        /// The attached devices.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        IObservable<IUsbDeviceNotification> ArrivedDeviceNotificationsOnly();
    }
}