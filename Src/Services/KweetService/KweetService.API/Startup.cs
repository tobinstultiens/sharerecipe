using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShareRecipe.Services.Common.API;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.KweetService.API.Application.Commands;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;
using ShareRecipe.Services.KweetService.Infrastructure;

namespace ShareRecipe.Services.KweetService.API
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
            services.AddConfigurations(Configuration);
            services.RegisterEasyNetQ(Configuration.GetValue<string>("RabbitMQ"));
            services.AddLogging(p => p.AddConsole());
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)),
                Assembly.GetAssembly(typeof(CreateKweetCommand)));
            services.AddScoped<IFactory<KweetContext>, KweetDatabaseFactory>();
            services.AddScoped<KweetContext>(p => p.GetRequiredService<IFactory<KweetContext>>().Create());
            services.AddScoped<IAggregateUnitOfWork>(p =>
                p.GetRequiredService<IFactory<KweetContext>>().Create());
            services.AddScoped<IKweetRepository, KweetRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "KweetService.API", Version = "v1"});
            });
            services.AddKeycloak(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, KweetContext kweetContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            kweetContext.Database.Migrate();
            
            var services = app.ApplicationServices.CreateScope().ServiceProvider;

            var lifeTime = services.GetService<IHostApplicationLifetime>();
            var bus = services.GetService<IBus>();
            lifeTime.ApplicationStarted.Register(() =>
            {
                var subscriber = new AutoSubscriber(bus, "Kweet");
                subscriber.Subscribe(Assembly.GetExecutingAssembly().GetTypes());
                subscriber.SubscribeAsync(Assembly.GetExecutingAssembly().GetTypes());
            });
            lifeTime.ApplicationStopped.Register(() => bus.Dispose());

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KweetService.API v1"));
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}