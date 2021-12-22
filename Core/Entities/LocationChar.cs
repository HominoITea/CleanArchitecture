using System;
using System.Collections.Generic;

using System.Text;

namespace Core.Entities
{
    public readonly struct LocationChar
    {
        public char[] Country { get; }      // название страны (случайная строка с префиксом "cou_")  [8]; 
        public char[] Region { get; }         // название области (случайная строка с префиксом "reg_") [12];
        public char[] Postal { get; }       // почтовый индекс (случайная строка с префиксом "pos_") [12]; 
        public char[] City { get; }         // название города (случайная строка с префиксом "cit_") [24]; 
        public char[] Organization { get; }   // название организации (случайная строка с префиксом "org_") [32];
        public Single Latitude { get; }      // широта
        public Single Longitude { get; }    // долгота
        public LocationChar(char[] country, char[] region, char[] postal, char[] city, char[] organization, float latitude, float longitude)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));
            Region = region ?? throw new ArgumentNullException(nameof(region));
            Postal = postal ?? throw new ArgumentNullException(nameof(postal));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Organization = organization ?? throw new ArgumentNullException(nameof(organization));
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
