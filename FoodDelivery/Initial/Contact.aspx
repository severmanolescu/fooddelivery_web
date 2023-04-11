﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="FoodDelivery.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="Panel2" HorizontalAlign="Center">
        <main>
            <section class="row" aria-labelledby="Title">
                <h1 id="Title">Contact us</h1>
                <p class="lead">Where can you find us?</p>
            </section>

            <div "Data">
                <img src="../Images/email_icon.png" width="40" height="40">
                <asp:Label runat="server" Text="binarybandits01@gmail.com"></asp:Label>

                <br />
                <img src="../Images/phone_icon.png" width="40" height="40">
                <asp:Label runat="server" Text="0799999999"></asp:Label>

                <br />
                <img src="../Images/address_icon.png" width="40" height="40">
                <asp:Label runat="server" Text="Timisoara"></asp:Label>

                <br />
                <br />
                <br />
                <asp:Button ID="buttonEmail" runat="server" Text="Trimite e-mail" OnClick="buttonEmail_Click" />    

            </div>
        </main>
    </asp:Panel>
</asp:Content>
