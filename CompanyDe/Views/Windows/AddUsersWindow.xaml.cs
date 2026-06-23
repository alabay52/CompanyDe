using System.Linq;
using System.Windows;
using CompanyDe.Model;

namespace CompanyDe.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUsersWindow.xaml
    /// </summary>
    public partial class AddUsersWindow : Window
    {
        public AddUsersWindow()
        {
            InitializeComponent();

            RoleCmb.ItemsSource = App.context.UserRole.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            // Проверка на существование логина
            string login = LoginTb.Text.Trim();

            if (App.context.SystemUser.Any(u => u.Login == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует!",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Customer newCustomer = new Customer()
            {
                Name = NameTb.Text,
                INN = InnTb.Text.Trim(),
                Addres = AdressTb.Text,
                Phone = PhoneTb.Text,
                IsSalesman = (bool)IsSalesmanCb.IsChecked,
                IsBuyer = (bool)IsBuyerCb.IsChecked,
            };
            App.context.Customer.Add(newCustomer);
            App.context.SaveChanges();

            SystemUser newUser = new SystemUser()
            {
                Login = LoginTb.Text,
                Password = PassTb.Text,
                IsBlocked = (bool)IsBlockedCb.IsChecked,
                UserRole = RoleCmb.SelectedItem as UserRole,
                Customer = newCustomer
            };
            App.context.SystemUser.Add(newUser);
            App.context.SaveChanges();
            MessageBox.Show("Данные добавлены");
            DialogResult = true;
        }
    }
}
