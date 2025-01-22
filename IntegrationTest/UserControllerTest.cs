using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTest;

public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public UserControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateUser_EmptyName_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var user = new
        {
            Name = "",
            BithDate = "1990-01-01",
            NrCpf = "123.456.789-00"
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/user", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("O campo nome é obrigatório.", responseContent);
    }

    [Fact]
    public async Task CreateUser_InvalidBirthDateFormat_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var user = new
        {
            Name = "João da Silva",
            BirthDate = "22/11/1990",
            NrCpf = "123.456.789-00"
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/user", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("A data de nascimento deve estar no formato yyyy-MM-dd.", responseContent);
    }

    [Fact]
    public async Task CreateUser_InvalidCpfFormat_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        var user = new
        {
            Name = "João da Silva",
            BithDate = "1990-01-01",
            NrCpf = "1234567890"
        };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/api/user", content);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("O CPF deve estar no formato 123.456.789-00.", responseContent);
    }

}
