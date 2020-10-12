using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using simple_crud_api.Config;

namespace simple_crud_api
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
            // requires using Microsoft.Extensions.Options
            services.Configure<MongoDBSettings>(
                Configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IMongoDBSettings>(sp =>
                new MongoDBSettings
                {
                    UsersCollectionName = Configuration.GetSection("MongoDBDatabaseSettings")["UserCollectionName"],
                    ConnectionString = Configuration.GetSection("MongoDBDatabaseSettings")["ConnectionString"],
                    DatabaseName = Configuration.GetSection("MongoDBDatabaseSettings")["DatabaseName"]
                }
            ); 
            //sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
