//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="DeviceManagementForInterfaceDetails.cs" company="None">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the DeviceManagementForInterfaceDetails type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.Core.DeviceManagement
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using UnsafeNative;
//    using UnsafeNative.DeviceInstallationFunctions;
//    using UnsafeNative.Structs;
//    using Wrappers;

//    /// <summary>
//    /// The device management for interface details.
//    /// </summary>
//    internal class DeviceManagementForInterfaceDetails : IDeviceManagementForInterfaceDetails
//    {
//        /// <summary>
//        /// The _get class devices factory.
//        /// </summary>
//        private readonly IGetClassDevicesFactory _getClassDevicesFactory;

//        /// <summary>
//        /// The _unsafe native methods wrapper.
//        /// </summary>
//        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

//        /// <summary>
//        /// The _marshal wrapper.
//        /// </summary>
//        private readonly IMarshalWrapper _marshalWrapper;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DeviceManagementForInterfaceDetails"/> class.
//        /// </summary>
//        /// <param name="getClassDevicesFactory">
//        /// The get class devices factory.
//        /// </param>
//        /// <param name="unsafeNativeMethodsWrapper">
//        /// The unsafe native methods wrapper.
//        /// </param>
//        /// <param name="marshalWrapper">
//        /// The marshal wrapper.
//        /// </param>
//        public DeviceManagementForInterfaceDetails(
//            IGetClassDevicesFactory getClassDevicesFactory,
//            IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper, 
//            IMarshalWrapper marshalWrapper)
//        {
//            _getClassDevicesFactory = getClassDevicesFactory;
//            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
//            _marshalWrapper = marshalWrapper;
//        }

//        /// <summary>
//        /// Gets the DBT_DEVICEREMOVECOMPLETE.
//        /// </summary>
//        public int DbtDeviceRemoveComplete
//        {
//            get { return 0x8004; }
//        }

//        /// <summary>
//        /// The get connected usb devices.
//        /// </summary>
//        /// <param name="guid">
//        /// The guid.
//        /// </param>
//        /// <returns>
//        /// The <see>
//        ///         <cref>IObservable</cref>
//        ///     </see>
//        ///     .
//        /// </returns>
//        public IObservable<IDeviceInterfaceDetails> GetConnectedUsbDevices(Guid guid)
//        {
//            return Observable.Create<IDeviceInterfaceDetails>(observer =>
//            {
//                IDisposable enumerateDeviceInterfacesDisposable = null;
//                IGetClassDevices getClassDevices = null;
//                var compositeDisposable = new CompositeDisposable();

//                try
//                {
//                    getClassDevices = _getClassDevicesFactory.Create(guid, _unsafeNativeMethodsWrapper, _marshalWrapper);

//                    enumerateDeviceInterfacesDisposable = _unsafeNativeMethodsWrapper.SetupDiEnumerateDeviceInterfaces(getClassDevices.HandleToDeviceInformationSet, IntPtr.Zero, guid)
//                        .Select(x => GetDeviceInterfaceDetail(getClassDevices, x))
//                        .ToObservable()
//                        .Subscribe(observer);
//                }
//                catch (Exception e)
//                {
//                    observer.OnError(e);
//                }
//                finally
//                {
//                    if (getClassDevices != null)
//                    {
//                        compositeDisposable.Add(getClassDevices);                        
//                    }

//                    if (enumerateDeviceInterfacesDisposable != null)
//                    {
//                        compositeDisposable.Add(enumerateDeviceInterfacesDisposable);
//                    }
//                }

//                return compositeDisposable;
//            });
//        }

//        /// <summary>
//        /// The get device interface detail.
//        /// </summary>
//        /// <param name="getClassDevices">
//        /// The get Class Devices.
//        /// </param>
//        /// <param name="x">
//        /// The x.
//        /// </param>
//        /// <returns>
//        /// The <see cref="DeviceInterfaceDetails"/>.
//        /// </returns>
//        private DeviceInterfaceDetails GetDeviceInterfaceDetail(IGetClassDevices getClassDevices, DeviceInterfaceData x)
//        {
//            return _unsafeNativeMethodsWrapper.SetupDiGetDeviceInterfaceDetail(getClassDevices, x);
//        }
//    }
//}