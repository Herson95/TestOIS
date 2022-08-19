using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestOIS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
       
        public ProductsPage()
        {
            InitializeComponent();

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}

