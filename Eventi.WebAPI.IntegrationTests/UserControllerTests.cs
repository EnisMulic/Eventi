using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Eventi.Contracts;
using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Responses;
using Eventi.Domain;
using Xunit;

namespace Eventi.WebAPI.IntegrationTests
{
    public class UserControllerTests : IntegrationTest
    {
        [Fact]
        public async Task Get_GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.User.Get);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // Returns one user that just registered
            (await response.Content.ReadAsAsync<PagedResponse<Person>>()).Data.Should().HaveCount(1);
        }
    }
}
