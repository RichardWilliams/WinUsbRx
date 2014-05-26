// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnsafeNativeMethodsWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UnsafeNativeMethodsWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using DeviceProperties;
    using Factory;
    using Microsoft.Win32.SafeHandles;
    using WinUsbRx.UnsafeNative;
    using Wrappers;

    // from pinvoke.net

    /// <summary>
    /// The unsafe native methods wrapper.
    /// </summary>
    internal class UnsafeNativeMethodsWrapper : IUnsafeNativeMethodsWrapper
    {
        /// <summary>
        /// The invalid handle.
        /// </summary>
        private readonly IntPtr _invalidHandle = new IntPtr(-1);

        /// <summary>
        /// The marshal wrapper.
        /// </summary>
        private readonly IMarshalWrapper _marshalWrapper;

        /// <summary>
        /// The _device information element factory.
        /// </summary>
        private readonly IDeviceInformationElementFactory _deviceInformationElementFactory;

        /// <summary>
        /// The _device info data factory.
        /// </summary>
        private readonly IDeviceInfoDataFactory _deviceInfoDataFactory;

        /// <summary>
        /// The _device interface data factory.
        /// </summary>
        private readonly IDeviceInterfaceDataFactory _deviceInterfaceDataFactory;

        /// <summary>
        /// The _device interface factory.
        /// </summary>
        private readonly IDeviceInterfaceFactory _deviceInterfaceFactory;

        /// <summary>
        /// The _device interface detail factory.
        /// </summary>
        private readonly IDeviceInterfaceDetailFactory _deviceInterfaceDetailFactory;

        /// <summary>
        /// The _device property factory.
        /// </summary>
        private readonly IDevicePropertyFactory _devicePropertyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsafeNativeMethodsWrapper"/> class.
        /// </summary>
        /// <param name="marshalWrapper">
        /// The marshal wrapper.
        /// </param>
        /// <param name="deviceInformationElementFactory">
        /// The device Information Element Factory.
        /// </param>
        /// <param name="deviceInfoDataFactory">
        /// The device Info Data Factory.
        /// </param>
        /// <param name="deviceInterfaceDataFactory">
        /// The device Interface Data Factory.
        /// </param>
        /// <param name="deviceInterfaceFactory">
        /// The device Interface Factory.
        /// </param>
        /// <param name="deviceInterfaceDetailFactory">
        /// The device Interface Detail Factory.
        /// </param>
        /// <param name="devicePropertyFactory">
        /// The device Property Factory.
        /// </param>
        public UnsafeNativeMethodsWrapper(
            IMarshalWrapper marshalWrapper,
            IDeviceInformationElementFactory deviceInformationElementFactory, 
            IDeviceInfoDataFactory deviceInfoDataFactory, 
            IDeviceInterfaceDataFactory deviceInterfaceDataFactory,
            IDeviceInterfaceFactory deviceInterfaceFactory,
            IDeviceInterfaceDetailFactory deviceInterfaceDetailFactory,
            IDevicePropertyFactory devicePropertyFactory)
        {
            _marshalWrapper = marshalWrapper;
            _deviceInformationElementFactory = deviceInformationElementFactory;
            _deviceInfoDataFactory = deviceInfoDataFactory;
            _deviceInterfaceDataFactory = deviceInterfaceDataFactory;
            _deviceInterfaceFactory = deviceInterfaceFactory;
            _deviceInterfaceDetailFactory = deviceInterfaceDetailFactory;
            _devicePropertyFactory = devicePropertyFactory;
        }

        /// <summary>
        /// The setup di destroy device info list, this calls into the unsafe methods that are pinvoked.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// The device info set.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet)
        {
            return UnsafeNativeMethods.SetupDiDestroyDeviceInfoList(deviceInfoSet);
        }

        /// <summary>
        /// The setup di get class devices, this calls into the unsafe methods that are pinvoked.
        /// </summary>
        /// <param name="classGuid">
        /// The class guid.
        /// </param>
        /// <param name="enumerator">
        /// The enumerator.
        /// </param>
        /// <param name="parentHandle">
        /// The parent handle.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        public IntPtr GetDeviceInformationSet(Guid classGuid, IntPtr enumerator, IntPtr parentHandle, uint flags)
        {
            return UnsafeNativeMethods.SetupDiGetClassDevs(ref classGuid, enumerator, parentHandle, flags);
        }

        /// <summary>
        /// The get device information elements.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IEnumerable<DeviceInformationElement> GetDeviceInformationElements(IntPtr deviceInformationSetHandle)
        {
            var memberIndex = 0U;
            bool success;

            if (IsHandleInvalid(deviceInformationSetHandle))
            {
                yield break;
            }

            do
            {
                var deviceInfoData = _deviceInfoDataFactory.Create();
                success = UnsafeNativeMethods.SetupDiEnumDeviceInfo(deviceInformationSetHandle, memberIndex++, deviceInfoData);
                if (success)
                {
                    yield return _deviceInformationElementFactory.Create(deviceInformationSetHandle, deviceInfoData);
                }
            } 
            while (success);
        }

        /// <summary>
        /// The get device interfaces.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <param name="deviceInfoData">
        /// The device Info Data.
        /// </param>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IEnumerable<DeviceInterface> GetDeviceInterfaces(IntPtr deviceInformationSetHandle, DeviceInfoData deviceInfoData, Guid guid)
        {
            var memberIndex = 0U;
            bool success;

            if (IsHandleInvalid(deviceInformationSetHandle))
            {
                yield break;
            }

            do
            {
                var deviceInterfaceData = _deviceInterfaceDataFactory.Create();
                success = UnsafeNativeMethods.SetupDiEnumDeviceInterfaces(deviceInformationSetHandle, deviceInfoData, ref guid, memberIndex++, deviceInterfaceData);
                if (success)
                {
                    yield return _deviceInterfaceFactory.Create(deviceInformationSetHandle, deviceInterfaceData);
                }
            }
            while (success);
        }

        /// <summary>
        /// The get device interface detail.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <param name="deviceInterfaceData">
        /// The device interface data.
        /// </param>
        /// <returns>
        /// The <see cref="DeviceInterfaceDetail"/>.
        /// </returns>
        public IDeviceInterfaceDetail GetDeviceInterfaceDetail(IntPtr deviceInformationSetHandle, DeviceInterfaceData deviceInterfaceData)
        {
            var bufferSize = 0;
            var success = UnsafeNativeMethods.SetupDiGetDeviceInterfaceDetail(deviceInformationSetHandle, deviceInterfaceData, IntPtr.Zero, 0, ref bufferSize, IntPtr.Zero);
            var lastError = _marshalWrapper.GetLastWin32Error();
            
            if (!success && lastError.IsInsufficientBuffer)
            {
                // TODO: Somehow we need to get rid of setting up properties of the deviceInterfacedetails and use them either direct and pass them into the constructor or think of another way.
                var deviceInterfaceDetails = _deviceInterfaceDetailFactory.Create(deviceInformationSetHandle, bufferSize);
                success = UnsafeNativeMethods.SetupDiGetDeviceInterfaceDetail(
                    deviceInformationSetHandle,
                    deviceInterfaceData, 
                    deviceInterfaceDetails.DeviceInterfaceDetailBuffer, 
                    bufferSize, 
                    ref bufferSize,
                    deviceInterfaceDetails.DeviceInfoData);

                return !success ? _deviceInterfaceDetailFactory.CreateNull() : (IDeviceInterfaceDetail)deviceInterfaceDetails;
            }

            return _deviceInterfaceDetailFactory.CreateNull();
        }

        /// <summary>
        /// The setup di get device interface detail.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="Win32Exception">
        /// Is thrown when the unsafe method calls fails.
        /// </exception>
        /// <summary>
        /// The get property.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// The device info set.
        /// </param>
        /// <param name="deviceInfoData">
        /// The device info data.
        /// </param>
        /// <param name="spdrpDeviceDescription">
        /// The spdrp Device Description.
        /// </param>
        /// <returns>
        /// The <see cref="BaseDeviceProperty"/>.
        /// </returns>
        public BaseDeviceProperty GetProperty(IntPtr deviceInfoSet, DeviceInfoData deviceInfoData, Spdrp spdrpDeviceDescription)
        {
            uint requiredSize;
            var success = UnsafeNativeMethods.SetupDiGetDeviceRegistryProperty(deviceInfoSet, deviceInfoData, spdrpDeviceDescription, IntPtr.Zero, IntPtr.Zero, 0, out requiredSize);
            var lastError = _marshalWrapper.GetLastWin32Error();
            if (!success && lastError.IsInsufficientBuffer)
            {
                var buffer = new byte[requiredSize];

                RegistryType regType;
                success = UnsafeNativeMethods.SetupDiGetDeviceRegistryProperty(deviceInfoSet, deviceInfoData, spdrpDeviceDescription, out regType, buffer, (uint)buffer.Length, out requiredSize);
                if (success)
                {
                    return _devicePropertyFactory.Create(regType, buffer);
                }
            }

            return _devicePropertyFactory.Create(RegistryType.Unknown, null);
        }

        /// <summary>
        /// The create file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="desiredAccess">
        /// The desired access.
        /// </param>
        /// <param name="shareMode">
        /// The share mode.
        /// </param>
        /// <param name="securityAttributes">
        /// The security attributes.
        /// </param>
        /// <param name="creationDisposition">
        /// The creation disposition.
        /// </param>
        /// <param name="flagsAndAttributes">
        /// The flags and attributes.
        /// </param>
        /// <param name="templateFile">
        /// The template file.
        /// </param>
        /// <returns>
        /// The <see cref="SafeFileHandle"/>.
        /// </returns>
        //public UsbFileHandle CreateFileAndInitialise(string fileName, uint desiredAccess, int shareMode, IntPtr securityAttributes, int creationDisposition, int flagsAndAttributes, int templateFile)
        //{
        //    SafeFileHandle safeFileHandle = null;
        //    var interfaceHandle = IntPtr.Zero;

        //    try
        //    {
        //        safeFileHandle = UnsafeNativeMethods.CreateFile(fileName, desiredAccess, shareMode, securityAttributes, creationDisposition, flagsAndAttributes, templateFile);
        //        if (safeFileHandle.IsInvalid)
        //        {
        //            throw _marshalWrapper.GetLastWin32ErrorAsException();
        //        }

        //        if (!WinUsb_Initialize(safeFileHandle, ref interfaceHandle))
        //        {
        //            throw _marshalWrapper.GetLastWin32ErrorAsException();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        if (safeFileHandle != null && !safeFileHandle.IsInvalid)
        //        {
        //            safeFileHandle.Dispose();
        //        }

        //        if (interfaceHandle != IntPtr.Zero)
        //        {
        //            WinUsb_Free(interfaceHandle);
        //        }

        //        throw;
        //    }

        //    return new UsbFileHandle(this, safeFileHandle, interfaceHandle);
        //}

        /// <summary>
        /// The win usb_ free.
        /// </summary>
        /// <param name="interfaceHandle">
        /// The interface handle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool WinUsb_Free(IntPtr interfaceHandle)
        {
            return UnsafeNativeMethods.WinUsb_Free(interfaceHandle);
        }

        /// <summary>
        /// The win usb_ initialize.
        /// </summary>
        /// <param name="deviceHandle">
        /// The device handle.
        /// </param>
        /// <param name="interfaceHandle">
        /// The interface handle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool WinUsb_Initialize(SafeFileHandle deviceHandle, ref IntPtr interfaceHandle)
        {
            return UnsafeNativeMethods.WinUsb_Initialize(deviceHandle, ref interfaceHandle);
        }

        /// <summary>
        /// The is handle invalid.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsHandleInvalid(IntPtr handle)
        {
            return handle == _invalidHandle;
        }
    }
}