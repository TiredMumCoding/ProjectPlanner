Imports System.Data
Imports System.Data.SqlClient
Partial Class NewProject
    Inherits System.Web.UI.Page

    Public Sub new_Project(sender As Object, e As EventArgs)

        'create a new project in project_projects

        Dim Project = projectName.Text
        Dim Dstart = startDate.Text
        Dim Dend = endDate.Text

        Dim Uname = Session("User")
        Session("Project") = Project

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn = New SqlConnection(ConnectionString)
        cnn.Open()

        Dim cmd As New SqlCommand("If Not exists (select * from project_projects where projectname = @proj) insert into project_projects(projectname, startDate, endDate) values(@proj, @dateStart, @dateEnd)", cnn)

        cmd.Parameters.Add("@proj", SqlDbType.VarChar)
        cmd.Parameters("@proj").Value = Project

        cmd.Parameters.Add("@datestart", SqlDbType.Date)
        cmd.Parameters("@datestart").Value = Dstart

        cmd.Parameters.Add("@dateend", SqlDbType.Date)
        cmd.Parameters("@dateend").Value = Dend

        Dim ret As Integer = cmd.ExecuteNonQuery()
        If ret < 1 Then
            alreadyExists.Text = "A project already exists by this name. Please try another name"
            projectName.Text = ""

        Else

            'authorise the person who set up the project to have access to it

            Dim cmd2 As New SqlCommand("declare  @project int, @user int set @project  = (select id from project_projects where projectname = @proj2) set @user = (select id from project_users where users= @usr) insert into project_auth(projectid, userid) values(@project, @user)", cnn)

            cmd2.Parameters.Add("@proj2", SqlDbType.VarChar)
            cmd2.Parameters("@proj2").Value = Project

            cmd2.Parameters.Add("@usr", SqlDbType.VarChar)
            cmd2.Parameters("@usr").Value = Uname

            Dim ret2 As Integer = cmd2.ExecuteNonQuery()
            If ret2 < 1 Then
                alreadyExists.Text = "update not worked"
            Else
                alreadyExists.Text = "update worked"
            End If

            'populate the project_tasks table with one line per day for the life of the project.
            Dim cmd3 As New SqlCommand("declare @startDate date = @datestart1, @endDate date = @dateend1, @project varchar(max) = @proj3 Declare @projectid int =(select id from project_projects where projectname = @project) , @weekSunday date = case when datepart(dw, @startDate) <> 1 then dateadd(d, -datepart(dw, @startDate) +1, @startDate) else @startDate end While @startDate <= @endDate Begin  Insert into Project_Tasks (taskdate, weekStartingSunday, projectname, projectid) values (@startDate, @weekSunday, @project, @projectid ) Set @startDate = dateadd(day, 1, @startDate) Set @weekSunday = Case When datepart(dw, @startDate) <> 1 Then dateadd(d, -datepart(dw, @startDate) +1, @startDate) else @startDate end End", cnn)

            cmd3.Parameters.Add("@datestart1", SqlDbType.Date)
            cmd3.Parameters("@datestart1").Value = Dstart

            cmd3.Parameters.Add("@dateend1", SqlDbType.Date)
            cmd3.Parameters("@dateend1").Value = Dend

            cmd3.Parameters.Add("@proj3", SqlDbType.VarChar)
            cmd3.Parameters("@proj3").Value = Project

            Dim ret3 As Integer = cmd3.ExecuteNonQuery()
            If ret3 < 1 Then
                alreadyExists.Text = "second update not worked"
            Else alreadyExists.Text = "second update worked"

            End If


        End If

    End Sub

    Public Sub ToNew_Proj(sender As Object, e As EventArgs)
        Response.Redirect("project.aspx")
    End Sub

End Class
