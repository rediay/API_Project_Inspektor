/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//using Rotativa.AspNetCore;
using Serilog;
using Serilog.Events;
//using System;
//using System.Security.Cryptography;

namespace Common.WebApiCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using var aes = Aes.Create();

            //// Generar claves aleatorias
            //aes.GenerateKey();
            //aes.GenerateIV();

            //// Convertir claves a cadenas base64
            //string aesKey = Convert.ToBase64String(aes.Key);
            //string aesIV = Convert.ToBase64String(aes.IV);

            //Console.WriteLine("AesKey: " + aesKey);
            //Console.WriteLine("AesIV: " + aesIV);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("logs.txt"))
                .UseStartup<Startup>();
    }

}
