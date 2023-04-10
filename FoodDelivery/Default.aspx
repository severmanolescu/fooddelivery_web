<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FoodDelivery._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel runat="server" ID="Panel2" HorizontalAlign="Center">
        <main>
            <section class="row" aria-labelledby="Title">
                <h1 id="Title">Sign in</h1>
                <p class="lead">Please enter your e-mail and password to continue.</p>
            </section>

            <div "Data">
                <asp:Label runat="server" Text="Email"></asp:Label>

                <br />
                <asp:TextBox runat="server" ID="text_email"></asp:TextBox>

                <br />
                <asp:Label runat="server" Text="Insert a valid email" ID="label_error_email" ForeColor="#FF3300"></asp:Label>
                
                <br />
                <asp:Label runat="server" Text="Password"></asp:Label>

                <br />
                <asp:TextBox runat="server" TextMode="Password" ID="text_password"></asp:TextBox>

                <br />
                <asp:Label runat="server" Text="Insert a valid password" ID="label_error_password" ForeColor="#FF3300"></asp:Label>

                <br />
                <br />
                <asp:Button runat="server" Text="Sign in" BorderStyle="Solid" OnClick="Sing_In_Button_Pressed"></asp:Button>
                <br />
                <asp:Label runat="server" Text=" Wrong email or password" ID="label_error_signin" ForeColor="#FF3300"></asp:Label>
            </div>
        </main>
    </asp:Panel>

</asp:Content>
