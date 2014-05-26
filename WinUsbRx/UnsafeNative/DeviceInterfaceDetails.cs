//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="DeviceInterfaceDetails.cs" company="None.">
////   TODO:
//// </copyright>
//// <summary>
////   The device interface details.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.UnsafeNative
//{
//    using System;
//    using System.Runtime.InteropServices;
//    using DeviceProperties;
//    using New;

//    /// <summary>
//    /// The device interface details.
//    /// </summary>
//    internal class DeviceInterfaceDetails : IDeviceInterfaceDetails
//    {
//        /// <summary>
//        /// Initializes a new instance of the <see cref="DeviceInterfaceDetails"/> class.
//        /// </summary>
//        /// <param name="devicePropertyFactory">
//        /// The device Property Factory.
//        /// </param>
//        /// <param name="unsafeNativeMethodsWrapper">
//        /// The unsafe Native Methods Wrapper.
//        /// </param>
//        /// <param name="detailDataBuffer">
//        /// The detail data buffer.
//        /// </param>
//        /// <param name="deviceInfoSet">
//        /// The device info set.
//        /// </param>
//        /// <param name="deviceInfoData">
//        /// The device info data.
//        /// </param>
//        public DeviceInterfaceDetails(
//            IDevicePropertyFactory devicePropertyFactory,
//            IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper, 
//            DetailDataBuffer detailDataBuffer, 
//            IntPtr deviceInfoSet, 
//            DeviceInfoData deviceInfoData)
//        {
//            // TODO: Must make sure to get all details from detaildatabuffer, DeviceInfoSet here in constructor 
//            // TODO: as it could be wiped earlier than the lifetime of this class. Unless we make a copy of it here.
//            var devicePathName = new IntPtr(detailDataBuffer.Pointer.ToInt64() + 4);
//            PathName = Marshal.PtrToStringUni(devicePathName);
//            DeviceDescription = unsafeNativeMethodsWrapper.GetProperty(devicePropertyFactory, deviceInfoSet, deviceInfoData, Spdrp.DeviceDesc);
//            Manufacturer = unsafeNativeMethodsWrapper.GetProperty(devicePropertyFactory, deviceInfoSet, deviceInfoData, Spdrp.Mfg);
//            //var hardwareIDs = GetMultiStringProperty(deviceInfoSet, deviceInfoData, SPDRP.SPDRP_HARDWAREID);

//            //var regex = new Regex("^USB\\\\VID_([0-9A-F]{4})&PID_([0-9A-F]{4})", RegexOptions.IgnoreCase);
//            //var foundVidPid = false;
//            //foreach (var hardwareID in hardwareIDs)
//            //{
//            //    var match = regex.Match(hardwareID);
//            //    if (match.Success)
//            //    {
//            //        details.VID = ushort.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
//            //        details.PID = ushort.Parse(match.Groups[2].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
//            //        foundVidPid = true;
//            //        break;
//            //    }
//            //}

//            //if (!foundVidPid)
//            //    throw new APIException("Failed to find VID and PID for USB device. No hardware ID could be parsed.");

//        }

//        /// <summary>
//        /// Gets the manufacturer.
//        /// </summary>
//        public BaseDeviceProperty Manufacturer { get; private set; }

//        /// <summary>
//        /// Gets the device description.
//        /// </summary>
//        public BaseDeviceProperty DeviceDescription { get; private set; }

//        /// <summary>
//        /// Gets the path name.
//        /// </summary>
//        public string PathName { get; private set; }
//    }
//}