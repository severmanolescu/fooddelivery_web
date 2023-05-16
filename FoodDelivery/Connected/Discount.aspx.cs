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
    public partial class Discount : Page
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

                discountButton.Enabled = false;
            }
        }

        private void ShowData(string disocunt)
        {
            textBoxDiscount.Text = disocunt;
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
                        ShowData(restaurant.Object.discount);

                        break;
                    }
                }
            }
        }

        protected async void DiscountChangeButton(object sender, EventArgs e)
        {
            try
            {
                var firebase = new FirebaseClient("https://fooddelivery-564e8-default-rtdb.firebaseio.com/");

                await firebase.Child("Restaurants" + restaurantCity).Child(restaurantName).Child("Discount").PutAsync(textBoxDiscount.Text);
            }
            catch
            {
                label_Error.Text = "Can't send data to the database, please try again!";

                label_Error.Visible = true;
            }

            discountButton.Enabled = true;
        }

    }
}