﻿using DAN_XLV_Milica_Karetic.ViewModel;
using System.Windows;

namespace DAN_XLV_Milica_Karetic.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }

        /// <summary>
        /// Disable login button unil both field are not empty
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">RoutedEventArgs event</param>
        private void txtChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length > 0 && txtUsername.Text.Length > 0)
            {
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
            }
        }
    }
}
