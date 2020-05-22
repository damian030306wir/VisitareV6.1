using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
    }
}