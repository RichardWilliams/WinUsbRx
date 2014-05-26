// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDeviceNotification.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUsbDeviceNotification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UsbDeviceNotifications
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The UsbDeviceNotification interface.
    /// </summary>
    internal interface IUsbDeviceNotification
    {
        /// <summary>
        /// Gets the guid.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        Message Message { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether is arrival notification.
        /// </summary>
        bool IsArrivalNotification { get; }
    }
}