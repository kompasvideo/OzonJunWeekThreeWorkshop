using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Api.Controllers.V1;
using Workshop.Api.Requests.V1;
using Xunit.Abstractions;

namespace Homework.PriceCalculator.Domain.IntegrationTests;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task App_SwaggerIsWorking()
    {
        // Arrange
        var app = new AppFixture();
        var httpClient = app.CreateClient();
        
        // Act
        var response = await httpClient.GetAsync($"/swagger/Index.html");
        
        // Assert
        response.EnsureSuccessStatusCode();
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }
    
    [Fact]
    public void App_V1DeliveryPrice_ShouldLogHistory()
    {
        // Arrange
        var app = new AppFixture();
        var request = new CalculateRequest(new[] { new GoodProperties(1, 1, 1) });
        var controller = app.Services.GetRequiredService<DeliveryPriceController>();
        
        // Act
        var response = controller.Calculate(request);
        
        // Assert
        var history = controller.GetHistory(new GetHistoryRequest(int.MaxValue));
        Assert.Single(history);
    }
}