using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using zip.api.Config;
using zip.api.Repositories;
using zip.api.Services;

namespace zip.api
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
            services.AddAutoMapper(typeof(Startup));

            var config = new ServerConfig();
            Configuration.Bind(config);

            var usersContext = new Database.UsersDbContext(config.MongoDb);
            var usersRepository = new UsersRepository(usersContext);

            services.AddSingleton<IUsersRepository>(usersRepository);
            services.AddScoped<IUsersService, UsersService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Constants.ApiVersion.V1, new Info
                {
                    Version = Constants.ApiVersion.V1,
                    Title = Configuration["ProjectsDetails:Title"],
                    Description = Configuration["ProjectsDetails:Description"],
                    Contact = new Contact
                    {
                        Name = Configuration["ProjectsDetails:ContactName"]
                    }
                });

//                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//
//                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["ProjectsDetails:DocumentationTitle"]);
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
