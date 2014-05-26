// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Spdrp.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The spdrp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceManagement.UnsafeNative
{
    /// <summary>
    /// The spdrp.
    /// </summary>
    internal enum Spdrp : uint
    {
        /// <summary>
        /// The deviceDesc.
        /// </summary>
        DeviceDesc = 0x00000000,
        HARDWAREID = 0x00000001,
        COMPATIBLEIDS = 0x00000002,
        NTDEVICEPATHS = 0x00000003,
        SERVICE = 0x00000004,
        CONFIGURATION = 0x00000005,
        CONFIGURATIONVECTOR = 0x00000006,
        CLASS = 0x00000007,
        CLASSGUID = 0x00000008,
        DRIVER = 0x00000009,
        CONFIGFLAGS = 0x0000000A,

        /// <summary>
        /// The mfg.
        /// </summary>
        Mfg = 0x0000000B,
        FRIENDLYNAME = 0x0000000C,
        LOCATION_INFORMATION = 0x0000000D,
        PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,
        CAPABILITIES = 0x0000000F,
        UI_NUMBER = 0x00000010,
        UPPERFILTERS = 0x00000011,
        LOWERFILTERS = 0x00000012,
        MAXIMUM_PROPERTY = 0x00000013,

        ENUMERATOR_NAME = 0x16,
    };
}