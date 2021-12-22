using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAsyncRepository <T> : IDisposable where T : IEntity
    {
        //public Dictionary<string, TStruct> GetData<TStruct>() where TStruct : struct;
        public T GetHeader();
        public T GetIpRange();
        //public Task<T> GetHeaderAsync();
        //public Task<T> GetIpRangeAsync();
        //public IAsyncEnumerable<T> ListAllAsync();
        //Task<T> GetByIdAsync();
    }
}
