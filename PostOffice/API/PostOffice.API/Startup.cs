using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PostOffice.DAL.Repositories.EntityFrameworkDataAccess;
using PostOffice.DAL.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Logic.ShipmentLogic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Logic.Mapper;
using FluentValidation.AspNetCore;
using PostOffice.API.Logic.Validators;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Logic.BagLogic;

namespace PostOffice.API
{
	public class Startup
	{
		readonly string _origin = "PostOfficeOrigin";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ShipmentValidator>())
				.ConfigureApiBehaviorOptions(options =>
				{

					options.InvalidModelStateResponseFactory = f =>
					{
						string errors = string.Join('\n', f.ModelState.Values.Where(v => v.Errors.Count > 0)
							.SelectMany(v => v.Errors)
							.Select(v => v.ErrorMessage));
						return new BadRequestObjectResult(new APIResponse { Error = errors });
					};
				});

			services.AddCors(options =>
			{
				options.AddPolicy(name: _origin,
					builder =>
					{
						builder.WithOrigins("http://localhost:3000")
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostOffice.API", Version = "v1" });
			});
			var a = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDBContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
					x => x.MigrationsAssembly("PostOffice.DAL.Migrations")));

			//automapper
			services.AddAutoMapper(typeof(APIProfile), typeof(APIProfile));

			//Logic
			services.AddTransient<IShipmentLogic, ShipmentLogic>();
			services.AddTransient<IBagLogic, BagLogic>();
			services.AddTransient<IParcelLogic, ParcelLogic>();

			//Repositories
			services.AddTransient<IShipmentRepository, ShipmentRepository>();
			services.AddTransient<IBagRepository, BagRepository>();
			services.AddTransient<IParcelRepository, ParcelRepository>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostOffice.API v1"));
			}
			app.UseExceptionHandler(c => c.Run(async context =>
			{
				var exception = context.Features
					.Get<IExceptionHandlerPathFeature>()
					.Error;
				var response = new APIResponse { Error = exception.Message };
				await context.Response.WriteAsJsonAsync(response);
			}));
			UpdateDatabase(app);
			app.UseHttpsRedirection();

			app.UseRouting(); 
			app.UseCors(_origin);
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope();
			using var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();
			context.Database.Migrate();
		}
	}
}
