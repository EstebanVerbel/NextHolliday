using MvvmHelpers;
using NextHolliday.Models;
using NextHolliday.Models.Repository;
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

        private DateTime _nextHollidayDate;

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

        #endregion

        #region -- Constructor --

        public MainViewModel()
        {
            // get the next holliday
            Holliday nextHolliday = GetNextHolliday();


            _nextHollidayDate = nextHolliday.Date;
            DateTime currentDate = DateTime.Now;



            
            
            long elapsedTicks = _nextHollidayDate.Ticks - currentDate.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            
            RemainingTime = new RemainingTime();
            
            RemainingTime.Days = elapsedSpan.Days;
            RemainingTime.Hours = elapsedSpan.Hours;
            RemainingTime.Minutes = elapsedSpan.Minutes;
            RemainingTime.Seconds = elapsedSpan.Seconds;
            
            // Attach a couple event handlers.
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);
        }

        #endregion


        private Holliday GetNextHolliday()
        {
            List<Holliday> loadedHollidays = HollidayLoader.LoadedHollidays.ToList();

            return loadedHollidays[1];

            //foreach (Holliday holliday in loadedHollidays)
            //{
            //    if (true)
            //    {

            //    }
            //}

        }


        private bool OnTimerTick()
        {
            DateTime currentDate = DateTime.Now;

            long elapsedTicks = _nextHollidayDate.Ticks - currentDate.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            RemainingTime.Days = elapsedSpan.Days;
            RemainingTime.Hours = elapsedSpan.Hours;
            RemainingTime.Minutes = elapsedSpan.Minutes;
            RemainingTime.Seconds = elapsedSpan.Seconds;
            
            return true;
        }




        #region -- Commands --

        private void UpdateRemainingTimeCommand()
        {
            if (IsBusy)
                return;

            

        }
        
        #endregion



    }
}
