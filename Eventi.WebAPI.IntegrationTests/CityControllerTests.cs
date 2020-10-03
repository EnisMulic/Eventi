using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eventi.WebAPI.IntegrationTests
{
    public class CityControllerTests : IntegrationTest
    {

        [Fact]
        public async Task Get_NoParametars_ReturnStatusCodeOK()
        {
            // Arrange
            //await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.City.Get);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ID1_ReturnCityWithID1()
        {
            // Arrange
            //await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.City.GetById.Replace("{id}", "1"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CityResponse>()).ID.Should().Be(1);
        }

        [Fact]
        public async Task Insert_NameCity_ReturnsStatusCodeCreatedAndCityOuagadougou()
        {
            // Arrange
            //await AuthenticateAsync();
            var city = new CityUpsertRequest()
            {
                Name = "Ouagadougou",
                CountryID = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(ApiRoutes.City.Post, stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            (await response.Content.ReadAsAsync<CityResponse>()).Name.Equals("Ouagadougou");
        }

        [Fact]
        public async Task Update_ID1NameOuagadougou_ReturnsID1NameOuagadougou()
        {
            // Arrange
            //await AuthenticateAsync();
            var city = new CityUpsertRequest()
            {
                Name = "Ouagadougou",
                CountryID = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(ApiRoutes.City.Put.Replace("{id}", "1"), stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CityResponse>()).Name.Equals("Ouagadougou");
        }

        [Fact]
        public async Task Delete_ID1_ReturnsStatusCodeOK()
        {
            // Arrange
            //await AuthenticateAsync();


            // Act
            var response = await _httpClient.DeleteAsync(ApiRoutes.City.Delete.Replace("{id}", "1"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
