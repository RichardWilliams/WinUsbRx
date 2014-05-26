//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="UsbDevices.cs" company="None.">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the UsbDevices type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.Core
//{
//    using System;
//    using System.Reactive.Linq;
//    using DeviceManagement;

//    /// <summary>
//    /// The usb devices.
//    /// </summary>
//    internal class UsbDevices : IUsbDevices
//    {
//        /// <summary>
//        /// The _guid.
//        /// </summary>
//        private readonly Guid _guid;

//        /// <summary>
//        /// The _usb device watcher.
//        /// </summary>
//        private readonly IUsbDeviceWatcher _usbDeviceWatcher;

//        /// <summary>
//        /// The _device management for interface details.
//        /// </summary>
//        private readonly IDeviceManagementForInterfaceDetails _deviceManagementForInterfaceDetails;

//        /// <summary>
//        /// The _usb device factory.
//        /// </summary>
//        private readonly IUsbDeviceFactory _usbDeviceFactory;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="UsbDevices"/> class.
//        /// </summary>
//        /// <param name="guid">
//        /// The guid.
//        /// </param>
//        /// <param name="usbDeviceWatcher">
//        /// The usb device watcher.
//        /// </param>
//        /// <param name="deviceManagementForInterfaceDetails">
//        /// The device management for interface details.
//        /// </param>
//        /// <param name="usbDeviceFactory">
//        /// The usb Device Factory.
//        /// </param>
//        public UsbDevices(Guid guid, IUsbDeviceWatcher usbDeviceWatcher, IDeviceManagementForInterfaceDetails deviceManagementForInterfaceDetails, IUsbDeviceFactory usbDeviceFactory)
//        {
//            _guid = guid;
//            _usbDeviceWatcher = usbDeviceWatcher;
//            _deviceManagementForInterfaceDetails = deviceManagementForInterfaceDetails;
//            _usbDeviceFactory = usbDeviceFactory;
//        }

//        /// <summary>
//        /// The devices.
//        /// </summary>
//        /// <returns>
//        /// The <see>
//        ///         <cref>IObservable</cref>
//        ///     </see>
//        ///     .
//        /// </returns>
//        public IObservable<IUsbDevice> Devices()
//        {
//            return Observable.Create<IUsbDevice>(observer =>
//            {
//                var usbWatcherObservable = _usbDeviceWatcher.ArrivedDeviceNotificationsOnly();
                
//                return usbWatcherObservable
//                    .Select(notification => notification.Name)
//                    .Merge(_deviceManagementForInterfaceDetails.GetConnectedUsbDevices(_guid).Select(deviceInterfaceDetails => deviceInterfaceDetails.PathName))
//                    .Distinct(pathName => pathName)
//                    .Select(path => _usbDeviceFactory.Create(path))
//                    .Subscribe(observer);
//            });
//        }
//    }
//}