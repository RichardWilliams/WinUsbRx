// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInformatioSetTests.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DiGetClassDevsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceManagement
{
    using System;
    using Moq;
    using WinUsbRx.Core.DeviceManagement;
    using WinUsbRx.Core.DeviceManagement.UnsafeNative;
    using Xunit;

    /// <summary>
    /// The get class devices tests.
    /// </summary>
    public class DeviceInformatioSetTests
    {
        /// <summary>
        /// The digcf device interface.
        /// </summary>
        private const int DigcfDeviceInterface = 0x2;

        /// <summary>
        /// The digcf preset.
        /// </summary>
        private const int DigcfPreset = 0x10;

        /// <summary>
        /// Tests constructor then unsafe native get device information set is called.
        /// </summary>
        [Fact]
        public void Constructor_ThenUnsafeNativeGetDeviceInformationSetIsCalled()
        {
            // ARRANGE and ACT
            var guid = Guid.NewGuid();
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            SetupDeviceInformationSet(guid, mockedUnsafeNativeMethodsWrapper);

            // ASSERT
            mockedUnsafeNativeMethodsWrapper.Verify(x => x.GetDeviceInformationSet(guid, IntPtr.Zero, IntPtr.Zero, DigcfDeviceInterface | DigcfPreset), Times.Once());
        }

        /// <summary>
        /// Tests constructor_ then unsafe native get device information elements is called.
        /// </summary>
        [Fact]
        public void Constructor_ThenUnsafeNativeGetDeviceInformationElementsIsCalled()
        {
            // ARRANGE and ACT
            var guid = Guid.NewGuid();
            var handle = new IntPtr(42);
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            SetupDeviceInformationSet(guid, handle, mockedUnsafeNativeMethodsWrapper);

            // ASSERT
            mockedUnsafeNativeMethodsWrapper.Verify(x => x.GetDeviceInformationElements(handle), Times.Once());
        }

        /// <summary>
        /// Tests dispose then unsafe native setup di destroy device info list is called.
        /// </summary>
        [Fact]
        public void Dispose_ThenUnsafeNativeSetupDiDestroyDeviceInfoListIsCalled()
        {
            // ARRANGE and ACT
            var guid = Guid.NewGuid();
            var handleToDeviceInformationSet = new IntPtr(42);
            var mockedUnsafeNativeMethodsWrapper = new Mock<IUnsafeNativeMethodsWrapper>();
            var getClassDevs = SetupDeviceInformationSet(guid, handleToDeviceInformationSet, mockedUnsafeNativeMethodsWrapper);
            getClassDevs.Dispose();

            // ASSERT
            mockedUnsafeNativeMethodsWrapper.Verify(x => x.SetupDiDestroyDeviceInfoList(handleToDeviceInformationSet), Times.Once());
        }

        /// <summary>
        /// The setup di get class devices.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="mockedUnsafeMethodsWrapper">
        /// The mocked unsafe methods wrapper.
        /// </param>
        private static void SetupDeviceInformationSet(Guid guid, Mock<IUnsafeNativeMethodsWrapper> mockedUnsafeMethodsWrapper)
        {
            SetupDeviceInformationSet(guid, IntPtr.Zero, mockedUnsafeMethodsWrapper);
        }

        /// <summary>
        /// The setup get class devices.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="handleToReturn">
        /// The handle to return.
        /// </param>
        /// <param name="mockedUnsafeNativeMethodsWrapper">
        /// The mocked unsafe native methods wrapper.
        /// </param>
        /// <returns>
        /// The <see cref="DeviceInformationSet"/>.
        /// </returns>
        private static DeviceInformationSet SetupDeviceInformationSet(Guid guid, IntPtr handleToReturn, Mock<IUnsafeNativeMethodsWrapper> mockedUnsafeNativeMethodsWrapper = null)
        {
            var mockedUnsafeNativeMethodsWrapperToUse = mockedUnsafeNativeMethodsWrapper ?? new Mock<IUnsafeNativeMethodsWrapper>();
            var handleReturnToUse = handleToReturn == IntPtr.Zero ? new IntPtr(42) : handleToReturn;

            mockedUnsafeNativeMethodsWrapperToUse.Setup(x => x.GetDeviceInformationSet(guid, IntPtr.Zero, IntPtr.Zero, DigcfDeviceInterface | DigcfPreset)).Returns(handleReturnToUse);

            return new DeviceInformationSet(guid, mockedUnsafeNativeMethodsWrapperToUse.Object);
        }
    }
}