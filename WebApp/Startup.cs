using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Repository;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => opt.EnableEndpointRouting = false);
            services.AddTransient<CategoryLogic, CategoryLogic>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<ProductLogic, ProductLogic>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<Price_SortTypeLogic, Price_SortTypeLogic>();
            services.AddTransient<IRepository<Price_SortType>, Price_SortTypeRepository>();
            services.AddTransient<ItemsCount_SortTypeLogic, ItemsCount_SortTypeLogic>();
            services.AddTransient<IRepository<ItemsCount_SortType>, ItemsCount_SortTypeRepository>();
            services.AddTransient<FiguresCount_SortTypeLogic, FiguresCount_SortTypeLogic>();
            services.AddTransient<IRepository<FiguresCount_SortType>, FiguresCount_SortTypeRepository>();
            services.AddTransient<Age_SortTypeLogic, Age_SortTypeLogic>();
            services.AddTransient<IRepository<Age_SortType>, Age_SortTypeRepository>();
            services.AddTransient<ProductCategoryLogic, ProductCategoryLogic>();
            services.AddTransient<IRepository<ProductCategory>, ProductCategoryRepository>();
            services.AddSingleton<DataContext, DataContext>();

            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}
