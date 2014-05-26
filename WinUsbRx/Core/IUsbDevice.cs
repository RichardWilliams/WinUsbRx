// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDevice.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUsbDevice type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core
{
    using System;

    /// <summary>
    /// The UsbDevice interface.
    /// </summary>
    internal interface IUsbDevice : IDisposable
    {
        /// <summary>
        /// Gets the path of this device.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The write.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        void Write(byte[] data);

        /// <summary>
        /// The read.
        /// </summary>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        byte[] Read();
    }
}