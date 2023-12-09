using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RickAndMorty.Abstractions;
using RickAndMorty.Abstractions.HttpClients;
using RickAndMorty.Commands.Pipelines;
using RickAndMorty.Infrastructure.Database;
using RickAndMorty.Infrastructure.HttpClients;
using RickAndMorty.Infrastructure.Migrations;
using RickAndMorty.Infrastructure.Repositaries;

namespace RickAndMorty.Infrastructure;

public static class ConfigureApp
{
    public static IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        //Configuration
        IConfiguration configuration;
#if DESKTOP
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional:false)
            .Build();
        serviceCollection.AddSingleton<IConfiguration>(configuration);
#else
        
#endif
        

        //Logging
        serviceCollection.AddLogging(builder => builder.AddConsole());

        //MediatR
        serviceCollection.AddMediatR(configuration => { configuration.RegisterServicesFromAssembly(typeof(LoggingBehavior<,>).Assembly); });
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //Validatror
        serviceCollection.AddValidatorsFromAssembly(typeof(LoggingBehavior<,>).Assembly);

        //SQLLite
        serviceCollection.AddDbContext<RickAndMortyDbContext>(async (provider, builder) =>
        {
#if DESKTOP
            var configurationService = provider.GetService<IConfiguration>()!;
            var connectionString = configurationService.GetSection("DatabaseConnectionStrings").GetValue<string>("SQLLite") ?? throw new NullReferenceException("Не указана ConnectionString к базе SqlLite!");
            const string envPath = "ENV_VAR";
            if (connectionString.Contains(envPath))
            {
                var environmentPath = Environment.CurrentDirectory;
                provider.GetService<ILoggerFactory>()!.CreateLogger(nameof(ConfigureApp)).LogInformation("В строке подключения был ENV_VAR, меняем на {Path}", environmentPath);
                connectionString = connectionString.Replace(envPath, environmentPath);
            }
#elif ANDROID
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var connectionString = $"Data Source={Path.Combine(path, "RickAndMorty1.db")}";
#endif
            
            builder.UseSqlite(connectionString);
        }, ServiceLifetime.Singleton);
        ConfigureServices(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        //Repositaries
        services.AddTransient<ILikedRepository, LikedRepository>();
        
        //HttpClients
        services.AddHttpClient<IRickAndMortyHttpClient,RickAndMortyHttpClient>();
    }
}