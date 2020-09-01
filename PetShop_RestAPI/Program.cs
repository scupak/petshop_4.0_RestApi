using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Petshop.core.ApplicationServices;
using Petshop.core.DomainServices;
using Petshop.infraStructure.Data;

namespace PetShop_RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FakeDB.InitData();
            CreateHostBuilder(args).Build().Run();
            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
