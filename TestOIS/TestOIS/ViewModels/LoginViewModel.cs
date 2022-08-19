using System.Threading.Tasks;
using System.Windows.Input;
using TestOIS.Data;
using TestOIS.Views;
using Xamarin.Forms;

namespace TestOIS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string userName;
        private string password;

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }


        #region Methods
        private async Task Action(string action)
        {
            if (action.Equals("Login"))
            {
                await LoginAsync();
            }
            else
            {
                UserName = string.Empty;
                Password = string.Empty;

                MainViewModel.Instance.CreateAccountViewModel = new CreateAccountViewModel() { User = new Models.User()};
                await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountPage());

            }
            
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert","Username is empty","Ok");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password is empty", "Ok");
                return;
            }

            var passwordMD5 = Encryptor.MD5Hash(Password);
            var user = await MainViewModel.Instance.SQLiteHelper.LoginAsync(UserName, passwordMD5);

            if (user==null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Username or Password incorrect", "Ok");
                return;
            }

            UserName = string.Empty;
            Password = string.Empty;

            Application.Current.Properties["userLogged"] = user;
            MainViewModel.Instance.ProductsViewModel = new ProductsViewModel();
            Application.Current.MainPage = new NavigationPage(new ProductsPage());
        }
        #endregion

        #region Command
        public ICommand ActionCommand
        {
            get
            {
                return new Command<string>(async (param) =>
                {

                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;

                    await Action(param);

                    IsBusy = false;
                });
            }
        }
        #endregion
    }
}

