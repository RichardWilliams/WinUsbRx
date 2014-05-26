// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarshalWrapper.cs" company="None">
//   TODO:
// </copyright>
// <summary>
//   Defines the MarshalWrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinUsbRx.Wrappers
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The marshal wrapper.
    /// </summary>
    internal class MarshalWrapper : IMarshalWrapper
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
        public IntPtr AllocHGlobal(int size)
        {
            return Marshal.AllocHGlobal(size);
        }

        /// <summary>
        /// The free h global.
        /// </summary>
        /// <param name="globalHandle">
        /// The global Handle.
        /// </param>
        public void FreeHGlobal(IntPtr globalHandle)
        {
            Marshal.FreeHGlobal(globalHandle);
        }

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
        /// The delete Old.
        /// </param>
        public void StructureToPointer(object structure, IntPtr structurePointer, bool deleteOld)
        {
            Marshal.StructureToPtr(structure, structurePointer, deleteOld);
        }

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
        /// This is a class with a default constructor.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T PointerToStructure<T>(IntPtr pointer, Type type) where T : class, new()
        {
            return Marshal.PtrToStructure(pointer, type) as T;
        }

        /// <summary>
        /// The get last win 32 error.
        /// </summary>
        /// <returns>
        /// The <see cref="Win32ErrorWrapper"/>.
        /// </returns>
        public Win32ErrorWrapper GetLastWin32Error()
        {
            return new Win32ErrorWrapper(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// The write integer 32.
        /// </summary>
        /// <param name="pointer">
        /// The pointer.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void WriteInteger32(IntPtr pointer, int value)
        {
            Marshal.WriteInt32(pointer, value);
        }
    }
}