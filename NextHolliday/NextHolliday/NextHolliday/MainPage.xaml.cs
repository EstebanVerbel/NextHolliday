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
    }
}
