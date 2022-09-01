using Infrastructure.Readers;
using Infrastructure.Middleware;
using System;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class DataLoading
    {
        [Fact]
        public void TestDataLoading()
        {
            var path = @"C:\Users\Kair\Source\Repos\MetaQuotes\Infrastructure\Db\geobase.dat";
            var buffer = File.ReadAllBytes(path) ?? throw new FileNotFoundException($"File {path} not found");
            //var dbReader = new BufferedByteReader(buffer);
            //var dataContext = new FileContext(dbReader);
            //Assert.NotNull(dataContext);
        }
    }
}
