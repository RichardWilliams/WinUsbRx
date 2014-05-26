// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDevicePropertyFactory.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   Defines the IDevicePropertyFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.DeviceProperties
{
    using UnsafeNative;

    /// <summary>
    /// The DevicePropertyFactory interface.
    /// </summary>
    internal interface IDevicePropertyFactory
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
        BaseDeviceProperty Create(RegistryType registryType, byte[] data);
    }
}