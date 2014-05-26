// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BroadcastDeviceInterface.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The dev broadcast device interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The DEV_BROADCAST_DEVICEINTERFACE.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class BroadcastDeviceInterface
    {
        /// <summary>
        /// The DEV_BROADCAST_HDR.
        /// </summary>
        private readonly BroadcastHdr _broadCastHdr;

        /// <summary>
        /// The _guid.
        /// </summary>
        private Guid _guid;

        /// <summary>
        /// The _name.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)] 
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastDeviceInterface"/> class.
        /// NOTE:This is used when using PointerToStructure, it needs the default constructor.
        /// </summary>
        public BroadcastDeviceInterface()
        {
            _broadCastHdr = new BroadcastHdr(Marshal.SizeOf(typeof(BroadcastDeviceInterface)), DeviceManagement.UnsafeNative.UnsafeNativeMethods.DbtDevtypeDeviceInterface);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BroadcastDeviceInterface"/> class. 
        /// NOTE: Both these constructors are required, this is called from the factory, the most complex constructor is used from the factory, it will inject in the guid.
        /// </summary>
        /// <param name="guid">
        /// The guid of the device.
        /// </param>
        public BroadcastDeviceInterface(Guid guid)
        {
            _broadCastHdr = new BroadcastHdr(Marshal.SizeOf(typeof(BroadcastDeviceInterface)), DeviceManagement.UnsafeNative.UnsafeNativeMethods.DbtDevtypeDeviceInterface);
            _guid = guid;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size
        {
            get { return _broadCastHdr.Size; }
        }

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
            internal set { _guid = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            internal set { _name = value; }
        }
    }
}