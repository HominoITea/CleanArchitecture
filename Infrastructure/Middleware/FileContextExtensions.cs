using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Core.Interfaces;
using Infrastructure.Readers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Middleware
{
    public static class FileContextExtensions
    {
        public static void AddFileContext<TContext>([NotNull] this IServiceCollection serviceCollection,
            Action<FileContextOptionsBuilder> options, ServiceLifetime lifeTime) where TContext : IContext
        {
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(TContext),
                typeof(TContext), 
                lifeTime));

            serviceCollection.TryAdd(new ServiceDescriptor(typeof(FileContextOptions),
                p => BuildContextOptions(options),
                lifeTime));

            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IContext),
                p => p.GetRequiredService<TContext>(),
                lifeTime));
        }

        private static FileContextOptions BuildContextOptions(Action<FileContextOptionsBuilder> options)
        {
            var fileContextOptions = FileContextOptions.BuildOptions();
            options?.Invoke(fileContextOptions);
            return fileContextOptions;
        }
    }
}
