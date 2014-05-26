// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbFileHandle.cs" company="NONE">
//   TODO:
// </copyright>
// <summary>
//   The usb file handle.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using WinUsbRx.Core.DeviceManagement.UnsafeNative;

namespace WinUsbRx.UnsafeNative
{
    using System;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// The usb file handle.
    /// </summary>
    internal class UsbFileHandle : IDisposable
    {
        /// <summary>
        /// The _unsafe native methods wrapper.
        /// </summary>
        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

        /// <summary>
        /// The _safe file handle.
        /// </summary>
        private readonly SafeFileHandle _safeFileHandle;

        /// <summary>
        /// The _win usb handle.
        /// </summary>
        private readonly IntPtr _winUsbHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsbFileHandle"/> class.
        /// </summary>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe native methods wrapper.
        /// </param>
        /// <param name="safeFileHandle">
        /// The safe file handle.
        /// </param>
        /// <param name="winUsbHandle">
        /// The win usb handle.
        /// </param>
        public UsbFileHandle(IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper, SafeFileHandle safeFileHandle, IntPtr winUsbHandle)
        {
            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
            _safeFileHandle = safeFileHandle;
            _winUsbHandle = winUsbHandle;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            _unsafeNativeMethodsWrapper.WinUsb_Free(_winUsbHandle);
            _safeFileHandle.Dispose();
        }
    }
}