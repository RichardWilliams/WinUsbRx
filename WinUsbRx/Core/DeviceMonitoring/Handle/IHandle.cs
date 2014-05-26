// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandle.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The Handle interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;

    /// <summary>
    /// The Handle interface.
    /// </summary>
    internal interface IHandle
    {
        /// <summary>
        /// Gets the handle.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        /// Processes what is needed to be done for this NativeWindow, 
        /// i.e. assignHandle if creating a new handle or ReleaseHandle for destroying a handle.
        /// </summary>
        /// <param name="usbDeviceWatcher">
        /// The usb Device Watcher.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        IProcessHandleResult ProcessFor(IUsbDeviceWatcher usbDeviceWatcher);
    }
}