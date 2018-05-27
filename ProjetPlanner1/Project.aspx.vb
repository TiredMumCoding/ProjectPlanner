Imports System.Data.SqlClient
Imports System.Data
Partial Class Project
    Inherits System.Web.UI.Page

    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'populating username
        If Session("user") IsNot Nothing Then
            welcome.Text = "Welcome " & Session("user") & "!"
        End If

        'populating project selections.  can't add parameters to data adapters so need sql command and data adapter
        If Not Page.IsPostBack Then
            Dim ConnectionString1 As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
            Dim cnn1 = New SqlConnection(ConnectionString1)
            cnn1.Open()

            Dim cmd1 As New SqlCommand("declare @userid int set @userid = (select id from project_users where users = @usr) Select projectname from project_projects p inner Join (select projectid from project_auth where userid = @userid) u On p.id = u.projectid", cnn1)
            cmd1.Parameters.Add("@usr", SqlDbType.VarChar)
            cmd1.Parameters("@usr").Value = Session("User")

            Dim DA1 As New SqlDataAdapter(cmd1)
            Dim DS1 As New DataSet
            DA1.Fill(DS1)

            projectList.DataSource = DS1
            projectList.DataTextField = "projectname"
            projectList.DataValueField = "projectname"

            If DS1.Tables.Count > 0 Then
                projectList.DataBind()
            End If

            'if a project has already been selected (e.g. returning to main project page after adding a task) remember it in the drop-down
            If Session("Project") IsNot Nothing Then
                    projectList.SelectedValue = Session("Project")
                End If

            End If
            'populating Project grid

            PopulateGrid()


    End Sub


    Public Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        ' hide the first column (used for ordering)
        e.Row.Cells(0).Visible = False
        e.Row.Cells(1).Visible = False
        e.Row.Cells(2).Visible = False
        e.Row.Cells(5).Visible = False


    End Sub

    Public Sub PopulateGrid()
        'populating Project grid
        Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("AdventureWorksDW_WroxSSRS2012ConnectionString").ConnectionString
        Dim cnn = New SqlConnection(ConnectionString)
        cnn.Open()

        Dim cmd2 As New SqlCommand("Declare @cols varchar(max) Declare @sql nvarchar(Max) Select @cols = STUFF((Select distinct ',' + QUOTENAME(cast(weekStartingSunday as varchar) ) from project_tasks For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)')  ,1,1,'') Set @sql = 'SELECT * FROM (select distinct ID, mark_complete, taskdate, cast(weekStartingSunday as varchar) as taskVar, username, projectname, projectstrand, task, theCount from project_tasks) p pivot (count(theCount) For taskvar in ('+@cols+')) pvt where task Is Not null and ProjectName = @proj order by taskdate, projectstrand' exec sp_executesql @sql, N'@proj varchar(max)', @proj = @project", cnn)

        cmd2.Parameters.Add("@project", SqlDbType.VarChar)
        ' if a project has alerady been selected, then remember it (e.g. when coming back to the main project page from updating a task)

        If Session("Project") IsNot Nothing Then
            cmd2.Parameters("@project").Value = Session("Project")
        Else
            cmd2.Parameters("@project").Value = projectList.Text
        End If

        'session variable to be used on new task /delete tasks pages
        Session("Project") = projectList.Text

        Dim DA As New SqlDataAdapter(cmd2)
        '("Declare @cols varchar(max) Declare @sql nvarchar(Max) Select @cols = STUFF((Select distinct ',' + QUOTENAME(cast(weekStartingSunday as varchar) ) from project_tasks For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)')  ,1,1,'') Set @sql = 'SELECT * FROM (select distinct cast(weekStartingSunday as varchar) as taskVar, username, projectstrand, task, theCount from project_tasks) p pivot (count(theCount) For taskvar in ('+@cols+')) pvt where task Is Not null order by projectstrand' exec sp_executesql @sql", cnn)

        Dim DS As New DataSet
        DA.Fill(DS)

        Grid.DataSource = DS

        If DS.Tables.Count > 0 Then

            Grid.DataBind()

            For Each row In Grid.Rows

                Dim i As Integer = 7
                Dim c As Integer = Grid.Rows(0).Cells.Count
                Dim LinkButton1 As New LinkButton

                'turn task name into a link button
                LinkButton1.Text = row.Cells(6).Text
                LinkButton1.ID = "LinkButton"
                row.Cells(6).Controls.Add(LinkButton1)
                AddHandler LinkButton1.Click, AddressOf LinkButton1_Click

                'turn the cell in the grid-view red if the count is populated in the query that creates the grid.  ie if there is  a task for the date in question

                Do While i < c

                    If row.Cells(i).text = 1 Then
                        row.cells(i).backcolor = System.Drawing.ColorTranslator.FromHtml("#ff1493")
                        'System.Drawing.Color.Red
                        If row.cells(1).text = 1 Then
                            row.cells(i).backcolor = System.Drawing.Color.Gray
                        End If
                    End If

                    row.Cells(i).text = " "

                    i = i + 1

                Loop
            Next

        End If

        cnn.Close()

    End Sub

    Public Sub projectChange(sender As Object, e As EventArgs)
        ' populating project grid when a new item is selected from the drop-down list
        PopulateGrid()

    End Sub

    Public Sub LinkButton1_Click(sender As Object, e As EventArgs)
        'Get the button that raised the event
        'Dim btn As Button = CType(sender, Button)

        'Get the row that contains this button
        Dim clickedRow As GridViewRow = TryCast(DirectCast(sender, LinkButton).NamingContainer, GridViewRow)
        Dim TaskID = CInt(clickedRow.Cells(0).Text)
        'task id is used in the mark complete screen
        Session("TaskID") = TaskID
        Response.Redirect("Markcomplete.aspx")


    End Sub

End Class
