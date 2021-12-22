using Core.Entities;
using Core.Entities.Models;
using Core.Entities.DTOs;

using System;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Index = Core.Entities.Models.Index;
using Core.Interfaces;
using Core.Entities.Models.Columns;

using BenchmarkDotNet;
using Infrastructure.Services;
using Infrastructure.Repository.DbSet;
using Core.Converters;

namespace Infrastructure.Repository
{
    public class GeoDataContext : IDisposable
    {
        public BinaryDbHeader<Header> Header { get; }
        public BinaryDbRows<IpRange> IpRanges { get; }
        public BinaryDbRows<Location> Locations { get; }
        public BinaryDbRows<Index> Index { get; }

        public GeoDataContext(IByteReader byteReader)
        {
            var stopwatch = Stopwatch.StartNew();

            Header = new BinaryDbHeader<Header>(byteReader);

            IpRanges = new BinaryDbRows<IpRange>(byteReader, Header.Data.OffsetRanges, Header.Data.Records);
            Locations = new BinaryDbRows<Location>(byteReader, Header.Data.OffsetLocations, Header.Data.Records);
            Index = new BinaryDbRows<Index>(byteReader, Header.Data.OffsetCities, Header.Data.Records);

            stopwatch.Stop();
            Trace.WriteLine($"Data loaded in {stopwatch.ElapsedMilliseconds} ms.");
            
            Trace.WriteLine($"All loaded");

            //FileInfo file = new FileInfo("C:\\locations.txt");

            //using var writer = file.AppendText();
            //for(int i= 0; i < Locations.Rows.Length; i++)
            //{ 
            //    writer.WriteLine(new string(Locations[i].CityAsChars) + "\t"  + Locations[i].Longitude + "\t" + Locations[i].Latitude);
            //}

            //Predicate<City> city = (name) => { return true; };
            //Array.FindAll(Locations.Rows, x => x.CityAsChars == new char[5]);
            //var chars = "cit_Yxyz".ToCharArray();
            //var chars = "cit_Uxyz".ToCharArray();
            //var chars = "cit_Y Ah ".ToCharArray();
            //var chars = "cit_E ".ToCharArray();
            var chars = "cit_Ydymav ".ToCharArray();
            //var chars = "cit_Ygyz".ToCharArray();
            var cityArray = Converter.MapSbyteArrayToObject<City>(Array.ConvertAll(chars, Convert.ToSByte));

            stopwatch.Reset();
            stopwatch.Start();
            //CityComparer com = new CityComparer();
            //Array.Sort(Locations.Rows, com);
            ReadOnlySpan<Location> cities = Locations.Rows
                                                .OrderBy(x => x.City)                                                
                                                .ToArray();            
            stopwatch.Stop();
            Trace.WriteLine($"Array sorted in {stopwatch.ElapsedMilliseconds} ms.");

            //FileInfo file1 = new FileInfo("C:\\locationsOrdered.txt");
            //using var writer1 = file1.AppendText();
            //for (int i = 0; i < cities.Length; i++)
            //{
            //    writer1.WriteLine(new string(cities[i].CityAsChars) + "\t" + cities[i].Longitude + "\t" + cities[i].Latitude);
            //}

            stopwatch.Reset();
            stopwatch.Start();

            var lowerIndex = BinarySearcher.LowerBoundSearch(cities, cityArray);
            var upperIndex = BinarySearcher.UpperBoundSearch(cities, cityArray);

            stopwatch.Stop();
            Trace.WriteLine($"Data found in {stopwatch.ElapsedMilliseconds} ms.");

            var ipIndex = BinarySearcher.BinarySearch(cities, cityArray).middle; //My own binary search solution instead of .net implementation 

            //var tupple = BinarySearcher.BinarySearch(cities, sbytes);
        }

        //Predicate<char[]> getCity(string name)
        //{
        //    City cit = new City();
        //}
        //[BenchmarkDotNet.Attributes.Benchmark]
        public Task<T> FindAsync<T>(object[] keyValues) where T : class
        {           
            throw new NotImplementedException();
        }
        //public Dictionary<string, T> GetAll<T>() where T : struct
        //{ 
           
        //        var list = new Dictionary<string, Index>();
        //        for (int i = 0; i < Header.Data.Records; ++i)
        //        {
        //        IpRanges.Rows.Where(x => x.IpFrom )
        //            list.Add(Index.Rows[i].Offset.ToString(), location);
        //        }
        //        //Location.Data.ToDictionary(x => new string(x.City)) as Dictionary<string, T>;
        //        return list as Dictionary<string, T>;            
        //}

        //public List<TEntity> Get<TEntity>() where TEntity: IEntity<TEntity>
        //{

        //    var list = new EnumerableQuery<TEntity>(this.);
        //}
        //public Header GetHeader()
        //{           
        //    return Context.Entity.Header; 
        //}
        //public IpRange[] GetIpRange()
        //{
        //    return Context.Entity.IpRanges;
        //}
        public void Dispose()
        {
            //Dispose it.
        }
    }
}
