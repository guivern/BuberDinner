using System.Reflection;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authtentication;
using BuberDinner.Application.Services.Authtentication.Commands;
using BuberDinner.Application.Services.Authtentication.Querys;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly); // add all handlers
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // add validation behavior
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // add validators

            return services;
        }
    }
}