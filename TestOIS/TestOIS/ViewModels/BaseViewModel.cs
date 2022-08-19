namespace TestOIS.ViewModels
{

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
       
        #region Attributes
        private bool isBusy;
        #endregion

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        #endregion


        #region Methods
        protected void OnProperty([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T backinfiel, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backinfiel, value))
            {
                return;
            }
            backinfiel = value;
            OnProperty(propertyName);
        }
        #endregion

    }
}

