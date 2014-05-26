// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnsafeNativeMethodsWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUnsafeNativeMethodsWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    using System;
    using System.Collections.Generic;
    using DeviceProperties;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// The UnsafeNativeMethodsWrapper interface.
    /// </summary>
    internal interface IUnsafeNativeMethodsWrapper
    {
        /// <summary>
        /// The setup di destroy device info list.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// The device info set.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        /// <summary>
        /// The setup di get class devices.
        /// </summary>
        /// <param name="classGuid">
        /// The class guid.
        /// </param>
        /// <param name="enumerator">
        /// The enumerator.
        /// </param>
        /// <param name="parentHandle">
        /// The parent Handle.
        /// </param>
        /// <param name="flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        IntPtr GetDeviceInformationSet(Guid classGuid, IntPtr enumerator, IntPtr parentHandle, uint flags);

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
        IEnumerable<DeviceInformationElement> GetDeviceInformationElements(IntPtr deviceInformationSetHandle);

        /// <summary>
        /// The get device interfaces.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <param name="deviceInfoData">
        /// The device info data.
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
        IEnumerable<DeviceInterface> GetDeviceInterfaces(IntPtr deviceInformationSetHandle, DeviceInfoData deviceInfoData, Guid guid);

        /// <summary>
        /// The get device interface detail.
        /// </summary>
        /// <param name="deviceInformationSetHandle">
        /// The device information set handle.
        /// </param>
        /// <param name="deviceInterfaceData">
        /// The device Interface Data.
        /// </param>
        /// <returns>
        /// The <see cref="IDeviceInterfaceDetail"/>.
        /// </returns>
        IDeviceInterfaceDetail GetDeviceInterfaceDetail(IntPtr deviceInformationSetHandle, DeviceInterfaceData deviceInterfaceData);

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
        /// The spdrp device description.
        /// </param>
        /// <returns>
        /// The <see cref="BaseDeviceProperty"/>.
        /// </returns>
        BaseDeviceProperty GetProperty(IntPtr deviceInfoSet, DeviceInfoData deviceInfoData, Spdrp spdrpDeviceDescription);

        /// <summary>
        /// The create file.
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <param name="desiredAccess">
        /// The desired Access.
        /// </param>
        /// <param name="shareMode">
        /// The share Mode.
        /// </param>
        /// <param name="securityAttributes">
        /// The security Attributes.
        /// </param>
        /// <param name="creationDisposition">
        /// The creation Disposition.
        /// </param>
        /// <param name="flagsAndAttributes">
        /// The flags And Attributes.
        /// </param>
        /// <param name="templateFile">
        /// The template File.
        /// </param>
        /// <returns>
        /// The <see cref="SafeFileHandle"/>.
        /// </returns>
        //UsbFileHandle CreateFileAndInitialise(string fileName, uint desiredAccess, int shareMode, IntPtr securityAttributes, int creationDisposition, int flagsAndAttributes, int templateFile);

        /// <summary>
        /// The win usb_ free.
        /// </summary>
        /// <param name="interfaceHandle">
        /// The interface handle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool WinUsb_Free(IntPtr interfaceHandle);
    }
}