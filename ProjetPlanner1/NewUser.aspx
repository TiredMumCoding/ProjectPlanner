<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NewUser.aspx.vb" Inherits="NewUser" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <fieldset>
    <h2 class="pink">Create a New User Account </h2>
    <p class="form">Surname:</p><asp:TextBox ID="surname" runat="server" Text="" CssClass="inp" /><br />
    <p class="form">Firstname:</p><asp:TextBox ID="firstname" runat="server" text="" CssClass="inp" /><br />
    <asp:button ID="submit" runat="server" Text="Submit" OnClick ="Create_User" CssClass="inp button" /><br />
    <asp:label ID="updated" runat="server" Text="" /> <br/> <br/>
        </fieldset>

    <fieldset>
    <h2 class="pink">Link an Existing Account to the Project </h2>
    <p class="form">Select a User:</p><asp:DropDownList ID="allUsers" runat="server" onselectedindexchanged="Add_User" AutoPostBack="true" CssClass="inp"/><br />
    <asp:button ID="ReturnProj" runat="server" Text="Return to Project" OnClick="Return_Proj" CssClass="inp button" />
        </fieldset>
</asp:Content>

