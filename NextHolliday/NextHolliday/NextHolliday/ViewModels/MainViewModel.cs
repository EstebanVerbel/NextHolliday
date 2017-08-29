using MvvmHelpers;
using NextHolliday.Models;
using NextHolliday.Models.Repository;
using NextHolliday.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NextHolliday.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        
        private const string COUNTRY = "Canada"; // move these constants to it's own class
        private const string PROVINCE = "Ontario";


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
        
        #endregion

        #region -- Constructor --

        public MainViewModel()
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

        #region -- Private Methods --

        private Holliday GetNextHolliday()
        {
            List<Holliday> loadedHollidays = HollidayLoader.LoadedHollidays.ToList();

            // this list will hold all loaded hollidays for selected COuntry and Province/State
            List<Holliday> provinceCountryHollidays = new List<Holliday>();
            
            foreach (Holliday holliday in loadedHollidays)
            {
                if ((holliday.Country == COUNTRY) && (holliday.Province == PROVINCE))
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

        #endregion

        #region -- Public Methods --

        public void SayNextHolliday()
        {
            var textToSpeechService = ServiceManager.Container.Resolve(typeof(ITextToSpeech), "TextToSpeechService") as ITextToSpeech;

            string text = $"{NextHolliday.Name} is in {RemainingTime.Days} days";

            textToSpeechService.Speak(text);
        }

        #endregion

        #region -- Commands --

        private void UpdateRemainingTimeCommand()
        {
            
        }
        
        #endregion
        
    }
}
