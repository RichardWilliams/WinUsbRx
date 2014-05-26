// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceNotificationsTests.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceManagementForNotificationsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceMonitoring.UnsafeNative
{
    using System;
    using Moq;
    using WinUsbRx.Core.DeviceMonitoring;
    using WinUsbRx.Core.DeviceMonitoring.Handle;
    using WinUsbRx.Core.DeviceMonitoring.UnsafeNativeMethods;
    using Wrappers;
    using Xunit;

    /// <summary>
    /// The device management for notifications tests.
    /// </summary>
    public class DeviceNotificationsTests
    {
        /// <summary>
        /// Tests register then creates broadcast device interface.
        /// </summary>
        [Fact]
        public void Register_ThenCreatesBroadcastDeviceInterface()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object,
                mockedProcessHandleResultFactory.Object);

            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedBroadcastDeviceInterfaceFactory.Verify(x => x.CreateBroadcastDeviceInterface(), Times.Once);
        }

        /// <summary>
        /// Tests register then alloc h global for buffer.
        /// </summary>
        [Fact]
        public void Register_ThenAllocHGlobalForBuffer()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object,
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedMarshallWrapper.Verify(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size), Times.Once);
        }

        /// <summary>
        /// Tests register then structure to pointer is called.
        /// </summary>
        [Fact]
        public void Register_ThenStructureToPointerIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedMarshallWrapper.Verify(x => x.StructureToPointer(devBroadcastDeviceInterface, intPtrForBuffer, false), Times.Once);
        }

        /// <summary>
        /// Tests register then unsafe method register device notification is called.
        /// </summary>
        [Fact]
        public void Register_ThenUnsafeMethodRegisterDeviceNotificationIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object, 
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedUnsafeNativeMethodsWrapper.Verify(x => x.RegisterDeviceNotification(handleReceivesNotifications, intPtrForBuffer, 0), Times.Once);
        }

        /// <summary>
        /// Tests register then process handle result created.
        /// </summary>
        [Fact]
        public void Register_ThenProcessHandleResultCreated()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object, 
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);
            var registeredHandle = new IntPtr(444);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);
            mockedUnsafeNativeMethodsWrapper.Setup(x => x.RegisterDeviceNotification(handleReceivesNotifications, intPtrForBuffer, 0)).Returns(registeredHandle);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedProcessHandleResultFactory.Verify(x => x.Create(registeredHandle), Times.Once);
        }

        /// <summary>
        /// Tests register then free global is called.
        /// </summary>
        [Fact]
        public void Register_ThenFreeGlobalIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);

            // ACT
            deviceManagement.Register(handleReceivesNotifications);

            // ASSERT
            mockedMarshallWrapper.Verify(x => x.FreeHGlobal(intPtrForBuffer), Times.Once);
        }

        /// <summary>
        /// Tests register when alloc h global throws exception then free global is not called.
        /// </summary>
        [Fact]
        public void Register_WhenAllocHGlobalThrowsException_ThenFreeGlobalIsNotCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Throws<InvalidOperationException>();

            // ACT
            var testDelegate = new Assert.ThrowsDelegate(() => deviceManagement.Register(handleReceivesNotifications));

            // ASSERT
            Assert.Throws<InvalidOperationException>(testDelegate);
            mockedMarshallWrapper.Verify(x => x.FreeHGlobal(intPtrForBuffer), Times.Never);
        }

        /// <summary>
        /// Tests register when structure to pointer throws exception then free global is called.
        /// </summary>
        [Fact]
        public void Register_WhenStructureToPointerThrowsException_ThenFreeGlobalIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);
            mockedMarshallWrapper.Setup(x => x.StructureToPointer(devBroadcastDeviceInterface, intPtrForBuffer, false)).Throws<InvalidOperationException>();

            // ACT
            var testDelegate = new Assert.ThrowsDelegate(() => deviceManagement.Register(handleReceivesNotifications));

            // ASSERT
            Assert.Throws<InvalidOperationException>(testDelegate);
            mockedMarshallWrapper.Verify(x => x.FreeHGlobal(intPtrForBuffer), Times.Once);
        }

        /// <summary>
        /// Tests register when register device notification throws exception then free global is called.
        /// </summary>
        [Fact]
        public void Register_WhenRegisterDeviceNotificationThrowsException_ThenFreeGlobalIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);
            mockedUnsafeNativeMethodsWrapper.Setup(
                x => x.RegisterDeviceNotification(It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<uint>()))
                .Throws<InvalidOperationException>();

            // ACT
            var testDelegate = new Assert.ThrowsDelegate(() => deviceManagement.Register(handleReceivesNotifications));

            // ASSERT
            Assert.Throws<InvalidOperationException>(testDelegate);
            mockedMarshallWrapper.Verify(x => x.FreeHGlobal(intPtrForBuffer), Times.Once);
        }

        /// <summary>
        /// Tests register when creating handle throws exception then free global is called.
        /// </summary>
        [Fact]
        public void Register_WhenCreatingHandleThrowsException_ThenFreeGlobalIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var handleReceivesNotifications = new IntPtr(42);
            var devBroadcastDeviceInterface = new BroadcastDeviceInterface();
            var intPtrForBuffer = new IntPtr(111);

            mockedBroadcastDeviceInterfaceFactory.Setup(x => x.CreateBroadcastDeviceInterface()).Returns(devBroadcastDeviceInterface);
            mockedMarshallWrapper.Setup(x => x.AllocHGlobal(devBroadcastDeviceInterface.Size)).Returns(intPtrForBuffer);
            mockedUnsafeNativeMethodsWrapper.Setup(x => x.RegisterDeviceNotification(It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<uint>()));
            mockedProcessHandleResultFactory.Setup(x => x.Create(It.IsAny<IntPtr>())).Throws<InvalidOperationException>();

            // ACT
            var testDelegate = new Assert.ThrowsDelegate(() => deviceManagement.Register(handleReceivesNotifications));

            // ASSERT
            Assert.Throws<InvalidOperationException>(testDelegate);
            mockedMarshallWrapper.Verify(x => x.FreeHGlobal(intPtrForBuffer), Times.Once);
        }

        /// <summary>
        /// Tests unregister then unsafe native method unregister device notifications is called.
        /// </summary>
        [Fact]
        public void Unregister_ThenUnsafeNativeMethodUnregisterDeviceNotificationsIsCalled()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var unregisterHandle = new IntPtr(42);

            // ACT
            deviceManagement.UnRegister(unregisterHandle);
            
            // ASSERT
            mockedUnsafeNativeMethodsWrapper.Verify(x => x.UnRegisterDeviceNotification(unregisterHandle), Times.Once);
        }

        /// <summary>
        /// Tests unregister then create process handle result.
        /// </summary>
        [Fact]
        public void Unregister_ThenCreateProcessHandleResult()
        {
            // ARRANGE
            var mockedBroadcastDeviceInterfaceFactory = new Mock<IBroadcastDeviceInterfaceFactory>();
            var mockedMarshallWrapper = new Mock<IMarshalWrapper>();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var mockedProcessHandleResultFactory = new Mock<IProcessHandleResultFactory>();
            var deviceManagement = new DeviceNotifications(
                mockedBroadcastDeviceInterfaceFactory.Object,
                mockedMarshallWrapper.Object,
                mockedUnsafeNativeMethodsWrapper.Object, 
                mockedProcessHandleResultFactory.Object);
            var unregisterHandle = new IntPtr(42);
            var handleReturned = new IntPtr(111);

            mockedUnsafeNativeMethodsWrapper.Setup(x => x.UnRegisterDeviceNotification(unregisterHandle)).Returns(handleReturned);

            // ACT
            deviceManagement.UnRegister(unregisterHandle);

            // ASSERT
            mockedProcessHandleResultFactory.Verify(x => x.Create(handleReturned), Times.Once);
        }
    }
}