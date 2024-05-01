using AutoFixture;
using Homework.PriceCalculator.Domain.Entities;
using Homework.PriceCalculator.Domain.Exceptions;
using Homework.PriceCalculator.Domain.Models;
using Homework.PriceCalculator.Domain.Seporated;
using Homework.PriceCalculator.Domain.Services;
using Moq;

namespace Homework.PriceCalculator.Domain.UnitTests;

public class PriceCalculatorServiceTest
{
    [Fact]
    public void PriceCalculatorService_WhenGoodsIsEmpty_ThrowException()
    {
        // Arrange
        var options = new PriceCalculatorOptions();
        var repositoryMock = new Mock<IGoodsRepository>();
        var storageMock = new Mock<IStorageRepository>();
        var goods = Array.Empty<GoodModel>();
        var cut = new PriceCalculatorService(
            options, 
            repositoryMock.Object,
            storageMock.Object);

        // Act
        // Assert
        Assert.Throws<DomainException>(() => cut.CalculatePrice(goods));
    }
    
    [Theory]
    [InlineData(1_0000, 1)]
    [InlineData(10_0000, 10)]
    public void PriceCalculatorService_WhenCalculatePriceByWeight_Success(int weight, int expected)
    {
        // Arrange
        var options = new PriceCalculatorOptions() {VolumeToPriceRatio = 1, WeightToPriceRatio = 1} ;
        
        var repositoryMock = new Mock<IGoodsRepository>();
        var storageMock = new Mock<IStorageRepository>();
        
        var goods = new[] {new GoodModel( 1,1,1,weight )};
        var cut = new PriceCalculatorService(
            options, 
            repositoryMock.Object,
            storageMock.Object);

        // Act
        var result = cut.CalculatePrice(goods);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [MemberData(nameof(CalculatePriceByVolumeData))]
    public void PriceCalculatorService_WhenCalculatePriceByVolume_Success(GoodModel model, int expected)
    {
        StorageEntity storageEntity = null;
        // Arrange
        var options = new PriceCalculatorOptions() {VolumeToPriceRatio = 1, WeightToPriceRatio = 1} ;
        
        var repositoryMock = new Mock<IGoodsRepository>();
        var storageMock = new Mock<IStorageRepository>(MockBehavior.Strict);
        storageMock
            .Setup(x => x.Save(It.IsAny<StorageEntity>()))
            .Callback<StorageEntity>(x => storageEntity = x);
        
        var cut = new PriceCalculatorService(
            options, 
            repositoryMock.Object,
            storageMock.Object);

        // Act
        var result = cut.CalculatePrice(new [] {model});

        // Assert
        Assert.NotNull(storageEntity);
        Assert.Equal(expected, result);
        storageMock.Verify(x => x.Save(It.IsAny<StorageEntity>()));
    }

    public static IEnumerable<object[]> CalculatePriceByVolumeData
    {
        get
        {
            yield return new object[] {new GoodModel( 10,10,10,1 ), 1};
            yield return new object[] {new GoodModel( 100,10,10,1 ), 10};
        }
    }
     
    [Fact]
    public void PriceCalculatorService_WhenAny_Success()
    {
        // Arrange
        var options = new PriceCalculatorOptions() {VolumeToPriceRatio = 1, WeightToPriceRatio = 1} ;
        
        var repositoryMock = new Mock<IGoodsRepository>();
        var storageMock = new Mock<IStorageRepository>();
        
        // var model = new Fixture().Create<GoodModel>();
        var model = new Fixture()
            .Build<GoodModel>()
            .With(x => x.Length, 1)
            .Create();
        
        var cut = new PriceCalculatorService(
            options, 
            repositoryMock.Object,
            storageMock.Object);

        // Act
        var result = cut.CalculatePrice(new [] {model});

        // Assert
        //Assert.Equal(expected, result);
    }
}