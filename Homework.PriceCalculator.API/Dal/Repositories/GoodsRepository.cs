using Workshop.Api.Dal.Entities;
using Workshop.Api.Dal.Repositories.Interfaces;

namespace Workshop.Api.Dal.Repositories;

public class GoodsRepository : IGoodsRepository
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

    public GoodViewModel Get(int id) => _store[id];
}