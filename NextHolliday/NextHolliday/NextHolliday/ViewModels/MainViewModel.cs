using MvvmHelpers;
using NextHolliday.Helpers;
using NextHolliday.Models;
using NextHolliday.Models.Repository;
using NextHolliday.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace NextHolliday.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string CANADA = "Canada"; // move these constants to it's own class
        private const string ONTARIO = "Ontario";

        #region -- Members --

        private string _selectedCountry;
        private string _selectedState;

        #endregion

        #region -- Commands --

        public ICommand SaveUserLocationSubmitCommand { get; private set; }
        
        #endregion

        #region -- Properties --

        private RemainingTime _remainingTime;

        public RemainingTime RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                _remainingTime = value;
                SetProperty(ref _remainingTime, value);
            }
        }

        private Holliday _nextHolliday;

        public Holliday NextHolliday
        {
            get { return _nextHolliday; }
            set
            {
                _nextHolliday = value;
                SetProperty(ref _nextHolliday, value);
            }
        }

        public string DateToday
        {
            get { return DateTime.Now.ToString("MMMM dd, yyyy"); }
        }

        private bool _isPickCountryAndProvince;

        public bool IsPickCountryAndProvince
        {
            get { return _isPickCountryAndProvince; }
            set
            {
                _isPickCountryAndProvince = value;
                OnPropertyChanged();
            }
        }

        private bool _isDisplayNextHolliday;
        public bool IsDisplayNextHolliday
        {
            get { return _isDisplayNextHolliday; }
            set
            {
                _isDisplayNextHolliday = value;
                OnPropertyChanged();
            }
        }

        private bool _isSubmitEnabled;
        public bool IsSubmitEnabled
        {
            get { return _isSubmitEnabled; }
            set { _isSubmitEnabled = value; OnPropertyChanged(); }
        }

        private int _selectedCountryIndex;
        public int SelectedCountryIndex
        {
            get { return _selectedCountryIndex; }
            set
            {
                _selectedCountryIndex = value;

                if (value != -1)
                    _selectedCountry = Countries[_selectedCountryIndex];

                IsSubmitReady();
            }
        }

        private int _selectedStateIndex;
        public int SelectedStateIndex
        {
            get { return _selectedStateIndex; }
            set
            {
                _selectedStateIndex = value;

                if (value != -1)
                    _selectedState = States[_selectedStateIndex];

                IsSubmitReady();
            }
        }

        public string SelectedCountry { set { _selectedCountry = value;  } }
        public string SelectedState { set { _selectedState = value; } }

        public ObservableCollection<string> Countries { get; set; }

        public ObservableCollection<string> States { get; set; }

        #endregion

        #region -- Constructor --

        public MainViewModel()
        {
            IsSubmitEnabled = false;
            Countries = new ObservableCollection<string>();
            States = new ObservableCollection<string>();
            
            Countries.Add(CANADA);
            Countries.Add("US");
            States.Add(ONTARIO);
            States.Add("Quebec");

            SaveUserLocationSubmitCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(_selectedCountry))
                {
                    Settings.CountrySetting = _selectedCountry;
                }

                if (!string.IsNullOrEmpty(_selectedState))
                {
                    Settings.ProvinceSetting = _selectedState;
                }
                
                // hide / display
                IsPickCountryAndProvince = false;

                // initialize holliday count down
                InitializeHollidayCountDown();

                IsDisplayNextHolliday = true;
            });








            // I NEED TO MOVE ALL THIS CODE TO A COMMAND (BELOW)

            


        }

        #endregion

        #region -- Private Methods --

        private Holliday GetNextHolliday()
        {
            IEnumerable<Holliday> loadedHollidays = HollidayLoader.LoadedHollidays;

            // this list will hold all loaded hollidays for selected COuntry and Province/State
            List<Holliday> provinceCountryHollidays = new List<Holliday>();

            foreach (Holliday holliday in loadedHollidays)
            {
                if ((holliday.Country == _selectedCountry) && (holliday.Province == _selectedState))
                    provinceCountryHollidays.Add(holliday);
            }

            // no hollidays exist for selected country and province
            if (provinceCountryHollidays.Count == 0)
                return null;

            // sort Hollidays by date 
            provinceCountryHollidays.Sort((x, y) => x.Date.CompareTo(y.Date));

            // return next Holliday by comparing it with today's date
            for (int i = 0; i < provinceCountryHollidays.Count; i++)
            {
                if (provinceCountryHollidays[i].Date > DateTime.Now)
                    return provinceCountryHollidays[i];
            }

            return null;
        }

        private bool OnTimerTick()
        {
            DateTime currentDate = DateTime.Now;

            long elapsedTicks = _nextHolliday.Date.Ticks - currentDate.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            RemainingTime.Days = elapsedSpan.Days;
            RemainingTime.Hours = elapsedSpan.Hours;
            RemainingTime.Minutes = elapsedSpan.Minutes;
            RemainingTime.Seconds = elapsedSpan.Seconds;

            return true;
        }

        private void IsSubmitReady()
        {
            if (_selectedCountryIndex != -1
                && _selectedStateIndex != -1
                && !string.IsNullOrEmpty(_selectedCountry)
                && !string.IsNullOrEmpty(_selectedState))
            {
                IsSubmitEnabled = true;
            }
            else
            {
                IsSubmitEnabled = false;
            }
        }

        #endregion

        #region -- Public Methods --

        public void SayNextHolliday()
        {
            var textToSpeechService = ServiceManager.Container.Resolve(typeof(ITextToSpeech), "TextToSpeechService") as ITextToSpeech;

            string text = $"{NextHolliday.Name} is in {RemainingTime.Days} days";

            textToSpeechService.Speak(text);
        }

        public void InitializeHollidayCountDown()
        {
            IsBusy = true;

            // get the next holliday
            Holliday nextHolliday = GetNextHolliday();

            NextHolliday = nextHolliday;

            DateTime currentDate = DateTime.Now;

            long elapsedTicks = _nextHolliday.Date.Ticks - currentDate.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            RemainingTime = new RemainingTime();

            RemainingTime.Days = elapsedSpan.Days;
            RemainingTime.Hours = elapsedSpan.Hours;
            RemainingTime.Minutes = elapsedSpan.Minutes;
            RemainingTime.Seconds = elapsedSpan.Seconds;

            // Attach a couple event handlers.
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);

            IsBusy = false;
        }

        #endregion

    }
}
