using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData.Builder;
using MS_Products.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;

namespace MS_Products
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOData();

            services.AddMvc();

            services.AddDbContext<DbContexts.ContextProduct>(o => o.UseSqlServer(Configuration.GetConnectionString("DBProducts")));

            services.AddTransient<Repository.IProductRepository, Repository.ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(b =>
            {
                var aaa = GetEdmModel();
                b.MapODataServiceRoute("odata", "odata", GetEdmModel());

                b.EnableDependencyInjection();

                b.Expand().Select().OrderBy().Filter();
            });
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Category>("Categories");
            return builder.GetEdmModel();
        }
    }
}
