// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsbDevice.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the UsbDevice type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using WinUsbRx.Core.DeviceMonitoring;

namespace WinUsbRx.Core
{
    using System;
    using System.Reactive.Linq;

    /// <summary>
    /// The usb device.
    /// </summary>
    internal class UsbDevice : IUsbDevice
    {
        /// <summary>
        /// The _usb device watcher.
        /// </summary>
        private readonly IUsbDeviceWatcher _usbDeviceWatcher;

        /// <summary>
        /// The _disposable for the subscription to the usbDeviceWatcher.
        /// </summary>
        private readonly IDisposable _disposable;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsbDevice"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="usbDeviceWatcher">
        /// The usb Device Watcher.
        /// </param>
        public UsbDevice(string path, IUsbDeviceWatcher usbDeviceWatcher)
        {
            _disposable = usbDeviceWatcher.ArrivedDeviceNotificationsOnly()
                                          .Select(x => x.Name)
                                          .Where(x => x == Path)
                                          .StartWith(path)
                                          .Subscribe(Connect, Error, Completed);
            _usbDeviceWatcher = usbDeviceWatcher;
            Path = path;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public void Write(byte[] data)
        {
        }

        /// <summary>
        /// The read.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>byte[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public byte[] Read()
        {
            return new byte[] { 0 };
        }

        /// <summary>
        /// The dispose, this will clean up the subscription to the usb device watcher.
        /// </summary>
        public void Dispose()
        {
            _disposable.Dispose();
        }

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        private void Connect(string path)
        {
        }

        /// <summary>
        /// The completed.
        /// </summary>
        private void Completed()
        {
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="exception">
        /// The obj.
        /// </param>
        private void Error(Exception exception)
        {
        }
    }
}