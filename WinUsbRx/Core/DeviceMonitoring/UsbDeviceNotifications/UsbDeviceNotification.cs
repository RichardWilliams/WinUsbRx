// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbDeviceNotification.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UsbDeviceNotification type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.UsbDeviceNotifications
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The usb device notification.
    /// </summary>
    internal class UsbDeviceNotification : IUsbDeviceNotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsbDeviceNotification"/> class.
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
        public UsbDeviceNotification(Guid guid, Message message, string name)
        {
            Guid = guid;
            Message = message;
            Name = name;
            IsArrivalNotification = message.WParam.ToInt32() == DbtDeviceArrival;
        }

        /// <summary>
        /// Gets the guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is arrival notification.
        /// </summary>
        public bool IsArrivalNotification { get; private set; }

        /// <summary>
        /// Gets the DBT_DEVICEARRIVAL.
        /// </summary>
        private int DbtDeviceArrival
        {
            get { return 0x8000; }
        }
    }
}