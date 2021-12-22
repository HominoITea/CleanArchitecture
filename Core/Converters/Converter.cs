using System;
using System.Runtime.InteropServices;

namespace Core.Converters
{
    public class Converter
    {
        public static char[] MapObjectToCharArray<T>(in T source) where T : struct 
            => Array.ConvertAll(MapObjectToSbyteArray(source), Convert.ToChar);
        //public static sbyte[] MapCharArrayToSbytes(in char[] source)
        //{
        //    var sbytes = new sbyte[source.Length];
        //    for (var i = 0; i < sbytes.Length; i++)
        //    {
        //        sbytes[i] = Convert.ToSByte(source[i]);
        //    }
        //    return sbytes;
        //}

        //public static sbyte[] MapObjectToSbyteArray<T>(in T source) where T : struct
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

        public static sbyte[] MapObjectToSbyteArray<T>(in T source) where T : struct
        {
            const int startIndex = 0;
            var size = Marshal.SizeOf(source);
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            try
            {                
                Marshal.StructureToPtr(source, ptr, true);
                Marshal.Copy(ptr, bytes, startIndex, size);
                return MemoryMarshal.Cast<byte, sbyte>(bytes).ToArray();                
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }          
        }
        public static T MapSbyteArrayToObject<T>(sbyte[] source) where T : struct //надо переписать
        {            
            var targetType = typeof(T);
            if (targetType.IsExplicitLayout)
            {
                var fields = targetType.GetFields();
                var targetObject = Activator.CreateInstance<T>();
                for (var i = 0; i < source.Length; i++)
                {
                    fields[i].SetValue(targetObject, source[i]);
                }
                return targetObject;
            } 
            else
            {
                throw new TypeLoadException();
            }             
        }
    }
}
