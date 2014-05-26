// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDeviceNotificationFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUsbDeviceNotificationFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UsbDeviceNotifications
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The UsbDeviceNotificationFactory interface.
    /// </summary>
    internal interface IUsbDeviceNotificationFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="IUsbDeviceNotification"/>.
        /// </returns>
        IUsbDeviceNotification Create(Guid guid, Message message, string name);
    }
}