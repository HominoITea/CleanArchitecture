using Infrastructure.Readers;
using Infrastructure.Repository;
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
            var dbReader = new BufferedByteReader(buffer);
            var dataContext = new GeoDataContext(dbReader);
            Assert.NotNull(dataContext);
        }
    }
}
