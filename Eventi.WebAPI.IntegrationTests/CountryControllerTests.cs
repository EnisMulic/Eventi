﻿using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Eventi.WebAPI.IntegrationTests
{
    public class CountryControllerTests : IntegrationTest
    {

        [Fact]
        public async Task Get_NoParametars_ReturnStatusCodeOK()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.Country.Get);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ID1_ReturnCountryWithID1()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.Country.GetById.Replace("{id}", "1"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CountryResponse>()).ID.Should().Be(1);
        }

        [Fact]
        public async Task Insert_NameBurkinaFaso_ReturnsStatusCodeCreatedAndCountryBurkinaFaso()
        {
            // Arrange
            //await AuthenticateAsync();
            var country = new CountryUpsertRequest()
            {
                Name = "Burkina Faso"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync(ApiRoutes.Country.Post, stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            (await response.Content.ReadAsAsync<CountryResponse>()).Name.Equals("Burkina Faso");
        }

        [Fact]
        public async Task Update_ID1NameBurkinaFaso_ReturnsID1NameBurkinaFaso()
        {
            // Arrange
            await AuthenticateAsync();
            var country = new CountryUpsertRequest()
            {
                Name = "Burkina Faso"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync(ApiRoutes.Country.Put.Replace("{id}", "1"), stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CountryResponse>()).Name.Equals("Burkina Faso");
        }

        [Fact]
        public async Task Delete_ID1_ReturnsStatusCodeOK()
        {
            // Arrange
            await AuthenticateAsync();


            // Act
            var response = await _httpClient.DeleteAsync(ApiRoutes.Country.Delete.Replace("{id}", "1"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
