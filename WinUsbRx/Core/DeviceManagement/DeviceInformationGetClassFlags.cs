// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeviceInformationGetClassFlags.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The device information get class flags.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement
{
    using System;

    /// <summary>
    /// The device information get class flags.
    /// </summary>
    [Flags]
    internal enum DeviceInformationGetClassFlags : uint
    {
        /// <summary>
        /// The default.
        /// </summary>
        Default = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE

        /// <summary>
        /// The present.
        /// </summary>
        Present = 0x00000002,

        /// <summary>
        /// The all classes.
        /// </summary>
        AllClasses = 0x00000004,

        /// <summary>
        /// The profile.
        /// </summary>
        Profile = 0x00000008,

        /// <summary>
        /// The device interface.
        /// </summary>
        DeviceInterface = 0x00000010,
    }
}