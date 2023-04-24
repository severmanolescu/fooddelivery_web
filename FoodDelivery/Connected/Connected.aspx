<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="FoodDelivery.Contact" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <address>
            <asp:Label runat="server" ID="label"></asp:Label>

            <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><asp:Panel runat="server"></asp:Panel>
            <br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        </address>

        <asp:Panel ID="panel_orders" runat="server">

            <asp:GridView ID="grid_Orders" runat="server" Width="100%" AllowSelection="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            </asp:GridView>

        </asp:Panel>

    </main>

    <style>
        .button-label {
            display: inline-block;
            padding: 6px 12px;
            color: #337ab7;
            text-decoration: none;
            border-radius: 4px;
            cursor: pointer;
        }
    </style>
</asp:Content>
