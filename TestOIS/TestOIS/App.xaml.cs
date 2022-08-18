using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestOIS
{
    using System.IO;
    using ViewModels;
    using Views;
    using Data;

    public partial class App : Application
    {
        public App ()
        {
            //MainViewModel.Instance.SQLiteHelper = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbOIS.db3"));
            //MainViewModel.Instance.ProductsViewModel = new ProductsViewModel();
            InitializeComponent();
         
            MainPage = new ProductsPage();
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

