using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Exceptions;
using Homework.PriceCalculator.Domain.Models;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services;

namespace Homework.PriceCalculator.Domain.UnitTests;

public class PriceCalculatorServiceTest
{
    public class GoodsRepositoryStub : IGoodsRepository
    {
        public void AddOrUpdate(GoodViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public ICollection<GoodViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public GoodViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
    }
    public class StorageRepositoryStub : IStorageRepository
    {
        public void Save(StorageEntity entity)
        {
            throw new NotImplementedException();
        }

        public StorageEntity[] Query()
        {
            throw new NotImplementedException();
        }
    }
    
    [Fact]
    public void PriceCalculatorService_WhenGoodsIsEmpty_ThrowException()
    {
        // Arrange
        var options = new PriceCalculatorOptions();
        var repository = new GoodsRepositoryStub();
        var storage = new StorageRepositoryStub();
        var goods = Array.Empty<GoodModel>();
        var cut = new PriceCalculatorService(options, repository, storage);

        // Act
        // Assert
        Assert.Throws<DomainException>(() => cut.CalculatePrice(goods));
    }
    
    [Fact]
    public void PriceCalculatorService_WhenGoodsIsNotEmpty_Success()
    {
        // Arrange
        var options = new PriceCalculatorOptions();
        var repository = new GoodsRepositoryStub();
        var storage = new StorageRepositoryStub();
        var goods = Array.Empty<GoodModel>();
        var cut = new PriceCalculatorService(options, repository, storage);

        // Act
        // Assert
        Assert.Throws<DomainException>(() => cut.CalculatePrice(goods));
    }
}