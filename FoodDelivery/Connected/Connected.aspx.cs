using Google.Api.Gax.ResourceNames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FoodDelivery
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Application["e-mail"] = "qgT6LlkQ1pRmJDN9HXSxRWkBhzA2";

            //label.Text = Application["user_id"].ToString();


            panel_orders.Controls.Add(new Label { Text = "asdasdasdasd" });
        }

        protected TableRow Row_Generator()
        {
            TableRow row = new TableRow();

            TableCell orderID = new TableCell();
            orderID.Text = "1";

            TableCell orderDate = new TableCell();
            orderDate.Text = "2";

            TableCell orderItems = new TableCell();
            orderItems.Text = "3";

            TableCell orderStatus = new TableCell();
            orderStatus.Text = "4";

            row.Cells.Add(orderID);
            row.Cells.Add(orderDate);
            row.Cells.Add(orderItems);
            row.Cells.Add(orderStatus);

            return row;
        }

        protected void Add_Orders_To_Table()
        {
            myTable.Rows.Add(Row_Generator());
        }
    }
}