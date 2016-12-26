using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FirstApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // caly setup odbywa sie pdoczas startu serwera
            // to jest setup request pipelinu       
            var host = new WebHostBuilder()
                .UseKestrel() // ten serwer jest uniwersalny dla wszystkich srodowisk
                .UseContentRoot(Directory.GetCurrentDirectory()) 
                // content root to sciezka dla serwera a webroot jest okreslony pod referencjami

                .UseIISIntegration() // to ustawia IIS uzywany w visual studio jako default - jako reverse proxy
                .UseStartup<Startup>() // dodaje do pipa zawartosc startup
                .Build();

            host.Run();
        }
    }
}
