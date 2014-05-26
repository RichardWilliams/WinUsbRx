// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInformationSet.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceInformationSet type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    using System;
    using System.Collections.Generic;
    using UnsafeNative;

    /// <summary>
    /// The device information set.
    /// </summary>
    internal class DeviceInformationSet : IDisposable
    {
        /// <summary>
        /// The _unsafe native methods wrapper.
        /// </summary>
        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInformationSet"/> class.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe native methods wrapper.
        /// </param>
        public DeviceInformationSet(Guid guid, IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper)
        {
            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
            Handle = _unsafeNativeMethodsWrapper.GetDeviceInformationSet(guid, IntPtr.Zero, IntPtr.Zero, (uint)(DeviceInformationGetClassFlags.DeviceInterface | DeviceInformationGetClassFlags.Present));
            DeviceInformationElements = _unsafeNativeMethodsWrapper.GetDeviceInformationElements(Handle);
        }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the device information elements.
        /// </summary>
        public IEnumerable<DeviceInformationElement> DeviceInformationElements { get; private set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                _unsafeNativeMethodsWrapper.SetupDiDestroyDeviceInfoList(Handle);                
            }
        }
    }
}