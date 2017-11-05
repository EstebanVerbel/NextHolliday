using NextHolliday.Helpers;
using NextHolliday.Models;
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

            countryPicker.SelectedIndex = -1;
            statePicker.SelectedIndex = -1;

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

                viewModel.IsPickCountryAndProvince = false;
                viewModel.IsDisplayNextHolliday = true;
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            (BindingContext as MainViewModel).SayNextHolliday();
        }
    }
}
