using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WaProject.App.API.Model;
using WaProject.Domain.Entities;
using WaProject.Domain.Interfaces;
using WaProject.Infra.Data.Context;
using WaProject.Infra.Data.Repository;
using WaProject.Service.Services;

namespace WaProject.App.API
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

            services.AddDbContext<DataContext>(options =>options.UseInMemoryDatabase("WaProjectDB"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WaProject.App.API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });


            services.AddScoped<IBaseRepository<Job>, BaseRepository<Job>>();
            services.AddScoped<IBaseService<Job>, BaseService<Job>>();
            services.AddScoped<IJobService, JobService>();
            

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<CreateJobModel, Job>().ReverseMap();
                config.CreateMap<UpdateJobModel, Job>()
                .ForMember(j => j.CreatedDate, opt => opt.Ignore()).ReverseMap();
                config.CreateMap<JobModel, Job>().ReverseMap();
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WaProject.App.API v1"));
            }

            app.UseHttpsRedirection();



            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
