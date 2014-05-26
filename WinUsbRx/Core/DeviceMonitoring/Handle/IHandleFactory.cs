// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHandleFactory.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   The HandleFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core.DeviceMonitoring.Handle
{
    using System;

    /// <summary>
    /// The HandleFactory interface.
    /// </summary>
    internal interface IHandleFactory
    {
        /// <summary>
        /// The create created handle.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="CreatedHandle"/>.
        /// </returns>
        CreatedHandle CreateCreatedHandle(IntPtr handle);

        /// <summary>
        /// The create destroyed handle.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="DestroyedHandle"/>.
        /// </returns>
        DestroyedHandle CreateDestroyedHandle(IntPtr handle);
    }
}