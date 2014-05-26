// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProcessHandleResult.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IProcessHandleResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;
    using UsbDeviceNotifications;
    using Wrappers;

    /// <summary>
    /// The ProcessHandleResult interface.
    /// </summary>
    internal interface IProcessHandleResult
    {
        /// <summary>
        /// Gets the error exception.
        /// </summary>
        Win32ErrorWrapper Win32Error { get; }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>
        /// The success test.
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        void SuccessTest(IObserver<IUsbDeviceNotification> observer);
    }
}