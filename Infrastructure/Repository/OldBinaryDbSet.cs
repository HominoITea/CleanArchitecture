using Core.Entities;
using Core.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BinaryDbSet : IDisposable
    {
        private readonly BinaryReader _binaryReader;
        public GeoDataDTO Entity { get; private set; }

        public BinaryDbSet(BinaryReader binaryReader)
        {
            _binaryReader = binaryReader;
            
            Set();
        }
        public void Set()
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //Header header = SetHeader();
            //var ranges = SetIpRanges(header.Records, header.OffsetRanges);
            //var locations = SetLocations(header.Records, header.OffsetLocations);
            ////LoadBytes();
            ////Entity.Indices = SetIndices(Entity.Header.Records, Entity.Header.OffsetCities);
            //Entity = new GeoData(header, ranges, locations);
            //stopwatch.Stop();
            //Trace.WriteLine($"Database loaded in {stopwatch.ElapsedMilliseconds} ms.");
        }

        private int[] SetIndices(int records, long offset)
        {
            _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            int[] indices = new int[records];
            for (int i = 0; i < records; ++i)
            {
                indices[i] = _binaryReader.ReadInt32();                    
            }
            return indices;
        }

        public Task<IpRange[]> GetRangesAsync() => Task.Run(() => Entity.IpRanges);
        public HeaderDTO GetHeader() => Entity.Header;

        //public Header SetHeader()
        //{
        //    _binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
        //    return new Header(
        //        version: _binaryReader.ReadInt32(),
        //        name: _binaryReader.ReadChars(32),
        //        timeStamp: _binaryReader.ReadUInt64(),
        //        records: _binaryReader.ReadInt32(),
        //        offsetRanges: _binaryReader.ReadUInt32(),
        //        offsetCities: _binaryReader.ReadUInt32(),
        //        offsetLocations: _binaryReader.ReadUInt32());
        //}

        public IpRange[] SetIpRanges(int records, long offset)
        {
            _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);            
            IpRange[] ranges = new IpRange[records];
            for (int i = 0; i < records; ++i)
            {
                ranges[i] = new IpRange(
                    ipFrom: _binaryReader.ReadUInt32(),
                    ipTo: _binaryReader.ReadUInt32(),
                    locationIndex: _binaryReader.ReadUInt32());
            }
            return ranges;
        }
        public Location[] SetLocations(int records, long offset)
        {
            _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            Location[] locations = new Location[records];
            //for (int i = 0; i < records; ++i)
            //{
            //    locations[i] = new Location(
            //    country: _binaryReader.ReadChars(8),
            //    region: _binaryReader.ReadChars(12),
            //    postal: _binaryReader.ReadChars(12),
            //    city: _binaryReader.ReadChars(24),
            //    organization: _binaryReader.ReadChars(32),
            //    latitude: _binaryReader.ReadSingle(),
            //    longitude: _binaryReader.ReadSingle());
            //}
            return locations;
        }
        //public LocationBytes[] SetLocations(int records, long offset)
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();
        //    _binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
        //    LocationBytes[] locations = new LocationBytes[records];
        //    for (int i = 0; i < records; ++i)
        //    {
        //           locations[i] = new LocationBytes(
        //            country: _binaryReader.ReadBytes(8),
        //            region: _binaryReader.ReadBytes(12),
        //            postal: _binaryReader.ReadBytes(12),
        //            city: _binaryReader.ReadBytes(24),
        //            organization: _binaryReader.ReadBytes(32),
        //            latitude: _binaryReader.ReadBytes(4),
        //            longitude: _binaryReader.ReadBytes(4));
        //    }
        //    stopwatch.Stop();
        //    Trace.WriteLine($"Location loaded in {stopwatch.ElapsedMilliseconds} ms.");
        //    return locations.ToArray();            
        //}

        //public void LoadBytes()
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();
        //    byte[] bytes = File.ReadAllBytes("../Infrastructure/Db/geobase.dat");

        //    Location[] locations = new Location[bytes.Length/96];
        //    for (int i = 0; i < locations.Length; ++i)
        //    {
        //        var index = i * 96;
        //        locations[i] = new Location(
        //         country: bytes[(index)..(index + 7)],
        //         region: bytes[(index + 8)..(index + 19)],
        //         postal: bytes[(index + 20)..(index + 31)],
        //         city: bytes[(index + 32)..(index + 55)],
        //         organization: bytes[(index + 56)..(index + 87)],
        //         latitude: bytes[(index + 88)..(index + 91)],
        //         longitude: bytes[(index + 92)..(index + 95)]);
        //    }
        //    stopwatch.Stop();
        //    Trace.WriteLine($"Location#2 loaded in {stopwatch.ElapsedMilliseconds} ms.");
        //}


//        for (int i = 0; i<records; i++)
//            {
//                locations[i] = new Location
//                {
//                    Country = bytes.AsSpan(i, 8).ToArray(),
//                    Region = bytes.AsSpan(i + 8, 12).ToArray(),
//                    Postal = bytes.AsSpan(i + 20, 12).ToArray(),
//                    City = bytes.AsSpan(i + 32, 24).ToArray(),
//                    Organization = bytes.AsSpan(i + 56, 32).ToArray(),
//                    Latitude = bytes.AsSpan(i + 88, 4).ToArray(),
//                    Longitude = bytes.AsSpan(i + 92, 4).ToArray()
//                };
//}
public void Dispose()
        {
            _binaryReader.Dispose();
        }
    }
}
