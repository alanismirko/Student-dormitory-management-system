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
using WPFClient.view_model;

namespace WPFClient.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {

        string baseUrl = "https://localhost:44382//api/Administrator/";
        HttpClient client = new HttpClient();
        string userCode = "A1B2C3";

        public RegisterView()
        {
            InitializeComponent();

        }

        /*Administrator Register
         * TO DO: decide on who decides whats your employee number
         */
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterViewModel rvm = new RegisterViewModel();
            emailErrorField.Content = "";
            employeeErrorField.Content = "";
            passwordErrorField.Content = "";
            int empNumb = 0;
            if (userCodeField.Text.Equals(userCode))
            {
                if (rvm.isEmailValid(emailField.Text))
                {
                    if (int.TryParse(employeeNumbField.Text, out empNumb))
                    {
                        if (password.Password == repeatPassword.Password)
                        {
                            string url = baseUrl + "Register";
                            var registerContent = new JObject();
                            registerContent.Add("employeeNumber", empNumb);
                            registerContent.Add("firstName", fNameField.Text);
                            registerContent.Add("lastName", lNameField.Text);
                            registerContent.Add("phoneNumber", phoneNumberField.Text);
                            registerContent.Add("email", emailField.Text);
                            registerContent.Add("password", password.Password);
                            HttpContent content = new StringContent(registerContent.ToString(), Encoding.UTF8, "application/json");
                            var responseBody = client.PostAsJsonAsync(url, registerContent).Result;
                            returnBox.Content = await responseBody.Content.ReadAsStringAsync();
                        }
                        else passwordErrorField.Content = "Passwords must match!";
                    }
                    else employeeErrorField.Content = "Numbers only!";

                }
                else emailErrorField.Content = "Not a valid email!";
            }
            else returnBox.Content = "Invalid user code!";
        }
    
    }
}
