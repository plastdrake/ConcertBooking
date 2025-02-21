using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ConcertBooking.MAUI.Services
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IMapper _mapper;

        public RestService(IHttpsClientHandlerService service, IMapper mapper)
        {
            _mapper = mapper;

#if DEBUG
            var handler = service.GetPlatformMessageHandler();
            _client = handler != null ? new HttpClient(handler) : new HttpClient();
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<ObservableCollection<Booking>> RefreshBookingDataAsync()
        {
            var bookings = new ObservableCollection<Booking>();
            try
            {
                var response = await _client.GetAsync(string.Format(Constants.BookingUrl, ""));
                if (response.IsSuccessStatusCode)
                {
                    var items = await response.Content.ReadFromJsonAsync<List<BookingDTO>>(_serializerOptions);
                    foreach (var item in items)
                        bookings.Add(_mapper.Map<Booking>(item));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }
            return bookings;
        }

        public async Task SaveBookingAsync(Booking item, bool isNewItem)
        {
            var json = JsonSerializer.Serialize(_mapper.Map<BookingDTO>(item), _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = isNewItem
                ? await _client.PostAsync(string.Format(Constants.BookingUrl, ""), content)
                : await _client.PutAsync(string.Format(Constants.BookingUrl, item.BookingId), content);
            Debug.WriteLine(response.IsSuccessStatusCode ? "Booking successfully saved." : $"ERROR: {response.ReasonPhrase}");
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var response = await _client.DeleteAsync(string.Format(Constants.BookingUrl, id));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateBookingAsync(BookingCreateDTO bookingDto)
        {
            return await PostAsync(Constants.CreateBookingUrl, bookingDto);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            try
            {
                var response = await _client.GetAsync($"{Constants.RestUrl}/booking/customer/{customerId}");
                return response.IsSuccessStatusCode
                    ? (await response.Content.ReadFromJsonAsync<List<BookingDTO>>(_serializerOptions)).Select(_mapper.Map<Booking>)
                    : Enumerable.Empty<Booking>();
            }
            catch
            {
                return Enumerable.Empty<Booking>();
            }
        }

        public async Task<Booking?> GetBookingByBookingIdAsync(int bookingId)
        {
            return await GetAsync<BookingDTO, Booking>(string.Format(Constants.BookingUrl, bookingId));
        }

        public async Task<bool> RegisterCustomerDataAsync(Customer customer)
        {
            var customerDto = new
            {
                firstName = customer.FirstName,
                lastName = customer.LastName,
                email = customer.Email,
                password = customer.Password
            };

            return await PostAsync(Constants.CustomerRegisterUrl, customerDto);
        }

        public async Task<Customer?> LoginAsync(string email, string password)
        {
            return await PostAsync<LoginDTO, CustomerDTO, Customer>(
                Constants.CustomerLoginUrl,
                new LoginDTO { Email = email, Password = password }
            );
        }

        public async Task<Customer?> GetProfileAsync(int customerId)
        {
            return await GetAsync<CustomerDTO, Customer>($"{Constants.RestUrl}/customer/getById/{customerId}");
        }

        public async Task<bool> UpdateProfileAsync(Customer customer)
        {
            return await PutAsync(string.Format(Constants.CustomerUpdateUrl, customer.Id), _mapper.Map<CustomerDTO>(customer));
        }

        public async Task<ObservableCollection<Concert>> RefreshConcertDataAsync()
        {
            return await GetCollectionAsync<ConcertDTO, Concert>(string.Format(Constants.ConcertUrl, ""));
        }

        public async Task<Concert?> GetConcertByIdAsync(int id)
        {
            return await GetAsync<ConcertDTO, Concert>(string.Format(Constants.ConcertUrl, id));
        }

        public async Task<ObservableCollection<Performance>> GetPerformancesAsync(int concertId, int customerId)
        {
            return await GetCollectionAsync<PerformanceDTO, Performance>($"{Constants.RestUrl}/performance/{concertId}/{customerId}");
        }

        #region Generic Helper Methods

        private async Task<TModel?> GetAsync<TDto, TModel>(string uri)
        {
            try
            {
                var response = await _client.GetAsync(uri);
                return response.IsSuccessStatusCode
                    ? _mapper.Map<TModel>(await response.Content.ReadFromJsonAsync<TDto>(_serializerOptions))
                    : default;
            }
            catch
            {
                return default;
            }
        }

        private async Task<ObservableCollection<TModel>> GetCollectionAsync<TDto, TModel>(string uri)
        {
            var collection = new ObservableCollection<TModel>();
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var items = await response.Content.ReadFromJsonAsync<List<TDto>>(_serializerOptions);
                    foreach (var item in items)
                        collection.Add(_mapper.Map<TModel>(item));
                }
            }
            catch
            {
                Debug.WriteLine("Error in GetCollectionAsync");
            }
            return collection;
        }

        private async Task<bool> PostAsync<TDto>(string uri, TDto dto)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(dto, _serializerOptions),
                    Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);

                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Response: {response.StatusCode} - {responseBody}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in PostAsync: {ex.Message}");
                return false;
            }
        }

        private async Task<TResult?> PostAsync<TRequest, TResponse, TResult>(string uri, TRequest dto)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(dto, _serializerOptions),
                    Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);

                return response.IsSuccessStatusCode
                    ? _mapper.Map<TResult>(await response.Content.ReadFromJsonAsync<TResponse>(_serializerOptions))
                    : default;
            }
            catch
            {
                return default;
            }
        }

        private async Task<bool> PutAsync<TDto>(string uri, TDto dto)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(dto, _serializerOptions),
                    Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(uri, content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}