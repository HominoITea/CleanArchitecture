using Core.Converters;
using Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.DTOs
{
    public class LocationsDTO : Entity
    {
        public Location[] DbLocations { get; }
        
        public LocationsDTO(Location[] dbLocations)
        {
            DbLocations = dbLocations;
        }
        public char[] City(int position) => Converter.MapObjectToCharArray(DbLocations[position]._city);
        //public char[] Country(int position) => Converter.SbytesToChars(DbLocations[position].Country);
        public char[] Organisation(int position) => Converter.MapObjectToCharArray(DbLocations[position]._organization);
        public char[] Postal(int position) => Converter.MapObjectToCharArray(DbLocations[position]._postal);
        public char[] Region(int position) => Converter.MapObjectToCharArray(DbLocations[position]._region);
        public float Latitude(int position) => DbLocations[position].Latitude;
        public float Longitude(int position) => DbLocations[position].Longitude;
    }
}
