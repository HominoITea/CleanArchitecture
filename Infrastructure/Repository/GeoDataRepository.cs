using Core.Entities;
using Core.Entities.DTOs;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GeoDataRepository<T> : BinaryRepository<T> where T: struct
    {
        public GeoDataRepository(GeoDataContext context) : base(context)
        {

        }
        public async Task<int> GetRecordsCountAsync() => await Task.Run(() => _context.Header.Data.Records);

        //public T GetHeader ()
        //{
        //    return _context.GetHeader() as T;
        //}
        //public int BinarySearch()
        //{

        //}

        //public T[] SortBy<T>(uint offset) where T:
        //{
        //    T.
        //}
    }
}
