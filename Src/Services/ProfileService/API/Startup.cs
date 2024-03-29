using System.Reflection;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using ShareRecipe.Services.ProfileService.API.Application.Commands;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;
using ShareRecipe.Services.ProfileService.Infrastructure;

namespace ShareRecipe.Services.ProfileService.API
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
            services.AddHttpContextAccessor();
            services.RegisterEasyNetQ(Configuration.GetValue<string>("RabbitMQ"));
            services.AddLogging(p => p.AddConsole());
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)),
                Assembly.GetAssembly(typeof(CreateUserProfileCommand)));
            services.AddScoped<IFactory<UserProfileContext>, UserProfileDatabaseFactory>();
            services.AddScoped<UserProfileContext>(p => p.GetRequiredService<IFactory<UserProfileContext>>().Create());
            services.AddScoped<IAggregateUnitOfWork>(p =>
                p.GetRequiredService<IFactory<UserProfileContext>>().Create());
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ProfileService.API", Version = "v1"});
            });
            services.AddKeycloak(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserProfileContext profileContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            profileContext.Database.Migrate();
            
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProfileService.API v1"));
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}