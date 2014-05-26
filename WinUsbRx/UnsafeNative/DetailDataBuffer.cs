//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="DetailDataBuffer.cs" company="None.">
////   TODO:
//// </copyright>
//// <summary>
////   Defines the DetailDataBuffer type.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace WinUsbRx.UnsafeNative
//{
//    using System;
//    using System.Runtime.InteropServices;

//    /// <summary>
//    /// The detail data buffer.
//    /// </summary>
//    internal class DetailDataBuffer : IDisposable
//    {
//        /// <summary>
//        /// The _details data buffer.
//        /// </summary>
//        private readonly IntPtr _detailsDataBuffer;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="DetailDataBuffer"/> class.
//        /// </summary>
//        /// <param name="size">
//        /// The size.
//        /// </param>
//        public DetailDataBuffer(int size)
//        {
//            _detailsDataBuffer = Marshal.AllocHGlobal(size);
//            Marshal.WriteInt32(_detailsDataBuffer, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);
//        }

//        /// <summary>
//        /// Gets the pointer.
//        /// </summary>
//        public IntPtr Pointer
//        {
//            get { return _detailsDataBuffer; }
//        }

//        /// <summary>
//        /// The dispose.
//        /// </summary>
//        public void Dispose()
//        {
//            if (_detailsDataBuffer != IntPtr.Zero)
//            {
//                Marshal.FreeHGlobal(_detailsDataBuffer);
//            }
//        }
//    }
//}