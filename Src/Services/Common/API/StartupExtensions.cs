using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareRecipe.Services.Common.API.Configurations;
using ShareRecipe.Services.Common.API.Startup;
using ShareRecipe.Services.Common.Infrastructure;

namespace ShareRecipe.Services.Common.API
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDefaultApplicationServices(this IServiceCollection serviceCollection, Assembly mappingAssembly, Assembly applicationAssembly)
        {
            serviceCollection.AddAutoMapper(mappingAssembly, Assembly.GetAssembly(typeof(StartupExtensions)));
            serviceCollection.AddValidatorsFromAssembly(applicationAssembly);
            serviceCollection.AddMediatR(applicationAssembly);
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ScopeBehaviour<,>));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionBehaviour<,>));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            return serviceCollection;
        }
        
        public static IServiceCollection AddConfigurations(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<DbConfiguration>(configuration.GetSection("Database").Bind);
            serviceCollection.Configure<ServiceConfiguration>(configuration.GetSection("Service").Bind);
            return serviceCollection;
        }

        public static IServiceCollection AddKeycloak(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.Authority = configuration["Authentication:KeycloakAuthentication:ServerAddress"] + "/auth/realms/" + configuration["Authentication:KeycloakAuthentication:Realm"];
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudiences = new string[] { "curl", "vueapp", "accountingApplication", "swagger"},
                    };
                    options.RequireHttpsMetadata = false; //for test only!
                    options.SaveToken = true;
                    options.Validate();
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            MapKeycloakRolesToRoleClaims(context);
                            return Task.CompletedTask;
                        }
                    };
                });
            
            return serviceCollection;
        }
        
        private static void MapKeycloakRolesToRoleClaims(TokenValidatedContext context)
        {
            //var resourceAccess = JObject.Parse(context.Principal.FindFirst("resource_access").Value);
            //var clientResource = resourceAccess[context.Principal.FindFirstValue("aud")];
            var clientRoles = context.Principal.Claims.Where(w=>w.Type== "realm_access_roles").ToList();
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return;
            }

            foreach (var clientRole in clientRoles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, clientRole.Value));
            }
        }
    }
}