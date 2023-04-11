<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="FoodDelivery.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your contact page.</h3>
        <address>
            One Microsoft Way<br />
            Redmond, WA 98052-6399<br />
            <abbr title="Phone">P:</abbr>
            425.555.0100
        </address>

        <address>
            <asp:Label runat="server" ID="label"></asp:Label>

            <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><asp:Panel runat="server"></asp:Panel>
            <br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
        </address>

        <asp:Panel ID="panel_orders" runat="server">

            <asp:Table ID="myTable" runat="server" Width="100%"> 
                <asp:TableRow>
                    <asp:TableCell>ID</asp:TableCell>
                    <asp:TableCell>Date</asp:TableCell>
                    <asp:TableCell>Items</asp:TableCell>
                    <asp:TableCell>Status</asp:TableCell>
                </asp:TableRow>
            </asp:Table>  

        </asp:Panel>
    </main>
</asp:Content>
