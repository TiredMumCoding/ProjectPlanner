
Imports System.Data
Imports System.Data.SqlClient

Partial Class NewUser
    Inherits System.Web.UI.Page

    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'populate a drop-down list with people who already have accounts on the site

        If Not Page.IsPostBack Then

            Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
            Dim cnn As New SqlConnection(ConnectionString)
            cnn.Open()

            Dim cmd2 As New SqlCommand("select id, users, password from project_users", cnn)

            Dim DA As New SqlDataAdapter(cmd2)
            Dim DS As New DataSet
            DA.Fill(DS)

            allUsers.DataSource = DS
            allUsers.DataTextField = "Users"
            allUsers.DataValueField = "id"
            allUsers.DataBind()

            cnn.Close()

        End If

    End Sub

    Public Sub Create_User(sender As Object, e As EventArgs)

        Dim sname As String = surname.Text
        Dim fname As String = firstname.Text
        Dim Project As String = Session("Project")


        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("if not exists (select users, password from project_users where users = @firstName and password = @surName) insert into project_users values (@firstName, @surName)", cnn)

        cmd.Parameters.Add("@firstName", SqlDbType.VarChar)
        cmd.Parameters("@firstName").Value = fname

        cmd.Parameters.Add("@surName", SqlDbType.VarChar)
        cmd.Parameters("@surName").Value = sname

        Dim ret As Integer
        ret = cmd.ExecuteNonQuery()
        If ret >= 1 Then
            updated.Text = "user account has been added"
        Else updated.Text = "this user already exists"
        End If

        'UID = Get_User_ID(sname, fname)

        'PID = Get_Project_ID(Project)

        ' once the user's been created, authorize them to view the project

        Dim cmd1 As New SqlCommand("insert into project_auth values (@proj, @usr)", cnn)

        cmd1.Parameters.Add("@proj", SqlDbType.Int)
        cmd1.Parameters("@proj").Value = Get_Project_ID(Project)

        cmd1.Parameters.Add("@usr", SqlDbType.Int)
        cmd1.Parameters("@usr").Value = Get_User_ID(sname, fname)

        Dim ret1 As Integer
        ret1 = cmd1.ExecuteNonQuery()
        If ret1 >= 1 Then

            updated.Text = "user account has been added"
        Else updated.Text = "Failed to add User to project"
        End If


        cnn.Close()
    End Sub

    Public Sub Add_User(sender As Object, e As EventArgs)
        Dim Project As String = Session("Project")

        'authorise a user selected from the drop-down list to view the project

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("If Not exists (select * from project_auth where projectid = @proj And userid = @usr) insert into project_auth values (@proj, @usr)", cnn)

        cmd.Parameters.Add("@proj", SqlDbType.Int)
        cmd.Parameters("@proj").Value = Get_Project_ID(Project)

        cmd.Parameters.Add("@usr", SqlDbType.Int)
        cmd.Parameters("@usr").Value = allUsers.SelectedValue

        cmd.ExecuteNonQuery()

    End Sub

    Public Function Get_User_ID(surname As String, firstname As String) As Integer

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("select id from project_users where users = @firstName and password = @surName", cnn)

        cmd.Parameters.Add("@firstName", SqlDbType.VarChar)
        cmd.Parameters("@firstName").Value = firstname

        cmd.Parameters.Add("@surName", SqlDbType.VarChar)
        cmd.Parameters("@surName").Value = surname

        Dim Reader As SqlDataReader = cmd.ExecuteReader()

        Reader.Read()
        Dim UserID As Integer = Reader.Item("id")
        Reader.Close()
        cnn.Close()

        Return UserID

    End Function

    Public Function Get_Project_ID(projectName As String) As Integer
        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("select id from project_projects where projectname = @projectName", cnn)

        cmd.Parameters.Add("@projectName", SqlDbType.VarChar)
        cmd.Parameters("@projectName").Value = projectName

        Dim Reader As SqlDataReader = cmd.ExecuteReader()

        Reader.Read()
        Dim ProjectID As Integer = Reader.Item("id")
        Reader.Close()
        cnn.Close()

        Return ProjectID

    End Function

    Public Sub Return_Proj(sender As Object, e As EventArgs)
        Response.Redirect("project.aspx")
    End Sub

End Class
