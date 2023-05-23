using Firebase.Database;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoodDelivery
{
    public partial class Connected : Page
    {
        Restaurant restaurantOrders;

        Color lightRed = Color.FromArgb(2, 252, 212, 212);
        Color lightYellow = Color.FromArgb(2, 250, 250, 212);
        Color lightGreen = Color.FromArgb(2, 214, 250, 212);
        Color lightYellowGreen = Color.FromArgb(2, 253, 250, 212);
        Color lightBlue = Color.FromArgb(2, 212, 215, 250);

        string[] ordersOrder = { "Order Placed", "Accepted", "Out for Delivery", "Delivered", "Completed", "Canceled" };

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

            //firebase.Child("Restaurant").AsObservable<object>().Subscribe(async data =>
            //{
            //    labelUpdates.Text = "Looking for updates...";
            //});
        }

        private void AddOrdersWithStatus(DataTable dataTable, string status)
        {
            int orderIndex = 0;

            if (restaurantOrders != null && restaurantOrders.orders != null)
            {
                foreach (Data data in restaurantOrders.orders)
                {
                    if (data.deliveryStatus == status)
                    {
                        data.index = orderIndex;

                        DataRow dataRow = dataTable.NewRow();

                        dataRow["NO"] = orderIndex;
                        dataRow["Items"] = data.name;
                        dataRow["Address"] = data.address;
                        dataRow["Person"] = data.username;
                        dataRow["Phone"] = data.phone;
                        dataRow["Date"] = data.date;
                        dataRow["Status"] = data.deliveryStatus;
                        dataRow["Price"] = data.price;

                        dataTable.Rows.Add(dataRow);
                    }
                    orderIndex += 1;
                }
            }
        }

        protected async void Timer1_Tick(object sender, EventArgs e)
        {
            if (labelUpdates.Text == "Looking for updates...")
            {
                await Firebase_Get_Data();
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

            foreach (string order in ordersOrder)
            {
                AddOrdersWithStatus(dataTable, order);
            }

            grid_Orders.DataSource = dataTable;
            grid_Orders.DataBind();

            labelUpdates.Text = "Updated!";
        }

        private async Task Firebase_Get_Data()
        {
            var firebaseClient = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            var restaurants = await firebaseClient.Child("RestaurantDetails").OnceAsync<RestaurantDetails>();

            foreach (var restaurant in restaurants)
            {
                if (restaurant.Key == Application["user_id"].ToString())
                {
                    var orders = await firebaseClient.Child("Restaurants" + restaurant.Object.city).OnceAsync<Restaurant>();

                    foreach (var order in orders)
                    {
                        if (order.Key == restaurant.Object.name.ToString())
                        {
                            restaurantOrders = order.Object;

                            restaurantOrders.city = restaurant.Object.city;

                            AddOrdersToTable();

                            break;
                        }
                    }
                }
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in grid_Orders.Rows)
            {
                switch (row.Cells[7].Text)
                {
                    case "Order Placed":
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

            OrderDetails orderDetails = new OrderDetails()
            {
                data = restaurantOrders.orders[row.RowIndex],
                city = restaurantOrders.city,
                name = restaurantOrders.name
            };

            var json = new JavaScriptSerializer().Serialize(orderDetails);

            var encodedData = HttpUtility.UrlEncode(json);

            int height = 300;

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