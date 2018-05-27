<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NewProject.aspx.vb" Inherits="NewProject" %>
<asp:Content ContentPlaceHolderID="jquery" runat="server">
        <script>
  $( function() {
      $( "#<%=startDate.ClientID %>" ).datepicker({dateFormat: 'dd/mm/yy'});
  } );
  </script>
     <script>
      $( function() {
          $("#<%=endDate.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
  } );
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <fieldset>
    <p class="form">Project Name</p><asp:TextBox ID="projectName" runat="server" Text="" CssClass="inp"></asp:TextBox>
    <p class="form">Start Date</p><asp:TextBox ID="startDate" runat="server" CssClass="inp" />
    <p class="form">End Date</p><asp:TextBox ID="endDate" runat="server" cssclass="inp"/><br />
    <asp:button ID="newProject" runat="server" Text="Submit" OnClick="new_Project" CssClass="inp button" /><br />
    <asp:Label ID="alreadyExists" runat="server" Text=""></asp:Label> <br/>
    <asp:button ID="NewProj" runat="server" Text="Go to New Project" OnClick="ToNew_Proj" cssclass="inp button"/>
        </fieldset>
</asp:Content>

