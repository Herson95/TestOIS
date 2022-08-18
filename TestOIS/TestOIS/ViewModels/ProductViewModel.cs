namespace TestOIS.ViewModels
{
    using Models;

    public class ProductViewModel : BaseViewModel
    {
       
        #region Constructor
        public ProductViewModel()
        {
        }
        #endregion

        #region Attributes
        private Product product;
        #endregion

        #region Properties
        public Product Product
        {
            get => product;
            set => SetProperty(ref product , value); }
        #endregion

    }
}

