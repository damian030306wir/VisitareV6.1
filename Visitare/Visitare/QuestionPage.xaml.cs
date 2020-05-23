using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Visitare
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        private Question question;

        private readonly UserQuiz pointsManager;


        public bool Result { get; set; }

        public QuestionPage(Question question)
        {
            InitializeComponent();
            BindingContext = question;
            this.question = question;

            Result = false;

            pointsManager = new UserQuiz();
            label1.BindingContext = pointsManager;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.ItemIndex == question.GoodAnswer)
            {
                Result = true;
            }
           

            Navigation.PopModalAsync();
        }
        private async void myEntry(object sender, EventArgs e)
        {


            int response = int.Parse(odpowiedzPrawidlowa.Text);

            if (response > 0 & response < 4)
            {

            }
            else
            {
                await DisplayAlert("Błąd", "Musisz podać odpowiedź z zakresu od 0 do 3", "OK");
            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(zagadkaEntry.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry1.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry2.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry3.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry4.Text) && String.IsNullOrWhiteSpace(odpowiedzPrawidlowa.Text))
                {
                    await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
                    return;
                }
           
            
            var question = new Question
            {
                Answers = new List<string> {
                odpowiedzEntry1.Text, odpowiedzEntry2.Text, odpowiedzEntry3.Text, odpowiedzEntry4.Text },
                GoodAnswer = Convert.ToInt16(odpowiedzPrawidlowa.Text),
                Question1 = zagadkaEntry.Text
            };
           

           

            var questionPage = new QuestionPage(question);
            questionPage.Disappearing += QuestionPageClosed;
            

            await Navigation.PushModalAsync(questionPage);

                var json = JsonConvert.SerializeObject(new
                {
                    Answers = new List<string> {
                odpowiedzEntry1.Text, odpowiedzEntry2.Text, odpowiedzEntry3.Text, odpowiedzEntry4.Text },
                    GoodAnswer = Convert.ToInt16(odpowiedzPrawidlowa.Text),
                    Question1 = zagadkaEntry.Text,
                    RouteId = Convert.ToInt16(numerTrasy.Text)
                });
                var token = Application.Current.Properties["MyToken"].ToString();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PostAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2", content);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
            }
        }

       private async void QuestionPageClosed(object sender, EventArgs e)
       {
         var questionPage = sender as QuestionPage;
         await DisplayAlert("", "Poprawna odpowiedź: " + (questionPage.Result ? "dobrze" : "źle"), "OK");
            if (questionPage.Result)
            {
                pointsManager.Points += 1;


                var token = Application.Current.Properties["MyToken"].ToString();

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/Get15");

            }

        }

        private async void TablicaWynikow(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Scoreboard());
        }

        private async void TwojePunkty(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyScore());
        }

    }
}