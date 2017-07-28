using MvvmHelpers;

namespace NextHolliday.Models
{
    public class RemainingTime : ObservableObject
    {
        
        private int _hours;

        public int Hours
        {
            get { return _hours; }
            set { _hours = value; OnPropertyChanged(); }
        }

        private int _minutes;

        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; OnPropertyChanged(); }
        }

        private int _seconds;

        public int Seconds
        {
            get { return _seconds; }
            set { _seconds = value; OnPropertyChanged(); }
        }
        
    }
}
