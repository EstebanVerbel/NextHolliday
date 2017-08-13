using NextHolliday.Services;
using NextHolliday.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NextHolliday
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MainViewModel viewModel = new MainViewModel();
            BindingContext = viewModel;
        }

        private void OnTapped(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).SayNextHolliday();
        }
    }
}
