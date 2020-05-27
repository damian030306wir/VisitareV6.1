using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Net.Http.Headers;

namespace Visitare
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public List<Points> pointsList = new List<Points>();
        public MainPage()
        {
            InitializeComponent();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(53.010281, 18.604922), Distance.FromMiles(1.0)));
        }
        public MainPage(RoutePoints points)
        {
            InitializeComponent();
            pointsList = points.routePoints;
            Polyline polyline = new Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 5
            };
            foreach (Points tmp in points.routePoints)
            {
                CustomPin pin = new CustomPin
                {
                    Type = PinType.SavedPin,
                    Position = new Position(tmp.X, tmp.Y),
                    Label = tmp.Name,
                    Address = tmp.Description,
                    Name = "Xamarin",
                    Url = "http://xamarin.com/about/",
                    Question = "",
                    Answer = ""
                };

                if (String.IsNullOrWhiteSpace(tmp.Name))
                    pin.Label = "Name";

                pin.MarkerClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    string pinName = ((CustomPin)s).Label;
                    // string pytanie = ((CustomPin)s).Question;
                    string opis = ((CustomPin)s).Address;
                    // string odpowiedz = ((CustomPin)s).Answer;
                    await DisplayAlert($"{pinName}", $"{opis}", "Ok");
                    // await DisplayAlert("Quiz", $"{pytanie}", "Przejdź do odpowiedzi");
                    //await Navigation.PushAsync(new QuestionPage(new Question()));

                };
                polyline.Geopath.Add(new Position(tmp.X, tmp.Y));
                customMap.Pins.Add(pin);
            }
            customMap.MapElements.Add(polyline);
            if (points.routePoints.Count > 0)
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(points.routePoints[0].X, points.routePoints[0].Y), Distance.FromMiles(2.0)));
            else
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(53.010281, 18.604922), Distance.FromMiles(2.0)));
        }
        private async void OnLogOut(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(this);

            Application.Current.Properties["MyToken"] = "";
        }
        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            customMap.Pins.Clear();
            customMap.MapElements.Clear();
        }

        private async void OnRoutesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoutesPage());
        }

        private async void OnCreatorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatorPage());
        }
      
        private async void Rank(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Scoreboard());
        }
        private async void OnHereClicked(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    foreach (Points tmp in pointsList)
                    {
                        if (Location.CalculateDistance(tmp.X, tmp.Y, location, DistanceUnits.Kilometers) <= 0.05)
                        {
                            var token = Application.Current.Properties["MyToken"].ToString();
                            var json = JsonConvert.SerializeObject(tmp);
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            HttpClient client = new HttpClient();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            var result = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/VisitedPoints/Check/" + $"{tmp.Id}");
                            var check = JsonConvert.DeserializeObject<bool>(result);
                            if(check)
                            {
                                await DisplayAlert("Odwiedzono", "To miejsce zostało już odwiedzone!", "Ok");
                            }
                            else 
                            {
                                var result2 = await client.PostAsync("http://dearjean.ddns.net:44301/api/VisitedPoints/Add/" + $"{tmp.Id}", content);
                                
                                var token1 = Application.Current.Properties["MyToken"].ToString();

                                HttpClient client1 = new HttpClient();

                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);

                                var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get50");
                                await DisplayAlert("Sukces", "Otrzymujesz 50 punktów ekstra za odwiedzenie tego miejsca!", "Ok");
                            }
                            return;
                        }
                    }
                    await DisplayAlert("Niestety", "Nie jesteś w pobliżu punktu", "Ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Błąd", "Twoje urządzenie nie obsługuje tej usługi", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Błąd", "Włącz usługę gps", "Ok");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Błąd", "Brak zgody na korzystanie z usługi", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", "Nie odnaleziono lokacji", "Ok");
            }
        }
    }
}
