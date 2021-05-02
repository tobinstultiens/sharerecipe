using System.Reflection;
using FluentValidation;
using MediatR;
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
    }
}