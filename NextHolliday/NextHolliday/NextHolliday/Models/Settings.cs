using Plugin.Settings;

namespace NextHolliday.Models
{
    public static class Settings
    {
        #region -- Keys --

        private const string CountryKey = "country";
        private const string ProvinceKey = "province";

        #endregion

        #region -- Properties --

        public static string Country
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault(CountryKey, string.Empty);
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue(CountryKey, value);
            }
        }

        public static string Province
        {
            get
            {
                return CrossSettings.Current.GetValueOrDefault(ProvinceKey, string.Empty);
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue(ProvinceKey, value);
            }
        }

        #endregion

    }
}
