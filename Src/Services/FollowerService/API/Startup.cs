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
using ShareRecipe.Services.Common.API.Startup;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.FollowerService.API.Application.Commands;
using ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserCreated;
using ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserDisplayNameUpdated;
using ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserImageUpdated;
using ShareRecipe.Services.FollowerService.Domain;
using ShareRecipe.Services.FollowerService.Infrastructure;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace ShareRecipe.Services.FollowerService.API
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
            //services.RegisterEasyNetQ(Configuration.GetValue<string>("RabbitMQ"));
            services.AddSingleton<IBus>(RabbitHutch.CreateBus(Configuration.GetValue<string>("RabbitMQ")));
            services.AddSingleton<AutoSubscriberMessageDispatcher>();
            services.AddSingleton<AutoSubscriber>(provider =>
            {
                var subscriber = new AutoSubscriber(provider.GetRequiredService<IBus>(), "Follower")
                {
                    AutoSubscriberMessageDispatcher = provider.GetRequiredService<AutoSubscriberMessageDispatcher>()
                };
                return subscriber;
            });
            
            services.AddScoped<CreatedUserIntegrationHandler>();
            services.AddScoped<UpdatedUserDisplayNameIntegrationHandler>();
            services.AddScoped<UpdatedUserImageIntegrationHandler>();
            
            services.AddLogging(p => p.AddConsole());
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)),
                Assembly.GetAssembly(typeof(CreatedFollowerCommand)));
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FollowerContext followerContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FollowerService.API v1"));
            }
            
            followerContext.Database.Migrate();
            
            // var services = app.ApplicationServices.CreateScope().ServiceProvider;
            //
            // var lifeTime = services.GetService<IHostApplicationLifetime>();
            // var bus = services.GetService<IBus>();
            // lifeTime.ApplicationStarted.Register(() =>
            // {
            //     var subscriber = new AutoSubscriber(bus, "Follower");
            //     subscriber.Subscribe(Assembly.GetExecutingAssembly().GetTypes());
            //     //subscriber.SubscribeAsync(Assembly.GetExecutingAssembly().GetTypes());
            // });
            // lifeTime.ApplicationStopped.Register(() => bus.Dispose());

            app.ApplicationServices.GetRequiredService<AutoSubscriber>().SubscribeAsync(Assembly.GetExecutingAssembly().GetTypes());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}