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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WPFClient.Models;
using WPFClient.view_model;

namespace WPFClient.Views
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : UserControl
    {
        string baseUrl = "https://localhost:44382/api/Administrator/";
        HttpClient client = new HttpClient();
        public LogInView()
        {
            InitializeComponent();
            
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {

            MainWindowHelper vm = MainWindowHelper.Instance;
            string okMessage = "\"ok\"";
            string url = baseUrl + "LogIn";
            var logInContent = new JObject();
            logInContent.Add("Email", emailField.Text);
            logInContent.Add("Password", passwordField.Password);
            HttpContent content = new StringContent(logInContent.ToString(), Encoding.UTF8, "application/json");
            var responseBody = client.PostAsJsonAsync(url, logInContent).Result;
            string response = await responseBody.Content.ReadAsStringAsync();
            if (okMessage.Equals(response))
            {
                string uri = baseUrl + "Info";
                var email = new JObject();
                email.Add("email", emailField.Text);
                HttpContent content2 = new StringContent(email.ToString(), Encoding.UTF8, "application/json");
                var response2 = client.PostAsync(uri, content2).Result;
                if (response2.IsSuccessStatusCode)
                {
                    var responseJsonString = await response2.Content.ReadAsStringAsync(); 
                    var deserialized = JsonConvert.DeserializeObject<AdministratorCast>(responseJsonString);
                    AdminUserHelper adminHelper = AdminUserHelper.Instance;
                    adminHelper.admin = deserialized;
                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    administratorWindow.Show();
                    vm.CloseAction();
                }
                else
                {
                    MessageBox.Show(response2.ReasonPhrase);
                }
            }
            else
            {
                responseBox.Content = response;
            }
            }
    }
}

