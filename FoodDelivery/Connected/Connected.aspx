<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="Connected.aspx.cs" Inherits="FoodDelivery.Contact" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        

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

                    <asp:CommandField ShowSelectButton="True" ></asp:CommandField>
                </Columns>
            </asp:GridView>

        </asp:Panel>

    </main>
</asp:Content>
