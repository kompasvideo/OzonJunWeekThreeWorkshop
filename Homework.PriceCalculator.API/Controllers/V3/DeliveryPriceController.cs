using FluentValidation;
using Homework.PriceCalculator.Domain.Models;
using Homework.PriceCalculator.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Requests.V3;
using Workshop.Api.Responses.V3;
using Workshop.Api.Validators;

namespace Workshop.Api.Controllers.V3;

[ApiController]
[Route("/v3/[controller]")]
public class DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculatorService _priceCalculatorService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<DeliveryPriceController> _logger;

    public DeliveryPriceController(IPriceCalculatorService priceCalculatorService,
        IHttpContextAccessor httpContextAccessor, ILogger<DeliveryPriceController> logger)
    {
        _priceCalculatorService = priceCalculatorService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }
    
    [HttpPost("calculate")]
    //[ProducesResponseType("")]
    //public async Task<CalculateResponse> Calculate()
    public async Task<CalculateResponse> Calculate(CalculateRequest request)
    {
        _httpContextAccessor.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
        var sr = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
        var bodyString = await sr.ReadToEndAsync();
        _logger.LogInformation(bodyString);

        var validator = new CalculateRequestValidator();
        //validator.Validate(request);
        validator.ValidateAndThrowAsync(request);
        
        // var sr = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
        // var bodyString = await sr.ReadToEndAsync();
        // _logger.LogInformation(bodyString);
        // var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        // var request = JsonSerializer.Deserialize<CalculateRequest>(bodyString, options);
        var result = _priceCalculatorService.CalculatePrice(
            request.Goods
                .Select(x => new GoodModel(
                    x.height,
                    x.length,
                    x.width,
                    x.weight))
                    .ToArray());
        return new CalculateResponse(result);
    }
    
    [HttpPost("get-history")]
    public GetHistoryResponse[] GetHistory(GetHistoryRequest request)
    {
        var log = _priceCalculatorService.QueryLog(request.Take);
        return log
            .Select(x => new GetHistoryResponse(
                new CargoResponse(x.Volume, x.Weight),
                x.Price))
            .ToArray();
    }
}