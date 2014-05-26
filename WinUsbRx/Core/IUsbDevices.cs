// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDevices.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUsbDevices type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core
{
    using System;

    /// <summary>
    /// The UsbDevices interface.
    /// </summary>
    internal interface IUsbDevices
    {
        /// <summary>
        /// The devices.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     .
        /// </returns>
        IObservable<IUsbDevice> Devices();
    }
}