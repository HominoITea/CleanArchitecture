using Core.Interfaces;
using System;

namespace Infrastructure.Repository
{
    public class BinaryRepository<T> : IAsyncRepository<T> where T: IEntity
    {       
        private readonly IContext _context;
        public BinaryRepository(IContext context)
        {
            _context = context;
        }

        //public async Task<int> GetRecordsCountAsync()
        //{
        //    return await Task.Run(() => _context.Header.Data.Records);
        //} 

        public void Find<T>()
        {
           // _context.
        }

        public void FindByIp(byte[] ip)
        {
           // _context.IpRanges
        }

        public void FindByCity(string cityName)
        {
            //Location[] locations = _context.Locations.ToArray();
            //locations.
        }

        public void BinarySeacrh()
        {

        }

        //public T GetData()
        //{
        //    return _context.Context.Entity as T;Hello how are you? Is everything fine, I hope
        //}

        //public T GetHeader ()
        //{
        //    return _context.GetHeader() as T;
        //}

        //public T GetIpRange()
        //{
        //    return _context.GetIpRange() as T;
        //}

        //public IAsyncEnumerable<T> ListAllAsync()
        //{
        //    _context.GetAll
        //}

        //public Dictionary<string, TStruct> GetData<TStruct>() where TStruct : struct
        //{
            //return _context.GetAll<TStruct>();
        //}

        public T GetHeader()
        {
            throw new NotImplementedException();
        }

        public T GetIpRange()
        {
            throw new NotImplementedException();
        }
    }
}
