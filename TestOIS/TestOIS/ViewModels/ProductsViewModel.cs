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

    public class ProductsViewModel : BaseViewModel
    {
        #region Constructor
        public ProductsViewModel()
        {
            fileHelper = DependencyService.Get<IFileHelper>();
            MainViewModel.Instance.ProductService = new ProductService();
            Task.Run(async () => await GetProductsAsync());
        }
        #endregion

        #region Attributes
        private readonly IFileHelper fileHelper;
        private ObservableCollection<Product> products;

        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetProperty(ref products,value);
        }
        #endregion


        #region Methods
        public async Task GetProductsAsync()
        {
            List<Product> listProduct = await MainViewModel.Instance.SQLiteHelper.GetProductsAsync();
           
            if (listProduct.Count<1)
            {
                ProductService service = MainViewModel.Instance.ProductService;

                Response checkConnection = await service.CheckConnectionAsync();
                if (!checkConnection.IsSuccess)
                {
                    var tryAgain = await Application.Current.MainPage.DisplayPromptAsync("Alert", checkConnection.Message, "Try again","Cancel");
                    if (tryAgain.Equals("Try again"))
                    {
                        await GetProductsAsync();

                    }
                    return;
                }

                Response response = await service.GetProductsAsync();
                if (!response.IsSuccess)
                {
                    var tryAgain = await Application.Current.MainPage.DisplayPromptAsync("Alert", "Server error", "Try again", "Cancel");
                    if (tryAgain.Equals("Try again"))
                    {
                        await GetProductsAsync();

                    }
                    return;
                }
                listProduct = response.Result as List<Product>;

                foreach (var item in listProduct)
                {
                    fileHelper.DownloadImage(item.Image);
                }
                await MainViewModel.Instance.SQLiteHelper.SaveProductsLocalAsync(listProduct);
            }

            

            Products = new ObservableCollection<Product>(listProduct);
            
        }
        #endregion



    }
}

