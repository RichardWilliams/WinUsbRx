//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="UsbDevicesTests.cs" company="None">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the UsbDevicesTests type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.Tests.Core
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reactive.Concurrency;
//    using System.Reactive.Subjects;
//    using System.Windows.Forms;
//    using Microsoft.Reactive.Testing;
//    using Moq;
//    using WinUsbRx.Core;
//    using WinUsbRx.Core.DeviceManagement;
//    using WinUsbRx.Core.UsbDeviceNotifications;
//    using WinUsbRx.UnsafeNative;
//    using Xunit;

//    /// <summary>
//    /// The usb devices tests.
//    /// </summary>
//    public class UsbDevicesTests
//    {
//        /// <summary>
//        /// Tests devices_ when usb device notified is arrived_ then connected usb device is called.
//        /// </summary>
//        [Fact]
//        public void Devices_WhenUsbDeviceNotifiedIsArrived_ThenConnectedUsbDeviceIsCalled()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var testScheduler = new TestScheduler();
//            var deviceInterfaceDetailsScheduler = new TestScheduler();
//            var mockedDeviceManagerForInterfaceDetails = new Mock<IDeviceManagementForInterfaceDetails>();
//            var usbDevices = SetupUsbDevices(guid, testScheduler, mockedDeviceManagerForInterfaceDetails, deviceInterfaceDetailsScheduler);
            
//            // ACT
//            usbDevices.Devices().Subscribe(next => { });
//            testScheduler.Start();
//            deviceInterfaceDetailsScheduler.Start();

//            // ASSERT
//            mockedDeviceManagerForInterfaceDetails.Verify(x => x.GetConnectedUsbDevices(guid), Times.Exactly(1));
//        }

//        /// <summary>
//        /// Tests devices when usb device notified is arrived then new usb device is observed.
//        /// </summary>
//        [Fact]
//        public void Devices_WhenUsbDeviceNotifiedIsArrived_ThenNewUsbDeviceIsObserved()
//        {
//            // ARRANGE
//            IUsbDevice observedUsbDevice = null;
//            var guid = Guid.NewGuid();
//            var testScheduler = new TestScheduler();
//            var deviceInterfaceDetailsScheduler = new TestScheduler();
//            var mockedDeviceManagerForInterfaceDetails = new Mock<IDeviceManagementForInterfaceDetails>();
//            var usbDevices = SetupUsbDevices(guid, testScheduler, mockedDeviceManagerForInterfaceDetails, deviceInterfaceDetailsScheduler);

//            // ACT
//            usbDevices.Devices().Subscribe(next => { observedUsbDevice = next; });
//            testScheduler.Start();
//            deviceInterfaceDetailsScheduler.Start();

//            // ASSERT
//            Assert.Equal("PathName", observedUsbDevice.Path);
//        }

//        /// <summary>
//        /// Tests devices when usb device notified is arrived then new usb device is observed.
//        /// </summary>
//        [Fact]
//        public void Devices_WhenOnlyAlreadyExistingDevices_ThenGetConnectedDevicesIsExecutedOnce()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var testScheduler = new TestScheduler();
//            var deviceInterfaceDetailsScheduler = new TestScheduler();
//            var mockedDeviceManagerForInterfaceDetails = new Mock<IDeviceManagementForInterfaceDetails>();
//            var usbDevices = SetupUsbDevices(guid, testScheduler, mockedDeviceManagerForInterfaceDetails, deviceInterfaceDetailsScheduler);

//            // ACT
//            usbDevices.Devices().Subscribe(next => { });
//            deviceInterfaceDetailsScheduler.Start();

//            // ASSERT
//            mockedDeviceManagerForInterfaceDetails.Verify(x => x.GetConnectedUsbDevices(guid), Times.Exactly(1));
//        }

