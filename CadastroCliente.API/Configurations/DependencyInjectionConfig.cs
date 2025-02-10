using CadastroCliente.Data.Context;
using CadastroCliente.Data.Repositories;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Services;

namespace CadastroCliente.Web.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAuthKeyService, AuthKeyService>();
            services.AddScoped<IAuthKeyRepository, AuthKeyRepository>();

            return services;
        }
    }
}
