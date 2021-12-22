using Core.Converters;
using Core.Entities.Models.Columns;
using Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Models
{
    public readonly struct Location : IEntity, IComparable<City>
    {
        internal readonly Country _country;    // название страны (случайная строка с префиксом "cou_")  [8]; 
        internal readonly Region _region;         // название области (случайная строка с префиксом "reg_") [12];
        internal readonly Postal _postal;      // почтовый индекс (случайная строка с префиксом "pos_") [12]; 
        internal readonly City _city;        // название города (случайная строка с префиксом "cit_") [24]; 
        internal readonly Organization _organization;  // название организации (случайная строка с префиксом "org_") [32];
        public readonly float Latitude { get; }     // широта
        public readonly float Longitude { get; }   // долгота
        public City City => _city;
        public char[] CityAsChars => Converter.MapObjectToCharArray(_city);
        public sbyte[] CityAsSbytes => Converter.MapObjectToSbyteArray(_city);            
        public int CompareTo(City cityName) => City.CompareTo(cityName);        

        //public int CompareTo(City cityName)
        //{
        //    var index = 0;
        //    var queryInSbytes = Array.ConvertAll(cityName, new Converter<char, sbyte>(Converter.ConvertCharToSbyte)); 
        //    var cityInSbytes = Converter.MapObjectToSbyteArray(_city);
        //    foreach (var cityByte in cityInSbytes)
        //    {
        //        var compareResult = cityByte.CompareTo(queryInSbytes[index]);
        //        if (compareResult != 0)
        //        {
        //            return compareResult;
        //        }
        //        index++;
        //    }
        //    return 0;
        //}

    }
}
