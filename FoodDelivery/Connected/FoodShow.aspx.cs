using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Cloud.Storage.V1;
using System.IO;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin.Auth;
using Firebase.Auth;
using Google.Apis.Storage.v1.Data;

namespace FoodDelivery
{
    public partial class FoodShow : Page
    {
        private string restaurantCity;

        private string restaurantName;

        protected async void Page_Load(object sender, EventArgs e)
        {
            restaurantCity = Request.QueryString["city"];
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

                label_Error.Visible = true;

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

                int itemIndex = 0;

                int realItemIndex = 0;

                foreach (Food food in foods)
                {
                    if (food != null)
                    {
                        dataTable.Rows.Add(realItemIndex,
                                           food.name,
                                           food.price);

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
            var firebaseClient = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            var restaurants = await firebaseClient.Child("Restaurants" + restaurantCity).OnceAsync<Restaurant>();

            if(restaurants != null)
            {
                foreach(var restaurant in restaurants)
                {
                    if(restaurant.Key == restaurantName)
                    {
                        ShowData(restaurant.Object.products);

                        break;
                    }
                }
            }
        }

        private string UploadImage()
        {
            string bucketName = "fooddelivery-564e8.appspot.com";

            string filePath = Path.Combine("D:\\Images", fileUpload.FileName);

            var storage = StorageClient.Create();

            string url = @"https://storage.googleapis.com/fooddelivery-564e8.appspot.com";

            using (var fileStream = File.OpenRead(filePath))
            {
                string folderName = "food/" + restaurantName;

                string fileExtension = fileUpload.FileName.Split('.')[1];

                var objectName = $"{folderName}/{textbox_Name.Text}.{fileExtension}";

                storage.UploadObject(bucketName, objectName, null, fileStream);

                url += "/" + objectName;
            }

            return url;
        }

        protected async Task SendData()
        {
            try
            {
                var firebase = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

                Food food = new Food { name = textbox_Name.Text, price = textbox_Price.Text};

                string url = UploadImage();

                food.image = url;

                int index = 0;

                if(grid_Items != null && grid_Items.Rows.Count > 0)
                {
                    index = int.Parse(grid_Items.Rows[grid_Items.Rows.Count - 1].Cells[0].Text) + 1;
                }

                await firebase.Child("Restaurants" + restaurantCity).Child(restaurantName).Child("Products").Child((index).ToString).PutAsync(food);

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

        private bool CheckFoodName()
        {
            foreach(GridViewRow row in grid_Items.Rows) 
            {
                if(row.Cells.Count > 0 && row.Cells[1].Text == textbox_Name.Text) 
                {
                    error_Label.Text = "This item already exist, please insert another one!";

                    error_Label.Visible = true;

                    return false;
                }
            }

            error_Label.Visible = false;

            return true;
        }

        private bool CheckData()
        {
            if(textbox_Name.Text != string.Empty && textbox_Price.Text != string.Empty && fileUpload.HasFile)
            {
                try
                {
                    int priceTest = int.Parse(textbox_Price.Text);

                    error_Label.Visible = false;

                    return CheckFoodName();
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
            var firebase = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

            await firebase.Child("Restaurants" + restaurantCity).Child(restaurantName).Child("Products").Child(grid_Items.Rows[e.RowIndex].Cells[0].Text).DeleteAsync();

            await GetData();
        }
    }
}