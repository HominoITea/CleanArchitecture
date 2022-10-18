using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces;

namespace Infrastructure.Repository
{
    public class SqlRepository<T> : IAsyncRepository<T> where T : IEntity
    {
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
