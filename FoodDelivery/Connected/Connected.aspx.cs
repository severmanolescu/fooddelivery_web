using System;
using System.Drawing;
using System.Data;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Database;
using System.Collections.Generic;
using System.IO;
using Firebase.Storage;
using System.Net.Http;
using Firebase.Database.Query;

namespace FoodDelivery
{
    public partial class Contact : Page
    {
        Order restaurantOrders;

        Color lightRed =         Color.FromArgb(2, 252, 212, 212);
        Color lightYellow =      Color.FromArgb(2, 250, 250, 212);
        Color lightGreen =       Color.FromArgb(2, 214, 250, 212);
        Color lightYellowGreen = Color.FromArgb(2, 253, 250, 212);
        Color lightBlue =        Color.FromArgb(2, 212, 215, 250);

        protected async void Page_Load(object sender, EventArgs e)
        {

            // Replace "my-project-id" and "my-bucket" with your Firebase project ID and storage bucket name, respectively.
            var bucket = "timisoara-83e5b.appspot.com";

            // Replace "C:\path\to\myfile.jpg" with the path to your local file.
            var path = @"D:\5977588.png";

            // Read the contents of the local file into a byte array.
            var data = File.ReadAllBytes(path);

            // Set up the HTTP client.
            var client = new HttpClient();
            client.BaseAddress = new Uri($"https://storage.googleapis.com/{bucket}/");

            // Create a new HTTP PUT request with the file data as the request body.
            var request = new HttpRequestMessage(HttpMethod.Put, "myfile.png");
            request.Content = new ByteArrayContent(data);

            // Send the HTTP request and get the response.
            var response = await client.SendAsync(request);

            Application["user_id"] = "qgT6LlkQ1pRmJDN9HXSxRWkBhzA2";

            await Firebase_Get_Data();

            Timer1.Enabled = true;
        }

        private string Get_String_Items(List<Item> items)
        {
            string itemsString = "";

            foreach (Item item in items)
            {
                itemsString += item.name + " ";
            }

            return itemsString;
        }


        protected void Add_Orders_To_Table()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("NO", typeof(int));
            dataTable.Columns.Add("Items", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Person", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));

            int orderIndex = 0;

            foreach(Data data in restaurantOrders.orders)
            {
                data.index = orderIndex;
                orderIndex++;

                DataRow dataRow = dataTable.NewRow();

                dataRow["NO"] = orderIndex;
                dataRow["Items"] = Get_String_Items(data.items);
                dataRow["Address"] = data.address;
                dataRow["Person"] = data.person;
                dataRow["Phone"] = data.phone;
                dataRow["Date"] = data.date.ToString("dd/MM/yyyy");
                dataRow["Status"] = data.status;

                dataTable.Rows.Add(dataRow);
            }

            grid_Orders.DataSource = dataTable;
            grid_Orders.DataBind();         
        }

        private async Task Firebase_Get_Data()
        {
            var firebaseClient = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            var orders = await firebaseClient.Child("Restaurants").OnceAsync<Order>();

            foreach(var order in orders)
            {
                if(order.Key == Application["user_id"].ToString())
                {
                    restaurantOrders = order.Object;

                    Add_Orders_To_Table();

                    break;
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Firebase_Get_Data();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in grid_Orders.Rows)
            {
                switch(row.Cells[6].Text)
                {
                    case "Placed":
                        {
                            row.BackColor = lightBlue;

                            break;
                        }
                    case "Accepted":
                        {
                            row.BackColor = lightYellow;

                            break;
                        }
                    case "Out for Delivery":
                        {
                            row.BackColor = lightYellowGreen;

                            break;
                        }
                    case "Delivered":
                        {
                            row.BackColor = lightGreen;

                            break;
                        }
                    case "Canceled":
                        {
                            row.BackColor = lightRed;

                            break;
                        }
                    case "Completed":
                        {
                            row.BackColor = lightGreen;

                            break;
                        }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grid_Orders.SelectedRow;

            OrderDetails orderDetails = new OrderDetails();

            orderDetails.data = restaurantOrders.orders[row.RowIndex];
            orderDetails.restaurantID = Application["user_id"].ToString();

            var json = new JavaScriptSerializer().Serialize(orderDetails);

            var encodedData = HttpUtility.UrlEncode(json);

            int height = 300 + orderDetails.data.items.Count * 40;

            string url = "OrderView.aspx?data=" + encodedData;
            string script = "var popup = window.open('" + url + "', '_blank', 'height=" + height.ToString() + ",width=600');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OrderView", script, true);
        }

        protected void FoodPageLoad(object sender, EventArgs e)
        {
            int height = 500;

            string url = "FoodShow.aspx?link=" + Server.UrlEncode(restaurantOrders.link) + "&name=" + Server.UrlEncode(restaurantOrders.name);
            string script = "var popup = window.open('" + url + "', '_blank', 'height=" + height.ToString() + ",width=600');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Food", script, true);
        }

        protected void DiscountPageLoad(object sender, EventArgs e)
        {
            int height = 150;

            string url = "Discount.aspx?link=" + Server.UrlEncode(restaurantOrders.link) + "&name=" + Server.UrlEncode(restaurantOrders.name);
            string script = "var popup = window.open('" + url + "', '_blank', 'height=" + height.ToString() + ",width=400');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Food", script, true);
        }
    }
}