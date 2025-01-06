using FluentValidation;
using Unicam.Project.Application.Abstractions.Services;
using Unicam.Project.Application.Services;

namespace Unicam.Project.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == "Unicam.Project.Application"));

            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<IUtenteService, UtenteService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
