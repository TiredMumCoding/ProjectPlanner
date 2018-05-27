<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NewTask.aspx.vb" Inherits="NewTask" %>
<asp:Content ContentPlaceHolderID="jquery" runat="server">
        <script>
  $( function() {
      $( "#<%=taskDate.ClientID %>" ).datepicker({dateFormat: 'dd/mm/yy'});
  } );
  </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <fieldset>
    <p class="form">Task:</p><asp:TextBox ID="Task" runat="server" Text="" CssClass="inp"></asp:TextBox>
    <p class="form">Task Date:</p><asp:TextBox ID="taskDate" runat="server" CssClass="inp" />
    <p class="form">User:</p><asp:dropdownlist ID="user" runat="server" cssclass="inp"/> <br/>
    <p class="form">Order:</p><asp:TextBox ID="order" runat="server" CssClass="inp" /> <br />
    <asp:button ID="newTask" runat="server" Text="submit" OnClick = "new_Task" cssclass="inp button" /> <br />
    <asp:Label ID="updateSuccessful" runat="server" Text=""></asp:Label> <br/>
    <asp:button ID="ReturnProj" runat="server" Text="Return to Project" OnClick="Return_Proj" CssClass="inp button" />
        </fieldset>
</asp:Content>


