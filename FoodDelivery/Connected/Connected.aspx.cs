using System;
using System.Data;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Firebase.Database;

namespace FoodDelivery
{
    public partial class Contact : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Application["user_id"] = "qgT6LlkQ1pRmJDN9HXSxRWkBhzA2";

            grid_Orders.EnableViewState = true;

            await Firebase_Get_Data();
        }

        protected void Add_Orders_To_Table(Orders orders)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("NO", typeof(int));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Items", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));

            foreach(Data data in orders.orders)
            {
                dataTable.Rows.Add(0, data.Date.ToString(), data.Items[0], data.Status);
            }

            grid_Orders.DataSource = dataTable;
            grid_Orders.DataBind();
        }

        private async Task Firebase_Get_Data()
        {
            // Initialize the Firebase client
            var firebaseClient = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            // Retrieve data from the "users" node in the database
            var orders = await firebaseClient.Child("Restaurants").OnceAsync<Orders>();

            foreach(var order in orders)
            {
                if(order.Key == Application["user_id"].ToString())
                {
                    Add_Orders_To_Table(order.Object);

                    break;
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = 3;
        }
    }
}