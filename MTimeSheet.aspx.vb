#Region "Imports"

Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports System.IO
Imports EeekSoft.Web
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts

#End Region

Partial Class MTimeSheet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strIP As String = Request.UserHostAddress

        If Mid(strIP, 1, 10) = "192.168.12" Then
            'Response.Redirect("http://jeddah.saudi-ericsson.com/timesheet/sestsmanager.aspx")
            Response.Redirect("http://inside.saudi-ericsson.com/timesheet/sestsmanager.aspx")
        Else
            If Mid(strIP, 1, 10) = "192.168.13" Then
                Response.Redirect("http://dammam.saudi-ericsson.com/timesheet/sestsmanager.aspx")
            Else
                Response.Redirect("http://inside.saudi-ericsson.com/timesheet/sestsmanager.aspx")
            End If
        End If

    End Sub

End Class
