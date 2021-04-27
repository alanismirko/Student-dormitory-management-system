using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.view_model;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowHelper vm = MainWindowHelper.Instance;
            vm.CloseAction = new Action(this.Hide);
        }


        private void RegisterViewClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RegisterViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LogInViewModel();
        }

        public void IsWindowOpen(AdministratorWindow admin)
        {
            if (admin != null)
                this.Close();    
        }
    }
}
