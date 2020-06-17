using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TableOfNothing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // var section = Configuration.GetSection("esotericSettings");
            System.Console.WriteLine(Configuration.GetValue(typeof(string), "esotericSettings"));
            System.Console.WriteLine(Configuration.GetValue(typeof(string), "wordOfPower"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // var builder = new ConfigurationBuilder()
            //     .SetBasePath(env.ContentRootPath)
            //     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //     .AddJsonFile("secrets/appsettings.secrets.json", optional: true)
            //     .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
