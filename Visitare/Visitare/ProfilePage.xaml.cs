using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            GetMine();
        }
        /*
        private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                avatar.Source = ImageSource.FromStream(() => stream);
            }
            (sender as Button).IsEnabled = true;
        }
        */
        private async void GetMine()
        {
            var token = Application.Current.Properties["MyToken"].ToString();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetStringAsync("http://dearjean.ddns.net:44301/api/Rewards/GetMine");

            var tablicaW = JsonConvert.DeserializeObject<RewardsGetMine>(response);

            this.BindingContext = tablicaW;
        }
        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(oldPassword.Text) || String.IsNullOrWhiteSpace(newPassword.Text) || String.IsNullOrWhiteSpace(confirmNewPassword.Text))
            {
                await DisplayAlert("Błąd", "Wypełnij puste pola", "OK");
                return;
            }
            var uri = new Uri(string.Format("http://dearjean.ddns.net:44301/api/Account/ChangePassword", string.Empty));
            var token = Application.Current.Properties["MyToken"].ToString();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var model = new ChangePasswordModel
            {
                OldPassword = oldPassword.Text,
                NewPassword = newPassword.Text,
                ConfirmPassword = confirmNewPassword.Text,
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Jest OK!");
                await DisplayAlert("Sukces", "Zmiana hasła zakończona sukcesem", "OK");
            }
            else
            {
                Debug.WriteLine(response);
                await DisplayAlert("Błąd", "Spróbuj ponownie", "OK");
            }
        }
    }
}