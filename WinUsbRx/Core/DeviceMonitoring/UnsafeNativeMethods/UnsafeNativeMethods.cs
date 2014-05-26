// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnsafeNativeMethods.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UnsafeNativeMethods type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UnsafeNativeMethods
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The unsafe native methods.
    /// </summary>
    internal static class UnsafeNativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, uint flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr UnRegisterDeviceNotification(IntPtr handle);        
    }
}