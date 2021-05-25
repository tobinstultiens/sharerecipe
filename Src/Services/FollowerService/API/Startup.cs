using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShareRecipe.Services.Common.API;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.Follower.API.Application.Commands;
using ShareRecipe.Services.Follower.Domain;
using ShareRecipe.Services.Follower.Infrastructure;

namespace ShareRecipe.Services.Follower.API
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
            services.AddConfigurations(Configuration);
            services.RegisterEasyNetQ(Configuration.GetValue<string>("RabbitMQ"));
            services.AddLogging(p => p.AddConsole());
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)),
                Assembly.GetAssembly(typeof(CreateFollowerCommand)));
            services.AddScoped<IFactory<FollowerContext>, FollowerDatabaseFactory>();
            services.AddScoped<FollowerContext>(p => p.GetRequiredService<IFactory<FollowerContext>>().Create());
            services.AddScoped<IAggregateUnitOfWork>(p =>
                p.GetRequiredService<IFactory<FollowerContext>>().Create());
            services.AddScoped<IFollowerRepository, FollowerRepository>();
            services.AddControllers();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FollowerService.API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FollowerService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}