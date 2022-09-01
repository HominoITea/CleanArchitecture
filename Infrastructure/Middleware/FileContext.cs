using Core.Converters;
using Core.Entities.Models;
using Core.Entities.Models.Columns;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Middleware.DbSet;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CityIndex = Core.Entities.Models.CityIndex;

namespace Infrastructure.Middleware
{
    public class FileContext : IContext
    {
        private BinaryDbHeader<Header> Header { get; }
        private BinaryDbRows<IpRange> IpRanges { get; }
        private BinaryDbRows<Location> Locations { get; }
        private BinaryDbRows<CityIndex> Index { get; }

        public FileContext(FileContextOptions options)
        {
            var stopwatch = Stopwatch.StartNew();
            using (options.Reader)
            {
                Header = new BinaryDbHeader<Header>(options.Reader);
                IpRanges = new BinaryDbRows<IpRange>(options.Reader, Header.IpRangeOffset, Header.Records);
                Locations = new BinaryDbRows<Location>(options.Reader, Header.LocationOffset, Header.Records);
                Index = new BinaryDbRows<CityIndex>(options.Reader, Header.CityIndexOffset, Header.Records);
            }
            stopwatch.Stop();
            Trace.WriteLine($"Data loaded in {stopwatch.ElapsedMilliseconds} ms.");
            Trace.WriteLine($"All loaded");

            //FileInfo file = new FileInfo("C:\\locations.txt");

            //using var writer = file.AppendText();
            //for (int i = 0; i < Locations.Rows.Length; i++)
            //{
            //    writer.WriteLine(new string(Locations[i].CityAsChars) + "\t" + Locations[i].Longitude + "\t" + Locations[i].Latitude);
            //}

            //Predicate<City> city = (name) => { return true; };
            //Array.FindAll(Locations.Rows, x => x.CityAsChars == new char[5]);
            var chars = new char[24];
            var chars1 = new char[24];
            var chars2 = new char[24];
            //var searchString = "cit_Yzuhurebatot"; //99931 99939
            var searchString = "cit_Yzyrinorijyqotok"; //99987 99999
            var searchString2 = "cit_Ixiluza"; //49994 50004
            //var searchString1 = "cit_E A"; //17018 17054
            //var searchString = "cit_Yz Tiza "; with whitespaces (sbyte=32)
            //var searchString1 = "cit_E "; //16872 17017
            var searchString1 = "cit_A"; //0 158
            //var chars = "cit_Yxyz".ToCharArray();
            //var chars = "cit_Az L Jor".ToCharArray();
            //var chars = "cit_Y Ah".ToCharArray();
            //var chars = "cit_Yzuhurebatot".ToCharArray();
            //var chars = "cit_Aq Xahi".ToCharArray();
            for (int i = 0; i < searchString.Length; i++)
            {
                chars[i] = searchString[i];
            }
            for (int i = 0; i < searchString1.Length; i++)
            {
                chars1[i] = searchString1[i];
            }for (int i = 0; i < searchString2.Length; i++)
            {
                chars2[i] = searchString2[i];
            }
            //var chars = "cit_Eviquxyjuzow".ToCharArray();  //75389
            //var chars = "cit_Yzuhurebatot".ToCharArray();  
            var cityArray = Map.SbyteArrayToObject<City>(Array.ConvertAll(chars, Convert.ToSByte));
            var cityArray1 = Map.SbyteArrayToObject<City>(Array.ConvertAll(chars1, Convert.ToSByte));
            var cityArray2 = Map.SbyteArrayToObject<City>(Array.ConvertAll(chars2, Convert.ToSByte));

            stopwatch.Reset();
            stopwatch.Start();
            //CityComparer com = new CityComparer();
            //Array.Sort(Locations.Rows, com);
            //var cities = Locations.AsSorted(x => x.City);
            //                                    //.Rows
            //                                    //.OrderBy(x => x.City)                                                
            //                                    //.ToArray();     
            ////RadixSort.Sort(Locations.Rows.ToArray(), 24);
            //stopwatch.Stop();
            //Trace.WriteLine($"Array sorted in {stopwatch.ElapsedMilliseconds} ms.");


            //stopwatch.Reset();
            //stopwatch.Start();
            //FileInfo file1 = new FileInfo("C:\\locationsOrdered.txt");
            //using var writer1 = file1.AppendText();
            //var dic = new Dictionary<uint, string>();
            //for (int i = 0; i < Index.AsReadOnlySpan().Length; i++)
            //{
            //    var index = (int)Index.AsReadOnlySpan()[i].Offset / 96;
            //    dic.Add(Index.AsReadOnlySpan()[i].Offset, $" \t{i}\t" + new string(Locations.AsReadOnlySpan()[index].CityAsChars));
            //    writer1.WriteLine($"{Index.AsReadOnlySpan()[i].Offset}\t{i}\t" + new string(Locations.AsReadOnlySpan()[index].CityAsChars));
            //}
            //writer1.Close();
            //stopwatch.Stop();
            //Trace.WriteLine($"Dictionary sorted in {stopwatch.ElapsedMilliseconds} ms.");
            //FileInfo file = new FileInfo("C:\\locations.txt");

            //using var writer = file.AppendText();
            //for (int i = 0; i < Locations.Rows.Length; i++)
            //{
            //    writer.WriteLine(new string(dic..CityAsChars) + "\t" + Locations[i].Longitude + "\t" + Locations[i].Latitude);
            //}

            try
            {
                stopwatch.Reset();
                stopwatch.Start();
                //var lower = BinarySearcher.LowerBound(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray, new Comparer().);
                var lowerIndex = BinarySearcher.LowerBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray);
                var upperIndex = BinarySearcher.UpperBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray);
                if (upperIndex < lowerIndex)
                {
                    throw new SearchNotFoundException();
                }
                var lowerIndex1 = BinarySearcher.LowerBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray1);
                var upperIndex1 = BinarySearcher.UpperBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray1);
                if (upperIndex1 < lowerIndex1)
                {
                    throw new SearchNotFoundException();
                }
                var lowerIndex2 = BinarySearcher.LowerBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray2);
                var upperIndex2 = BinarySearcher.UpperBoundSearch2(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf, cityArray2);
                if (upperIndex2 < lowerIndex2)
                {
                    throw new SearchNotFoundException();
                }
                stopwatch.Stop();
                Trace.WriteLine($"Data found in {stopwatch.ElapsedMilliseconds} ms.");
                stopwatch.Reset();
            }
            catch (SearchNotFoundException)
            {
                Trace.WriteLine("Object doesn't found");
            }

            //var ipIndex = BinarySearcher.BinarySearch(cities, cityArray).middle; //My own binary search solution instead of .net implementation 

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
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileContext()
        {
            Dispose(false);
        }
    }
}
