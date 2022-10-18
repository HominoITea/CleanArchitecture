using Infrastructure.Readers;
using Infrastructure.Middleware;
using System;
using System.Diagnostics;
using System.IO;
using BenchmarkDotNet.Attributes;
using Core.Converters;
using Core.Entities.Models;
using Core.Entities.Models.Columns;
using Core.Exceptions;
using Infrastructure.Db;
using Infrastructure.Services.Search;
using Xunit;

namespace UnitTests
{
    public class DataLoading
    {
        public FileContextOptions Options { get; private set; }
        private void SetupData()
        {
            var path = @"C:\Users\Kair\Source\Repos\MetaQuotes\Infrastructure\Db\geobase.dat";
            var data = File.ReadAllBytes(path) ?? throw new FileNotFoundException($"File {path} not found");
            var buffer = new DbBuffer().Set(data);

            Options = new FileContextOptions
            {
                Buffer = buffer,
                Reader = new FileByteReader().Set(buffer)
            };

        }

        [Fact]
        public FileContext TestDataLoading()
        {
            SetupData();

            var dataContext = new FileContext(Options);

            Assert.NotNull(dataContext);

            return dataContext;
        }
        [Fact]
        public void GetCitiesByNameTest()
        {
            var stopwatch = Stopwatch.StartNew();
            var context = TestDataLoading();

            stopwatch.Stop();
            Trace.WriteLine($"Data loaded in {stopwatch.ElapsedMilliseconds} ms.");
            stopwatch.Reset();

            stopwatch.Start();

            var searchComponents = new SearchComponents<City, Location, CityIndex>(context.Index.AsReadOnlySpan(),
                context.Locations.AsReadOnlySpan(), Location.SizeOf);
            var foundIndeces = IndexLocator.RangeSearch(
                searchComponents,
                Map.SbyteArrayToObject<City>(Array.ConvertAll("cit_Yzyrinorijyqotok".ToCharArray(),
                    Convert.ToSByte))); //99987 99999
            var foundIndeces1 = IndexLocator.RangeSearch(
                searchComponents,
                Map.SbyteArrayToObject<City>(Array.ConvertAll("cit_Ixiluza".ToCharArray(),
                    Convert.ToSByte))); //49994 50004
            var foundIndeces2 = IndexLocator.RangeSearch(
                searchComponents,
                Map.SbyteArrayToObject<City>(Array.ConvertAll("cit_E ".ToCharArray(), Convert.ToSByte))); //16872 17017
            var foundIndeces3 = IndexLocator.RangeSearch(
                searchComponents,
                Map.SbyteArrayToObject<City>(Array.ConvertAll("cit_A".ToCharArray(), Convert.ToSByte))); //0 158

            stopwatch.Stop();
            Trace.WriteLine($"Data found in {stopwatch.ElapsedMilliseconds} ms.");
            stopwatch.Reset();

            Assert.Equal(foundIndeces, (99987, 99999));
            Assert.Equal(foundIndeces1, (49994, 50004));
            Assert.Equal(foundIndeces2, (16872, 17017));
            Assert.Equal(foundIndeces3, (0, 158));

        }
    }
}
