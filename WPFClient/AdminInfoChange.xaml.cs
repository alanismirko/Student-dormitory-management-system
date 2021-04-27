using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFClient.Models;
using WPFClient.view_model;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for AdminInfoChange.xaml
    /// </summary>
    public partial class AdminInfoChange : Window
    {
        string baseUrl = "https://localhost:44382//api/Administrator/";
        HttpClient client = new HttpClient();
        AdminUserHelper ah = AdminUserHelper.Instance;
        
        public AdminInfoChange()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string okResponse = "\"ok\"";
            AdministratorCast admin = ah.admin;
            RegisterViewModel rvm = new RegisterViewModel();
            if (rvm.isEmailValid(emailField.Text))
            {
                string url = baseUrl + "Update";
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", admin.Token);
                var updateContent = new JObject();
                updateContent.Add("Id", admin.Id);
                updateContent.Add("employeeNumber", Convert.ToInt32(employeeNumberField.Content));
                updateContent.Add("firstName", firstNameField.Text);
                updateContent.Add("lastName", lastNameField.Text);
                updateContent.Add("phoneNumber", phoneNumberField.Text);
                updateContent.Add("email", emailField.Text);
                updateContent.Add("modificationDate", admin.modificationDate);
                HttpContent content = new StringContent(updateContent.ToString(), Encoding.UTF8, "application/json");
                var responseBody = client.PostAsJsonAsync(url, updateContent).Result;
                string response = await responseBody.Content.ReadAsStringAsync();
                if (response.Equals(okResponse))
                {
                    admin.Email = emailField.Text;
                    this.Close();
                }
                else responseField.Content = response;

            }
            else responseField.Content = "Not a valid email!";

        }
    }
}
