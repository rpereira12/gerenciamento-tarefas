using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RGP.GestaoTarefas.Application.Handlers.Tarefa;
using RGP.GestaoTarefas.Application.Handlers.Usuario;
using RGP.GestaoTarefas.Data.Repository;
using RGP.GestaoTarefas.Domain.Repository;

namespace RGP.GestaoTarefas.API
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gestão de tarefas", Version = "v1" });
            });

            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<TarefaCommandHandler, TarefaCommandHandler>();
            services.AddScoped<UsuarioCommandHandler, UsuarioCommandHandler>();

            services.AddDbContext<RgpDbContext>(options =>
            {
                options.UseInMemoryDatabase("RgpDbInMemory");
            });
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

            using var serviceScope = app.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<RgpDbContext>();
            UsuarioInitializer.Initialize(dbContext);
            TarefaInitializer.Initialize(dbContext);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão de tarefas v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
