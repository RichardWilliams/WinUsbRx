// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbFormTests.cs" company="None">
//   Some copyright TODO:
// </copyright>
// <summary>
//   Defines the UsbConnectionNotifierTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Tests.Core.DeviceMonitoring
{
    using System;
    using System.Reactive;
    using System.Reactive.Linq;
    using Moq;
    using WinUsbRx.Core.DeviceMonitoring;
    using WinUsbRx.Core.DeviceMonitoring.Handle;
    using Xunit;

    /// <summary>
    /// The usb form tests.
    /// </summary>
    public class UsbFormTests
    {
        /// <summary>
        /// Tests Run when subscribed then handle created.
        /// </summary>
        [Fact]
        public void Run_WhenSubscribed_ThenHandleCreatedFromSubscription()
        {
            // ARRANGE
            var handleCreated = false;
            using (var usbForm = SetupUsbForm())
            {
                // ACT
                using (usbForm.Run().OfType<CreatedHandle>().Subscribe(next => handleCreated = true))
                {
                }
            }

            // ASSERT
            Assert.True(handleCreated);
        }

        /// <summary>
        /// Tests run_ when exception thrown_ then error is caught.
        /// </summary>
        [Fact]
        public void Run_WhenExceptionThrownInCreateCreatedHandle_ThenErrorIsCaught()
        {
            // ARRANGE
            var handleCreated = false;
            var error = false;
            var mockedHandleFactory = new Mock<IHandleFactory>();
            var observer = Observer.Create<IHandle>(
                next =>
                {
                    handleCreated = true;
                    Console.WriteLine("Handle Created!!!!");
                },
                exception =>
                {
                    error = true;
                    Console.WriteLine("Handle Errored!!!!");
                });
            using (var usbForm = SetupUsbForm(mockedHandleFactory))
            {
                mockedHandleFactory.Setup(x => x.CreateCreatedHandle(It.IsAny<IntPtr>())).Throws<InvalidOperationException>();

                // ACT
                using (usbForm.Run().SubscribeSafe(observer))
                {
                }
            }

            // ASSERT
            Assert.False(handleCreated);
            Assert.True(error);
        }

        /// <summary>
        /// Tests run_ when exception thrown in create destroyed handle_ then error is not caught.
        /// </summary>
        [Fact]
        public void Run_WhenExceptionThrownInCreateDestroyedHandle_ThenErrorIsNotCaught()
        {
            // ARRANGE
            var handleCreated = false;
            var error = false;
            var mockedHandleFactory = new Mock<IHandleFactory>();
            var observer = Observer.Create<IHandle>(
                next =>
                {
                    handleCreated = true;
                    Console.WriteLine("Handle Created!!!!");
                },
                exception =>
                {
                    error = true;
                    Console.WriteLine("Handle Errored!!!!");
                });
            using (var usbForm = SetupUsbForm(mockedHandleFactory))
            {
                mockedHandleFactory.Setup(x => x.CreateDestroyedHandle(It.IsAny<IntPtr>())).Throws<InvalidOperationException>();

                // ACT
                using (usbForm.Run().SubscribeSafe(observer))
                {
                }
            }

            // ASSERT
            Assert.True(handleCreated);
            
            // This is false due to the observer already being stopped before we can observe any errors, as the dispose occurs which calls the CreateDestroyedHandle.
            Assert.False(error);
        }

        /// <summary>
        /// The setup usb form.
        /// </summary>
        /// <param name="mockedHandleFactory">
        /// The mocked Handle Factory.
        /// </param>
        /// <returns>
        /// The <see cref="UsbForm"/>.
        /// </returns>
        private UsbForm SetupUsbForm(Mock<IHandleFactory> mockedHandleFactory = null)
        {
            var mockedHandleFactoryToUse = mockedHandleFactory ?? new Mock<IHandleFactory>();
            var usbForm = new UsbForm(mockedHandleFactoryToUse.Object);
            var handle = new IntPtr(42);
            mockedHandleFactoryToUse.Setup(x => x.CreateCreatedHandle(It.IsAny<IntPtr>())).Returns(() => new CreatedHandle(handle, new Mock<IDeviceNotifications>().Object));
            return usbForm;
        }
    }
}
