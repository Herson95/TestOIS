namespace TestOIS.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Models;
    using Services;
    using Xamarin.Forms;
    using Interfaces;
    using System.Windows.Input;
    using Views;
    using System.Linq;
    using System.Diagnostics;

    public class ProductsViewModel : BaseViewModel
    {
        #region Constructor
        public ProductsViewModel()
        {
            fileHelper = DependencyService.Get<IFileHelper>();
            MainViewModel.Instance.ProductService = new ProductService();
            Device.InvokeOnMainThreadAsync(async () => await GetProductsAsync());

        }
        #endregion

        #region Attributes
        private readonly IFileHelper fileHelper;
        private ObservableCollection<Product> products;
        private string search;

        #endregion

        #region Properties
        public User UserLogged { get; set; } = Application.Current.Properties["userLogged"] as User;

        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public string Search
        {
            get => search;
            set
            {
              SetProperty(ref search, value);
              SearchBy();
            }
        }
        #endregion


        #region Methods
        public async Task GetProductsAsync()
        {
            IsBusy = true;
            List<Product> listProduct = await MainViewModel.Instance.SQLiteHelper.GetProductsAsync();

            if (listProduct.Count < 1)
            {
                ProductService service = MainViewModel.Instance.ProductService;

                Response checkConnection = await service.CheckConnectionAsync();
                if (!checkConnection.IsSuccess)
                {
                    var tryAgain = await Application.Current.MainPage.DisplayAlert("Alert", checkConnection.Message, "Try again", "Cancel");
                    if (tryAgain)
                    {
                        await GetProductsAsync();

                    }
                    IsBusy = false;
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                Response response = await service.GetProductsAsync();
                if (!response.IsSuccess)
                {
                    var tryAgain = await Application.Current.MainPage.DisplayAlert("Alert", "Server error", "Try again", "Cancel");
                    if (tryAgain)
                    {
                        await GetProductsAsync();

                    }
                    IsBusy = false;
                    Process.GetCurrentProcess().Kill();
                    return;
                }
                listProduct = response.Result as List<Product>;

                foreach (Product item in listProduct)
                {
                    fileHelper.DownloadImage(item.Image);
                }
                await MainViewModel.Instance.SQLiteHelper.SaveProductsLocalAsync(listProduct);
            }


            Products = new ObservableCollection<Product>(listProduct);

            IsBusy = false;

        }

        public async void SearchBy()
        {
            var fullProducts = await MainViewModel.Instance.SQLiteHelper.GetProductsAsync();
            if (fullProducts.Count>0)
            {
                if (string.IsNullOrEmpty(Search))
                {
                    Products = new ObservableCollection<Product>(fullProducts);
                }
                else
                {
                    Products = new ObservableCollection<Product>(fullProducts.Where(x=>x.Title.ToLower().Contains(Search.ToLower())));
                }



            }
        }
        #endregion

        #region Commands
        public ICommand SelectedProductCommand
        {
            get
            {
                return new Command<Product>(async (product) =>
                {
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;
                    MainViewModel.Instance.ProductViewModel = new ProductViewModel
                    {
                        Product = product
                    };
                    await Application.Current.MainPage.Navigation.PushAsync(new ProductPage());
                    IsBusy = false;
                });
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Application.Current.Properties["userLogged"] = null;
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    
                });
            }
        }
        #endregion



    }
}

