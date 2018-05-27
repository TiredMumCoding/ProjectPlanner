
Imports System.Data.SqlClient
Imports System.Data

Partial Class MarkComplete
    Inherits System.Web.UI.Page

    Public Sub Page_Load(Sender As Object, e As EventArgs) Handles Me.Load

        'get information about the task that's been clicked on and display it in a grid
        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()
        Dim cmd As New SqlCommand("select projectname, cast(taskdate as date) as TaskDate, username, task from project_tasks where id = @id", cnn)

        cmd.Parameters.Add("@id", SqlDbType.Int)
        cmd.Parameters("@id").Value = Session("TaskID")

        Dim DA As New SqlDataAdapter(cmd)
        Dim DS As New DataSet
        DA.Fill(DS)

        Grid.DataSource = DS
        Grid.DataBind()

        cnn.Close()
    End Sub

    Public Sub Make_Complete(sender As Object, e As EventArgs)
        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.Open()

        Dim cmd As New SqlCommand("update project_tasks set mark_complete = 1 where ID = @id", cnn)

        cmd.Parameters.Add("@id", SqlDbType.Int)
        cmd.Parameters("@id").Value = Session("TaskID")

        Dim ret As Integer = cmd.ExecuteNonQuery()
        If ret >= 1 Then
            UpdateSuccessful.Text = "Task Succesfully Marked Complete"
        Else UpdateSuccessful.Text = "Update Unsucessful"
        End If

    End Sub

    Public Sub Delete_Task(sender As Object, e As EventArgs)

        ' delete task and adjust order of surrounding tasks

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)

        cnn.Open()

        Dim cmd As New SqlCommand("declare @project varchar(max), " _
        & "@deletedOrder int, " _
        & "@taskdate varchar(max) " _
        & "set @deletedOrder = (select projectstrand from project_tasks where id = @id) " _
        & "set @taskdate =(select taskdate from project_tasks where id = @id) " _
        & "set @project = (select projectname from project_tasks where id = @id) " _
        & "update project_tasks " _
        & "set projectstrand = projectstrand - 1 " _
        & "where projectname = @project and taskdate = @taskdate " _
        & "and projectstrand >= @deletedOrder " _
        & "delete From project_tasks Where ID = @id ", cnn)



        cmd.Parameters.Add("@id", SqlDbType.Int)
        cmd.Parameters("@id").Value = Session("TaskID")

        Dim ret As Integer = cmd.ExecuteNonQuery()
        If ret >= 1 Then
            UpdateSuccessful.Text = "Record Deleted"
        Else UpdateSuccessful.Text = "Record not Deleted"
        End If

        cnn.Close()

    End Sub

    Public Sub Return_Proj(sender As Object, e As EventArgs)
        Response.Redirect("project.aspx")
    End Sub

End Class
