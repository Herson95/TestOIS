namespace TestOIS.ViewModels
{
    using Services;
    using Data;

    public class MainViewModel
    {
        #region Constructor
        public MainViewModel()
        {
            instance = this;
        }
        #endregion

        #region Attributes
        private static MainViewModel instance;
        #endregion

        #region Properties
        //pattern singleton
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    return new MainViewModel();
                }
                return instance;
            }
        }

        public ProductService ProductService { get; set; }

        public SQLiteHelper SQLiteHelper { get; set; }

        public ProductsViewModel ProductsViewModel { get; set; }

        public ProductViewModel ProductViewModel { get; set; }

      
        #endregion

    }
}

