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
    public partial class Contact : Page
    {
        Orders restaurantOrders;

        Color lightRed =         Color.FromArgb(2, 252, 212, 212);
        Color lightYellow =      Color.FromArgb(2, 250, 250, 212);
        Color lightGreen =       Color.FromArgb(2, 214, 250, 212);
        Color lightYellowGreen = Color.FromArgb(2, 253, 250, 212);
        Color lightBlue =        Color.FromArgb(2, 212, 215, 250);

        protected async void Page_Load(object sender, EventArgs e)
        {
            Application["user_id"] = "qgT6LlkQ1pRmJDN9HXSxRWkBhzA2";

            await Firebase_Get_Data();
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

            var orders = await firebaseClient.Child("Restaurants").OnceAsync<Orders>();

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
            string s = $"window.open('" + url + "', 'popup_window', 'width=600,height=" + height.ToString() + ",left=100,top=100,resizable=no');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}