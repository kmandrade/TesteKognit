using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTest;

public class WalletControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WalletControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }


    [Fact]
    public async Task CreateWalletAsync_ValidModel_ReturnsCreatedWallet()
    {
        // Arrange
        var client = _factory.CreateClient();

        var wallet = new
        {
            UserId = 1,
            Bank = "Banco do Brasil",
            CurrentValue = 100.0
        };
        var content = new StringContent(JsonConvert.SerializeObject(wallet), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/wallet", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("Banco do Brasil", responseContent);
    }

    [Fact]
    public async Task CreateWallet_MissingUserId_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var wallet = new
        {
            Bank = "Banco do Brasil",
            CurrentValue = 100.0
        };
        var content = new StringContent(JsonConvert.SerializeObject(wallet), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/wallet", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("O usuário é obrigatório.", responseContent);
    }

    [Fact]
    public async Task CreateWallet_MissingCurrentValue_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var wallet = new
        {
            UserId = 1,
            Bank = "Banco do Brasil"
        };
        var content = new StringContent(JsonConvert.SerializeObject(wallet), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/wallet", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("O valor atual é obrigatório.", responseContent);
    }

    [Fact]
    public async Task CreateWallet_EmptyBank_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var wallet = new
        {
            UserId = 1,
            Bank = "",
            CurrentValue = 100.0
        };
        var content = new StringContent(JsonConvert.SerializeObject(wallet), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/wallet", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("O campo banco é obrigatório.", responseContent);
    }


}



