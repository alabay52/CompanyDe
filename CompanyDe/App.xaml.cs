using System.Windows;
using CompanyDe.Model;

namespace CompanyDe
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ManufacturerDbEntities context = new ManufacturerDbEntities();
        public static SystemUser currentUser { get; set; }
        public static Customer currentCus { get; set; }
    }
}
