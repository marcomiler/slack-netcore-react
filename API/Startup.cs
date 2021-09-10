using MediatR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Domain;
using Persistence;
using API.Middleware;
using Application.Channels;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddControllers()
                .AddFluentValidation(cfg => {
                    cfg.RegisterValidatorsFromAssemblyContaining<Create>();
                });

            services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            //agregadon CORS
            //definimos en este caso el origin desde el cual consumiremos los recursos de nuestra API
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000");
                    }
                );
            });

            //servicio MediatR
            services.AddMediatR(typeof(List.Handler).Assembly);

            //servicio identity
            var builder = services.AddIdentityCore<AppUser>();
            //crear instancia de IdentityBuilder
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();//usando nuestro middleware

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
