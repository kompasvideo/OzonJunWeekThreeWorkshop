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
}