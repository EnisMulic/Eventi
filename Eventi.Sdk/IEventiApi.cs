using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Refit;
using System.Threading.Tasks;

namespace Eventi.Sdk
{
    //[Headers("Authorization: Bearer")]
    public interface IEventiApi
    {
        #region Country
        [Get("/api/v1/Country")]
        Task<ApiResponse<PagedResponse<CountryResponse>>> GetCountryAsync(CountrySearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> GetCountryAsync(int id);

        [Post("/api/v1/Country")]
        Task<ApiResponse<CountryResponse>> CreateCountryAsync([Body] CountryUpsertRequest request);

        [Put("/api/v1/Country/{id}")]
        Task<ApiResponse<CountryResponse>> UpdateCountryAsync(int id, [Body] CountryUpsertRequest request);

        [Delete("/api/v1/Country/{id}")]
        Task <ApiResponse<bool>> DeleteCountryAsync(int id);
        #endregion

        #region City
        [Get("/api/v1/City")]
        Task<ApiResponse<PagedResponse<CityResponse>>> GetCityAsync(CitySearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/City/{id}")]
        Task<ApiResponse<CityResponse>> GetCityAsync(int id);

        [Post("/api/v1/City")]
        Task<ApiResponse<CityResponse>> CreateCityAsync([Body] CityUpsertRequest request);

        [Put("/api/v1/City/{id}")]
        Task<ApiResponse<CityResponse>> UpdateCityAsync(int id, [Body] CityUpsertRequest request);

        [Delete("/api/v1/City/{id}")]
        Task<ApiResponse<bool>> DeleteCityAsync(int id);
        #endregion

        #region Event
        [Get("/api/v1/Event")]
        Task <ApiResponse<PagedResponse<EventResponse>>> GetEventAsync(EventSearchRequest request, PaginationQuery pagination);
        #endregion

        #region Client
        [Get("/api/v1/Client")]
        Task<ApiResponse<PagedResponse<ClientResponse>>> GetClientAsync(ClientSearchRequest request = default, PaginationQuery pagination = default);

        [Delete("/api/v1/Client/{id}")]
        Task<ApiResponse<bool>> DeleteClientAsync(int id);
        #endregion

        #region Administrator
        [Get("/api/v1/Administrator")]
        Task<ApiResponse<PagedResponse<AdministratorResponse>>> GetAdministratorAsync(AdministratorSearchRequest request = default, PaginationQuery pagination = default);
        #endregion


        #region Organizer
        [Get("/api/v1/Organizer")]
        Task<ApiResponse<PagedResponse<OrganizerResponse>>> GetOrganizerAsync(OrganizerSearchRequest request = default, PaginationQuery pagination = default);
        #endregion

        #region Performer
        [Get("/api/v1/Performer")]
        Task<ApiResponse<PagedResponse<PerformerResponse>>> GetPerformerAsync(PerformerSearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Performer/{id}")]
        Task<ApiResponse<PerformerResponse>> GetPerformerAsync(int id);

        [Post("/api/v1/Performer")]
        Task<ApiResponse<PerformerResponse>> CreatePerformerAsync(PerformerUpsertRequest request);

        [Put("/api/v1/Performer/{id}")]
        Task<ApiResponse<PerformerResponse>> UpdatePerformerAsync(int id, PerformerUpsertRequest request);

        [Delete("/api/v1/Performer/{id}")]
        Task<ApiResponse<bool>> DeletePerformerAsync(int id);
        #endregion

        #region Sponsor
        [Get("/api/v1/Sponsor")]
        Task<ApiResponse<PagedResponse<SponsorResponse>>> GetSponsorAsync(SponsorSearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Sponsor/{id}")]
        Task<ApiResponse<SponsorResponse>> GetSponsorAsync(int id);

        [Post("/api/v1/Sponsor")]
        Task<ApiResponse<SponsorResponse>> CreateSponsorAsync(SponsorUpsertRequest request);

        [Put("/api/v1/Sponsor/{id}")]
        Task<ApiResponse<SponsorResponse>> UpdateSponsorAsync(int id, SponsorUpsertRequest request);

        [Delete("/api/v1/Sponsor/{id}")]
        Task<ApiResponse<bool>> DeleteSponsorAsync(int id);
        #endregion

        #region Venue
        [Get("/api/v1/Venue")]
        Task<ApiResponse<PagedResponse<VenueResponse>>> GetVenueAsync(VenueSearchRequest request = default, PaginationQuery pagination = default);

        [Get("/api/v1/Venue/{id}")]
        Task<ApiResponse<VenueResponse>> GetVenueAsync(int id);

        [Post("/api/v1/Venue")]
        Task<ApiResponse<VenueResponse>> CreateVenueAsync(VenueUpsertRequest request);

        [Put("/api/v1/Venue/{id}")]
        Task<ApiResponse<VenueResponse>> UpdateVenueAsync(int id, VenueUpsertRequest request);

        [Delete("/api/v1/Venue/{id}")]
        Task<ApiResponse<bool>> DeleteVenueAsync(int id);
        #endregion

    }
}