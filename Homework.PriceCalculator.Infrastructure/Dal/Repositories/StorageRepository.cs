using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Seporated;

namespace Homework.PriceCalculator.Infrastructure.Dal.Repositories;

public class StorageRepository : IStorageRepository
{
    private readonly List<StorageEntity> _storage = new();
    public void Save(StorageEntity entity)
    {
        _storage.Add(entity);
    }

    public StorageEntity[] Query()
    {
        return _storage.ToArray();
    }
}