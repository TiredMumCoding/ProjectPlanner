Imports System.Data
Imports System.Data.SqlClient

Partial Class Login
    Inherits System.Web.UI.Page

    Public Sub submit_button(sender As Object, e As EventArgs)
        Dim Uname = username.Text
        Dim Pword = password.Text
        NotExists.Text = ""

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn = New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("select * from project_users where users = @user And password = @password", cnn)

        cmd.Parameters.Add("@user", SqlDbType.VarChar)
        cmd.Parameters("@user").Value = Uname

        cmd.Parameters.Add("@password", SqlDbType.VarChar)
        cmd.Parameters("@password").Value = Pword

        Dim Reader As SqlDataReader = cmd.ExecuteReader()
        If Reader.HasRows() = True Then
            Session("User") = Uname
            Response.Redirect("Project.aspx")
        Else
            NotExists.Text = "Username and password not found, please try again"
            username.Text = ""
            password.Text = ""
        End If



    End Sub

End Class
