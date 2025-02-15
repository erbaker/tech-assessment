using CSharp.Services;
using CSharp.Authorization;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
            services.AddControllers().AddXmlSerializerFormatters();
			services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders", Version = "v1" });
            });
            services.AddScoped<IUserAuthenticateService, UserAuthenticateService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "");
                      
            });
 
			app.UseHttpsRedirection();

			app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<Authentication>();

            app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
   
        }
	}
}
