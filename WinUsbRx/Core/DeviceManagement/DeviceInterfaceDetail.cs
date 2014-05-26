// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInterfaceDetail.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceInterfaceDetail type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    using System;
    using System.Runtime.InteropServices;
    using Factory;
    using UnsafeNative;
    using Wrappers;

    /// <summary>
    /// The device interface detail.
    /// </summary>
    internal class DeviceInterfaceDetail : IDeviceInterfaceDetail, IDisposable
    {
        /// <summary>
        /// The _device information set handle.
        /// </summary>
        private readonly IntPtr _deviceInformationSetHandle;

        /// <summary>
        /// The _marshal wrapper.
        /// </summary>
        private readonly IMarshalWrapper _marshalWrapper;

        /// <summary>
        /// The _unsafe native methods wrapper.
        /// </summary>
        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

        /// <summary>
        /// The _device path.
        /// </summary>
        private string _devicePath;

        /// <summary>
        /// The _description.
        /// </summary>
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInterfaceDetail"/> class.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device Information Set Handle.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="marshalWrapper">
        /// The marshal Wrapper.
        /// </param>
        /// <param name="deviceInfoDataFactory">
        /// The device Info Data Factory.
        /// </param>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe Native Methods Wrapper.
        /// </param>
        public DeviceInterfaceDetail(
            IntPtr deviceInformationSetHandle, 
            int size, 
            IMarshalWrapper marshalWrapper, 
            IDeviceInfoDataFactory deviceInfoDataFactory, 
            IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper)
        {
            _deviceInformationSetHandle = deviceInformationSetHandle;
            DeviceInfoData = deviceInfoDataFactory.Create();
            _marshalWrapper = marshalWrapper;
            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
            DeviceInterfaceDetailBuffer = Marshal.AllocHGlobal(size);
            _marshalWrapper.WriteInteger32(DeviceInterfaceDetailBuffer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);
        }

        /// <summary>
        /// Gets the device info data.
        /// </summary>
        public DeviceInfoData DeviceInfoData { get; private set; }

        /// <summary>
        /// Gets the device interface detail buffer.
        /// </summary>
        public IntPtr DeviceInterfaceDetailBuffer { get; private set; }

        /// <summary>
        /// Gets the device path.
        /// </summary>
        public string DevicePath
        {
            get
            {
                if (string.IsNullOrEmpty(_devicePath))
                {
                    var devicePathName = new IntPtr(DeviceInterfaceDetailBuffer.ToInt64() + sizeof(int));
                    _devicePath = Marshal.PtrToStringAuto(devicePathName);
                    return _devicePath;
                }

                return _devicePath;
            }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    _description = _unsafeNativeMethodsWrapper.GetProperty(_deviceInformationSetHandle, DeviceInfoData, Spdrp.DeviceDesc).ToString();                    
                }

                return _description;
            }
        }
            //Manufacturer = unsafeNativeMethodsWrapper.GetProperty(devicePropertyFactory, deviceInfoSet, deviceInfoData, Spdrp.Mfg);
            //var hardwareIDs = GetMultiStringProperty(deviceInfoSet, deviceInfoData, SPDRP.SPDRP_HARDWAREID);

            //var regex = new Regex("^USB\\\\VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
            //var foundVidPid = false;
            //foreach (var hardwareID in hardwareIDs)
            //{
            //    var match = regex.Match(hardwareID);
            //    if (match.Success)
            //    {
            //        details.VID = ushort.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
            //        details.PID = ushort.Parse(match.Groups[2].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
            //        foundVidPid = true;
            //        break;
            //    }
            //}

            //if (!foundVidPid)
            //    throw new APIException("Failed to find VID and PID for USB device. No hardware ID could be parsed.");
        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            if (DeviceInterfaceDetailBuffer != IntPtr.Zero)
            {
                _marshalWrapper.FreeHGlobal(DeviceInterfaceDetailBuffer);
            }
        }
    }
}