<%@ Page Title="Contact" Language="C#" MasterPageFile="../MasterPages/MasterConnected.Master" AutoEventWireup="true" CodeBehind="ContactConnected.aspx.cs" Inherits="FoodDelivery.ContactConnected" %>

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
                        <li class="nav-item"><a class="nav-link" runat="server" href="ContactConnected.aspx">Contact</a></li>
                        <li class="nav-item"><a class="nav-link" runat="server" href="../Initial/Signin.aspx">Sign out</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </asp:Panel>

    <asp:Panel runat="server" ID="Panel2" HorizontalAlign="Center">
        <main>

            <section class="row" aria-labelledby="Title">
                <h1 id="Title">Contact us</h1>
                <p class="lead">Where can you find us?</p>
            </section>

                <div "Data">
                    <asp:Table ID="myTable" runat="server" Width="20%" HorizontalAlign ="Center"> 
                        <asp:TableRow>
                            <asp:TableCell><img src="../Images/email_icon.png" width="40" height="40"></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" Text="binarybandits01@gmail.com"></asp:Label></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell><img src="../Images/phone_icon.png" width="40" height="40"></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" Text="0799999999"></asp:Label></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell><img src="../Images/address_icon.png" width="40" height="40"></asp:TableCell>
                            <asp:TableCell><asp:Label runat="server" Text="Timisoara"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>  

                    <br />
                    <br />
                    <br />
                    <asp:Button ID="buttonEmail" runat="server" Text="Send us an e-mail" OnClick="buttonEmail_Click" />    
                </div>
        </main>
    </asp:Panel>
</asp:Content>
