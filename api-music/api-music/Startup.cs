using api_music.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace api_music
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Este método se utiliza para configurar los servicios de la aplicación.
        public void ConfigureServices(IServiceCollection services)
        {
            // Aquí puedes agregar servicios a la inyección de dependencias.
            // Por ejemplo:
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IFileUploader, StorageFileLocal>();
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options=>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers().AddNewtonsoftJson();

            //services.AddEndpointsApiExplorer();

            // services.AddSingleton<IMiServicio, MiServicio>();
        }

        // Este método se utiliza para configurar el pipeline de la solicitud HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}




