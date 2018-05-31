using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ondeTem.Data.Context;
using ondeTem.Data.Repositories;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.EstabelecimentoRoot;
using ondeTem.Domain.Interfaces;
using ondeTem.Domain.ProdutoRoot;
using ondeTem.Domain.StoryRoot;
using Swashbuckle.AspNetCore.Swagger;

namespace ondeTem.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "ondetem.com.br",
                    ValidAudience = "ondetem.com.br",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f5349a80dd6937efa8ca39b61cc8c3aa"))
                };
            });

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                            new Info 
                            {
                                Title = "OndeTem API V1",
                                Version = "v1"
                            });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("fiver",
                    policy => policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                );
            });

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddMvc()
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            //Validator
            services.AddScoped<IValidator<Estabelecimento>, EstabelecimentoValidator>();
            services.AddScoped<IValidator<Categoria>, CategoriaValidator>();
            services.AddScoped<IValidator<Produto>, ProdutoValidator>();
            services.AddScoped<IValidator<EstabelecimentoUser>, EstabelecimentoUserValidator>();
            services.AddScoped<IValidator<Story>, StoryValidator>();

            //Interface -> Repository
            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();

            services.AddDbContext<OndeTemContext>(
                x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }*/

            app.UseAuthentication();

            app.UseCors("fiver");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OndeTem API V1");
            });

            app.UseMvc();

            var optionsBuilder = new DbContextOptionsBuilder<OndeTemContext>();
            optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            var db = new OndeTemContext(optionsBuilder.Options);
            var TotalMigrations = db.Database.GetMigrations();
            var AppliedMigrations = db.Database.GetAppliedMigrations();
            if (AppliedMigrations.Count() < TotalMigrations.Count())
            {
                db.Database.Migrate();
            }
        }
    }
}
