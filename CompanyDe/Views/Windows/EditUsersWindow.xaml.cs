using System.Linq;
using System.Windows;
using CompanyDe.Model;

namespace CompanyDe.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUsersWindow.xaml
    /// </summary>
    public partial class EditUsersWindow : Window
    {
        Customer selectedCustomer;
        SystemUser sysus;
        public EditUsersWindow(Customer selectedCustomer)
        {
            InitializeComponent();

            this.selectedCustomer = selectedCustomer;
            DataContext = selectedCustomer;

            IsSalesmanCb.IsChecked = selectedCustomer.IsSalesman;
            IsBuyerCb.IsChecked = selectedCustomer.IsBuyer;

            sysus = App.context.SystemUser.FirstOrDefault(u => u.Customer.Id == selectedCustomer.Id);

            if (sysus != null)
            {
                LoginTb.Text = sysus.Login;
                PassTb.Text = sysus.Password;

                IsBlockedCb.IsChecked = sysus.IsBlocked;
                RoleCmb.SelectedItem = sysus.UserRole;
            }



            RoleCmb.ItemsSource = App.context.UserRole.ToList();
        }


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedCustomer.IsSalesman = (bool)IsSalesmanCb.IsChecked;
            selectedCustomer.IsBuyer = (bool)IsBuyerCb.IsChecked;



            if (sysus != null)
            {
                sysus.Login = LoginTb.Text;
                sysus.Password = PassTb.Text;

                sysus.IsBlocked = (bool)IsBlockedCb.IsChecked;
                sysus.UserRole = RoleCmb.SelectedItem as UserRole;

            }


            App.context.SaveChanges();
            DialogResult = true;
            MessageBox.Show("Данные отредактированы и сохранены.");
        }
    }
}
