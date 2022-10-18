using Core.Converters;
using Core.Entities.Models;
using Core.Entities.Models.Columns;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Middleware.DbSet;
using Infrastructure.Services.Search;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Infrastructure.Middleware
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileContext : IContext
    {
        public BinaryDbHeader<Header> Header { get; private set; }
        public BinaryDbRows<IpRange> IpRanges { get; private set; }
        public BinaryDbRows<Location> Locations { get; private set; }
        public BinaryDbRows<CityIndex> Index { get; private set; }


        public FileContext(FileContextOptions options)
        {
            Initialize(options);
        }

        [Benchmark]
        private void Initialize(FileContextOptions options)
        {
            //var stopwatch = Stopwatch.StartNew();
            using (options.Reader)
            {
                Header = new BinaryDbHeader<Header>(options.Reader);
                IpRanges = new BinaryDbRows<IpRange>(options.Reader, Header.IpRangeOffset, Header.Records);
                Locations = new BinaryDbRows<Location>(options.Reader, Header.LocationOffset, Header.Records);
                Index = new BinaryDbRows<CityIndex>(options.Reader, Header.CityIndexOffset, Header.Records);
            }
            //stopwatch.Stop();
            //Trace.WriteLine($"Data loaded in {stopwatch.ElapsedMilliseconds} ms.");
            //Trace.WriteLine($"All loaded");
        }

        public Task<T> FindAsync<T>(object[] keyValues) where T : class
        {
            var searchComponents = new SearchComponents<City, Location, CityIndex>(Index.AsReadOnlySpan(), Locations.AsReadOnlySpan(), Location.SizeOf);
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
