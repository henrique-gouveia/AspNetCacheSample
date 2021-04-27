using System.Text.Json;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using CacheSample.Domain.Tracks;
using CacheSample.Infrastructure.Data;
using CacheSample.Infrastructure.Repositories;

namespace CacheSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    //options.JsonSerializerOptions.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CacheSample", Version = "v1" });
            });


            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "CacheSample:";
            });

            services.AddDbContext<AppDbContext>(options => options
                .UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<TrackRepository>();

            services.AddTransient<ITrackRepository>(sp =>
            {
                var cache = sp.GetService<IMemoryCache>();
                var repository = sp.GetService<TrackRepository>();

                return new MemCacheTrackRepository(cache, repository);
            });

            //services.AddTransient<ITrackRepository>(sp =>
            //{
            //    var cache = sp.GetService<IDistributedCache>();
            //    var repository = sp.GetService<TrackRepository>();

            //    return new RedisCacheTrackRepository(cache, repository);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CacheSample v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
