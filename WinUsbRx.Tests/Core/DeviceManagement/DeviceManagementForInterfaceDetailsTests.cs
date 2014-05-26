//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="DeviceManagementForInterfaceDetailsTests.cs" company="None">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the DeviceManagementForInterfaceDetailsTests type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//using WinUsbRx.UnsafeNative;
//using WinUsbRx.UnsafeNative.Structs;

//namespace WinUsbRx.Tests.Core.DeviceManagement
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel;
//    using Moq;
//    using UnsafeNative;
//    using UnsafeNative.DeviceInstallationFunctions;
//    using UnsafeNative.Structs;
//    using WinUsbRx.Core.DeviceManagement;
//    using Wrappers;
//    using Xunit;

//    /// <summary>
//    /// The device management for interface details tests.
//    /// </summary>
//    public class DeviceManagementForInterfaceDetailsTests
//    {
//        /// <summary>
//        /// Tests dbt device removal complete then correct value returned.
//        /// </summary>
//        [Fact]
//        public void DbtDeviceRemovalComplete_ThenCorrectValueReturned()
//        {
//            // ARRANGE
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            // ACT
//            var dbtDeviceRemovalComplete = deviceManagement.DbtDeviceRemoveComplete;

//            // ASSERT
//            Assert.Equal(0x8004, dbtDeviceRemovalComplete);
//        }

//        /// <summary>
//        /// Tests get connected usb devices then get class device factory create is called.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_ThenGetClassDeviceFactoryCreateIsCalled()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var mockedDiGetClassDevs = new Mock<IGetClassDevices>();
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Returns(mockedDiGetClassDevs.Object);

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid).Subscribe(next => { });

//            // ASSERT
//            mockedGetClassDevsFactory.Verify(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object), Times.Once());
//        }

//        /// <summary>
//        /// Tests get connected usb devices when get class device factory throws exception then observe error.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_WhenGetClassDeviceFactoryThrowsException_ThenObserveError()
//        {
//            // ARRANGE
//            var exceptionOccured = false;
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Throws<Win32Exception>();

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid)
//                .Subscribe(next => { }, exception => { exceptionOccured = true; });

//            // ASSERT
//            Assert.True(exceptionOccured);
//        }

//        /// <summary>
//        /// Tests get connected usb devices when get class device factory throws exception then observe exception thrown as error.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_WhenGetClassDeviceFactoryThrowsException_ThenObserveExceptionThrownAsError()
//        {
//            // ARRANGE
//            Exception exceptionOccured = null;
//            var expectedException = new Win32Exception(42);
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Throws(expectedException);

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid)
//                .Subscribe(next => { }, exception => { exceptionOccured = exception; });

//            // ASSERT
//            Assert.IsType<Win32Exception>(exceptionOccured);
//            Assert.Equal(expectedException, exceptionOccured);
//        }

//        /// <summary>
//        /// Tests get connected usb devices then setup enumeration device interfaces is called.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_ThenSetupEnumerationDeviceInterfacesIsCalled()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var mockedGetClassDevs = new Mock<IGetClassDevices>();
//            var handleToDeviceInformationSet = new IntPtr(42);
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Returns(mockedGetClassDevs.Object);
//            mockedGetClassDevs.Setup(x => x.HandleToDeviceInformationSet).Returns(handleToDeviceInformationSet);

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid).Subscribe(next => { });

//            // ASSERT
//            mockedUnsafeNativeMethodsWrapper.Verify(x => x.SetupDiEnumerateDeviceInterfaces(handleToDeviceInformationSet, IntPtr.Zero, guid), Times.Once());
//        }

//        /// <summary>
//        /// Tests get connected usb devices when setup enumeration device interfaces throws exception then observe error.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_WhenSetupEnumerationDeviceInterfacesThrowsException_ThenObserveError()
//        {
//            // ARRANGE
//            var exceptionOccured = false;
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var mockedDiGetClassDevs = new Mock<IGetClassDevices>();
//            var handleToDeviceInformationSet = new IntPtr(42);
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Returns(mockedDiGetClassDevs.Object);
//            mockedDiGetClassDevs.Setup(x => x.HandleToDeviceInformationSet).Returns(handleToDeviceInformationSet);
//            mockedUnsafeNativeMethodsWrapper.Setup(x => x.SetupDiEnumerateDeviceInterfaces(handleToDeviceInformationSet, IntPtr.Zero, guid)).Throws<Win32Exception>();

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid)
//                .Subscribe(next => { }, exception => { exceptionOccured = true; });

