using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using System.Web.DynamicData;
using System.Windows.Forms;

namespace FoodDelivery
{
    public partial class FoodShow : Page
    {
        private string foodLink;

        private string restaurantName;

        protected async void Page_Load(object sender, EventArgs e)
        {
            foodLink = Request.QueryString["link"];
            restaurantName = Request.QueryString["name"];
            try
            {
                if (!IsPostBack)
                {
                    await GetData();
                }
            }
            catch
            {
                label_Error.Text = "Something went wrong! Try again!";

                addFood_Button.Enabled = false;
            }
        }

        private void ShowData(List<Food> foods)
        {
            grid_Items.DataSource = null;
            grid_Items.DataBind();

            if (foods != null)
            {
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("NO", typeof(int));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Price", typeof(int));
                dataTable.Columns.Add("ActualIndex", typeof(int));

                int itemIndex = 0;

                int realItemIndex = 0;

                foreach (Food food in foods)
                {
                    if (food != null)
                    {
                        dataTable.Rows.Add(realItemIndex,
                                           food.name,
                                           food.price,
                                           realItemIndex);

                        itemIndex += 1;

                        grid_Items.DataSource = null;
                    }

                    realItemIndex += 1;
                }

                grid_Items.DataSource = dataTable;
                grid_Items.DataBind();
            }
        }

        private async Task GetData()
        {
            var firebaseClient = new FirebaseClient(foodLink);

            var restaurants = await firebaseClient.Child("Restaurants").OnceAsync<Restaurant>();

            if(restaurants != null)
            {
                foreach(var restaurant in restaurants)
                {
                    if(restaurant.Key == restaurantName)
                    {
                        ShowData(restaurant.Object.food);

                        break;
                    }
                }
            }
        }

        protected async Task SendData()
        {
            try
            {
                var firebase = new FirebaseClient(foodLink);

                Food food = new Food { name = textbox_Name.Text, price = textbox_Price.Text, image = "Test"};

                int index = 0;

                if(grid_Items != null && grid_Items.Rows.Count > 0)
                {
                    index = int.Parse(grid_Items.Rows[grid_Items.Rows.Count - 1].Cells[0].Text) + 1;
                }

                await firebase.Child("Restaurants").Child(restaurantName).Child("Food").Child((index).ToString).PutAsync(food);

                await GetData();

                RestoreTextBoxs();
            }
            catch
            {
                error_Label.Text = "Can't send data to the database, please try again!";

                error_Label.Visible = true;
            }

            addFood_Button.Enabled = true;
        }

        private void RestoreTextBoxs()
        {
            textbox_Name.Text = string.Empty;
            textbox_Price.Text = string.Empty;
            fileUpload.Dispose();
        }

        protected async void AddFoodButton(object sender, EventArgs e)
        {
            addFood_Button.Enabled = false;

            if(CheckData())
            {
                await SendData();
            }

            addFood_Button.Enabled = true;
        }

        private bool CheckData()
        {
            if(textbox_Name.Text != string.Empty && textbox_Price.Text != string.Empty)
            {
                try
                {
                    int priceTest = int.Parse(textbox_Price.Text);

                    error_Label.Visible = false;

                    return true;
                }
                catch
                {
                    error_Label.Text = "Please insert a valid price!";

                    error_Label.Visible = true;

                    return false;
                }
            }
            else
            {
                error_Label.Text = "Please insert all the data!";

                error_Label.Visible = true;
            }

            return false;
        }

        protected async void grid_Items_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var firebase = new FirebaseClient(foodLink);

            await firebase.Child("Restaurants").Child(restaurantName).Child("Food").Child(grid_Items.Rows[e.RowIndex].Cells[0].Text).DeleteAsync();

            await GetData();
        }
    }
}