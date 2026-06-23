using System.Linq;
using System.Windows;
using CompanyDe.Model;

namespace CompanyDe.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        int failedEntryCount = 0;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(LoginTb.Text) ||
                  string.IsNullOrEmpty(PasswordPb.Password))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                App.currentUser = App.context.SystemUser.FirstOrDefault(systemUser => systemUser.Login == LoginTb.Text && systemUser.Password == PasswordPb.Password);

                if (App.currentUser != null)
                {
                    if (App.currentUser.IsBlocked == false)
                    {

                        // Авторизация
                        if (App.currentUser.RoleId == 1)
                        {
                            AdminWndow administratorWindow = new AdminWndow();
                            administratorWindow.Show();
                            MessageBox.Show("Вы успешно авторизовались.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        else
                        {
                            UserWindow userWindow = new UserWindow();
                            userWindow.Show();
                        }

                        Close();


                    }
                    else
                    {
                        MessageBox.Show($"Вы заблокированы. Обратитесь к администратору!.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    // Блокировка
                    App.currentUser = App.context.SystemUser.FirstOrDefault(u => u.Login == LoginTb.Text);

                    if (App.currentUser != null)
                    {
                        //Подсчет кол-ва неудачных попыток
                        failedEntryCount++;
                        MessageBox.Show($"Введен неверный пароль. Осталось попыток:{3 - failedEntryCount} из 3", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                        if (failedEntryCount == 3)
                        {
                            MessageBox.Show("Вы заблокированы. Обратитесь к администратору!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                            failedEntryCount = 0;
                            SystemUser userToBlock = App.context.SystemUser.FirstOrDefault(s => s.Login == LoginTb.Text);
                            userToBlock.IsBlocked = true;
                            App.context.SaveChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

    }


}


