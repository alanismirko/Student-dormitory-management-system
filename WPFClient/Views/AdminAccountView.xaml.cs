using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WPFClient.Models;

namespace WPFClient.Views
{
    /// <summary>
    /// Interaction logic for AdminAccountView.xaml
    /// </summary>
    public partial class AdminAccountView : UserControl
    {
        string baseUrl = "https://localhost:44382//api/Administrator/";
        HttpClient client = new HttpClient();
        AdminUserHelper ah = AdminUserHelper.Instance;
        public AdminAccountView()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            AdministratorCast admin = ah.GetAdministrator();
            firstNameField.Content = admin.FirstName;
            lastNameField.Content = admin.LastName;
            phoneNumberField.Content = admin.PhoneNumber;
            emailField.Content = admin.Email;
            employeeNumberField.Content = admin.EmployeeNumber;   
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            AdministratorCast admin = ah.GetAdministrator();
            AdminInfoChange am = new AdminInfoChange();
            am.firstNameField.Text = firstNameField.Content.ToString();
            am.lastNameField.Text = lastNameField.Content.ToString();
            am.emailField.Text = emailField.Content.ToString();
            am.phoneNumberField.Text = phoneNumberField.Content.ToString();
            am.employeeNumberField.Content = employeeNumberField.Content;
            am.ShowDialog();
            string uri = baseUrl + "Info";
            var email = new JObject();
            email.Add("email", admin.Email);
            HttpContent content2 = new StringContent(email.ToString(), Encoding.UTF8, "application/json");
            var response2 = client.PostAsync(uri, content2).Result;
            if (response2.IsSuccessStatusCode)
            {
                var responseJsonString = await response2.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<AdministratorCast>(responseJsonString);
                admin = deserialized;
                ah.admin = admin;
            }
            GetData();
        }
    }
}
