using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Seporated;
using Microsoft.AspNetCore.Mvc;

namespace Workshop.Api.Controllers.V3;

public class GoodsViewController : Controller
{
    private readonly IGoodsRepository _repository;

    public GoodsViewController(IGoodsRepository repository)
    {
        _repository = repository;
    }
    public IActionResult Index()
    {
        var entities = _repository.GetAll();
        var viewModel = new GoodsPageViewModel();
        viewModel.Goods = entities.Select(
            x => new GoodViewModel(
                x.Name,
                x.Id,
                x.Height,
                x.Length,
                x.Width,
                x.Weight,
                x.Count,
                x.Price)).ToArray();
        return View("/Views/GoodsPageView.cshtml", viewModel);
    }
}