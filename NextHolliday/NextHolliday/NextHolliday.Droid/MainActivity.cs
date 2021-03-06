﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NextHolliday.Models.Repository;
using Microsoft.Practices.Unity;
using NextHolliday.Services;

namespace NextHolliday.Droid
{
    [Activity(Label = "NextHolliday", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        
        protected async override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            // load hollidays from Json file
            HollidayLoader.Loader = new StreamLoader(this);
            await HollidayLoader.Load();

            // register TextToSpeech service to unity container
            ServiceManager.Container = new UnityContainer();
            ServiceManager.Container.RegisterType<ITextToSpeech, TextToSpeechService>("TextToSpeechService");
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            
        }
        
    }
}

