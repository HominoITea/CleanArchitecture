using System;
using System.Runtime.InteropServices;

namespace Core.Converters
{
    public static class Map
    {
        public static char[] ObjectToCharArray<T>(in T source) where T : struct
        {
            return Array.ConvertAll(
                ObjectToSbyteArray(source),
                Convert.ToChar);
        }
        //public static sbyte[] CharArrayToSbytes(in char[] source)
        //{
        //    var sbytes = new sbyte[source.Length];
        //    for (var i = 0; i < sbytes.Length; i++)
        //    {
        //        sbytes[i] = Convert.ToSByte(source[i]);
        //    }
        //    return sbytes;
        //}

        //public static sbyte[] ObjectToSbyteArray<T>(in T source) where T : struct
        //{
        //    var fields = source.GetType().GetFields();
        //    var sbytes = new sbyte[fields.Length];
        //    var index = 0;
        //    foreach (var singleValue in fields)
        //    {
        //        if (singleValue.FieldType.Equals(typeof(sbyte)))
        //        {
        //            sbytes[index++] = (sbyte)singleValue.GetValue(source);
        //        } 
        //        else
        //        {
        //            throw new InvalidCastException();
        //        }
        //    }
        //    return sbytes;
        //}

        public static sbyte[] ObjectToSbyteArray<T>(in T source) where T : struct
        {
            const int startIndex = 0;
            var bytes = new byte[Marshal.SizeOf(source)];
            var pointer = Marshal.AllocHGlobal(bytes.Length);
            try
            {
                Marshal.StructureToPtr(source, pointer, true);
                Marshal.Copy(pointer, bytes, startIndex, bytes.Length);
                return MemoryMarshal.Cast<byte, sbyte>(bytes).ToArray();
            }
            finally
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

        //public static T SbyteArrayToObject<T>(sbyte[] source) where T : struct //надо переписать
        //{
        //    var targetType = typeof(T);
        //    if (!targetType.IsExplicitLayout)
        //    {
        //        throw new TypeLoadException();
        //    }
        //    var fields = targetType.GetFields();
        //    var targetObject = Activator.CreateInstance<T>();
        //    for (var i = 0; i < source.Length; i++)
        //    {
        //        fields[i].SetValue(targetObject, source[i]);
        //    }
        //    return targetObject;
        //}

        //public static unsafe T SbyteArrayToObject<T>(sbyte[] source) where T : struct
        //{
        //    fixed (sbyte* pointer = source)
        //    {
        //        var result = (T)Marshal.PtrToStructure(new IntPtr(pointer), typeof(T));
        //        return result;
        //    }
        //}
        public static T SbyteArrayToObject<T>(sbyte[] source) where T : struct
        {
            var handle = GCHandle.Alloc(source, GCHandleType.Pinned);
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }
    }
}
