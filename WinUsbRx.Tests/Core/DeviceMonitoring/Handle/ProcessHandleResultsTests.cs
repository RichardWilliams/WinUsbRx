// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessHandleResultsTests.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the ProcessHandleResultsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceMonitoring.Handle
{
    using System;
    using System.ComponentModel;
    using System.Reactive.Subjects;
    using Moq;
    using WinUsbRx.Core.DeviceMonitoring.Handle;
    using WinUsbRx.Core.DeviceMonitoring.UsbDeviceNotifications;
    using Wrappers;
    using Xunit;

    /// <summary>
    /// The process handle results tests.
    /// </summary>
    public class ProcessHandleResultsTests
    {
        /// <summary>
        /// Tests constructor_ when int ptr is zero_ then error is not null.
        /// </summary>
        [Fact]
        public void Constructor_WhenIntPtrIsZero_ThenErrorIsNotNull()
        {
            // ARRANGE
            var win32ErrorWrapper = new Win32ErrorWrapper(2);
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
            mockedMarshalWrapper.Setup(x => x.GetLastWin32Error()).Returns(win32ErrorWrapper);

            var processHandleResult = new ProcessHandleResult(IntPtr.Zero, mockedMarshalWrapper.Object);

            // ACT
            var error = processHandleResult.Win32Error;

            // ASSERT
            Assert.NotNull(error);
        }

        /// <summary>
        /// Tests constructor_ when int ptr is zero_ then error is set to get last error.
        /// </summary>
        [Fact]
        public void Constructor_WhenIntPtrIsZero_ThenErrorIsSetToGetLastError()
        {
            // ARRANGE
            var win32ErrorWrapper = new Win32ErrorWrapper(2);
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
            mockedMarshalWrapper.Setup(x => x.GetLastWin32Error()).Returns(win32ErrorWrapper);

            var processHandleResult = new ProcessHandleResult(IntPtr.Zero, mockedMarshalWrapper.Object);

            // ACT
            var error = processHandleResult.Win32Error;

            // ASSERT
            Assert.True(error.IsError(2));
        }

        /// <summary>
        /// Tests constructor_ when int ptr is not zero_ then get las win 32 error code is not called.
        /// </summary>
        [Fact]
        public void Constructor_WhenIntPtrIsNotZero_ThenGetLasWin32ErrorCodeIsNotCalled()
        {
            // ARRANGE
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();

            // ACT
            new ProcessHandleResult(new IntPtr(42), mockedMarshalWrapper.Object);

            // ASSERT
            mockedMarshalWrapper.Verify(x => x.GetLastWin32Error(), Times.Never());
        }

        /// <summary>
        /// Tests constructor_ when int ptr is not zero_ then error exception is success.
        /// </summary>
        [Fact]
        public void Constructor_WhenIntPtrIsNotZero_ThenErrorExceptionIsSuccess()
        {
            // ARRANGE
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();

            // ACT
            var processHandleResult = new ProcessHandleResult(new IntPtr(42), mockedMarshalWrapper.Object);

            // ASSERT
            Assert.True(processHandleResult.Win32Error.IsSuccess);
        }

        /// <summary>
        /// Tests success test_ when int ptr is not zero_ then error not notified.
        /// </summary>
        [Fact]
        public void SuccessTest_WhenIntPtrIsNotZero_ThenErrorNotNotified()
        {
            // ARRANGE
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
            var processHandleResult = new ProcessHandleResult(new IntPtr(42), mockedMarshalWrapper.Object);
            var subject = new Subject<IUsbDeviceNotification>();
            Exception exceptionCaught = null;

            // ACT
            subject.Subscribe(next => { }, exception => { exceptionCaught = exception; });
            processHandleResult.SuccessTest(subject);

            // ASSERT
            Assert.Null(exceptionCaught);
        }

        /// <summary>
        /// Tests success test_ when int ptr is zero_ then error notified.
        /// </summary>
        [Fact]
        public void SuccessTest_WhenIntPtrIsZero_ThenErrorNotified()
        {
            // ARRANGE
            const int errorCode = 2;
            var errorExceptionSent = new Win32ErrorWrapper(errorCode);
            var mockedMarshalWrapper = new Mock<IMarshalWrapper>();
            mockedMarshalWrapper.Setup(x => x.GetLastWin32Error()).Returns(errorExceptionSent);
            var processHandleResult = new ProcessHandleResult(IntPtr.Zero, mockedMarshalWrapper.Object);
            var subject = new Subject<IUsbDeviceNotification>();
            Exception exceptionCaught = null;

            // ACT
            subject.Subscribe(next => { }, exception => { exceptionCaught = exception; });
            processHandleResult.SuccessTest(subject);

            // ASSERT
            Assert.NotNull(exceptionCaught);
            Assert.IsType<Win32Exception>(exceptionCaught);
            Assert.Equal(errorCode, ((Win32Exception)exceptionCaught).NativeErrorCode);
        }
    }
}