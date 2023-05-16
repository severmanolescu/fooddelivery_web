<%@ Page Title="Food" Language="C#" MasterPageFile="../MasterPages/MasterConnected.Master" Async="true" CodeBehind="Discount.aspx.cs" Inherits="FoodDelivery.Discount" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <asp:Panel ID="panel_orders" runat="server">

            <asp:Label ID="label_Error" runat="server" Visible ="false" ForeColor="Red">  </asp:Label>
            <br />

            <asp:Label runat="server">Discount:</asp:Label>
            <asp:TextBox runat="server" ID="textBoxDiscount"></asp:TextBox>
            <asp:Button runat="server" Text="Change" OnClick="DiscountChangeButton" ID="discountButton" />

        </asp:Panel>
    </main>
</asp:Content>
