Imports System.Data
Imports System.Data.SqlClient
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Public ReadOnly Property currentUser() As String
        Get
            Return Session("User")
        End Get
    End Property




End Class

