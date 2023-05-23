using System;
using System.Diagnostics;
using System.Web.UI;

namespace FoodDelivery
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonEmail_Click(object sender, EventArgs e)
        {
            Process.Start("mailto: binarybandits01@gmail.com");
        }
    }
}