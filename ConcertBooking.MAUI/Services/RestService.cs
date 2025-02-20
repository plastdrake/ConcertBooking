using AutoMapper;
using ConcertBooking.DTO;
using ConcertBooking.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertBooking.MAUI.Services
{
    public class RestService : IRestService
    {
        private HttpClient _client;
        private JsonSerializerOptions _serializerOptions;
        private IHttpsClientHandlerService _httpsClientHandlerService;
        private IMapper _mapper;
        public ObservableCollection<Booking>? Bookings { get; private set; }

        public RestService(IHttpsClientHandlerService service, IMapper mapper)
        {
            _mapper = mapper;
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
#else
_client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<ObservableCollection<Booking>?> RefreshBookingDataAsync()
        {
            Bookings = new ObservableCollection<Booking>();
            var uri = new Uri(string.Format(Constants.BookingUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonSerializer.Deserialize<List<BookingDTO>>(content, _serializerOptions);
                    foreach (var item in items)
                    {
                        Bookings.Add(_mapper.Map<Booking>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return Bookings;
        }
        public async Task SaveBookingAsync(Booking item, bool isNewItem = false)
        {
            var uri = new Uri(string.Format(Constants.BookingUrl, string.Empty));
            try
            {
                var json = JsonSerializer.Serialize(_mapper.Map<BookingDTO>(item), _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    response = await _client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tBooking successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.BookingUrl, id));
            try
            {
                var response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tBooking successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> CreateBookingAsync(BookingCreateDTO bookingDto)
        {
            var uri = new Uri(string.Format(Constants.BookingUrl, string.Empty));
            try
            {
                var json = JsonSerializer.Serialize(bookingDto, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tBooking successfully created.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var uri = new Uri($"{Constants.RestUrl}/booking/customer/{customerId}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonSerializer.Deserialize<List<BookingDTO>>(content, _serializerOptions);
                    return items.Select(item => _mapper.Map<Booking>(item));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return new List<Booking>();
        }

        public async Task<Booking?> GetBookingByBookingIdAsync(int bookingId)
        {
            var uri = new Uri(string.Format(Constants.BookingUrl, bookingId));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var item = JsonSerializer.Deserialize<BookingDTO>(content, _serializerOptions);
                    return _mapper.Map<Booking>(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<bool> RegisterCustomerDataAsync(Customer customer)
        {
            var uri = new Uri(Constants.CustomerRegisterUrl);
            try
            {
                var json = JsonSerializer.Serialize(_mapper.Map<CustomerDTO>(customer), _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tCustomer successfully registered.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<Customer?> LoginAsync(string email, string password)
        {
            var uri = new Uri($"{Constants.RestUrl}/customer/login/{email}/{password}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var item = JsonSerializer.Deserialize<CustomerDTO>(content, _serializerOptions);
                    return _mapper.Map<Customer>(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<Customer?> GetProfileAsync(int customerId)
        {
            var uri = new Uri($"{Constants.RestUrl}/customer/getById/{customerId}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var item = JsonSerializer.Deserialize<CustomerDTO>(content, _serializerOptions);
                    return _mapper.Map<Customer>(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<bool> UpdateProfileAsync(Customer customer)
        {
            var uri = new Uri(string.Format(Constants.CustomerUpdateUrl, customer.Id));
            try
            {
                var json = JsonSerializer.Serialize(_mapper.Map<CustomerDTO>(customer), _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tCustomer successfully updated.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<ObservableCollection<Concert>?> RefreshConcertDataAsync()
        {
            var concerts = new ObservableCollection<Concert>();
            var uri = new Uri(string.Format(Constants.ConcertUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonSerializer.Deserialize<List<ConcertDTO>>(content, _serializerOptions);
                    foreach (var item in items)
                    {
                        concerts.Add(_mapper.Map<Concert>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return concerts;
        }

        public async Task<Concert?> GetConcertByIdAsync(int id)
        {
            var uri = new Uri(string.Format(Constants.ConcertUrl, id));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var item = JsonSerializer.Deserialize<ConcertDTO>(content, _serializerOptions);
                    return _mapper.Map<Concert>(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Performance>> GetPerformancesAsync(int concertId, int customerId)
        {
            var performances = new ObservableCollection<Performance>();
            var uri = new Uri($"{Constants.RestUrl}/performance/{concertId}/{customerId}");
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonSerializer.Deserialize<List<PerformanceDTO>>(content, _serializerOptions);
                    foreach (var item in items)
                    {
                        performances.Add(_mapper.Map<Performance>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return performances;
        }
    }
}
