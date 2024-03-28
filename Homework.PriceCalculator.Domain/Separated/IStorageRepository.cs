using Homework.PriceCalculator.Domain.Entities;

namespace Homework.PriceCalculator.Domain.Seporated;

public interface IStorageRepository
{
    void Save(StorageEntity entity);
    StorageEntity[] Query();
}