//        /// <summary>
//        /// The devices_ when usb device notified is arrived_ then new distinct device is observed.
//        /// </summary>
//        [Fact]
//        public void Devices_WhenUsbDeviceNotifiedIsArrived_ThenNewDistinctDeviceIsObserved()
//        {
//            // ARRANGE
//            var timesCalled = 0;
//            var guid = Guid.NewGuid();
//            var testScheduler = new TestScheduler();
//            var deviceInterfaceDetailsScheduler = new TestScheduler();
//            var mockedDeviceManagerForInterfaceDetails = new Mock<IDeviceManagementForInterfaceDetails>();
//            var mockedUsbDevice1 = new Mock<IUsbDevice>();
//            var mockedUsbDevice2 = new Mock<IUsbDevice>();
//            var mockedDeviceInterfaceDetails1 = new Mock<IDeviceInterfaceDetails>();
//            var mockedDeviceInterfaceDetails2 = new Mock<IDeviceInterfaceDetails>();

//            var usbDevices = SetupUsbDevices(
//                guid, 
//                testScheduler, 
//                mockedDeviceManagerForInterfaceDetails, 
//                deviceInterfaceDetailsScheduler, 
//                new List<Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>>
//                {
//                    new Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>(mockedDeviceInterfaceDetails1, mockedUsbDevice1),
//                    new Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>(mockedDeviceInterfaceDetails2, mockedUsbDevice2),
//                });

//            // ACT
//            usbDevices.Devices().Subscribe(next =>
//            {
//                timesCalled++;
//            });
//            testScheduler.Start();
//            deviceInterfaceDetailsScheduler.Start();

//            // ASSERT
//            Assert.Equal(1, timesCalled);
//        }

//        /// <summary>
//        /// Tests devices_ when duplicate devices_ then usb device factory create is called once.
//        /// </summary>
//        [Fact]
//        public void Devices_WhenDuplicateDevices_ThenUsbDeviceFactoryCreateIsCalledOnce()
//        {
//            // ARRANGE
//            var guid = Guid.NewGuid();
//            var testScheduler = new TestScheduler();
//            var deviceInterfaceDetailsScheduler = new TestScheduler();
//            var mockedDeviceManagerForInterfaceDetails = new Mock<IDeviceManagementForInterfaceDetails>();
//            var mockedUsbDevice1 = new Mock<IUsbDevice>();
//            var mockedUsbDevice2 = new Mock<IUsbDevice>();
//            var mockedDeviceInterfaceDetails1 = new Mock<IDeviceInterfaceDetails>();
//            var mockedDeviceInterfaceDetails2 = new Mock<IDeviceInterfaceDetails>();
//            var mockedUsbDeviceFactory = new Mock<IUsbDeviceFactory>();

//            var usbDevices = SetupUsbDevices(
//                guid,
//                testScheduler,
//                mockedDeviceManagerForInterfaceDetails,
//                deviceInterfaceDetailsScheduler,
//                new List<Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>>
//                {
//                    new Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>(mockedDeviceInterfaceDetails1, mockedUsbDevice1),
//                    new Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>(mockedDeviceInterfaceDetails2, mockedUsbDevice2),
//                },
//                mockedUsbDeviceFactory);

//            // ACT
//            usbDevices.Devices().Subscribe(next => { });
//            testScheduler.Start();
//            deviceInterfaceDetailsScheduler.Start();

//            // ASSERT
//            mockedUsbDeviceFactory.Verify(x => x.Create("PathName"), Times.Once());
//        }

