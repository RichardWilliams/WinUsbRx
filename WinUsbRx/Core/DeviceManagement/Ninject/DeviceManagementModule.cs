// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceManagementModule.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the DeviceManagementModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using WinUsbRx.Core.DeviceManagement.DeviceProperties;

namespace WinUsbRx.Core.DeviceManagement.Ninject
{
    using Factory;
    using global::Ninject.Extensions.Factory;
    using global::Ninject.Modules;
    using UnsafeNative;

    /// <summary>
    /// The device management module.
    /// </summary>
    internal class DeviceManagementModule : NinjectModule
    {
        /// <summary>
        /// The load.
        /// </summary>
        public override void Load()
        {
            Bind<IDeviceInformationElementFactory>().ToFactory().InSingletonScope();
            Bind<IDeviceInfoDataFactory>().ToFactory().InSingletonScope();
            Bind<IDeviceInterfaceDataFactory>().ToFactory().InSingletonScope();
            Bind<IDeviceInterfaceFactory>().ToFactory().InSingletonScope();
            Bind<IDeviceInterfaceDetailFactory>().ToFactory().InSingletonScope();
            Bind<IDevicePropertyFactory>().ToFactory().InSingletonScope();


            Bind<IUnsafeNativeMethodsWrapper>().To<UnsafeNativeMethodsWrapper>().InSingletonScope();
            Bind<DeviceInformationSet>().ToSelf().InSingletonScope();
        }
    }
}