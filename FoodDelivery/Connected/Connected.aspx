<%@ Page Title="Orders" Language="C#" MasterPageFile="../MasterPages/MasterConnected.Master" Async="true" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="Connected.aspx.cs" Inherits="FoodDelivery.Contact" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel runat="server" Width="100%">
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
            <div class="container">
                <a class="navbar-brand" runat="server" href="~/">Food Atlas</a>
                <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Toggle navigation" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item"><a class="nav-link" runat="server" href="Connected.aspx">Orders</a></li>
                        <li class="nav-item"><asp:Button class="nav-link" BackColor="Transparent" BorderColor="Transparent" runat="server" href="../Initial/Contact" Text="Food" OnClick="FoodPageLoad"></asp:Button></li>
                    </ul>
                </div>
            </div>
        </nav>
    </asp:Panel>

    <main aria-labelledby="title">

        <asp:Timer ID="Timer1" runat="server" Interval="30000" OnTick="Timer1_Tick"></asp:Timer>

        <asp:Panel ID="panel_orders" runat="server">

            <asp:GridView ID="grid_Orders" runat="server" Width="100%" AllowSelection="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowCustomPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="OnRowDataBound">
                <Columns>
                    <asp:BoundField DataField="NO" HeaderText="NO" />
                    <asp:BoundField DataField="Items" HeaderText="Items" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:BoundField DataField="Person" HeaderText="Person" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Date" HeaderText="Date" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />

                    <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                </Columns>
            </asp:GridView>

        </asp:Panel>

    </main>
</asp:Content>
