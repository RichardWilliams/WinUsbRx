// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestroyedHandle.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DestroyedHandle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;
    using Wrappers;

    /// <summary>
    /// The destroyed handle.
    /// </summary>
    internal class DestroyedHandle : IHandle
    {
        /// <summary>
        /// The _device management.
        /// </summary>
        private readonly IDeviceNotifications _deviceNotifications;

        /// <summary>
        /// Initializes a new instance of the <see cref="DestroyedHandle"/> class.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <param name="deviceNotifications">
        /// The device Management.
        /// </param>
        public DestroyedHandle(IntPtr handle, IDeviceNotifications deviceNotifications)
        {
            _deviceNotifications = deviceNotifications;
            Handle = handle;
        }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// This will unregister the device from being watched.
        /// </summary>
        /// <param name="usbDeviceWatcher">
        /// The usb device watcher.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        public IProcessHandleResult ProcessFor(IUsbDeviceWatcher usbDeviceWatcher)
        {
            var registeredCreatedHandle = usbDeviceWatcher.RegisteredCreatedHandle;
            if (registeredCreatedHandle == null)
            {
                return new ProcessHandleResult(Handle, new MarshalWrapper());
            }

            var processHandleResult = _deviceNotifications.UnRegister(registeredCreatedHandle.DeviceNotificationHandle);
            usbDeviceWatcher.ReleaseHandle();

            return processHandleResult;
        }
    }
}