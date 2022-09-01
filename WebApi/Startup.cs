using Core.Entities;
using Core.Interfaces;
using Infrastructure.Readers;
using Infrastructure.Repository;
using Infrastructure.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Db;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });
            //services.AddOptions<>()

            //services.AddScoped(typeof(IAsyncRepository<>), typeof(BinaryRepository<>));

            //var buffer = File.ReadAllBytes(path) ?? throw new FileNotFoundException($"File {path} not found"); //надо прочитать, потом освободить память
            //services.AddScoped<IByteReader>( x => new BufferedByteReader(buffer)); //Создание ридера файла 
            //services.AddSingleton<GeoDataContext>(); //Загрузка DB в память/ 27 июня 2022 сделать интерфейс для контекста, чтоб можно было подменять разные контексты, но методы извлечения данных определялись интерфейсом и позволяли их менять. Вызов методов определить в репозиторий
            //services.Configure<DbBuffer>(options =>
            //{
            //    var path = Configuration.GetSection("FileDB").Value;
            //    options.Data = File.ReadAllBytes(path);
            //});

            services.AddFileContext<FileContext>(
                f =>
                {
                    f.UseFileDb<DbBuffer>(Configuration.GetSection("FileDB").Value);
                    f.UseByteReader<FileByteReader>();
                }, ServiceLifetime.Singleton);

            services.AddSingleton(typeof(IAsyncRepository<>), typeof(BinaryRepository<>));

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IpLocator API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(100)
                    };
                context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
                    new string[] { "Accept-Encoding" };
                await next();
            });

            app.UseSwagger(x => x.RouteTemplate = "1");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
