using Core.Entities.Models;
using Core.Interfaces;

namespace Infrastructure.Middleware.DbSet
{
    public class BinaryDbHeader<THeader> where THeader : struct, IHeader
    {
        private readonly THeader _data; 

        public BinaryDbHeader(in IByteReader reader)
        {
            _data = reader.HeaderToStruct<THeader>();
        }

        public int Records => _data.Records;

        public int GetOffset<TEntity>() where TEntity : struct, IEntity => new TEntity() switch
        {
            Location _ => _data.LocationOffset,
            IpRange _ => _data.IpRangeOffset,
            CityIndex _ => _data.CityIndexOffset,
            _ => throw new System.NotImplementedException()
        };

        public int LocationOffset => _data.LocationOffset;
        public int IpRangeOffset => _data.IpRangeOffset;
        public int CityIndexOffset => _data.CityIndexOffset;
    }
}