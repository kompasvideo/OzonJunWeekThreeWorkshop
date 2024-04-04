using System.Net;
using Homework.PriceCalculator.Domain;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services;
using Homework.PriceCalculator.Domain.Services.Interfaces;
using Homework.PriceCalculator.Infrastructure.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Workshop.Api.ActionFilters;
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
        services.AddScoped(x => x.GetRequiredService<IOptionsSnapshot<PriceCalculatorOptions>>().Value);
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
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(x =>
        {
            var options = x.GetRequiredService<IOptionsSnapshot<PriceCalculatorOptions>>().Value;
            return new PriceCalculatorService(options, x.GetRequiredService<IStorageRepository>());
        });
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