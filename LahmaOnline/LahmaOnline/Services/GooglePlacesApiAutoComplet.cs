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
    public class GooglePlacesApiAutoComplet: Property.BaseProperty
    {
        public readonly string GooglePlacesApiAutoCompletePath 
            =  "https://maps.googleapis.com/maps/api/place/autocomplete/json?" +
               "key={0}&" +
               "input={1}&" +
               "components=country:{2}&" +
               "language={3}&" +
               $"sessiontoken={Guid.NewGuid()}&" +
               "radius=500"; //Adding country:us limits results to us

        public readonly string GooglePlacesApiKey = "AIzaSyCUpBOvZ-YcTW7aIz3mZ7VJccC_YiIpw_E";

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

        public async Task GetPlacesPredictionsAsync()
        {

            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time
            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, 
                string.Format(GooglePlacesApiAutoCompletePath, GooglePlacesApiKey, WebUtility.UrlEncode(_addressText), "JO", AppStatics.CluterLanguage)))
            { 
                //Be sure to UrlEncode the search term they enter
                using (HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        PlacesLocationPredictions predictionList = await Task.Run(() => JsonConvert.DeserializeObject<PlacesLocationPredictions>(json)).ConfigureAwait(false);

                        if (predictionList.Status == "OK")
                        {
                            Addresses.Clear();
                            if (predictionList.Predictions.Count > 0)
                            {
                                foreach (var prediction in predictionList.Predictions)
                                {
                                    Addresses.Add(new AddressInfo
                                    {
                                        Address = prediction.Description,                                        
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
