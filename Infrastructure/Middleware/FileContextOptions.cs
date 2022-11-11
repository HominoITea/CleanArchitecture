﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Core.Interfaces;
using Infrastructure.Db;
using Infrastructure.Readers;
using Microsoft.Extensions.Options;

namespace Infrastructure.Middleware
{
    public class FileContextOptions
    {
        public IHeader Header { get; set; }
        public IByteReader Reader { get; set; }
        public IBuffer Buffer { get; set; }
        public static FileContextOptionsBuilder BuildOptions() => new FileContextOptionsBuilder();
    }
}
