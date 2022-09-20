using System.Text.Json.Serialization;
using CleverWashWebApiTestTask.Model;
using CleverWashWebApiTestTask.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CleverWashWebApiTestTask
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
			services.AddControllers().AddJsonOptions(o =>
			{
				o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
				o.JsonSerializerOptions.MaxDepth = 0;
				o.JsonSerializerOptions.IncludeFields = true;
			});
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseInMemoryDatabase(databaseName: "Test");
				options.UseLazyLoadingProxies();
				options.EnableSensitiveDataLogging();
			});

			services.AddScoped<BuyService>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleverWashWebApiTestTask", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleverWashWebApiTestTask v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}