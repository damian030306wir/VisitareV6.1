using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Visitare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserQuestionPage : ContentPage
    {

        public List<Question> pointsList = new List<Question>();

        Question pytania = new Question();
       

        public bool Result { get; set; }
        public UserQuestionPage(RouteQuestions siema)
        {

            InitializeComponent();

            pointsList = siema.routeQuestions;


            elo.ItemsSource = pointsList;
            Result = false;

        }
        private async void indexOne(object sender, EventArgs args)
        {



            var button = sender as Button;

       

            var route = button?.BindingContext as Routes;

            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2");
            List<Question> punkty = JsonConvert.DeserializeObject<List<Question>>(response);
            List<Question> list = new List<Question>();


            foreach (Question tmp in pointsList)
            {
                Result = false;
                if (Convert.ToString(tmp.Correct) == Convert.ToString((sender as Button).Text))
                {
                    Result = true;
                    break;
                }
            }
            await DisplayAlert("", "Poprawna odpowiedź: " + (Result ? "dobrze" : "źle"), "OK");
            if(Result)
            {
                var token1 = Application.Current.Properties["MyToken"].ToString();

                HttpClient client1 = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);

                var response1 = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get50");
                await DisplayAlert("Sukces", "Otrzymujesz 50 punktów za poprawną odpowiedź!", "Ok");
            }
        }



        private async void indexTwo(object sender, EventArgs args)
        {

            var button = sender as Button;

            var route = button?.BindingContext as Routes;

            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2");
            List<Question> punkty = JsonConvert.DeserializeObject<List<Question>>(response);
            List<Question> list = new List<Question>();
            foreach (Question tmp in pointsList)
            {
                Result = false;
                if (Convert.ToString(tmp.Correct) == Convert.ToString((sender as Button).Text))
                {
                    Result = true;
                    break;
                }
            }
            await DisplayAlert("", "Poprawna odpowiedź: " + (Result ? "dobrze" : "źle"), "OK");
            if (Result)
            {
                var token1 = Application.Current.Properties["MyToken"].ToString();

                HttpClient client1 = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);

                var response1 = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get50");
                await DisplayAlert("Sukces", "Otrzymujesz 50 punktów za poprawną odpowiedź!", "Ok");
            }

        }
        private async void indexThree(object sender, EventArgs args)
        {



            var button = sender as Button;
            var route = button?.BindingContext as Routes;
            
            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2");
            List<Question> punkty = JsonConvert.DeserializeObject<List<Question>>(response);
            List<Question> list = new List<Question>();


            foreach (Question tmp in pointsList)
            {
                Result = false;
                if (Convert.ToString(tmp.Correct) == Convert.ToString((sender as Button).Text))
                {
                    Result = true;
                    break;
                }
            }
            await DisplayAlert("", "Poprawna odpowiedź: " + (Result ? "dobrze" : "źle"), "OK");
            if (Result)
            {
                var token1 = Application.Current.Properties["MyToken"].ToString();

                HttpClient client1 = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);

                var response1 = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get50");
                await DisplayAlert("Sukces", "Otrzymujesz 50 punktów za poprawną odpowiedź!", "Ok");
            }
        }
        private async void indexFour(object sender, EventArgs args)
        {


            var button = sender as Button;
            
            var route = button?.BindingContext as Routes;
            var token = Application.Current.Properties["MyToken"].ToString();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2");
            List<Question> punkty = JsonConvert.DeserializeObject<List<Question>>(response);
            List<Question> list = new List<Question>();

            foreach (Question tmp in pointsList)
            {
                Result = false;
                if (Convert.ToString(tmp.Correct) == Convert.ToString((sender as Button).Text))
                {
                    Result = true;
                    break;
                }
            }
            await DisplayAlert("", "Poprawna odpowiedź: " + (Result ? "dobrze" : "źle"), "OK");
            if (Result)
            {
                var token1 = Application.Current.Properties["MyToken"].ToString();

                HttpClient client1 = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);

                var response1 = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get50");
                await DisplayAlert("Sukces", "Otrzymujesz 50 punktów za poprawną odpowiedź!", "Ok");
            }
        }


    }

}
