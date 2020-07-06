namespace PolicySearchService.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Steeltoe.Discovery.Client;
    using Newtonsoft.Json.Serialization;

    using PolicySearchService.Application;
    using PolicySearchService.Infrastructure;
    using PolicySearchService.API.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);

            services
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddApiComponents();

            services.AddHealthChecks();

            services
                .AddControllers(setupAction => setupAction.ReturnHttpNotAcceptable = true)
                //.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductDbContext>())
                .AddNewtonsoftJson(setupAction => setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction => setupAction.UseCustomInvalidModelUnprocessableEntityResponse());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else app.UseHsts();

            //app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDiscoveryClient();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
