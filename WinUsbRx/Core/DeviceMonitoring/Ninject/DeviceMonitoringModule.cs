// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceMonitoringModule.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceMonitoringModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Ninject
{
    using Handle;
    using global::Ninject.Extensions.Factory;
    using global::Ninject.Modules;
    using UnsafeNativeMethods;
    using UsbDeviceNotifications;

    /// <summary>
    /// The device monitoring module.
    /// </summary>
    internal class DeviceMonitoringModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<IUsbDeviceNotification>().To<UsbDeviceNotification>();
            Bind<IUsbDeviceNotificationFactory>().ToFactory().InSingletonScope();

            Bind<IBroadcastDeviceInterfaceFactory>().ToFactory().InSingletonScope();
            Bind<IUnsafeNativeMethodsWrapper>().To<UnsafeNativeMethodsWrapper>().InSingletonScope();
            Bind<IProcessHandleResult>().To<ProcessHandleResult>();
            Bind<IProcessHandleResultFactory>().ToFactory().InSingletonScope();
            Bind<IDeviceNotifications>().To<DeviceNotifications>().InSingletonScope();
            
            Bind<IHandleFactory>().ToFactory().InSingletonScope();
            Bind<IUsbForm>().To<UsbForm>().InSingletonScope();
            Bind<IUsbDeviceWatcher>().To<UsbDeviceWatcher>().InSingletonScope();
        }
    }
}