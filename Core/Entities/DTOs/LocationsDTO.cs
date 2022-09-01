using Core.Converters;
using Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.DTOs
{
    public class LocationsDto : Entity
    {
        public Location[] DbLocations { get; }
        
        public LocationsDto(Location[] dbLocations)
        {
            DbLocations = dbLocations;
        }
        //public char[] City(int position) => Map.ObjectToCharArray(DbLocations[position].City);
        ////public char[] Country(int position) => Converter.SbytesToChars(DbLocations[position].Country);
        //public char[] Organisation(int position) => Map.ObjectToCharArray(DbLocations[position].Organization);
        //public char[] Postal(int position) => Map.ObjectToCharArray(DbLocations[position].Postal);
        //public char[] Region(int position) => Map.ObjectToCharArray(DbLocations[position].Region);
        //public float Latitude(int position) => DbLocations[position].Latitude;
        //public float Longitude(int position) => DbLocations[position].Longitude;
    }
}
