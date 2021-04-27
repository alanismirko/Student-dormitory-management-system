using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFClient.Models;
using WPFClient.view_model;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
            
        }

        private void CreateRoom_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CreateRoomViewModel();
        }

        private void AllRooms_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AllRoomViewModel();
        }

        private void MyAccount_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AdminAccountViewModel();
        }

        private void LogOff_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Log off confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MainWindowHelper vm = MainWindowHelper.Instance;
                AdminUserHelper adminHelper = AdminUserHelper.Instance;
                adminHelper.admin = null;
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();



            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PendingBookingsViewModel();
        }

        private void ApprovedBookings_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ApprovedBookingsViewModel();
        }
    }
}
