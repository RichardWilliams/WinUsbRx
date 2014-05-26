// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceNotifications.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The device management.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using Handle;
    using UnsafeNativeMethods;
    using Wrappers;

    /// <summary>
    /// The device management.
    /// </summary>
    internal class DeviceNotifications : IDeviceNotifications
    {
        /// <summary>
        /// The device notify window handle.
        /// </summary>
        private const uint DeviceNotifyWindowHandle = 0x00000000;

        /// <summary>
        /// The _device management structure factory.
        /// </summary>
        private readonly IBroadcastDeviceInterfaceFactory _broadcastDeviceInterfaceFactory;

        /// <summary>
        /// The _marshall wrapper.
        /// </summary>
        private readonly IMarshalWrapper _marshallWrapper;

        /// <summary>
        /// The _unsafe native methods wrapper.
        /// </summary>
        private readonly IUnsafeNativeMethodsWrapper _unsafeNativeMethodsWrapper;

        /// <summary>
        /// The _process handle result factory.
        /// </summary>
        private readonly IProcessHandleResultFactory _processHandleResultFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceNotifications"/> class.
        /// </summary>
        /// <param name="broadcastDeviceInterfaceFactory">
        /// The broadcast Device Interface Factory.
        /// </param>
        /// <param name="marshallWrapper">
        /// The marshall wrapper.
        /// </param>
        /// <param name="unsafeNativeMethodsWrapper">
        /// The unsafe native methods wrapper.
        /// </param>
        /// <param name="processHandleResultFactory">
        /// The process handle result factory.
        /// </param>
        public DeviceNotifications(
            IBroadcastDeviceInterfaceFactory broadcastDeviceInterfaceFactory, 
            IMarshalWrapper marshallWrapper, 
            IUnsafeNativeMethodsWrapper unsafeNativeMethodsWrapper,
            IProcessHandleResultFactory processHandleResultFactory)
        {
            _broadcastDeviceInterfaceFactory = broadcastDeviceInterfaceFactory;
            _marshallWrapper = marshallWrapper;
            _unsafeNativeMethodsWrapper = unsafeNativeMethodsWrapper;
            _processHandleResultFactory = processHandleResultFactory;
        }

        /// <summary>
        /// The register for device notifications.
        /// </summary>
        /// <param name="windowHandleToReceiveNotifications">
        /// The window handle to receive notifications.
        /// </param>
        /// <returns>
        /// The <see cref="ProcessHandleResult"/>.
        /// </returns>
        public IProcessHandleResult Register(IntPtr windowHandleToReceiveNotifications)
        {
            var devBroadcastDeviceInterface = _broadcastDeviceInterfaceFactory.CreateBroadcastDeviceInterface();
            var devBroadcastDeviceInterfaceBuffer = IntPtr.Zero;

            try
            {
                devBroadcastDeviceInterfaceBuffer = _marshallWrapper.AllocHGlobal(devBroadcastDeviceInterface.Size);
                _marshallWrapper.StructureToPointer(devBroadcastDeviceInterface, devBroadcastDeviceInterfaceBuffer, false);
                var deviceNotificationHandle = _unsafeNativeMethodsWrapper.RegisterDeviceNotification(
                                                                        windowHandleToReceiveNotifications,
                                                                        devBroadcastDeviceInterfaceBuffer,
                                                                        DeviceNotifyWindowHandle);

                return _processHandleResultFactory.Create(deviceNotificationHandle);
            }
            finally 
            {
                if (devBroadcastDeviceInterfaceBuffer != IntPtr.Zero)
                {
                    _marshallWrapper.FreeHGlobal(devBroadcastDeviceInterfaceBuffer);                    
                }
            }
        }

        /// <summary>
        /// The un register for device notifications.
        /// </summary>
        /// <param name="handleFromRegistration">
        /// The handle from registration.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        public IProcessHandleResult UnRegister(IntPtr handleFromRegistration)
        {
            var handle = _unsafeNativeMethodsWrapper.UnRegisterDeviceNotification(handleFromRegistration);
            return _processHandleResultFactory.Create(handle);
        }
    }
}