/* The MIT License(MIT)

Copyright(c) 2017 James Montemagno

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace NextHolliday.Droid.Helpers
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