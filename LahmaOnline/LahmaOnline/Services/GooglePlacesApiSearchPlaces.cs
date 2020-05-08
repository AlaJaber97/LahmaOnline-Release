using BLL.M.Mobile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace LahmaOnline.Services
{
    public class GooglePlacesApiSearchPlaces : Property.BaseProperty
    {
        public readonly string GooglePlacesApiAutoCompletePath
            = "https://maps.googleapis.com/maps/api/place/textsearch/json?" +
               "key={0}&" +
               "query={1}&" +
               "fields=place_id&" +/*,formatted_address,name,geometry*/
               "radius=500"; //Adding country:us limits results to us

        public readonly string GooglePlacesApiKey = "AIzaSyC4cbA5t6kqf7O9TmuGcWPAdMwEn_OZXL8";

        private static HttpClient _httpClientInstance;
        public static HttpClient HttpClientInstance => _httpClientInstance ?? (_httpClientInstance = new HttpClient());

        private ObservableCollection<AddressInfo> _addresses;
        public ObservableCollection<AddressInfo> Addresses
        {
            get => _addresses ?? (_addresses = new ObservableCollection<AddressInfo>());
            set
            {
                if (_addresses != value)
                {
                    _addresses = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _addressText;
        public string AddressText
        {
            get => _addressText;
            set
            {
                if (_addressText != value)
                {
                    _addressText = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task FindPlaceAsync()
        {

            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time
            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                string.Format(GooglePlacesApiAutoCompletePath, GooglePlacesApiKey, WebUtility.UrlEncode(_addressText))))
            {
                //Be sure to UrlEncode the search term they enter
                using (HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        SearchPlacesResponses predictionList = await Task.Run(() => JsonConvert.DeserializeObject<SearchPlacesResponses>(json)).ConfigureAwait(false);

                        if (predictionList.Status == "OK")
                        {
                            Addresses.Clear();
                            if (predictionList.Results.Count > 0)
                            {
                                foreach (var prediction in predictionList.Results)
                                {
                                    Addresses.Add(new AddressInfo
                                    {
                                        Address = prediction.Name,
                                        Latitude = prediction.Geometry.Location.Lat,
                                        Longitude = prediction.Geometry.Location.Lng,
                                    });
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(predictionList.Status);
                        }
                    }
                }
            }
        }
    }
}

