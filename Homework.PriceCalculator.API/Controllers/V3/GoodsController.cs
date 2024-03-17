using Microsoft.AspNetCore.Mvc;
using Workshop.Api.Bll.Models;
using Workshop.Api.Bll.Services;
using Workshop.Api.Bll.Services.Interfaces;
using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;
using Workshop.Api.Responses.V2;

namespace Workshop.Api.Controllers.V3;

[ApiController]
[Route("goods")]
public class GoodsController : ControllerBase
{
    private readonly IGoodsRepository _repository;
    private readonly ILogger<GoodsController> _logger;

    public GoodsController(IHttpContextAccessor httpContextAccessor,
        IGoodsRepository repository, ILogger<GoodsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    [HttpGet]
    public ICollection<GoodViewModel> GetAll()
    {
        return _repository.GetAll();
    }

    [HttpGet("{id}")]
    public CalculateResponse Calculate([FromServices] IPriceCalculatorService priceCalculatorService, int id)
    {
        _logger.LogInformation(HttpContext.Request.Path);
        var good = _repository.Get(id);
        var model = new GoodModel(
            good.Height,
            good.Length,
            good.Width,
            good.Weight);
        var price = priceCalculatorService.CalculatePrice( new [] {model});
        return new CalculateResponse(price);
    }
}