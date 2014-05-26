// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DevicePropertyFactory.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   Defines the DevicePropertyFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    using UnsafeNative;

    /// <summary>
    /// The device property factory.
    /// </summary>
    internal class DevicePropertyFactory : IDevicePropertyFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="registryType">
        /// The registry type.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="BaseDeviceProperty"/>.
        /// </returns>
        public BaseDeviceProperty Create(RegistryType registryType, byte[] data)
        {
            switch (registryType)
            {
                case RegistryType.Unknown:
                    return new DeviceNullProperty(data);

                case RegistryType.Sz:
                    return new DeviceStringProperty(data);

                case RegistryType.MultiSz:
                    return new DeviceMultiStringProperty(data);

                default:
                    return new DeviceNullProperty(data);
            }
        }
    }
}