// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryType.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   Defines the RegistryType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    /// <summary>
    /// The registry type.
    /// </summary>
    internal enum RegistryType
    {
        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// The registry sz type.
        /// </summary>
        Sz = 1,

        /// <summary>
        /// The registry multi sz.
        /// </summary>
        MultiSz = 7,
    }
}