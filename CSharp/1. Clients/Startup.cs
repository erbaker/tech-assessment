using CSharp.Accessors;
using CSharp.Managers;
using CSharp.Managers.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CSharp
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
			services.AddControllers();
			services.AddSwaggerGen();

			// Allows IMapper to be injected through dependency injection using the defined mapping profiles.
            services.AddAutoMapper(mapperConfiguration =>
			{
				mapperConfiguration.AddProfile(typeof(AddressMappingProfile));
				mapperConfiguration.AddProfile(typeof(CustomerMappingProfile));
				mapperConfiguration.AddProfile(typeof(OrderMappingProfile));
			});

            services.AddTransient<IOrderManager, OrderManager>();
			services.AddTransient<IOrderAccessor, OrderAccessor>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
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
