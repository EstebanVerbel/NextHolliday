using MvvmHelpers;

namespace NextHolliday.Models
{
    public class RemainingTime : ObservableObject
    {

        private int _days;

        public int Days
        {
            get { return _days; }
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _hours;

        public int Hours
        {
            get { return _hours; }
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _minutes;

        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _seconds;

        public int Seconds
        {
            get { return _seconds; }
            set { _seconds = value; OnPropertyChanged(); }
        }
        
    }
}
