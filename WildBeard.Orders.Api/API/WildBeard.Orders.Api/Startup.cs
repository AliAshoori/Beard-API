using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Buffers;
using System.Linq;
using System.Reflection;
using WildBeard.Orders.Api.Utils;
using WildBeard.Orders.ApplicationServices;
using WildBeard.Orders.ApplicationServices.RequestHandlers;
using WildBeard.Orders.InfraServices;

namespace WildBeard.Orders.Api
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
            services.AddDbContext<AppDbContext>();
            services.RegisterApplicationServices();
            services.RegisterRepositoryServices();
            services.AddScoped<ValidateMediaTypeAttribute>();

            services.Configure<ApiConfigsOptions>(Configuration.GetSection(ApiConfigsOptions.ApiConfigs));

            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidateMediaTypeAttribute());
                //config.ReturnHttpNotAcceptable = true;
                //config.RespectBrowserAcceptHeader = true;
                config.OutputFormatters.Add(new HateosOutputFormatter(new Newtonsoft.Json.JsonSerializerSettings(), ArrayPool<char>.Create(), config));

            }).AddNewtonsoftJson().AddFluentValidation(s => s.RegisterValidatorsFromAssembly(typeof(PlaceNewOrderRequestHandler).GetTypeInfo().Assembly));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "WildBeard Order API",
                    Description = "A stand-alone RESTful set of APIs to deal with placing orders for an imaginary shop called Beard Shop.",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "ali-ashoori@outlook.com",
                        Name = "Ali Ashoori",
                        Url = new System.Uri("https://linkedin.com/in/aliashoori")
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            dbContext.Database.Migrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c => { c.SerializeAsV2 = true; });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "WildBeard-Order-API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
