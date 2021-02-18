using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Juan_du_Toit_Has_Test.Startup))]
namespace Juan_du_Toit_Has_Test
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
            string connectionString = Environment.GetEnvironmentVariable("MyConnectionString");
            builder.Services.AddDbContext<PracticalTestContext>(
                options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
            Random r = new Random();
            var environment = r.Next(1, 3); //meant to represent which environment this code is performing hence representing slightly different code that can execute
            if (environment == 1)
            {
                builder.Services.AddSingleton<ICustomLogger, Log>();
            }
            else if (environment == 2)
            {

                builder.Services.AddSingleton<ICustomLogger, Log1>();
            }
            else
            {

                builder.Services.AddSingleton<ICustomLogger, Log2>();
            }
        }
    }
}
