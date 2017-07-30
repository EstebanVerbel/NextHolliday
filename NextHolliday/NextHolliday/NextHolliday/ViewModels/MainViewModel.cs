using MvvmHelpers;
using NextHolliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHolliday.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

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
            DateTime centuryBegin = new DateTime(2017, 1, 1);
            DateTime currentDate = DateTime.Now;

            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            //_hours = elapsedSpan.Hours;
            //_minutes = elapsedSpan.Minutes;
            //_seconds = elapsedSpan.Seconds;

            RemainingTime = new Models.RemainingTime();

            _days = elapsedSpan.Days;
            _hours = elapsedSpan.Hours;

            RemainingTime.Days = _days;
            RemainingTime.Hours = _hours;
            RemainingTime.Minutes = _minutes;
            RemainingTime.Hours = _hours;
        }

        #endregion

        private int _days;
        private int _hours;
        private int _minutes;
        private int _seconds;

        #region -- Commands --

        private void UpdateRemainingTimeCommand()
        {
            if (IsBusy)
                return;

            RemainingTime.Hours = _hours;
            RemainingTime.Minutes = _minutes;
            RemainingTime.Hours = _hours;

        }
        
        #endregion

    }
}
