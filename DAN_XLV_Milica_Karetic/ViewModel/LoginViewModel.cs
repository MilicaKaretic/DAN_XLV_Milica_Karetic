using DAN_XLV_Milica_Karetic.Commands;
using DAN_XLV_Milica_Karetic.View;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLV_Milica_Karetic.ViewModel
{
    class LoginViewModel :  BaseViewModel
    {
        Login view;
        Service service = new Service();

        #region Constructors

        public LoginViewModel(Login view)
        {
            this.view = view;
            service.GetCurrentWarehouseQuantity();
        }
        #endregion

        #region Properties

        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string labelInfo;

        public string LabelInfo
        {
            get { return labelInfo; }
            set
            {
                labelInfo = value;
                OnPropertyChanged("LabelInfo");
            }
        }

        #endregion

        #region Commands
        private ICommand login;

        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
            set { login = value; }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;

            if(Username == "Mag2019" && password == "Mag2019")
            {
                Storekeeper sk = new Storekeeper();
                LabelInfo = "Logged in";
                view.Close();
                sk.Show();
            }
            else if(Username == "Man2019" && password == "Man2019")
            {
                MainWindow mw = new MainWindow();
                LabelInfo = "Logged in";
                view.Close();
                mw.Show();
            }
            else
            {
                LabelInfo = "Wrong Username or Password";
            }

        }

        #endregion
    }
}
