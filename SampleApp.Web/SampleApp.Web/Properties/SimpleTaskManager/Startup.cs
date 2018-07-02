using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTaskManager.Core;
using SimpleTaskManager.Core.Repositories;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.Dal;
using SimpleTaskManager.Repositories;
using SimpleTaskManager.Services;

namespace SimpleTaskManager
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            var connection = _configuration
                .GetConnectionString("DefaultConnection");

            services
                .AddDbContext<TaskManagerContext>
                (options => options.UseSqlServer(connection, o => o.MigrationsAssembly("SimpleTaskManager")));

            services
                .AddScoped<ITaskManagerService, TaskManagerService>();
            services
                .AddScoped<IRequestService, RequestService>();
            services
                .AddScoped<ITaskManagerRepository, TaskManagerRepository>();
            services
                .AddScoped<IUnitOfWork, UnitOfWork>();
            services
                .AddScoped<IMapper, Mapper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
