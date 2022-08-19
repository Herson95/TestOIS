using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestOIS
{
    using System.IO;
    using Models;
    using ViewModels;
    using Views;
    using Data;

    public partial class App : Application
    {
        public App ()
        {
            
            InitializeComponent();
            Current.Properties["userLogged"] = null;
            MainViewModel.Instance.SQLiteHelper = new SQLiteHelper(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "dbOIS.db3"));
            MainViewModel.Instance.LoginViewModel = new LoginViewModel();
          
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
            
        }
    }
}

