
Partial Class Welcome
    Inherits System.Web.UI.Page

    Public Sub create_button(sender As Object, e As EventArgs)
        Response.Redirect("createaccount.aspx")

    End Sub

    Public Sub Login_button(sender As Object, e As EventArgs)
        Response.Redirect("login.aspx")
    End Sub


End Class
