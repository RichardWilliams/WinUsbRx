// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInfoData.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The device info data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The device info data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class DeviceInfoData
    {
        /// <summary>
        /// The _size.
        /// </summary>
        private int _size;

        /// <summary>
        /// The _guid.
        /// </summary>
        private Guid _guid;

        /// <summary>
        /// The _device instance.
        /// </summary>
        private int _deviceInstance;

        /// <summary>
        /// The _reserved.
        /// </summary>
        private IntPtr _reserved;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfoData"/> class.
        /// </summary>
        public DeviceInfoData()
        {
            _size = Marshal.SizeOf(typeof(DeviceInfoData));
            _reserved = IntPtr.Zero;
        }
    }
}