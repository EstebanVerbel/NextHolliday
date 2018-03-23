using NextHolliday.Helpers;
using NextHolliday.ViewModels;
using System;
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
            
            if (string.IsNullOrEmpty(Settings.CountrySetting))
            {
                // set boolean to hide stacklayout with selection to pick country and province
                viewModel.IsPickCountryAndProvince = true;
                viewModel.IsDisplayNextHolliday = false;
            }
            else
            {
                // load data with picked values
                string country = Settings.CountrySetting;
                string province = Settings.ProvinceSetting;

                viewModel.SelectedCountry = country;
                viewModel.SelectedState = province;

                viewModel.IsPickCountryAndProvince = false;
                viewModel.InitializeHollidayCountDown();
                viewModel.IsDisplayNextHolliday = true;
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).SayNextHolliday();
        }
    }
}
