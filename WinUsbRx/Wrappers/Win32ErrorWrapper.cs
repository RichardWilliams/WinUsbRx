// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32ErrorWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the Win32ErrorWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Wrappers
{
    using System.ComponentModel;

    /// <summary>
    /// The win 32 error wrapper.
    /// </summary>
    internal class Win32ErrorWrapper
    {
        /// <summary>
        /// The error insufficient buffer constant in windows.
        /// </summary>
        private const int ErrorInsufficientBuffer = 122;

        /// <summary>
        /// The _win 32 error.
        /// </summary>
        private readonly int _win32Error;

        /// <summary>
        /// Initializes a new instance of the <see cref="Win32ErrorWrapper"/> class.
        /// </summary>
        /// <param name="win32Error">
        /// The win 32 error.
        /// </param>
        public Win32ErrorWrapper(int win32Error)
        {
            _win32Error = win32Error;
            Exception = new Win32Exception(win32Error);
        }

        /// <summary>
        /// Gets a value indicating whether is insufficient buffer.
        /// </summary>
        public bool IsInsufficientBuffer
        {
            get { return _win32Error == ErrorInsufficientBuffer; }
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get { return Exception.Message; }
        }

        /// <summary>
        /// Gets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess
        {
            get { return _win32Error == 0; }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Win32Exception Exception { get; private set; }

        /// <summary>
        /// The is error.
        /// </summary>
        /// <param name="errorToCheck">
        /// The error to check.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsError(int errorToCheck)
        {
            return errorToCheck == _win32Error;
        }
    }
}