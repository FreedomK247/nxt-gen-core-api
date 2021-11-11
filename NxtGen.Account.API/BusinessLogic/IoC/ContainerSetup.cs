using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxtGen.Account.API.BusinessLogic.Helpers;
using NxtGen.Account.API.BusinessLogic.Services;
using NxtGen.Account.API.Data;
using NxtGen.Account.API.Data.Contracts;
using NxtGen.Account.API.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;


namespace NxtGen.Account.API.BusinessLogic.IoC
{
    public static class ContainerSetup
    {
        public static void Setup(
            IServiceCollection services,
            IConfiguration configuration
            )
        {
            AddUnitOfWork(services, configuration);
            ConfigureEmail(services, configuration);
            AddWebServices(services);
            ConfigureFormOptions(services);
            ConfigureCors(services);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }

        private static void AddUnitOfWork(
               IServiceCollection services,
            IConfiguration configuration
            )
        {
            var connectionString =
                          configuration["ConnectionStrings:DefaultConnection"];

            services
              .AddDbContext<AccountsDbContext>(options =>
                  options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork>(
                uow => new EFUnitOfWork(uow.GetRequiredService<AccountsDbContext>()));
        }

        private static void AddWebServices(IServiceCollection services)
        {
            var serviceType = typeof(AccountService);
            var types =
               (
               from t in serviceType.GetTypeInfo().Assembly.GetTypes()
               where
               t.Namespace == serviceType.Namespace && // namespace BusinessLogic.Services
               t.GetTypeInfo().IsClass && // Returns all concrete class under the namespace.
               t
                   .GetTypeInfo()
                   .GetCustomAttribute<CompilerGeneratedAttribute>() ==
               null
               select t
               ).ToArray();

            // Get interfaces for these services
            foreach (var type in types)
            {
                var iServiceType = type.GetTypeInfo().GetInterfaces().First();
                services.AddScoped(iServiceType, type);
            }
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options
                        .AddPolicy("CorsPolicy",
                        builder =>
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
                });
        }

        private static void ConfigureEmail(IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
        }

        private static void ConfigureFormOptions(IServiceCollection services)
        {
            services.Configure<FormOptions>(op =>
            {
                op.ValueLengthLimit = int.MaxValue;
                op.MultipartBodyLengthLimit = int.MaxValue;
                op.MemoryBufferThreshold = int.MaxValue;
            });
        }
    }
}