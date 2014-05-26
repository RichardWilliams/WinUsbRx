// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BroadcastHdr.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The DEV_BROADCAST_HDR used in sending information to .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace WinUsbRx.Core.DeviceMonitoring
{
    /// <summary>
    /// The DEV_BROADCAST_HDR used in sending information to .
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class BroadcastHdr
    {
        /// <summary>
        /// The _size.
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// The _device type.
        /// </summary>
        private readonly uint _deviceType;

        /// <summary>
        /// The _reserved.
        /// </summary>
        private readonly uint _reserved;

        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastHdr"/> class. 
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="deviceType">
        /// The device Type.
        /// </param>
        public BroadcastHdr(int size, uint deviceType)
        {
            _size = size;
            _deviceType = deviceType;
            _reserved = 0;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size
        {
            get { return _size; }
        }
    }
}