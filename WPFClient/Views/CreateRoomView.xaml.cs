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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFClient.Models;

namespace WPFClient.Views
{
    /// <summary>
    /// Interaction logic for CreateRoomView.xaml
    /// </summary>
    public partial class CreateRoomView : UserControl
    {
        string baseUrl = "https://localhost:44382/api/Rooms";
        HttpClient client = new HttpClient();
        public CreateRoomView()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            int roomNbr;
            int floorNbr;
            int capacityNbr;
            double areaNbr;
            double priceNbr;
            if (int.TryParse(roomNumberField.Text, out roomNbr))
            {
                if (int.TryParse(floorField.Text, out floorNbr))
                {
                    if (int.TryParse(capacityField.Text, out capacityNbr))
                    {
                        if (double.TryParse(areaField.Text, out areaNbr))
                        {
                            if (double.TryParse(priceField.Text, out priceNbr))
                            {
                                AdminUserHelper ah = AdminUserHelper.Instance;
                                AdministratorCast admin = ah.admin;
                                client.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue("Bearer", admin.Token);
                                var registerContent = new JObject();
                                registerContent.Add("RoomNumber", roomNbr);
                                registerContent.Add("Floor", floorNbr);
                                registerContent.Add("Capacity", capacityNbr);
                                registerContent.Add("Area", areaNbr);
                                registerContent.Add("Price", priceNbr);
                                registerContent.Add("Description", descriptionField.Text);
                                registerContent.Add("isAvailable", 1);
                                HttpContent content = new StringContent(registerContent.ToString(), Encoding.UTF8, "application/json");
                                var responseBody = client.PostAsync(baseUrl, content).Result;
                                responseField.Content = await responseBody.Content.ReadAsStringAsync();
                            }
                            else responseField.Content = "Price can not contain letters!";
                        }
                        else responseField.Content = "Area can not contain letters!";
                    }
                    else responseField.Content = "Capacity must be described only in numbers!";
                }
                else responseField.Content = "Floor must only be numbers!";
            }
            else responseField.Content = "Room number must contain only numbers!";
        }

    }
}
