// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace NextHolliday.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
                return CrossSettings.Current;
            }
		}

        #region Setting Constants

        private const string CountryKey = "country_key";
        private const string ProvinceKey = "province_key";

        private static readonly string SettingsDefault = string.Empty;
        
        #endregion
        
        public static string CountrySetting
		{
			get
			{
				return AppSettings.GetValueOrDefault(CountryKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CountryKey, value);
			}
		}

        public static string ProvinceSetting
        {
            get
            {
                return AppSettings.GetValueOrDefault(ProvinceKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ProvinceKey, value);
            }
        }
        
    }
}