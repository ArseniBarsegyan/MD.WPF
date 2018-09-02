using System.Windows;
using System.Windows.Controls;
using MyDiary.WPF.Commands;
using MyDiary.WPF.Helpers;
using MyDiary.WPF.Models;
using MyDiary.WPF.Properties;

namespace MyDiary.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private RestClientCommand _loginCommand;
        private string _email;
        private string _errorMessage;

        public string Email
        {
            get => _email;
            set => SetValue(ref _email, value, nameof(Email));
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetValue(ref _errorMessage, value, nameof(ErrorMessage));
        }

        public RestClientCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RestClientCommand(async (param) =>
                       {
                           var passwordBox = param as PasswordBox;
                           var password = passwordBox?.Password;

                           if (string.IsNullOrWhiteSpace(Email))
                           {
                               ErrorMessage = ConstantsHelper.EmailEmptyError;
                               return;
                           }

                           if (string.IsNullOrWhiteSpace(password))
                           {
                               ErrorMessage = ConstantsHelper.PasswordEmptyError;
                               return;
                           }

                           var loginModel = new LoginModel
                           {
                               Email = Email,
                               Password = password
                           };
                           await ServiceClient.Login(loginModel);

                           if (string.IsNullOrEmpty(Settings.Default.Token))
                           {
                               ErrorMessage = ConstantsHelper.NotRegisteredError;
                           }
                           else
                           {
                               ErrorMessage = string.Empty;
                               Settings.Default.UserName = Email;
                               Settings.Default.IsUserLogin = true;
                               var mainWindow = new MainWindow();
                               mainWindow.Show();
                           }
                       }));
            }
        }
    }
}