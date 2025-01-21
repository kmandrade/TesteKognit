using Domain.Context;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.Services;

namespace ApiTesteKognit.Ioc;

public static class DependencyInjection
{
    /// <summary>
    /// Destinado a injetar as dependências do ciclo de vida da solution.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.ResolveDependecies()
                .AddDbContext(configuration)
                .AddCustomSwaggerGen();

        return services;
    }
    /// <summary>
    /// Injeção para controle dos servicos e repositórios
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection ResolveDependecies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
    /// <summary>
    /// Injeção do contexto de banco de dados.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<MyContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MyConnection")));

        return services;
    }
}