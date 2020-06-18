using Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Support.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
       public ListView ListView => listview;
        public MenuPage()
        {
            InitializeComponent();
            listview.ItemsSource = App.Locator.Menu.MenuItems;
        }

        private void supportFormClicked_Clicked(object sender, EventArgs e)
        {

        }
        
    }
}
