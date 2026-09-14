using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabWork2_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string errorMessage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrtionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                errorMessage = "";
                TestLogin(LoginTextBox.Text.Trim());
                TestPassword(PasswordBox.Password, ConfirmPasswordBox.Password.Trim());
                TestEmail(EmailTextBox.Text.Trim());

                if (!String.IsNullOrWhiteSpace(errorMessage))
                {
                    ShowError(errorMessage);
                    return;
                }

                ShowSucces(LoginTextBox.Text.Trim());
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        void TestPassword(string password, string confirmPassword)
        {
            if (String.IsNullOrEmpty(password))
            {
                errorMessage += "Введите пароль.\n";
                return;
            }

            string regex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,30}$";
            if (!Regex.Match(password, regex).Success)
            {
                errorMessage += "Пароль дожен быть от 8 до 30 сиволов, содержать латинские буквы верхнего и нижнего регистра, цифры и спецсимволы.\n";
                return;
            }

            if (String.IsNullOrEmpty(confirmPassword))
            {
                errorMessage += "Введите подтверждение пароля.\n";
                return;
            }

            if (password != confirmPassword)
            {
                errorMessage += "Пароли не сопадают.\n";
                return;
            }
        }

        void TestLogin(string login)
        {
            if (String.IsNullOrEmpty(login))
            {
                errorMessage += "Введите логин.\n";
                return;
            }
        }

        void TestEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                errorMessage += "Введите почту.\n";
                return;
            }

            string regex = @"^\S+@\S+\.\S+$";
            if (!Regex.Match(email, regex).Success)
            {
                errorMessage += "Некорректно введена почта.\n";
                return;
            }
        }

        void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        void ShowSucces(string login)
        {
            MessageBox.Show($"Вы зарегистрировались с логином {login}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}