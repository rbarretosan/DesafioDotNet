using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using DesafioDotNet.Application;
using DesafioDotNet.Application.Contracts;
using DesafioDotNet.Persistence;
using DesafioDotNet.Persistence.Contexts;
using DesafioDotNet.Persistence.Contracts;

namespace DesafioDotNet.API.ADO
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
            services.AddDbContext<DesafioDotNetContext>(
                context => context.UseSqlServer(Configuration.GetConnectionString("Default"))
            );
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
         
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductServiceEF, ProductServiceEF>();
            services.AddScoped<IGenericPersistEF, GenericPersistEF>();
            services.AddScoped<IProductPersist, ProductPersist>();
            services.AddScoped<IProductPersistEF, ProductPersistEF>();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioDotNet.API com ADO.Net", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioDotNet.API.ADO v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
