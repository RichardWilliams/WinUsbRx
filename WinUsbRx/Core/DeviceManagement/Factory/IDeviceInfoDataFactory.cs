// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeviceInfoDataFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IDeviceInfoDataFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.Factory
{
    using UnsafeNative;

    /// <summary>
    /// The DeviceInfoDataFactory interface.
    /// </summary>
    internal interface IDeviceInfoDataFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="DeviceInfoData"/>.
        /// </returns>
        DeviceInfoData Create();
    }
}