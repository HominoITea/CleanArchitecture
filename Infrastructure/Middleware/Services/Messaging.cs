using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Middleware.Services
{
    class Messaging
    {
        public static string MissingParametrizedConstructor<TType>() =>
            $"Missing constructor with params for type {typeof(TType)}";
    }
}
