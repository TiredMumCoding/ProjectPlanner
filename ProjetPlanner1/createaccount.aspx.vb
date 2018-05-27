Imports System.Data
Imports System.Data.SqlClient

Partial Class createaccount
    Inherits System.Web.UI.Page

    Public Sub submit_new_details(sender As Object, e As EventArgs)
        notAvailable.Text = ""
        Dim uname = create_username.Text
        Dim pword = create_password.Text

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn = New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("if not exists (select * from project_users where users = @user) insert into project_users(users, password)  values (@user, @password)", cnn)

        cmd.Parameters.Add("@user", SqlDbType.VarChar)
        cmd.Parameters("@user").Value = uname

        cmd.Parameters.Add("@password", SqlDbType.VarChar)
        cmd.Parameters("@password").Value = pword

        Dim ret As Integer = cmd.ExecuteNonQuery()
        If ret < 1 Then
            notAvailable.Text = "This username is already taken, please create another"
            create_username.Text = ""
            create_password.Text = ""
        Else
            Session("User") = uname
            Response.Redirect("project.aspx")

        End If



    End Sub

End Class
