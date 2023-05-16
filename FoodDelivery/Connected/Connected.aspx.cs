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

namespace FoodDelivery
{
    public partial class Connected : Page
    {
        Order restaurantOrders;

        Color lightRed =         Color.FromArgb(2, 252, 212, 212);
        Color lightYellow =      Color.FromArgb(2, 250, 250, 212);
        Color lightGreen =       Color.FromArgb(2, 214, 250, 212);
        Color lightYellowGreen = Color.FromArgb(2, 253, 250, 212);
        Color lightBlue =        Color.FromArgb(2, 212, 215, 250);

        string[] ordersOrder = { "Placed", "Accepted", "Out for Delivery", "Delivered", "Completed", "Canceled" };

        protected async void Page_Load(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable(
            "GOOGLE_APPLICATION_CREDENTIALS",
            @"D:\fooddelivery-564e8-firebase-adminsdk-s0459-465242102c.json");

            await Firebase_Get_Data();

            SetListener();
        }

        private void SetListener()
        {
            FirebaseClient firebase = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            firebase.Child("Restaurants").AsObservable<object>().Subscribe(async data =>
            {
                 await Firebase_Get_Data();
            });
        }

        private string GetStringItems(List<Item> items)
        {
            string itemsString = "";

            foreach (Item item in items)
            {
                itemsString += item.name + " ";
            }

            return itemsString;
        }

        private void AddOrdersWithStatus(DataTable dataTable, string status)
        {
            int orderIndex = 0;

            foreach (Data data in restaurantOrders.orders)
            {
                if(data.status == status)
                {
                    data.index = orderIndex;

                    DataRow dataRow = dataTable.NewRow();

                    dataRow["NO"] = orderIndex;
                    dataRow["Items"] = GetStringItems(data.items);
                    dataRow["Address"] = data.address;
                    dataRow["Person"] = data.person;
                    dataRow["Phone"] = data.phone;
                    dataRow["Date"] = data.date.ToString("dd/MM/yyyy");
                    dataRow["Status"] = data.status;
                    dataRow["Price"] = data.price;

                    dataTable.Rows.Add(dataRow);
                }
                orderIndex += 1;
            }
        }

        private void AddOrdersToTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("NO", typeof(int));
            dataTable.Columns.Add("Items", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Person", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Price", typeof(int));
            dataTable.Columns.Add("Status", typeof(string));

            foreach(string order in ordersOrder)
            {
                AddOrdersWithStatus(dataTable, order);
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

                    AddOrdersToTable();

                    break;
                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in grid_Orders.Rows)
            {
                switch(row.Cells[7].Text)
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

            string url = "FoodShow.aspx?city=" + Server.UrlEncode(restaurantOrders.city) + "&name=" + Server.UrlEncode(restaurantOrders.name);
            string script = "var popup = window.open('" + url + "', '_blank', 'height=" + height.ToString() + ",width=600');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Food", script, true);
        }

        protected void DiscountPageLoad(object sender, EventArgs e)
        {
            int height = 150;

            string url = "Discount.aspx?city=" + Server.UrlEncode(restaurantOrders.city) + "&name=" + Server.UrlEncode(restaurantOrders.name);
            string script = "var popup = window.open('" + url + "', '_blank', 'height=" + height.ToString() + ",width=400');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Food", script, true);
        }
    }
}