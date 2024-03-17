using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Workshop.Api.ActionFilters;
using Workshop.Api.Bll;
using Workshop.Api.Bll.Services;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Repositories;
using Workshop.Api.Dal.Repositories.Interfaces;
using Workshop.Api.HostedServices;
using Workshop.Api.Middlewaries;

namespace Workshop.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment, 
        IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _webHostEnvironment = webHostEnvironment;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy.ToString());
        services.AddMvc().AddMvcOptions(x =>
            {
                x.Filters.Add(new ExceptionFilterAttribute());
                x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
                x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
                x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
            });
        services.Configure<PriceCalculatorOptions>(_configuration.GetSection("PriceCalculatorOptions"));
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.CustomSchemaIds(x => x.FullName);
        });
        // services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(
        //     x => new PriceCalculatorService(
        //         x.GetRequiredService<IOptions<PriceCalculatorOptions>>(),
        //         // _configuration.GetValue<double>("PriceCalculatorOptions:VolumeToPriceRatio"),
        //         // _configuration.GetValue<double>("PriceCalculatorOptions:WeightToPriceRatio"),
        //         x.GetRequiredService<IStorageRepository>()
        //         ));
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
        services.AddSingleton<IStorageRepository, StorageRepository>();
        services.AddSingleton<IGoodsRepository, GoodsRepository>();
        services.AddHostedService<GoodsSyncHostedService>();
        services.AddScoped<IGoodsService, GoodsService>();
        services.AddHttpContextAccessor();
    }

    public void Configure(IHostEnvironment environment, IApplicationBuilder app)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next.Invoke();
        });
        app.UseMiddleware<ErrorMiddleware>();
        app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute("goods-page", "goods-page", new
                {
                    Controller = "GoodsView",
                    Action = "Index"
                });
            }
        );         
    }
}