using System;

namespace NextHolliday.Models
{
    public class Holliday
    {

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private string _province;

        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        private int _year;

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private int _month;

        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }

        private int _day;

        public int Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private DateTime _date;

        public DateTime Date
        {
            get
            {
                if (_date == default(DateTime))
                    _date = new DateTime(_year, _month, _day);
                
                return _date;
            }
        }
        
    }
}
