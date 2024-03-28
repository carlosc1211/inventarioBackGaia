using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Domain.Interfaces.Services;
using Inventario.Infrastructure.ESEntityFramework;
using Inventario.Infrastructure.Repositories;
using Inventario.Service.Articulos;
using Inventario.Service.AutoMapper;
using Inventario.Service.Inventario;
using Inventario.Service.Inventario.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Inventario.API
{
    public class Startup
	{
		private IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			Environment = env;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
			.AddDbContext<DbContextInventario>(options => options.UseSqlServer(Configuration["ConexionRepositorio"]))
			//.AddScoped<IDbConnection>(c => new SqlConnection(Configuration["ConexionRepositorioPedidos"]))
			.AddMediatR(typeof(CommandHandler).GetTypeInfo().Assembly)
			.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddCors(options =>
		    {
				options.AddPolicy("PoliticaCors", builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "Inventario",
						Version = "v1"
					});
			});

			services.AddTransient<IUsuarioService, InventarioService>();
			services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IArticuloService, ArticuloService>();
            services.AddTransient<IArticuloRepository, ArticuloRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddCollectionMappers();
				cfg.AddProfile<MappingProfile>();
			});

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole(); 
            });

            services.AddAutoMapper(typeof(MappingProfile));
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton<IMapper>(mapper);

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			var configuration = app.ApplicationServices.GetService<IConfiguration>();

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseCors(x => x
						 .AllowAnyMethod()
						 .AllowAnyHeader()
						 .SetIsOriginAllowed(origin => true)); // allow any origin

			app.UseAuthorization();

			app.UseSwagger(c =>
			{
				c.RouteTemplate = "Inventario/swagger/{documentName}/swagger.json";
			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/Inventario/swagger/v1/swagger.json", "Inventario v1");
				c.RoutePrefix = "Inventario/swagger";
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}