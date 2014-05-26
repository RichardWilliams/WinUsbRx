// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessHandleResult.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the ProcessHandleResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;
    using UsbDeviceNotifications;
    using Wrappers;

    /// <summary>
    /// The device notification handle.
    /// </summary>
    internal class ProcessHandleResult : IProcessHandleResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessHandleResult"/> class. 
        /// </summary>
        /// <param name="handle">
        /// The device notification handle.
        /// </param>
        /// <param name="marshalWrapper">
        /// The marshal static class wrapper.
        /// </param>
        public ProcessHandleResult(IntPtr handle, IMarshalWrapper marshalWrapper)
        {
            Handle = handle;
            Win32Error = new Win32ErrorWrapper(0);
            if (handle != IntPtr.Zero)
            {
                return;
            }

            Win32Error = marshalWrapper.GetLastWin32Error();
        }

        /// <summary>
        /// Gets the error exception.
        /// </summary>
        public Win32ErrorWrapper Win32Error { get; private set; }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// This will test if the exception has an error code that is not 0 (0 is success), if it does then it sends the observer an OnError.
        /// </summary>
        /// <param name="observer">
        /// The observer.
        /// </param>
        public void SuccessTest(IObserver<IUsbDeviceNotification> observer)
        {
            if (!Win32Error.IsSuccess)
            {
                observer.OnError(Win32Error.Exception);
            }
        }
    }
}