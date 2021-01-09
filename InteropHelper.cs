using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace accEncodetTest
{
    public static class InteropHelper
    {
        public static IntPtr AllocObject(object obj, bool deleteOld = false)
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
            var current = ptr;
            Marshal.StructureToPtr(obj, current, deleteOld);
            return ptr;
        }

        public static IntPtr AllocObjectArray<T>(T[] array)
        {
            var total = array.Length * Marshal.SizeOf(typeof(T));
            var ptr = Marshal.AllocHGlobal(total);
            var current = ptr;
            foreach (var obj in array)
            {
                Marshal.StructureToPtr(obj, current, false);
                current = (IntPtr) ((long) current + Marshal.SizeOf(obj));
            }

            return ptr;
        }

        public static IntPtr AllocByteArray(byte[] bytes, bool deleteOld = false)
        {
            var ptr = Marshal.AllocHGlobal(bytes.Length);
            for (var index = 0; index < bytes.Length; index++)
            {
                Marshal.WriteByte(ptr, index, bytes[index]);
            }

            return ptr;
        }

        public static T ParsePtrToObject<T>(IntPtr ptr)
        {
            var obj = (T) Marshal.PtrToStructure(ptr, typeof(T));
            return obj;
        }

        public static T[] ParsePtrToObjectArray<T>(IntPtr ptr, int length)
        {
            var offset = 0;
            var list = new List<T>();
            var count = 0;

            while (count < length)
            {
                var current = (IntPtr) ((long) ptr + offset);
                var obj = ParsePtrToObject<T>(current);
                list.Add(obj);
                count++;

                offset += Marshal.SizeOf(obj);
            }

            return list.ToArray();
        }

        public static bool ParsePtrToByteArray(IntPtr ptr, int length, out byte[] bytes)
        {
            bytes = new byte[length];
            Marshal.Copy(ptr, bytes, 0, length);
            return true;
        }

        public static void FreeObject(IntPtr ptr, Type type)
        {
            Marshal.DestroyStructure(ptr, type);
        }

        public static void FreeObjectArray(IntPtr ptr, Type type, int size)
        {
            var offset = 0;
            for (var i = 0; i < size; i++)
            {
                var current = (IntPtr) ((long) ptr + offset);
                Marshal.DestroyStructure(current, type);
                offset += Marshal.SizeOf(type);
            }
        }
    }
}