// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbDeviceWatcherTests.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The usb device watcher tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceMonitoring
{
    using System;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Reactive.Testing;
    using Moq;
    using WinUsbRx.Core.DeviceMonitoring;
    using WinUsbRx.Core.DeviceMonitoring.Handle;
    using WinUsbRx.Core.DeviceMonitoring.UsbDeviceNotifications;
    using Wrappers;
    using Xunit;

    /// <summary>
    /// The usb device watcher tests.
    /// </summary>
    public class UsbDeviceWatcherTests
    {
        /// <summary>
        /// Tests start when handle observed then process for on handle is called.
        /// </summary>
        [Fact]
        public void Start_WhenHandleObserved_ThenProcessForOnHandleIsCalled()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var usbDeviceWatcher = SetupDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher))
                .Returns(mockedProcessHandleResult.Object)
                .Verifiable();

            // ACT
            var observable = usbDeviceWatcher.AllDeviceNotifications();
            observable.Subscribe(next => { });
            testScheduler.Start();

            // ASSERT
            mockedHandle.Verify(x => x.ProcessFor(usbDeviceWatcher), Times.Once());
        }

        /// <summary>
        /// Tests start and attached devices_ when handle observed_ then process for on handle is called once.
        /// </summary>
        [Fact]
        public void StartAndAttachedDevices_WhenHandleObserved_ThenProcessForOnHandleIsCalledOnce()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var usbDeviceWatcher = SetupDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher))
                .Returns(mockedProcessHandleResult.Object)
                .Verifiable();

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next => { });
            usbDeviceWatcher.ArrivedDeviceNotificationsOnly().Subscribe(next => { });
            testScheduler.Start();

            // ASSERT
            mockedHandle.Verify(x => x.ProcessFor(usbDeviceWatcher), Times.Once());
        }

        /// <summary>
        /// Tests start and attached devices_ when start disposed_ then attached devices still receives notification.
        /// </summary>
        [Fact]
        public void StartAndAttachedDevices_WhenStartDisposed_ThenAttachedDevicesStillReceivesNotification()
        {
            // ARRANGE
            var manualResetEventSlim = new ManualResetEventSlim(false);
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var message = new Message { Msg = 0x219, WParam = new IntPtr(0x8000) };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, message);
            IUsbDeviceNotification nextObservedFromStart = null;
            IUsbDeviceNotification nextObservedFromAttachedDevices = null;

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher))
                .Returns(mockedProcessHandleResult.Object)
                .Verifiable();

            // ACT
            var disposable = usbDeviceWatcher.AllDeviceNotifications().Subscribe(next =>
            {
                nextObservedFromStart = next;
            });
            disposable.Dispose();
            usbDeviceWatcher.ArrivedDeviceNotificationsOnly().Subscribe(next =>
            {
                nextObservedFromAttachedDevices = next;
                manualResetEventSlim.Set();
            });
            testScheduler.Start();

            // ASSERT
            manualResetEventSlim.Wait();
            Assert.Null(nextObservedFromStart);
            Assert.NotNull(nextObservedFromAttachedDevices);
        }

        /// <summary>
        /// Tests start and attached devices_ when handle observed_ then usb form run is called once.
        /// </summary>
        [Fact]
        public void StartAndAttachedDevices_WhenHandleObserved_ThenUsbFormRunIsCalledOnce()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var mockedUsbForm = new Mock<IUsbForm>();
            var guid = Guid.NewGuid();
            var usbDeviceWatcher = SetupDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, mockedUsbForm);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher))
                .Returns(mockedProcessHandleResult.Object)
                .Verifiable();

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next => { });
            usbDeviceWatcher.ArrivedDeviceNotificationsOnly().Subscribe(next => { });
            testScheduler.Start();

            // ASSERT
            mockedUsbForm.Verify(x => x.Run(), Times.Once());
        }

        /// <summary>
        /// Tests start when handle observed then process handle result success test is called.
        /// </summary>
        [Fact]
        public void Start_WhenHandleObserved_ThenProcessHandleResultSuccessTestIsCalled()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var usbDeviceWatcher = SetupDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next => { });
            testScheduler.Start();

            // ASSERT
            mockedProcessHandleResult.Verify(x => x.SuccessTest(It.IsAny<IObserver<IUsbDeviceNotification>>()), Times.Once());
        }

        /// <summary>
        /// Tests start when device changed message sent then watched device is observed.
        /// </summary>
        [Fact]
        public void Start_WhenDeviceChangedMessageSent_ThenWatchedDeviceIsObserved()
        {
            // ARRANGE
            IUsbDeviceNotification deviceArrived = null;
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var message = new Message { Msg = 0x219 };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, message);
            var manualResetEventSlim = new ManualResetEventSlim(false);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next =>
            {
                deviceArrived = next;
                manualResetEventSlim.Set();
            });
            testScheduler.Start();
            usbDeviceWatcher.SimulateMessage(message);
            manualResetEventSlim.Wait();

            // ASSERT
            Assert.NotNull(deviceArrived);
        }

        /// <summary>
        /// Tests start when device changed message sent and an exception occurs then error is observed.
        /// </summary>
        [Fact]
        public void Start_WhenDeviceChangedMessageSentAndExceptionOccurs_ThenErrorIsObserved()
        {
            // ARRANGE
            Exception errorArrived = null;
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var message = new Message { Msg = 0x219 };
            const bool mockedMarshalWrapperThrowsException = true;
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, message, null, mockedMarshalWrapperThrowsException);
            var manualResetEventSlim = new ManualResetEventSlim(false);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications()
                .Subscribe(
                    next => manualResetEventSlim.Set(),
                    error =>
                    {
                        errorArrived = error;
                        manualResetEventSlim.Set();
                    });
            testScheduler.Start();
            usbDeviceWatcher.SimulateMessage(message);
            manualResetEventSlim.Wait();

            // ASSERT
            Assert.NotNull(errorArrived);
        }

        /// <summary>
        /// Tests start when device changed message sent and an exception occurs then device is not observed.
        /// </summary>
        [Fact]
        public void Start_WhenDeviceChangedMessageSentAndExceptionOccurs_ThenDeviceIsNotObserved()
        {
            // ARRANGE
            IUsbDeviceNotification deviceArrived = null;
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var message = new Message { Msg = 0x219 };
            const bool mockedMarshalWrapperThrowsException = true;
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, message, null, mockedMarshalWrapperThrowsException);
            var manualResetEventSlim = new ManualResetEventSlim(false);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications()
                .Subscribe(
                    next =>
                    {
                        deviceArrived = next; 
                        manualResetEventSlim.Set();
                    },
                    error => manualResetEventSlim.Set());
            testScheduler.Start();
            usbDeviceWatcher.SimulateMessage(message);
            manualResetEventSlim.Wait();

            // ASSERT
            Assert.Null(deviceArrived);
        }

        /// <summary>
        /// Tests start when device changed message sent then usb device notification factory create is called.
        /// </summary>
        [Fact]
        public void Start_WhenDeviceChangedMessageSent_ThenUsbDeviceNotificationFactoryCreateIsCalled()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedUsbDeviceNotificaitonFactory = new Mock<IUsbDeviceNotificationFactory>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var deviceChangeMessage = new Message { Msg = 0x219, WParam = new IntPtr(0x8000), LParam = new IntPtr(42) };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(
                testScheduler,
                mockedHandle,
                guid,
                deviceChangeMessage,
                mockedUsbDeviceNotificaitonFactory);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next => { });
            testScheduler.Start();

            // ASSERT
            mockedUsbDeviceNotificaitonFactory.Verify(x => x.Create(guid, deviceChangeMessage, "Name"), Times.Once());
        }

        /// <summary>
        /// Tests start when test message is sent then watched device is not observed.
        /// </summary>
        [Fact]
        public void Start_WhenTestMessageSent_ThenWatchedDeviceIsNotObserved()
        {
            // ARRANGE
            IUsbDeviceNotification deviceArrived = null;
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var testMessage = new Message { Msg = 1 };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(
                testScheduler,
                mockedHandle,
                guid,
                testMessage);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next => deviceArrived = next);
            testScheduler.Start();
            usbDeviceWatcher.SimulateMessage(testMessage);

            // ASSERT
            Assert.Null(deviceArrived);
        }

        /// <summary>
        /// Tests start when observer then thread id should be different to subscription thread.
        /// </summary>
        [Fact]
        public void Start_WhenObserver_ThenThreadIdShouldBeDifferentToSubscriptionThread()
        {
            // ARRANGE
            IUsbDeviceNotification deviceArrived = null;
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var guid = Guid.NewGuid();
            var deviceChangeMessage = new Message { Msg = 0x219 };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, deviceChangeMessage);
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            var observerdThreadId = -1;
            var manualResetEventSlim = new ManualResetEventSlim(false);

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Returns(mockedProcessHandleResult.Object);

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(next =>
            {
                observerdThreadId = Thread.CurrentThread.ManagedThreadId;
                deviceArrived = next;
                manualResetEventSlim.Set();
            });
            testScheduler.Start();
            usbDeviceWatcher.SimulateMessage(deviceChangeMessage);
            manualResetEventSlim.Wait();

            // ASSERT
            Assert.NotEqual(currentThreadId, observerdThreadId);
            Assert.NotNull(deviceArrived);
        }

        /// <summary>
        /// Tests start_ when handle notification throws exception_ then error is caught.
        /// </summary>
        [Fact]
        public void Start_WhenHandleNotificationThrowsException_ThenErrorIsCaught()
        {
            // ARRANGE
            var testScheduler = new TestScheduler();
            var mockedHandle = new Mock<IHandle>();
            var guid = Guid.NewGuid();
            var deviceChangeMessage = new Message { Msg = 0 };
            var usbDeviceWatcher = SetupTestDeviceWatcherWithHandleCreated(testScheduler, mockedHandle, guid, deviceChangeMessage);
            var manualResetEventSlim = new ManualResetEventSlim(false);
            var errorCaught = false;

            mockedHandle.Setup(x => x.ProcessFor(usbDeviceWatcher)).Throws<InvalidOperationException>();

            // ACT
            usbDeviceWatcher.AllDeviceNotifications().Subscribe(
                next => { },
                error =>
                {
                    errorCaught = true;
                    manualResetEventSlim.Set();
                });
            testScheduler.Start();
            manualResetEventSlim.Wait();

            // ASSERT
            Assert.True(errorCaught);
        }

        /// <summary>
        /// The setup device watcher with handle created.
        /// </summary>
        /// <param name="testScheduler">
        /// The test scheduler.
        /// </param>
        /// <param name="mockedHandle">
        /// The mocked Handle.
        /// </param>
        /// <param name="guid">
        /// The guid to use to register for device notifications.
        /// </param>
        /// <param name="mockedUsbForm">
        /// The mocked Usb Form.
        /// </param>
        /// <param name="numberOfNotifications">
        /// The number Of Notifications.
        /// </param>
        /// <returns>
        /// The <see cref="UsbDeviceWatcher"/>.
        /// </returns>
        private UsbDeviceWatcher SetupDeviceWatcherWithHandleCreated(
            TestScheduler testScheduler,
            Mock<IHandle> mockedHandle,
            Guid guid,
            Mock<IUsbForm> mockedUsbForm = null,
            int numberOfNotifications = 1)
        {
            var guidToUse = guid == Guid.Empty ? Guid.NewGuid() : guid;
            var subject = new Subject<IHandle>();
            var mockedUsbFormToUse = mockedUsbForm ?? new Mock<IUsbForm>();
            var mockedUsbDeviceNotificationFactory = new Mock<IUsbDeviceNotificationFactory>();
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();

            Enumerable.Range(0, numberOfNotifications)
                .ToList()
                .ForEach(x => testScheduler.Schedule(() => subject.OnNext(mockedHandle.Object)));
            mockedUsbFormToUse.Setup(x => x.Run()).Returns(subject);

            return new UsbDeviceWatcher(mockedMarshalWrapper.Object, mockedUsbDeviceNotificationFactory.Object, mockedUsbFormToUse.Object, guidToUse);
        }

        /// <summary>
        /// The setup device watcher with handle created.
        /// </summary>
        /// <param name="testScheduler">
        /// The test scheduler.
        /// </param>
        /// <param name="mockedHandle">
        /// The mocked Handle.
        /// </param>
        /// <param name="guid">
        /// The guid to use to register for device notifications.
        /// </param>
        /// <param name="message">
        /// The message to use when creating a UsbDeviceNotification.
        /// </param>
        /// <param name="mockedUsbDeviceNotificationFactory">
        /// The mocked Usb Device Notification Factory.
        /// </param>
        /// <param name="mockedMarshalWrapperThrowsException">
        /// The mocked Marshal Wrapper Throws Exception.
        /// </param>
        /// <param name="numberOfNotifications">
        /// The number Of Notifications.
        /// </param>
        /// <returns>
        /// The <see cref="UsbDeviceWatcher"/>.
        /// </returns>
        private TestUsbDeviceWatcher SetupTestDeviceWatcherWithHandleCreated(
            TestScheduler testScheduler,
            Mock<IHandle> mockedHandle,
            Guid guid,
            Message message = default(Message),
            Mock<IUsbDeviceNotificationFactory> mockedUsbDeviceNotificationFactory = null,
            bool mockedMarshalWrapperThrowsException = false,
            int numberOfNotifications = 1)
        {
            const string NAME = "Name";
            var lParam = new IntPtr(42);
            var messageToUse = message == default(Message) ? new Message { Msg = 0 } : message;
            messageToUse.LParam = lParam;
            var guidToUse = guid == Guid.Empty ? Guid.NewGuid() : guid;
            var subject = new Subject<IHandle>();
            var mockedUsbForm = new Mock<IUsbForm>();
            var mockedUsbDeviceNotificationFactoryToUse = mockedUsbDeviceNotificationFactory ?? new Mock<IUsbDeviceNotificationFactory>();
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
            var mockedDevBroadcastDeviceInterface = new BroadcastDeviceInterface {Name = NAME, Guid = guidToUse};

            if (mockedMarshalWrapperThrowsException)
            {
                mockedMarshalWrapper.Setup(x => x.PointerToStructure<BroadcastDeviceInterface>(lParam, typeof(BroadcastDeviceInterface))).Throws<InvalidOperationException>();
            }
            else
            {
                mockedMarshalWrapper.Setup(x => x.PointerToStructure<BroadcastDeviceInterface>(lParam, typeof(BroadcastDeviceInterface))).Returns(mockedDevBroadcastDeviceInterface);
            }
            mockedUsbForm.Setup(x => x.Run()).Returns(subject);
            mockedUsbDeviceNotificationFactoryToUse.Setup(x => x.Create(guidToUse, messageToUse, NAME))
                .Returns(() => new UsbDeviceNotification(guidToUse, messageToUse, NAME));

            var testUsbDeviceWatcher = new TestUsbDeviceWatcher(mockedMarshalWrapper.Object, mockedUsbDeviceNotificationFactoryToUse.Object, mockedUsbForm.Object, guidToUse);

            Enumerable.Range(0, numberOfNotifications)
                        .ToList()
                        .ForEach(x => testScheduler.Schedule(() =>
                        {
                            subject.OnNext(mockedHandle.Object);
                            testUsbDeviceWatcher.SimulateMessage(messageToUse);
                        }));
            
            return testUsbDeviceWatcher;
        }

        /// <summary>
        /// The test usb device watcher, this class is used to test a message sent to the UsbDeviceWatcher, needed to derive from it.
        /// </summary>
        private class TestUsbDeviceWatcher : UsbDeviceWatcher
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestUsbDeviceWatcher"/> class.
            /// </summary>
            /// <param name="marshalWrapper">
            /// The marshal Wrapper.
            /// </param>
            /// <param name="usbDeviceNotificationFactory">
            /// The usb Device Notification Factory.
            /// </param>
            /// <param name="usbForm">
            /// The usb form.
            /// </param>
            /// <param name="guid">
            /// The guid.
            /// </param>
            public TestUsbDeviceWatcher(
                IMarshalWrapper marshalWrapper,
                IUsbDeviceNotificationFactory usbDeviceNotificationFactory,
                IUsbForm usbForm,
                Guid guid)
                : base(marshalWrapper, usbDeviceNotificationFactory, usbForm, guid)
            {
            }

            /// <summary>
            /// The simulate message.
            /// </summary>
            /// <param name="message">
            /// The message.
            /// </param>
            public void SimulateMessage(Message message)
            {
                WndProc(ref message);
            }
        }
    }
}
