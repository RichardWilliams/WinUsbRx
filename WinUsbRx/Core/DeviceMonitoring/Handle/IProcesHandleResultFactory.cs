// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProcesHandleResultFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IProcessHandleResultFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;

    /// <summary>
    /// The ProcessHandleResultFactory interface.
    /// </summary>
    internal interface IProcessHandleResultFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="IProcessHandleResult"/>.
        /// </returns>
        IProcessHandleResult Create(IntPtr handle);
    }
}