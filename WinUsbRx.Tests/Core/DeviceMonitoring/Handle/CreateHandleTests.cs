// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateHandleTests.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the CreateHandleTests type.
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
    /// The create handle tests.
    /// </summary>
    public class CreateHandleTests
    {
        /// <summary>
        /// Tests process for then assign handle is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenAssignHandleIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var createdHandle = SetupCreatedHandle(null, testHandle);
            
            // ACT
            createdHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedUsbDeviceWatcher.Verify(x => x.AssignHandle(testHandle), Times.Once);
        }

        /// <summary>
        /// Tests process for then device management register for notifications is called.
        /// </summary>
        [Fact]
        public void ProcessFor_ThenDeviceManagementRegisterForNotificationsIsCalled()
        {
            // ARRANGE
            var testHandle = new IntPtr(42);
            var mockedDeviceManagement = new Mock<IDeviceNotifications>();
            var mockedUsbDeviceWatcher = new Mock<IUsbDeviceWatcher>();
            var createdHandle = SetupCreatedHandle(mockedDeviceManagement, testHandle);

            // ACT
            createdHandle.ProcessFor(mockedUsbDeviceWatcher.Object);

            // ASSERT
            mockedDeviceManagement.Verify(x => x.Register(testHandle), Times.Once);
        }

        /// <summary>
        /// The setup created handle.
        /// </summary>
        /// <param name="mockedDeviceManagement">
        /// The mocked device management.
        /// </param>
        /// <param name="testHandle">
        /// The test handle.
        /// </param>
        /// <returns>
        /// The <see cref="CreatedHandle"/>.
        /// </returns>
        private CreatedHandle SetupCreatedHandle(
            Mock<IDeviceNotifications> mockedDeviceManagement = null, 
            IntPtr testHandle = default(IntPtr))
        {
            var testHandleToUse = testHandle == IntPtr.Zero ? new IntPtr(42) : testHandle;
            var deviceNotificationHandle = new IntPtr(111);
            var mockedProcessHandleResult = new Mock<IProcessHandleResult>();
            var mockedDeviceManagementToUse = mockedDeviceManagement ?? new Mock<IDeviceNotifications>();

            mockedProcessHandleResult.Setup(x => x.Handle).Returns(deviceNotificationHandle);
            mockedDeviceManagementToUse.Setup(x => x.Register(testHandleToUse)).Returns(mockedProcessHandleResult.Object);

            return new CreatedHandle(testHandleToUse, mockedDeviceManagementToUse.Object);
        }
    }
}