using System.Threading.Tasks;
using System.Windows.Input;
using TestOIS.Views;
using Xamarin.Forms;

namespace TestOIS.ViewModels
{
    using Models;
    using TestOIS.Data;

    public class CreateAccountViewModel : BaseViewModel
    {
        #region Attributes
        private User user;
        #endregion



        #region Properties
        public User User
        {
            get => user;
            set => SetProperty(ref user , value);
        }
        #endregion

        #region Methods
        private async Task Action(string action)
        {
            if (action.Equals("Login"))
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await CreateAccontAsync();
            }
        }

        private async Task CreateAccontAsync()
        {
            if (User==null)
            {
                return;
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert","First name is empty","Ok");
                return;
            }

            if (string.IsNullOrEmpty(User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Last name is empty", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(User.UserName))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Username is empty", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(User.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password is empty", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(User.ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Confirm password is empty", "Ok");
                return;
            }

            if (User.Password != User.ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Password and confirmation do not match", "Ok");
                return;
            }

            User exist = await MainViewModel.Instance.SQLiteHelper.ExistUserAsync(User.UserName);
            if (exist != null)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "This user is already registered", "Ok");
                return;
            }

            user.Password = Encryptor.MD5Hash(User.Password);
            await MainViewModel.Instance.SQLiteHelper.SaveUserAsync(user);
            User = await MainViewModel.Instance.SQLiteHelper.LoginAsync(user.UserName,user.Password);
            Application.Current.Properties["userLogged"] = User;
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

