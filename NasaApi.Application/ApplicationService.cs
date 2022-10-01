using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using MediatR;

namespace NasaApi.Application
{
    public static class ApplicationService
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

    }
}
