using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Application.Services.Authtentication.Commands;
using BuberDinner.Application.Services.Authtentication.Querys;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}