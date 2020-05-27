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




        public QuestionPage()
        {
            InitializeComponent();

        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(zagadkaEntry.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry1.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry2.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry3.Text) && String.IsNullOrWhiteSpace(odpowiedzEntry4.Text) && String.IsNullOrWhiteSpace(odpowiedzPrawidlowa.Text) && String.IsNullOrWhiteSpace(numerTrasy.Text))
                {
                    await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
                    return;
                }

                var json = JsonConvert.SerializeObject(new
                {
                    Answers = new List<string> {
                odpowiedzEntry1.Text, odpowiedzEntry2.Text, odpowiedzEntry3.Text, odpowiedzEntry4.Text },
                    Correct = odpowiedzPrawidlowa.Text,
                    Question1 = zagadkaEntry.Text,
                    RouteId = Convert.ToInt16(numerTrasy.Text)
                });
                var token = Application.Current.Properties["MyToken"].ToString();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PostAsync("http://dearjean.ddns.net:44301/api/AnswerAndQuestion2", content);
                if (result.StatusCode == HttpStatusCode.OK)
                    await DisplayAlert("Błąd", "Nie masz odpowiednich uprawnień na kreatora! Skontaktuj się z administratorem", "Anuluj");
                else
                    await DisplayAlert("Sukces", "Dodanie zagadki do Trasy przebiegło pomyślnie", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", "Uzupełnij wszystkie pola", "Ok");
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