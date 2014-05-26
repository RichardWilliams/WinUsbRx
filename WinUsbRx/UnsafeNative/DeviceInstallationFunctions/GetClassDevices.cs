//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="GetClassDevices.cs" company="None.">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the GetClassDevices type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.UnsafeNative.DeviceInstallationFunctions
//{
//    using System;
//    using System.ComponentModel;
//    using Wrappers;

//    /// <summary>
//    /// The get class devices.
//    /// </summary>
//    internal class GetClassDevices : IGetClassDevices
//    {
//        /// <summary>
//        /// The unsafe native methods wrapper.
//        /// </summary>
//        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="GetClassDevices"/> class.
//        /// </summary>
//        /// <param name="guid">
//        /// The guid.
//        /// </param>
//        /// <param name="unsafeNativeMethodsWrapper">
//        /// The unsafe native methods wrapper.
//        /// </param>
//        /// <param name="marshalWrapper">
//        /// The marshal wrapper.
//        /// </param>
//        /// <exception cref="Win32Exception">
//        /// Will throw a Win32Exception when fails to get class devices.
//        /// </exception>
//        public GetClassDevices(Guid guid, IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper, IMarshalWrapper marshalWrapper)
//        {
//            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
//            HandleToDeviceInformationSet = _unsafeNativeMethodsWrapper.SetupDiGetClassDevices(guid, IntPtr.Zero, IntPtr.Zero, DigcfDeviceInterface | DigcfPresent);

//            if (HandleToDeviceInformationSet == InvalidHandle)
//            {
//                throw marshalWrapper.GetLastWin32ErrorAsException();
//            }
//        }

//        /// <summary>
//        /// Gets the handle to device information set.
//        /// </summary>
//        public IntPtr HandleToDeviceInformationSet { get; private set; }

//        /// <summary>
//        /// Gets the digcf device interface.
//        /// </summary>
//        private int DigcfDeviceInterface
//        {
//            get { return 0x10; }
//        }

//        /// <summary>
//        /// Gets the digcf present.
//        /// </summary>
//        private int DigcfPresent
//        {
//            get { return 0x2; }
//        }

//        /// <summary>
//        /// Gets the invalid handle.
//        /// </summary>
//        private IntPtr InvalidHandle
//        {
//            get { return new IntPtr(-1); }
//        }

//        /// <summary>
//        /// The dispose, this will free up any memory allocated by the setup get class devices.
//        /// </summary>
//        public void Dispose()
//        {
//            _unsafeNativeMethodsWrapper.SetupDiDestroyDeviceInfoList(HandleToDeviceInformationSet);
//        }
//    }
//}