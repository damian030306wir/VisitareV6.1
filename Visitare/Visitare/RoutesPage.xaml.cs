using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Visitare.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Visitare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutesPage : ContentPage
    {
        public List<Routes> trasy;
        public RoutesPage()
        {
            InitializeComponent();
            GetRoutes();
        }
        private async void GetRoutes()
        {
            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Routes");
            var trasy = JsonConvert.DeserializeObject<List<Routes>>(response);
            routesList.BeginRefresh();
            routesList.ItemsSource = trasy;
            routesList.EndRefresh();

        }
        private async void MethodSearchRoutes(object sender, TextChangedEventArgs e)
        {
            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Routes");
            var trasy = JsonConvert.DeserializeObject<List<Routes>>(response);
            routesList.ItemsSource = trasy;

            var keyword = SearchRoutes.Text;
            var result = trasy.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            routesList.ItemsSource = result;
          
        }
        private async void OnTapped(object sender, ItemTappedEventArgs e)
        {
            var token = Application.Current.Properties["MyToken"].ToString();
            var tapped = (Routes)e.Item;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/points3");
            List<Points> punkty = JsonConvert.DeserializeObject<List<Points>>(response);
            List<Points> list = new List<Points>();
            foreach (Points tmp in punkty)
            {
                if(tmp.RouteId == tapped.Id)
                {
                    list.Add(tmp);
                }
            }
            await Navigation.PushAsync(new MainPage(new RoutePoints(list)));

        }
        private async void OnQuizClicked(object sender, EventArgs e)
        {
             var button = sender as ImageButton;
             var route = button?.BindingContext as Routes;
            
            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2");
            List<Question> punkty = JsonConvert.DeserializeObject<List<Question>>(response);
            List<Question> list = new List<Question>();
            foreach (Question tmp in punkty)
            {
                if (tmp.RouteId == route.Id)
                {
                    list.Add(tmp);
                }
            }
            await Navigation.PushAsync(new UserQuestionPage(new RouteQuestions(list)));
        }
    }
}