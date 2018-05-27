Imports System.Data
Imports System.Data.SqlClient

Partial Class NewTask
    Inherits System.Web.UI.Page

    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' populate the dropdown with the users linked to the project in question
        If Not Page.IsPostBack Then
            Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
            Dim cnn = New SqlConnection(ConnectionString)
            cnn.Open()

            Dim cmd As New SqlCommand("select distinct t.projectid, t.projectname, a.userid, u.Users from project_tasks t inner join project_auth a on t.projectid = a.projectid inner join Project_Users u on a.userid = u.id where projectname = @project", cnn)
            cmd.Parameters.Add("@project", SqlDbType.VarChar)
            cmd.Parameters("@project").Value = Session("Project")

            Dim DA = New SqlDataAdapter(cmd)
            Dim DS = New DataSet
            DA.Fill(DS)

            user.DataSource = DS
            user.DataTextField = "Users"
            user.DataValueField = "userid"
            user.DataBind()

            cnn.Close()

        End If

    End Sub

    Public Sub new_Task(sender As Object, e As EventArgs)
        Dim Tsk As String = Task.Text
        Dim DStart As Date = CDate(taskDate.Text)
        Dim Usr = user.SelectedValue
        Dim UsrName As String
        Dim ProjectId As Integer
        Dim ProjStrand As String = order.Text

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn1 = New SqlConnection(ConnectionString)
        cnn1.Open()

        'get the max and min date range for the project
        Dim Cmd4 As New SqlCommand("select min(taskdate) as minTask, max(taskDate) as MaxTask from project_tasks where projectname = @project", cnn1)

        Cmd4.Parameters.Add("@project", SqlDbType.VarChar)
        Cmd4.Parameters("@project").Value = Session("Project")

        Dim Reader2 As SqlDataReader = Cmd4.ExecuteReader

        Reader2.Read()

        Dim MinDate As Date = CDate(Reader2.Item("minTask"))
        Dim MaxDate As Date = CDate(Reader2.Item("maxTask"))

        Reader2.Close()

        ' Only try to inser the task if the dates are within the range of the project
        If DStart <= MinDate Then
            updateSuccessful.Text = "this date is before the start of the project"
        Else
            If DStart >= Maxdate Then
                updateSuccessful.Text = "this date is after the end of the project"

            Else

                'get the user name which corresponds to the selected id, ready to be added to the project_tasks table
                Dim Cmd2 As New SqlCommand("select users from project_users where id = @id", cnn1)

                Cmd2.Parameters.Add("@id", SqlDbType.Int)
                Cmd2.Parameters("@id").Value = Usr

                Dim Reader As SqlDataReader = Cmd2.ExecuteReader()

                Do While Reader.Read()
                    UsrName = Reader.Item("users")
                Loop

                Reader.Close()

                'get project id that corresponds to the selected project, ready to be inserted into project_tasks
                Dim cmd3 As New SqlCommand("select id from Project_Projects where ProjectName = @project", cnn1)

                cmd3.Parameters.Add("@project", SqlDbType.VarChar)
                cmd3.Parameters("@project").Value = Session("project")

                Dim Reader1 As SqlDataReader = cmd3.ExecuteReader

                Do While Reader1.Read()
                    ProjectId = Reader1.Item("id")
                Loop

                Reader1.Close()

                'add the task to the project and adjust the order of the surrounding tasks
                Dim cmd1 As New SqlCommand("if exists (select taskdate from project_tasks where taskdate" _
& "= @taskstart and task is null and projectname = @project)" _
& "Begin " _
& "Update project_tasks " _
& "set task = @task," _
& "projectstrand = @projectstrand," _
& "thecount = 1," _
& "mark_complete = 0," _
& "userid = @user, " _
& "username = @username " _
& "where taskdate = @taskstart and task is null and projectname = @project " _
& "End " _
& "Else " _
& "Begin " _
& "insert into project_tasks " _
& "values(@taskstart, @username, @projectstrand, @task," _
& "case when datepart(dw, @taskstart) <> 1 then " _
& "dateadd(d, -datepart(dw, @taskstart) +1, @taskstart) " _
& "else @taskstart end," _
& "1, @project, @projectid, @user, 0) " _
        & "declare @newRecord int, " _
        & "@newOrder int " _
        & "set @neworder = (select projectstrand from project_tasks p inner join (select max(id) as id from project_tasks)t on p.id = t.id) " _
        & "set @newrecord =(select max(id) as id from project_tasks) " _
        & "update project_tasks " _
        & "set projectstrand = projectstrand + 1 " _
        & "where projectname = @project and taskdate = @taskstart " _
        & "and projectstrand >= @neworder " _
        & "and id <> @newrecord END", cnn1)

                cmd1.Parameters.Add("@task", SqlDbType.VarChar)
                cmd1.Parameters("@task").Value = Tsk

                cmd1.Parameters.Add("@user", SqlDbType.Int)
                cmd1.Parameters("@user").Value = Usr

                cmd1.Parameters.Add("@username", SqlDbType.VarChar)
                cmd1.Parameters("@username").Value = UsrName

                cmd1.Parameters.Add("@projectstrand", SqlDbType.Int)
                cmd1.Parameters("@projectstrand").Value = ProjStrand

                cmd1.Parameters.Add("@taskstart", SqlDbType.Date)
                cmd1.Parameters("@taskstart").Value = DStart

                cmd1.Parameters.Add("@project", SqlDbType.VarChar)
                cmd1.Parameters("@project").Value = Session("Project")

                cmd1.Parameters.Add("@projectid", SqlDbType.Int)
                cmd1.Parameters("@projectid").Value = ProjectId

                Dim ret As Integer
                ret = cmd1.ExecuteNonQuery()
                If ret >= 1 Then
                    updateSuccessful.Text = "New task has been added successfully"
                Else
                    updateSuccessful.Text = "Update Failed"
                End If
                cnn1.Close()

            End If

        End If

    End Sub

    Public Sub Return_Proj(sender As Object, e As EventArgs)
        Response.Redirect("project.aspx")
    End Sub

End Class