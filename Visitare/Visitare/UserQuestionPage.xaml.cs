using Newtonsoft.Json;
using Syncfusion.XForms.Buttons;
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
            Result = false;
            InitializeComponent();

            pointsList = siema.routeQuestions;


            elo.ItemsSource = pointsList;

            SfRadioButton radioButton = new SfRadioButton();
            
            
         
           
            
        }
        private async void state(object sender, StateChangedEventArgs e)
        {




        }


    }
}