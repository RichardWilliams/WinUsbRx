// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMarshalWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the IMarshalWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Wrappers
{
    using System;

    /// <summary>
    /// The MarshalWrapper interface.
    /// </summary>
    internal interface IMarshalWrapper
    {
        /// <summary>
        /// The alloc h global.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        IntPtr AllocHGlobal(int size);

        /// <summary>
        /// The free h global.
        /// </summary>
        /// <param name="globalHandle">
        /// The global Handle.
        /// </param>
        void FreeHGlobal(IntPtr globalHandle);

        /// <summary>
        /// The structure to pointer.
        /// </summary>
        /// <param name="structure">
        /// The structure.
        /// </param>
        /// <param name="structurePointer">
        /// The structure Pointer.
        /// </param>
        /// <param name="deleteOld">
        /// The delete old.
        /// </param>
        void StructureToPointer(object structure, IntPtr structurePointer, bool deleteOld);

        /// <summary>
        /// The pointer to structure.
        /// </summary>
        /// <param name="pointer">
        /// The pointer.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="T">
        /// This is a class with default constructor.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T PointerToStructure<T>(IntPtr pointer, Type type) where T : class, new();

        /// <summary>
        /// The get last win 32 error.
        /// </summary>
        /// <returns>
        /// The <see cref="Win32ErrorWrapper"/>.
        /// </returns>
        Win32ErrorWrapper GetLastWin32Error();

        /// <summary>
        /// The write integer 32.
        /// </summary>
        /// <param name="pointer">
        /// The pointer.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void WriteInteger32(IntPtr pointer, int value);
    }
}