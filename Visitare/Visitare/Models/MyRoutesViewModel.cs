using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Visitare.Models
{
    public class MyRoutesViewModel
    {
        public ObservableCollection<Routes> Routes { get; set; }
        public Command<Routes> RemoveCommand
        {
            get
            {
                return new Command<Routes>((route) =>
                {
                    Routes.Remove(route);
                });
            }
        }

        public MyRoutesViewModel() { }
        
    }
}
