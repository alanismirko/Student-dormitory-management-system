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
using WPFClient.Helpers;
using WPFClient.Models;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for EditRoomInfoWindow.xaml
    /// </summary>
    public partial class EditRoomInfoWindow : Window
    {
        string baseUrl = "https://localhost:44382/api/Rooms/";
        HttpClient client = new HttpClient();
        public EditRoomInfoWindow()
        {
            InitializeComponent();
        }

        public void getSelectedRoomInfo()
        {
            SelectedRoomHelper rh = SelectedRoomHelper.Instance;
            RoomCast updateRoom = rh.GetSelectedRoom(Convert.ToInt32(idField.Content));
            roomNumberField.Text = updateRoom.RoomNumber.ToString();
            areaField.Text = updateRoom.Area.ToString();
            priceField.Text = updateRoom.Price.ToString();
            capacityField.Text = updateRoom.Capacity.ToString();
            floorField.Text = updateRoom.Floor.ToString();
            descriptionField.Text = updateRoom.Description;

        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string okResponse = "\"ok\"";
            string url = baseUrl + "Update";
            int id = Convert.ToInt32(idField.Content);
            int roomNbr;
            int floorNbr;
            int capacityNbr;
            double areaNbr;
            double priceNbr;
            AdminUserHelper ah = AdminUserHelper.Instance;
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
                                SelectedRoomHelper rh = SelectedRoomHelper.Instance;
                                AdministratorCast admin = ah.admin;
                                RoomCast updateRoom = rh.GetSelectedRoom(Convert.ToInt32(idField.Content));
                                client.DefaultRequestHeaders.Authorization =
                                new AuthenticationHeaderValue("Bearer", admin.Token);
                                var registerContent = new JObject();
                                registerContent.Add("Id", id);
                                registerContent.Add("RoomNumber", roomNbr);
                                registerContent.Add("Floor", floorNbr);
                                registerContent.Add("Capacity", capacityNbr);
                                registerContent.Add("Area", areaNbr);
                                registerContent.Add("Price", priceNbr);
                                registerContent.Add("Description", descriptionField.Text);
                                registerContent.Add("isAvailable", 1);
                                registerContent.Add("modificationDate", updateRoom.modificationDate);
                                HttpContent content = new StringContent(registerContent.ToString(), Encoding.UTF8, "application/json");
                                var responseBody = client.PostAsync(url, content).Result;
                                string response = await responseBody.Content.ReadAsStringAsync();
                                if (response.Equals(okResponse))
                                    this.Close();
                                else responseField.Content = response;
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

