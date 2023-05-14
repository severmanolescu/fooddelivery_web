<%@ Page Title="Food" Language="C#" MasterPageFile="../MasterPages/MasterSignin.Master" Async="true" CodeBehind="FoodShow.aspx.cs" Inherits="FoodDelivery.FoodShow" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <asp:Panel ID="panel_orders" runat="server">

            <asp:Label ID="label_Error" runat="server" Visible ="false" ForeColor="Red">  </asp:Label>
            <br />

            <asp:GridView ID="grid_Items" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDeleting="grid_Items_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="NO" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                </Columns>
            </asp:GridView>

            <asp:Label runat="server">Add new item:</asp:Label><br />

            <asp:Label runat ="server">Name: </asp:Label> <asp:TextBox runat="server" ID="textbox_Name"></asp:TextBox> <br />
            <asp:Label runat ="server">Price: </asp:Label> <asp:TextBox runat="server" ID="textbox_Price"></asp:TextBox> <br />

            <asp:Label runat="server">Chose an image for the item: </asp:Label>
            <asp:FileUpload runat="server" accept="image/*" mutiple="false" ID="fileUpload" /> <br />

            <asp:Button runat="server" Text="Add" OnClick="AddFoodButton" ID="addFood_Button" /><br />

            <asp:Label runat="server" ID="error_Label" ForeColor="Red" Visible="false">Can't send data to the database, please try again!</asp:Label>

        </asp:Panel>
    </main>
</asp:Content>
