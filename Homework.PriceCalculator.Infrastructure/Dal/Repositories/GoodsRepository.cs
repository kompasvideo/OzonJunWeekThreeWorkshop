using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services.Exceptions;

namespace Homework.PriceCalculator.Infrastructure.Dal.Repositories;

internal sealed class GoodsRepository : IGoodsRepository
{
    private readonly Dictionary<int, GoodViewModel> _store = new();

    public void AddOrUpdate(GoodViewModel viewModel)
    {
        if (_store.ContainsKey(viewModel.Id))
            _store.Remove(viewModel.Id);
        _store.Add(viewModel.Id, viewModel);
    }

    public ICollection<GoodViewModel> GetAll()
    {
        return _store.Select(x => x.Value).ToArray();
    }

    public GoodViewModel Get(int id)
    {
        try
        {
            return _store[id];
        }
        catch (KeyNotFoundException e)
        {
            throw new EntityNotFoundException("Not found ", e);
        }
    }
}