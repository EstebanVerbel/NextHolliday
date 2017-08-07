﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NextHolliday.Models.Repository
{
    public static class HollidayLoader
    {
        
        private const string FILE_NAME = "hollidays.json";
        
        public static IStreamLoader Loader { get; set; }
        
        public static async Task<IEnumerable<Holliday>> Load()
        {
            Stream stream = OpenData();

            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<List<Holliday>>(await reader.ReadToEndAsync());
            }
        }
        
        private static Stream OpenData()
        {
            if (Loader == null)
                throw new Exception("The IStreamLoader hasn't been set");

            return Loader.GetStreamForFilename(FILE_NAME);
        }
        
    }
}