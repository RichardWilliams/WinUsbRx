// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbForm.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IUsbForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring
{
    using System;
    using Handle;

    /// <summary>
    /// The UsbForm interface.
    /// </summary>
    internal interface IUsbForm : IDisposable
    {
        /// <summary>
        /// The run.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IObservable</cref>
        ///     </see>
        ///     to when the handle is created.
        /// </returns>
        IObservable<IHandle> Run();
    }
}