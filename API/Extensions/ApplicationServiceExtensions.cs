using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using API.Helpers;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILinkRepository, LinkRepository>();

            return services;
        }
    }
}