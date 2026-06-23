using System.Linq;
using System.Windows;
using CompanyDe.Model;

namespace CompanyDe.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWndow.xaml
    /// </summary>
    public partial class AdminWndow : Window
    {
        SystemUser sysus;


        public AdminWndow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            UsersLv.ItemsSource = App.context.Customer.ToList();

        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUsersWindow addUsersWindow = new AddUsersWindow();

            if (addUsersWindow.ShowDialog() == true)
            {
                LoadData();

            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Customer selectedCustomer = UsersLv.SelectedItem as Customer;
            if (selectedCustomer != null)
            {
                EditUsersWindow editTaskWindow = new EditUsersWindow(selectedCustomer);
                if (editTaskWindow.ShowDialog() == true)
                {
                    LoadData();

                }
            }
        }

        private void UsersLv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {


        }
    }
}