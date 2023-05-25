using System;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                label_Error.Text = "Something went wrong! Try again!";

                label_Error.Visible = true;
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
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("NO", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Amount", typeof(int));


                
            dataTable.Rows.Add(0,
                                orderDetails.data.name,
                                1);

            grid_Items.DataSource = null;

            label_Price.Text += orderDetails.data.price.ToString();

            grid_Items.DataSource = dataTable;
            grid_Items.DataBind();
        }

        private void Show_Data()
        {
            if (orderDetails != null)
            {
                label_Address.Text = "Address: " + orderDetails.data.address;
                label_Date.Text = "Date: " + orderDetails.data.date;
                label_Person.Text = "Person: " + orderDetails.data.username;
                label_Phone.Text = "Phone: " + orderDetails.data.phone;
                label_Price.Text = "Price: " + orderDetails.data.price;

                ListItem dropDownItem = dropDown_Status.Items.FindByText(orderDetails.data.deliveryStatus);

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

            string path = "/Restaurants" + orderDetails.city + "/" + orderDetails.name + "/Orders/" + orderDetails.data.index.ToString() + "/deliveryStatus.json";

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