//            // ASSERT
//            Assert.True(exceptionOccured);
//        }

//        /// <summary>
//        /// Tests get connected usb devices_ when setup enumeration device interfaces throws exception_ then get class devices is disposed.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_WhenSetupEnumerationDeviceInterfacesThrowsException_ThenGetClassDevicesIsDisposed()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var mockedDiGetClassDevs = new Mock<IGetClassDevices>();
//            var handleToDeviceInformationSet = new IntPtr(42);
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Returns(mockedDiGetClassDevs.Object);
//            mockedDiGetClassDevs.Setup(x => x.HandleToDeviceInformationSet).Returns(handleToDeviceInformationSet);
//            mockedUnsafeNativeMethodsWrapper.Setup(x => x.SetupDiEnumerateDeviceInterfaces(handleToDeviceInformationSet, IntPtr.Zero, guid)).Throws<Win32Exception>();

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid).Subscribe(next => { }, exception => { });

//            // ASSERT
//            mockedDiGetClassDevs.Verify(x => x.Dispose(), Times.Once);
//        }

//        /// <summary>
//        /// Tests get connected usb devices_ when 3 devices_ then setup get device interfaces detail is called 3 times.
//        /// </summary>
//        [Fact]
//        public void GetConnectedUsbDevices_When3Devices_ThenSetupGetDeviceInterfacesDetailIsCalled3Times()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
//            var mockedGetClassDevsFactory = new Mock<IGetClassDevicesFactory>();
//            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
//            var mockedGetClassDevs = new Mock<IGetClassDevices>();
//            var handleToDeviceInformationSet = new IntPtr(42);
//            var deviceInterfaceData1 = new DeviceInterfaceData(guid);
//            var deviceInterfaceData2 = new DeviceInterfaceData(guid);
//            var deviceInterfaceData3 = new DeviceInterfaceData(guid);
//            var deviceInterfaceDatas = new List<DeviceInterfaceData>
//            {
//                deviceInterfaceData1,
//                deviceInterfaceData2,
//                deviceInterfaceData3
//            };
//            var deviceManagement = new DeviceManagementForInterfaceDetails(
//                mockedGetClassDevsFactory.Object,
//                mockedUnsafeNativeMethodsWrapper.Object,
//                mockedMarshalWrapper.Object);

//            mockedGetClassDevsFactory.Setup(x => x.Create(guid, mockedUnsafeNativeMethodsWrapper.Object, mockedMarshalWrapper.Object)).Returns(mockedGetClassDevs.Object);
//            mockedGetClassDevs.Setup(x => x.HandleToDeviceInformationSet).Returns(handleToDeviceInformationSet);
//            mockedUnsafeNativeMethodsWrapper.Setup(x => x.SetupDiEnumerateDeviceInterfaces(handleToDeviceInformationSet, IntPtr.Zero, guid)).Returns(deviceInterfaceDatas);

//            // ACT
//            deviceManagement.GetConnectedUsbDevices(guid).Subscribe(next => { });

//            // ASSERT
//            mockedUnsafeNativeMethodsWrapper.Verify(x => x.SetupDiGetDeviceInterfaceDetail(mockedGetClassDevs.Object, deviceInterfaceData1), Times.Once());
//            mockedUnsafeNativeMethodsWrapper.Verify(x => x.SetupDiGetDeviceInterfaceDetail(mockedGetClassDevs.Object, deviceInterfaceData2), Times.Once());
//            mockedUnsafeNativeMethodsWrapper.Verify(x => x.SetupDiGetDeviceInterfaceDetail(mockedGetClassDevs.Object, deviceInterfaceData3), Times.Once());
//        }
//    }
//}