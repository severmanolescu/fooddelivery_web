<%@ Page Title="OrderView" Language="C#" MasterPageFile="~/Site.Master" Async="true" CodeBehind="OrderView.aspx.cs" Inherits="FoodDelivery.OrderView" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <asp:Panel ID="panel_orders" runat="server">

            <asp:Label ID="label_Person" runat="server">  </asp:Label>
            <br />
            <asp:Label ID="label_Address" runat="server">  </asp:Label>
            <br />
            <asp:Label ID="label_Phone" runat="server">  </asp:Label>
            <br />
            <asp:Label ID="label_Date" runat="server">  </asp:Label>
            <br />

            <asp:Label ID="label_Status" runat="server"> Status: </asp:Label>

            <asp:DropDownList ID="dropDown_Status" runat="server" OnSelectedIndexChanged="DropDown_Index_Changed" AutoPostBack="True">
                <asp:ListItem>Placed</asp:ListItem>
                <asp:ListItem>Accepted</asp:ListItem>
                <asp:ListItem>Out for Delivery</asp:ListItem>
                <asp:ListItem>Delivered</asp:ListItem>
                <asp:ListItem>Canceled</asp:ListItem>
                <asp:ListItem>Completed</asp:ListItem> 
            </asp:DropDownList>


            <asp:GridView ID="grid_Items" runat="server" Width="100%" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="NO" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                </Columns>
            </asp:GridView>

        </asp:Panel>
    </main>
</asp:Content>
