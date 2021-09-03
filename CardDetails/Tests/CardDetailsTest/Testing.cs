using CardDetails.Core;
using CardDetails.Data;
using CardDetails.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using Respawn;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CardDetailsTest
{
    [SetUpFixture]
    public class Testing
    {
        public static IConfiguration _configuration;
        public static IServiceCollection _services;
        public static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkPoint;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            _services = new ServiceCollection();
            var startup = new Startup(_configuration);
            _services.AddSingleton(s => new Mock<IHostEnvironment>().Object);
            _services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.ApplicationName == "CardDetails.Core"
            && w.EnvironmentName == "Development"));
            _services.AddScoped(_ => _configuration);

            _services.AddLogging();

            startup.ConfigureServices(_services);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkPoint = new Checkpoint
            {
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory",
                }
            };
        }

        public static async Task ResetDbState()
        {
            await _checkPoint.Reset(_configuration.GetConnectionString("Connection"));
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static async Task AddRangeAsync<TEntity>(List<TEntity> entities)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public static void WithCardDetails()
        {
            AddRangeAsync(SeedCardDetails()).GetAwaiter().GetResult();
        }

        private static List<CardDetail> SeedCardDetails()
        {
            return new List<CardDetail>
            {
                new CardDetail
                {
                    Bank = new Bank()
                    {
                        Name = "GTBank",
                        Phone = "2348039003900",
                    },
                    Bin = 53998344,
                    Brand = "Debit",
                    Scheme = "mastercard",
                    Type = "debit",
                    Country = new Country()
                    {
                        Name = "Nigeria",
                        Alpha2 = "NG",
                        Currency = "NGN",
                        Emoji = "NG",
                        Latitude = 10,
                        Longitude = 8,
                        Numeric = "566"
                    },
                    Number = new Number()
                }
            };
        }
    }
}
