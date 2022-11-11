using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Infrastructure.Middleware.DbSet
{
    public abstract class BinaryDbSet
    {
        protected IByteReader Reader { get; }

        protected BinaryDbSet(in IByteReader reader)
        {
            Reader = reader ?? throw new NullReferenceException();
        }
        protected BinaryDbSet(in IByteReader reader, int offset, int rows)
        {
            Reader = reader ?? throw new NullReferenceException();
            if (offset == 0 || rows == 0)
            {
                throw new Exception();
            }
        }
    }
}
