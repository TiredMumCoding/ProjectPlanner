Imports System.Data
Imports System.Data.SqlClient

Partial Class ListView
    Inherits System.Web.UI.Page

    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn As New SqlConnection(ConnectionString)
        cnn.open()

        'creat two datasets that will be joined to make parent and child repeaters

        Dim cmd As New SqlCommand("select distinct taskdate, convert(varchar, taskdate, 106) as DateWord, projectid from project_tasks where projectname = @project and task is not null order by taskdate", cnn)
        cmd.Parameters.Add("@project", SqlDbType.VarChar)
        cmd.Parameters("@project").Value = Session("Project")

        Dim cmd2 As New SqlCommand("select taskdate, task, projectstrand, projectid from project_tasks where projectname = @project And task Is Not null order by taskdate, projectstrand", cnn)
        cmd2.Parameters.Add("@project", SqlDbType.VarChar)
        cmd2.Parameters("@project").Value = Session("Project")

        Dim DA As New SqlDataAdapter(cmd)
        Dim DA1 As New SqlDataAdapter(cmd2)

        ' put both tables into a dataset
        Dim DS As New DataSet
        DA.Fill(DS, "taskdate")
        DA1.Fill(DS, "task")

        ' join them together
        DS.Relations.Add("myrelation",
        DS.Tables("taskdate").Columns("taskdate"),
        DS.Tables("task").Columns("taskdate"))

        ParentRepeater.DataSource = DS.Tables("taskdate")
        Page.DataBind()
        cnn.Close()

    End Sub

    Public Sub Return_Proj(sender As Object, e As EventArgs)

        Response.Redirect("project.aspx")
    End Sub

End Class
