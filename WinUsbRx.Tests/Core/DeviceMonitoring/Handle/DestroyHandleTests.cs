// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DestroyHandleTests.cs" company="None">
//  TODO:  
// </copyright>
// <summary>
//   Defines the DestroyHandleTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceMonitoring.Handle
{
    using System;
    using Moq;
    using WinUsbRx.Core.DeviceMonitoring;
    using WinUsbRx.Core.DeviceMonitoring.Handle;
    using Xunit;

    /// <summary>
    /// The destroy handle tests.
    /// </summary>
    public class DestroyHandleTests
    {
        /// <summary>
        /// Tests ProcessFor then RegisteredCreatedHandle is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenRegisteredCreatedHandleIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var destroyedHandle = new DestroyedHandle(testHandle, mockedDeviceManagement.Object);

            // ACT
            destroyedHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedUsbDeviceWatcher.Verify(x => x.RegisteredCreatedHandle, Times.Once);
        }

        /// <summary>
        /// Tests ProcessFor when RegisteredCreatedHandle is null then UnregisterForDeviceNotifications is not called.
        /// </summary>
        [Fact]
        public void ProcessFor_WhenRegisteredCreatedHandleIsNull_ThenUnRegisterForDeviceNotificationsIsNotCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var destroyedHandle = new DestroyedHandle(testHandle, mockedDeviceManagement.Object);

            mockedUsbDeviceWatcher.Setup(x => x.RegisteredCreatedHandle).Returns((CreatedHandle)null);

            // ACT
            destroyedHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedDeviceManagement.Verify(x => x.Register(testHandle), Times.Never());
        }

        /// <summary>
        /// Tests ProcessFor then registered created handle device notifications is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenRegisteredCreatedHandleDeviceNotificationsIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var mockedCreatedHandle = new Mock<CreatedHandle>(testHandle, mockedDeviceManagement.Object);
            var destroyedHandle = new DestroyedHandle(testHandle, mockedDeviceManagement.Object);

            mockedUsbDeviceWatcher.Setup(x => x.RegisteredCreatedHandle).Returns(mockedCreatedHandle.Object);

            // ACT
            destroyedHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedCreatedHandle.Verify(x => x.DeviceNotificationHandle, Times.Once);
        }

        /// <summary>
        /// Tests ProcessFor then un register for device notifications is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenUnRegisterForDeviceNotificationsIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var deviceNotificationHandle = new IntPtr(111);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var mockedCreatedHandle = new Mock<CreatedHandle>(testHandle, mockedDeviceManagement.Object);
            var destroyedHandle = new DestroyedHandle(testHandle, mockedDeviceManagement.Object);

            mockedUsbDeviceWatcher.Setup(x => x.RegisteredCreatedHandle).Returns(mockedCreatedHandle.Object);
            mockedCreatedHandle.Setup(x => x.DeviceNotificationHandle).Returns(deviceNotificationHandle);

            // ACT
            destroyedHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedDeviceManagement.Verify(x => x.UnRegister(deviceNotificationHandle), Times.Once());
        }

        /// <summary>
        /// Tests ProcessFor then release handle is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenReleaseHandleIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var mockedCreatedHandle = new Mock<CreatedHandle>(testHandle, mockedDeviceManagement.Object);
            var destroyedHandle = new DestroyedHandle(testHandle, mockedDeviceManagement.Object);

            mockedUsbDeviceWatcher.Setup(x => x.RegisteredCreatedHandle).Returns(mockedCreatedHandle.Object);

            // ACT
            destroyedHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedUsbDeviceWatcher.Verify(x => x.ReleaseHandle(), Times.Once);
        }
    }
}