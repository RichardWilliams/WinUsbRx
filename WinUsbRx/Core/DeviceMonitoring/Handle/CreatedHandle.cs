// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreatedHandle.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The created handle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;

    /// <summary>
    /// The created handle.
    /// </summary>
    internal class CreatedHandle : IHandle
    {
        /// <summary>
        /// The device management, this keeps all the methods for the management of the device.
        /// </summary>
        private readonly IDeviceNotifications _deviceNotifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedHandle"/> class.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <param name="deviceNotifications">
        /// The device Management.
        /// </param>
        public CreatedHandle(IntPtr handle, IDeviceNotifications deviceNotifications)
        {
            _deviceNotifications = deviceNotifications;
            Handle = handle;
        }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the device notification handle, this is set when the this CreateHandle has been registered fir device notifications.
        /// </summary>
        public virtual IntPtr DeviceNotificationHandle { get; private set; }

        /// <summary>
        /// This will AssignHandle to nativeWindow and register for device notifications.
        /// </summary>
        /// <param name="usbDeviceWatcher">
        /// The usb Device Watcher.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        public IProcessHandleResult ProcessFor(IUsbDeviceWatcher usbDeviceWatcher)
        {
            usbDeviceWatcher.AssignHandle(Handle);
            var processHandleResult = _deviceNotifications.Register(Handle);
            DeviceNotificationHandle = processHandleResult.Handle;
            return processHandleResult;
        }
    }
}