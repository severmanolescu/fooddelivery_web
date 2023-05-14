using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoodDelivery
{
    public partial class ContactConnected : Page
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