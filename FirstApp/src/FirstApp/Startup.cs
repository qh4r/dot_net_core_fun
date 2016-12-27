using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FirstApp
{
    using FirstApp.Services;

    using Microsoft.AspNetCore.Mvc.Formatters;
    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                // mozna teztu robic clear formatterow i usuwac pojedyncze
                // dostep do input formatterow tyż jest
                .AddMvcOptions(x => x.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                // nadpisywanie formatowania jsona              
                .AddJsonOptions(
                    x =>
                        {
                            if (x.SerializerSettings.ContractResolver != null)
                            {
                                var resolver = x.SerializerSettings.ContractResolver as DefaultContractResolver;
                                resolver.NamingStrategy = new DefaultNamingStrategy(); // default to pascal case
                            }
                        });
            // dodaje serwisy i je konfiguruje - do DI                

            // nie mozna dodawac do pielina, rejestracja serwisu
            services.AddTransient<IDummyService, DummyService>();
        }


        //TO JEST WOLANE PO COFIGURE SERVICES
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //powyzej przyklady wbudowanych w .netcore web api seriwsow
            loggerFactory.AddConsole();

            //dodatkowy logging
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler(); // to zamaist strony z opisem po prostu handlowalo by po cichu error
            }
            else
            {
                app.UseExceptionHandler();
            }
            // zwraca proste strony ze statusem - automatycznie
            // np 
            //Status Code: 404; Not Found
            app.UseStatusCodePages();

            app.UseMvc();
            
            
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
