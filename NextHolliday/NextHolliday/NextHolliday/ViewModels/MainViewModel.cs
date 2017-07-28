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

        }

        #endregion

    }
}
