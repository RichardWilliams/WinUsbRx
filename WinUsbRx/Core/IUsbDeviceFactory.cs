// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUsbDeviceFactory.cs" company="None.">
//   TODO:
// </copyright>
// <summary>
//   The UsbDeviceFactory interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Core
{
    /// <summary>
    /// The UsbDeviceFactory interface.
    /// </summary>
    internal interface IUsbDeviceFactory
    {
        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IUsbDevice"/>.
        /// </returns>
        IUsbDevice Create(string path);
    }
}