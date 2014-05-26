// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInterfaceData.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the SpDeviceInterfaceData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The device interface data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal class DeviceInterfaceData
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
        /// The _flags.
        /// </summary>
        private int _flags;

        /// <summary>
        /// The _reserved.
        /// </summary>
        private IntPtr _reserved;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInterfaceData"/> class. 
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        public DeviceInterfaceData(Guid guid)
        {
            _guid = guid;
            _size = Marshal.SizeOf(typeof(DeviceInterfaceData));
            _flags = 0;
            _reserved = IntPtr.Zero;
        }
    }
}