//        /// <summary>
//        /// The setup usb devices.
//        /// </summary>
//        /// <param name="guid">
//        /// The guid.
//        /// </param>
//        /// <param name="usbDeviceWatcherScheduler">
//        /// The usb device watcher scheduler.
//        /// </param>
//        /// <param name="mockedDeviceManagementForInterfaceDetails">
//        /// The mocked device management for interface details.
//        /// </param>
//        /// <param name="deviceInterfaceDetailsScheduler">
//        /// The device interface details scheduler.
//        /// </param>
//        /// <param name="mockedDeviceInterfaceDetailswithUsbDevicesList">
//        /// The mocked device interface details list.
//        /// </param>
//        /// <param name="mockedUsbDeviceFactory">
//        /// The mocked Usb Device Factory.
//        /// </param>
//        /// <returns>
//        /// The <see cref="UsbDevices"/>.
//        /// </returns>
//        private static UsbDevices SetupUsbDevices(
//            Guid guid,
//            TestScheduler usbDeviceWatcherScheduler,
//            Mock<IDeviceManagementForInterfaceDetails> mockedDeviceManagementForInterfaceDetails,
//            TestScheduler deviceInterfaceDetailsScheduler,
//            List<Tuple<Mock<IDeviceInterfaceDetails>, Mock<IUsbDevice>>> mockedDeviceInterfaceDetailswithUsbDevicesList = null,
//            Mock<IUsbDeviceFactory> mockedUsbDeviceFactory = null)
//        {
//            const string PATH_NAME = "PathName";
//            var message = new Message { WParam = new IntPtr(0x8000) };
//            var usbDeviceNotification = new UsbDeviceNotification(guid, message, PATH_NAME);
//            var arrivedDeviceNotificationsSubject = new Subject<IUsbDeviceNotification>();
//            var startSubject = new Subject<IUsbDeviceNotification>();
//            var observableInterfaceDetails = new Subject<IDeviceInterfaceDetails>();

//            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
//            var mockedDeviceManagerForInterfaceDetailsToUse = mockedDeviceManagementForInterfaceDetails ?? new Mock<IDeviceManagementForInterfaceDetails>();
//            var mockedUsbDeviceFactoryToUse = mockedUsbDeviceFactory ?? new Mock<IUsbDeviceFactory>();

//            mockedUsbDeviceWatcher.Setup(x => x.ArrivedDeviceNotificationsOnly()).Returns(() => arrivedDeviceNotificationsSubject);
//            mockedUsbDeviceWatcher.Setup(x => x.AllDeviceNotifications()).Returns(() => startSubject);
//            mockedDeviceManagerForInterfaceDetailsToUse.Setup(x => x.GetConnectedUsbDevices(guid)).Returns(observableInterfaceDetails);

//            if (mockedDeviceInterfaceDetailswithUsbDevicesList == null || !mockedDeviceInterfaceDetailswithUsbDevicesList.Any())
//            {
//                var mockedUsbDevice = new Mock<IUsbDevice>();
//                var mockedDeviceInterfaceDetails = new Mock<IDeviceInterfaceDetails>();

//                mockedDeviceInterfaceDetails.Setup(x => x.PathName).Returns(PATH_NAME);
//                mockedUsbDeviceFactoryToUse.Setup(x => x.Create(PATH_NAME)).Returns(mockedUsbDevice.Object);
//                mockedUsbDevice.Setup(x => x.Path).Returns(PATH_NAME);
                
//                deviceInterfaceDetailsScheduler.Schedule(() => observableInterfaceDetails.OnNext(mockedDeviceInterfaceDetails.Object));
//            }
//            else
//            {
//                mockedDeviceInterfaceDetailswithUsbDevicesList.ForEach(x =>
//                {
//                    x.Item1.Setup(y => y.PathName).Returns(PATH_NAME);
//                    mockedUsbDeviceFactoryToUse.Setup(y => y.Create(PATH_NAME)).Returns(x.Item2.Object);
//                    x.Item2.Setup(y => y.Path).Returns(PATH_NAME);
//                    deviceInterfaceDetailsScheduler.Schedule(() => observableInterfaceDetails.OnNext(x.Item1.Object));
//                });
//            }


//            var usbDevices = new UsbDevices(guid, mockedUsbDeviceWatcher.Object, mockedDeviceManagerForInterfaceDetailsToUse.Object, mockedUsbDeviceFactoryToUse.Object);
//            usbDeviceWatcherScheduler.Schedule(() => arrivedDeviceNotificationsSubject.OnNext(usbDeviceNotification));

//            return usbDevices;
//        }
//    }
//}