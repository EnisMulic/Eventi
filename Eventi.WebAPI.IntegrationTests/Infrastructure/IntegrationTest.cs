using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Eventi.WebAPI.IntegrationTests
{
    public class IntegrationTest : APIFactory
    {
        protected HttpClient _httpClient;

        public IntegrationTest()
        {
            _httpClient = base.CreateClient();
        }

        public async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Auth.Login, new LoginRequest
            {
                Username = "admin",
                Password = "password1"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

    }
}
