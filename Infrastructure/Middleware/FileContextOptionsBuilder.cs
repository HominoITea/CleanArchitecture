using System;
using System.IO;
using Core.Interfaces;

namespace Infrastructure.Middleware
{
    public class FileContextOptionsBuilder
    {
        private FileContextOptions Options { get; }
        public FileContextOptionsBuilder() => Options = new FileContextOptions();

        public void UseByteReader<TReader>() where TReader : IByteReader, new() =>
            Options.Reader = new TReader()
                .Set(Options.Buffer) 
                             ?? throw new NullReferenceException();

        public void UseFileDb<TBuffer>(string path) where TBuffer: IBuffer, new() => 
            Options.Buffer = new TBuffer()
                .Set(File.ReadAllBytes(path));

        public void UseHeader<THeader>() where THeader : IHeader => 
        Options.Header = 

        public static implicit operator FileContextOptions(FileContextOptionsBuilder builder) => builder.Options;
    }
}
