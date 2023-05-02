﻿using System;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Text;

namespace FoodDelivery
{
    public partial class OrderView : Page
    {
        private OrderDetails orderDetails;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetDataJson();

                if (!IsPostBack)
                {
                    Show_Data();
                }
            }
            catch
            {
                label_Address.Text = "Something went wrong! Try again!";
            }
        }

        private void GetDataJson()
        {
            string encodedData = Request.QueryString["data"];

            string json = HttpUtility.UrlDecode(encodedData);

            var serializer = new JavaScriptSerializer();
            orderDetails = serializer.Deserialize<OrderDetails>(json);
        }

        private void Show_Items()
        {
            if(orderDetails.data.items != null)
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("NO", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Amount", typeof(int));

                int itemIndex = 0;

                foreach (Item item in orderDetails.data.items)
                {
                    if(item != null)
                    {
                        itemIndex += 1;

                        dataTable.Rows.Add(itemIndex,
                                           item.name,
                                           item.amount);

                        grid_Items.DataSource = dataTable;
                        grid_Items.DataBind();

                        grid_Items.DataSource = null;
                    }
                }
            }
        }

        private void Show_Data()
        {
            if (orderDetails != null)
            {
                label_Address.Text = "Address: " + orderDetails.data.address;
                label_Date.Text = "Date: " + orderDetails.data.date.ToString("dd/MM/yyyy");
                label_Person.Text = "Person: " + orderDetails.data.person;
                label_Phone.Text = "Phone: " + orderDetails.data.phone;

                ListItem dropDownItem = dropDown_Status.Items.FindByText(orderDetails.data.status);

                if (dropDownItem != null)
                { 
                    dropDownItem.Selected = true;
                }

                Show_Items();
            }
        }
        private async void Check()
        {
            string newStatus = dropDown_Status.SelectedValue;

            string path = "/Restaurants/" + orderDetails.restaurantID + "/Orders/" + orderDetails.data.index.ToString() + "/Status.json";

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://fooddelivery-564e8-default-rtdb.firebaseio.com");

            var response = await httpClient.PutAsync(path, new StringContent("\"" + newStatus + "\"", Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Status updated successfully!");
            }
            else
            {
                Console.WriteLine("Error updating status: " + response.StatusCode);
            }
        }

        protected void DropDown_Index_Changed(object sender, EventArgs e)
        {
            Check();
        }
    }
}