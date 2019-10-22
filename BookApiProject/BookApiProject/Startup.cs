using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookApiProject
{
    public class Startup
    {
        // inject to use connetion string for db through IConfiguration
        // allows to store string in app.json file
        // then create constructor 
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // configure project to use MVC
            services.AddMvc();
            // create connection string in JSON Format and will be used in appsettings.json
            var connectionString = Configuration["connectionStrings:bookDbConnectionString"];
            // add the BookDbContext and specify server using the connection string
            services.AddDbContext<BookDbContext>(c => c.UseSqlServer(connectionString));
        }

        // Middleware, interfaces, database services will be registered here
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BookDbContext context)
        {
            // development mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // run seed data class 
            // context.SeedDataContext();

            // add MVC to the request execution pipline
            app.UseMvc(); 
        }
    }
}
