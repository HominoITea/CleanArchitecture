using Core.Entities;
using Core.Entities.Data;
using Infrastructure.Readers;
using System;

namespace Infrastructure.Data
{
    public class BufferedDbSet : IDisposable
    {
        private readonly BufferedReader _reader;
        public readonly GeoData Entity;
        public BufferedDbSet(BufferedReader reader)
        {
            _reader = reader;
            _reader.FillHeader();
            var header = SetHeader();
            var ranges = SetDbRows(header.Records, SetIpRange, 12);
            var locations = SetDbRows(header.Records, SetLocation, 96);
            Entity = new GeoData(header, ranges, locations);
        }
        public T[] SetDbRows<T>(int records, Func<T> delegateDbRow, int bytesInRow) where T : struct
        {
            T[] rows = new T[records];
            var index = 0;
            bool isContinue;
            do
            {
                isContinue = _reader.FillBuffer();
                foreach (var counter in new int[_reader.BytesAvailable / bytesInRow])
                {
                    if (index >= records)
                    {
                        isContinue = false;
                    }
                    else
                    {
                        rows[index++] = delegateDbRow();
                    }
                }
            } 
            while (isContinue);
            return rows;
        }

        public Header SetHeader() => new Header();
                            //version: _reader.ReadInt32(),
                            //name: _reader.ReadChars(32),
                            //timeStamp: _reader.ReadUInt64(),
                            //records: _reader.ReadInt32(),
                            //offsetRanges: _reader.ReadUInt32(),
                            //offsetCities: _reader.ReadUInt32(),
                            //offsetLocations: _reader.ReadUInt32());
        private IpRange SetIpRange() => new IpRange(
                            ipFrom: _reader.ReadUInt32(),
                            ipTo: _reader.ReadUInt32(),
                            locationIndex: _reader.ReadUInt32());
        private Location SetLocation() => new Location();
                            //country: _reader.ReadChars(8),
                            //region: _reader.ReadChars(12),
                            //postal: _reader.ReadChars(12),
                            //city: _reader.ReadChars(24),
                            //organization: _reader.ReadChars(32),
                            //latitude: _reader.ReadSingle(),
                            //longitude: _reader.ReadSingle());
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
