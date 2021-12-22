using Core.Entities.Models.Columns;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class CityComparer : IComparer<City>
    {
        public int Compare(City x, City y)
        {
            return x.CompareTo(y);
        }
    }
}
