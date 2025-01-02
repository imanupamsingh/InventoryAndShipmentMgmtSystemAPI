using InventoryBAL.Implementation;
using InventoryBAL.Interface;
using InventoryDAL;
using InventoryRepository.Implementation;
using InventoryRepository.Interface;
using InventoryUtility;
using Microsoft.EntityFrameworkCore;

namespace InventoryShipmentMgmtAPI
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddScoped<IProduct, ProductBAL>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Configure database connection strings.

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ConstantResources.InventoryDbConnection)));
            services.AddSwaggerGen();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(op => op.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API"));
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors("AllowSpecificOrigin"); // This must be placed between UseRouting and UseEndpoints


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Other endpoints mapping
            });

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), ConstantResource.ACLImages, ConstantResource.Images)),
            //    RequestPath = ConstantResource.ImagePath
            //});
        }
    }
}
