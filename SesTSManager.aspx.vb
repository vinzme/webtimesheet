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

Partial Class SesTSManager
    Inherits System.Web.UI.Page

    Dim pubDomain As String
    Dim pubUser As String
    Dim pubServerDate As Date
    Dim pubConnStr As String
    Dim pubCLevel As Integer
    Dim pubDestin As String
    Dim pubDraftNotEmpty As Boolean = False
    Dim pubTimeSheetRef As String
    Dim pubTimeSheetNotes As Boolean = False
    Dim pubTimeSheetNotesAdded As Boolean = False
    Dim pubTxtNotes As String
    Dim pubTxtLog As String
    Dim pubRlPeriod As String
    Dim pubEmpNo As String
    Dim pubSql1 As String
    Dim pubSql2 As String
    Dim pubGroup As String
    Dim pubTSGroup As String
    Dim pubTSGRoupCount As Integer

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(User.Identity.Name, "\")
        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubDomain = UCase(sDomain)

        'pubUser = UCase(sUserId)
        pubUser = "STEFAN.ULMSTEDT"


        If pubUser.Trim = "ALVIN.DELEON" Then
            pubUser = "STEFAN.ULMSTEDT"
        End If

        'domain checking
        If pubDomain <> "SAUDI-ERICSSON" Then
            Response.Redirect("MgtUnauthorized.aspx")
        End If

        Label1.Text = Mid(pubUser.Trim, 1, InStr(1, pubUser.Trim, ".") - 1) & " " & Mid(pubUser.Trim, InStr(1, pubUser.Trim, ".") + 1, Len(pubUser.Trim) - InStr(1, pubUser.Trim, "."))

        MultiView1.ActiveViewIndex = 0

        CheckDestination()

        CheckManagerGroup()

        'check from sir_rlusers
        GetServerDateTime()

        If Page.IsPostBack = False Then

            CheckGroup()

            CheckCLevel()

            CheckPubUser()

            DeleteToTempTSManager()

            If pubUser.Trim = "NAEIM.NOUFAL" Then
                DeleteToTempTSManagerDam()
                DeleteToTempTSManagerJed()
            End If

            InsertIntoTSManager()

            If pubUser.Trim = "NAEIM.NOUFAL" Then
                InsertIntoTSManagerJed()
                InsertIntoTSManagerDam()
                CheckBranchConnection()
            End If

            CheckDraftIfEmpty()

            ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
            ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")

            ImageButton5.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
            ImageButton5.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")

            ImageButton1.ImageUrl = "~/tsimages/menubar1_sel.gif"

            HideDetailsLogControls()
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        ImageButton1.ImageUrl = "~/tsimages/menubar1_sel.gif"
        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_sel.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1_sel.gif'")

        MultiView1.ActiveViewIndex = 0

        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")
        ImageButton3.ImageUrl = "~/tsimages/menubar2.gif"

        ImageButton5.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
        ImageButton5.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")
        ImageButton5.ImageUrl = "~/tsimages/menubar4.gif"

    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click

        ImageButton3.ImageUrl = "~/tsimages/menubar2_sel.gif"
        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_sel.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar2_sel.gif'")

        MultiView1.ActiveViewIndex = 1

        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_on.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1.gif'")
        ImageButton1.ImageUrl = "~/tsimages/menubar1.gif"

        ImageButton5.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
        ImageButton5.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")
        ImageButton5.ImageUrl = "~/tsimages/menubar4.gif"

    End Sub

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click

        ImageButton5.ImageUrl = "~/tsimages/menubar4_sel.gif"
        ImageButton5.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_sel.gif'")
        ImageButton5.Attributes.Add("onmouseout", "this.src='tsimages/menubar4_sel.gif'")

        MultiView1.ActiveViewIndex = 2

        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_on.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1.gif'")
        ImageButton1.ImageUrl = "~/tsimages/menubar1.gif"

        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")
        ImageButton3.ImageUrl = "~/tsimages/menubar2.gif"

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Label4.Text = Mid(DropDownList1.SelectedValue.Trim, 1, 3)
        pubGroup = Label4.Text

        'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
        'If Label4.Text.Trim = "CGM" Then
        'Label24.Text = "A"
        'pubDestin = "A"
        'Else
        'Label24.Text = "M"
        'pubDestin = "M"
        'End If
        'End If

        UpdateGroup()
        Select Case Mid(Label4.Text.Trim, 3, 1)
            Case "J"
                pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                'GridView1.DataSourceID = "ObjectDataSource7"
                'GridView2.DataSourceID = "ObjectDataSource8"
                'GridView3.DataSourceID = "ObjectDataSource9"
            Case "D"
                pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                'GridView1.DataSourceID = "ObjectDataSource4"
                'GridView2.DataSourceID = "ObjectDataSource5"
                'GridView3.DataSourceID = "ObjectDataSource6"
            Case Else
                pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                'GridView1.DataSourceID = "ObjectDataSource1"
                'GridView2.DataSourceID = "ObjectDataSource2"
                'GridView3.DataSourceID = "ObjectDataSource3"
        End Select

        If pubUser.Trim = "NAEIM.NOUFAL" Then
            Select Case Mid(Label4.Text.Trim, 3, 1)
                Case "J"
                    pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                    GridView1.DataSourceID = "ObjectDataSource7"
                    GridView2.DataSourceID = "ObjectDataSource8"
                    GridView3.DataSourceID = "ObjectDataSource9"
                Case "D"
                    pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                    GridView1.DataSourceID = "ObjectDataSource4"
                    GridView2.DataSourceID = "ObjectDataSource5"
                    GridView3.DataSourceID = "ObjectDataSource6"
                Case Else
                    pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                    GridView1.DataSourceID = "ObjectDataSource1"
                    GridView2.DataSourceID = "ObjectDataSource2"
                    GridView3.DataSourceID = "ObjectDataSource3"
            End Select
        End If

        CheckCLevel()
        CheckDraftIfEmpty()
        HideDetailsLogControls()
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand

        Select Case e.CommandName
            Case "Select"
                Dim rowindex As Integer = CInt(e.CommandArgument)

                Label11.Text = GridView1.Rows(rowindex).Cells(3).Text

                'display details
                GetHeaderInfo()
                Label10.Text = Label11.Text
                Label12.Text = GridView1.Rows(rowindex).Cells(7).Text
                Label13.Text = GridView1.Rows(rowindex).Cells(10).Text
                Label19.Text = GridView1.Rows(rowindex).Cells(5).Text
                Label20.Text = GridView1.Rows(rowindex).Cells(8).Text
                Label21.Text = GridView1.Rows(rowindex).Cells(9).Text

                GridView2.DataBind()

                UnHideDetailsLogControls()
                Label23.Visible = False
                GridView3.Visible = False

            Case "Edit"
                HideDetailsLogControls()

            Case "Log"
                Dim rowindex As Integer = CInt(e.CommandArgument)

                Label11.Text = GridView1.Rows(rowindex).Cells(3).Text
                HideDetailsLogControls()

                Label23.Visible = True
                GridView3.Visible = True
                GridView3.DataBind()

                GridView2.Visible = False

            Case "Update"
                Dim rowindex As Integer = CInt(e.CommandArgument)
                Dim row As GridViewRow = GridView1.Rows(rowindex)

                'reference
                pubTimeSheetRef = GridView1.Rows(rowindex).Cells(3).Text
                pubRlPeriod = Mid(GridView1.Rows(rowindex).Cells(10).Text.Trim, 1, 5)
                pubEmpNo = GridView1.Rows(rowindex).Cells(4).Text.Trim

                CheckForNoteFlag()

                'Notes
                Dim txtNotes As TextBox = DirectCast(row.FindControl("TextBox1"), TextBox)

                If txtNotes.Text.Trim = "" Then
                    UpdateNoteFlag2()
                    DeleteTimeSheetNotes()
                Else
                    pubTxtNotes = txtNotes.Text.Trim
                    UpdateNoteFlag1()
                    If pubTimeSheetNotes = True Then
                        UpdateTimeSheetNotes()
                    Else
                        InsertTimeSheetNotes()
                    End If
                End If

                pubTxtLog = "Notes added, deleted or updated"
                InsertTimeSheetLog()

        End Select

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        CheckedAllItems()
        HideDetailsLogControls()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        UnCheckAllItems()
        HideDetailsLogControls()
    End Sub

#End Region

#Region "Procedures and Functions"

    Private Sub CheckDestination()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
            'sSql = "SELECT Timesheet.scheme.sir_rluserstatus.Status as destin FROM Timesheet.scheme.sir_rluserstatus WHERE Timesheet.scheme.sir_rluserstatus.UserId='" & _
            '        pubUser.Trim & "' and Status = '" & Label24.Text.Trim & "'"
            'Else
            sSql = "SELECT Timesheet.scheme.sir_rlusers.destin FROM Timesheet.scheme.sir_rlusers WHERE Timesheet.scheme.sir_rlusers.userid='" & _
                    pubUser.Trim & "'"
            'End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubDestin = mReader("destin")
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetServerDateTime()

        Dim ConnStr As String
        Dim sSql As String

        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "select getdate() as logdate from Timesheet.scheme.sir_rlusers where upper(userid) = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then

                While mReader.Read()

                    pubServerDate = mReader("logdate")
                    Label5.Text = pubUser.Trim
                    Label3.Text = "Today : " & Format(pubServerDate, "dd MMM yyyy")

                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckGroup()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            Select Case pubDestin.Trim
                Case "S"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Supervisor) = '" & pubUser.Trim & "' order by cgroup"
                Case "M"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Manager) = '" & pubUser.Trim & "' order by cgroup"
                Case "A"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & pubUser.Trim & "' order by cgroup"
            End Select

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    mcounter = mcounter + 1
                    If mcounter = 1 Then
                        Label4.Text = mReader("cgroup")
                        pubGroup = mReader("cgroup")
                        UpdateGroup()
                    End If
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckBranchConnection()

        Select Case Mid(Label4.Text.Trim, 3, 1)
            Case "J"
                pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                GridView1.DataSourceID = "ObjectDataSource7"
                GridView2.DataSourceID = "ObjectDataSource8"
                GridView3.DataSourceID = "ObjectDataSource9"
            Case "D"
                pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                GridView1.DataSourceID = "ObjectDataSource4"
                GridView2.DataSourceID = "ObjectDataSource5"
                GridView3.DataSourceID = "ObjectDataSource6"
            Case Else
                pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                GridView1.DataSourceID = "ObjectDataSource1"
                GridView2.DataSourceID = "ObjectDataSource2"
                GridView3.DataSourceID = "ObjectDataSource3"
        End Select

    End Sub

    Private Sub CheckPubUser()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
            'sSql = "Select Ugroup+' - '+GrpDesc as tsgroup, clevel from Timesheet.scheme.sir_rluserstatus where UserId = '" & _
            '        pubUser.Trim & "' group by Ugroup+' - '+GrpDesc, clevel order by tsgroup"

            'sSql = "Select Ugroup+' - '+GrpDesc as tsgroup, clevel from Timesheet.scheme.sir_rluserstatus where UserId = '" & _
            '        pubUser.Trim & "' group by Ugroup+' - '+GrpDesc, clevel order by tsgroup"
            'sSql = "Select Ugroup+' - '+GrpDesc as tsgroup, clevel from Timesheet.scheme.sir_rluserstatus where UserId = '" & _
            '       pubUser.Trim & "' group by Ugroup+' - '+GrpDesc, clevel order by tsgroup"

            'sSql = "Select cgroup+' - '+cdesc as tsgroup, clevel from Timesheet.scheme.sir_rlgroup where rtrim(Manager) = '" & _
            '       pubUser.Trim & "' group by cgroup+' - '+cdesc, clevel order by tsgroup"
            'Else
            Select Case pubDestin.Trim
                Case "S"
                    sSql = "Select cgroup+' - '+cdesc as tsgroup, clevel from Timesheet.scheme.sir_rlgroup where rtrim(Supervisor) = '" & _
                            pubUser.Trim & "' group by cgroup+' - '+cdesc, clevel order by tsgroup"
                Case "M"
                    sSql = "Select cgroup+' - '+cdesc as tsgroup, clevel from Timesheet.scheme.sir_rlgroup where rtrim(Manager) = '" & _
                            pubUser.Trim & "' group by cgroup+' - '+cdesc, clevel order by tsgroup"
                Case "A"
                    sSql = "Select cgroup+' - '+cdesc as tsgroup, clevel from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & _
                            pubUser.Trim & "' group by cgroup+' - '+cdesc, clevel order by tsgroup"
            End Select
            'End If

            DropDownList1.Items.Clear()

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubTSGroup = Mid(Trim(mReader("tsgroup")), 1, 3)

                    If pubUser.Trim = "NAEIM.NOUFAL" Then
                        Select Case Mid(pubTSGroup, 3, 1)
                            Case "J"
                                pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource7"
                                GridView2.DataSourceID = "ObjectDataSource8"
                                GridView3.DataSourceID = "ObjectDataSource9"
                            Case "D"
                                pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource4"
                                GridView2.DataSourceID = "ObjectDataSource5"
                                GridView3.DataSourceID = "ObjectDataSource6"
                            Case Else
                                pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource1"
                                GridView2.DataSourceID = "ObjectDataSource2"
                                GridView3.DataSourceID = "ObjectDataSource3"
                        End Select
                    Else
                        Select Case Mid(Label4.Text.Trim, 3, 1)
                            Case "J"
                                pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource7"
                                GridView2.DataSourceID = "ObjectDataSource8"
                                GridView3.DataSourceID = "ObjectDataSource9"
                            Case "D"
                                pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource4"
                                GridView2.DataSourceID = "ObjectDataSource5"
                                GridView3.DataSourceID = "ObjectDataSource6"
                            Case Else
                                pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                                GridView1.DataSourceID = "ObjectDataSource1"
                                GridView2.DataSourceID = "ObjectDataSource2"
                                GridView3.DataSourceID = "ObjectDataSource3"
                        End Select
                    End If



                    CheckGroupCount()

                    If pubTSGRoupCount <> 0 Then
                        DropDownList1.Items.Add(Trim(mReader("tsgroup")) + "   : " & pubTSGRoupCount.ToString)
                    Else
                        DropDownList1.Items.Add(mReader("tsgroup"))
                    End If

                    mcounter = mcounter + 1
                    If mcounter = 1 Then
                        pubCLevel = mReader("clevel")
                        Label4.Text = Mid(mReader("tsgroup"), 1, 3)


                    End If
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckCLevel()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
            'pubDestin = Label24.Text.Trim
            'End If

            Select Case pubDestin.Trim
                Case "S"
                    sSql = "Select clevel from Timesheet.scheme.sir_rlgroup where cgroup = '" & Label4.Text.Trim & "' and rtrim(Supervisor) = '" & pubUser.Trim & "'"
                Case "M"
                    sSql = "Select clevel from Timesheet.scheme.sir_rlgroup where cgroup = '" & Label4.Text.Trim & "' and rtrim(Manager) = '" & pubUser.Trim & "'"
                Case "A"
                    sSql = "Select clevel from Timesheet.scheme.sir_rlgroup where cgroup = '" & Label4.Text.Trim & "' and rtrim(Director) = '" & pubUser.Trim & "'"
            End Select

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubCLevel = mReader("clevel")
                End While
            Else
                Response.Redirect("MgtUnauthorized.aspx")
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckDraftIfEmpty()

        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "SELECT timesheet_ref FROM Timesheet.scheme.and_tsmanager where userid='" & pubUser.Trim & "' and tsgroup='" & Label4.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                pubDraftNotEmpty = True
                UnHideButtons()
            Else
                pubDraftNotEmpty = False
                HideButtons()
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DeleteToTempTSManager()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.and_tsmanager where userid = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub DeleteToTempTSManagerDam()

        Dim strConnStr As String
        strConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.and_tsmanager where userid = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub DeleteToTempTSManagerJed()

        Dim strConnStr As String
        strConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.and_tsmanager where userid = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoTSManager()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
        ' InsertIntoTSManagerOstman1()
        '  InsertIntoTSManagerOstman2()
        '   InsertIntoTSManagerOstman3()
        'Else
        Select Case pubDestin.Trim
            Case "S"
                cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
                "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
                "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
                "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
                "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
                "where a.status = 'S' and a.ugroup in (Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Supervisor) = '" & pubUser.Trim & "')"
            Case "M"
                cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
                "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
                "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
                "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
                "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
                "where a.status = 'M' and a.ugroup in (Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Manager) = '" & pubUser.Trim & "')"
            Case "A"
                cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
                "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
                "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
                "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
                "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
                "where a.status = 'A' and a.ugroup in (Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & pubUser.Trim & "')"
        End Select

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

        'End If

    End Sub

    Private Sub InsertIntoTSManagerDam()

        Dim strConnStr As String
        strConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
                    "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
                    "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
                    "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
                    "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
                    "where a.status = 'A' and a.ugroup in (Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & pubUser.Trim & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoTSManagerJed()

        Dim strConnStr As String
        strConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
                    "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
                    "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
                    "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
                    "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
                    "where a.status = 'A' and a.ugroup in (Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & pubUser.Trim & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoTSManagerOstman1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
        "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
        "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
        "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
        "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
        "where a.status = 'M' and a.ugroup = 'CGG'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoTSManagerOstman2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
        "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
        "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
        "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
        "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
        "where a.status = 'A' and a.ugroup = 'CGM'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertIntoTSManagerOstman3()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager " & _
        "Select a.timesheet_ref, a.resource_code, a.description as resource_name, a.cost_centre, a.entry_date, " & _
        "a.regular_unit, a.ot_unit, rtrim(a.rl_period)+' - '+substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date)))+' to '+" & _
        "substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) as week_period, c.notes as tsnotes, '" & pubUser.Trim & "' as userid, a.ugroup " & _
        "from Timesheet.scheme.sir_rldraft a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period left join Timesheet.scheme.sir_rlnotes c on a.timesheet_ref = c.timesheet_ref " & _
        "where a.status = 'M' and a.ugroup = 'CGP'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckedAllItems()

        Dim dr As GridViewRow
        Dim gIndex As Integer = -1
        For Each dr In GridView1.Rows

            If gIndex = -1 Then
                gIndex = 0
            End If

            Dim RowCheckBox As CheckBox = CType(GridView1.Rows(gIndex).FindControl("CheckBox1"), CheckBox)

            RowCheckBox.Checked = True

            gIndex += 1

        Next

    End Sub

    Private Sub UnCheckAllItems()

        Dim dr As GridViewRow
        Dim gIndex As Integer = -1
        For Each dr In GridView1.Rows

            If gIndex = -1 Then
                gIndex = 0
            End If

            Dim RowCheckBox As CheckBox = CType(GridView1.Rows(gIndex).FindControl("CheckBox1"), CheckBox)

            RowCheckBox.Checked = False

            gIndex += 1

        Next

    End Sub

    Private Sub HideButtons()
        Button1.Visible = False
        Button2.Visible = False
        Button3.Visible = False
        Button4.Visible = False
    End Sub

    Private Sub UnHideButtons()
        Button1.Visible = True
        Button2.Visible = True
        Button3.Visible = True
        Button4.Visible = True
    End Sub

    Private Sub HideDetailsLogControls()
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
        Label10.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        Label14.Visible = False
        Label15.Visible = False
        Label16.Visible = False
        Label17.Visible = False
        Label18.Visible = False
        Label19.Visible = False
        Label20.Visible = False
        Label21.Visible = False
        Label22.Visible = False
        GridView2.Visible = False
        Label23.Visible = False
        GridView3.Visible = False
    End Sub

    Private Sub UnHideDetailsLogControls()
        Label6.Visible = True
        Label7.Visible = True
        Label8.Visible = True
        Label9.Visible = True
        Label10.Visible = True
        Label12.Visible = True
        Label13.Visible = True
        Label14.Visible = True
        Label15.Visible = True
        Label16.Visible = True
        Label17.Visible = True
        Label18.Visible = True
        Label19.Visible = True
        Label20.Visible = True
        Label21.Visible = True
        Label22.Visible = True
        GridView2.Visible = True
        Label23.Visible = True
        GridView3.Visible = True
    End Sub

    Private Sub GetHeaderInfo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select pm_period from Timesheet.scheme.sir_rlthm where timesheet_ref  = '" & Label11.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label14.Text = mReader("pm_period")
                    Label22.Text = "HRS"
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateNoteFlag1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set notflg = 'N' where timesheet_ref = '" & _
                pubTimeSheetRef.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateNoteFlag2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set notflg = '' where timesheet_ref = '" & _
                pubTimeSheetRef.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckForNoteFlag()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select notflg from Timesheet.scheme.sir_rldraft where timesheet_ref  = '" & pubTimeSheetRef.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If mReader("notflg") = "N" Then
                        pubTimeSheetNotes = True
                    Else
                        pubTimeSheetNotes = False
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckIfExistInNotesTable()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rlnotes where timesheet_ref  = '" & pubTimeSheetRef.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                pubTimeSheetNotesAdded = True
            Else
                pubTimeSheetNotesAdded = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DeleteTimeSheetNotes()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.sir_rlnotes where timesheet_ref = '" & _
                pubTimeSheetRef.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateTimeSheetNotes()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlnotes set notes = '" & pubTxtNotes.Trim & "' where timesheet_ref = '" & _
                pubTimeSheetRef.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertTimeSheetNotes()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.sir_rlnotes(timesheet_ref,notes,cuser,cdate,luser,ldate) " & _
                            "values('" & pubTimeSheetRef.Trim & "','" & pubTxtNotes.Trim & "','" & _
                            pubUser.Trim & "','" & Format(pubServerDate, "dd MMM yyyy hh:mm") & Right(pubServerDate.ToString.Trim, 2) & "','" & pubUser.Trim & _
                            "','" & Format(pubServerDate, "dd MMM yyyy hh:mm") & Right(pubServerDate.ToString.Trim, 2) & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Protected Sub InsertTimeSheetLog()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.sir_rlsyslog(userid,timesheet_ref,description,rl_period,resource_code,ldate,ltime) " & _
                            "values('" & pubUser.Trim & "','" & pubTimeSheetRef.Trim & "','" & pubTxtLog.Trim & "','" & _
                            pubRlPeriod.Trim & "','" & pubEmpNo.Trim & " ','" & Format(pubServerDate, "dd MMM yyyy hh:mm") & _
                            Right(pubServerDate.ToString.Trim, 2) & "','" & _
                            Format(pubServerDate, "dd MMM yyyy hh:mm") & Right(pubServerDate.ToString.Trim, 2) & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

#End Region

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        CheckCLevel()

        Dim dr As GridViewRow
        Dim gIndex As Integer = -1
        For Each dr In GridView1.Rows

            If gIndex = -1 Then
                gIndex = 0
            End If

            Dim RowCheckBox As CheckBox = CType(GridView1.Rows(gIndex).FindControl("CheckBox1"), CheckBox)

            pubTimeSheetRef = GridView1.Rows(gIndex).Cells(3).Text

            If RowCheckBox.Checked = True Then
                MoveTimeSheet()
                UpdateApprove1()
                UpdateApprove2()
                pubTxtLog = "Timesheet Approved"
                pubRlPeriod = " "
                pubEmpNo = " "
                InsertTimeSheetLog()
            End If

            gIndex += 1

        Next

        DeleteToTempTSManager()
        InsertIntoTSManager()
        CheckDraftIfEmpty()
        GridView1.DataBind()
        HideDetailsLogControls()

        pubTSGroup = Mid(DropDownList1.SelectedItem.Text.Trim, 1, 3)

        CheckGroupCount()

        If pubTSGRoupCount = 0 Then
            DropDownList1.Items(DropDownList1.SelectedIndex).Text = Mid(DropDownList1.SelectedItem.Text.Trim, 1, InStr(1, DropDownList1.SelectedItem.Text.Trim, ":") - 1)
        Else
            DropDownList1.Items(DropDownList1.SelectedIndex).Text = Mid(DropDownList1.SelectedItem.Text.Trim, 1, InStr(1, DropDownList1.SelectedItem.Text.Trim, ":") - 1) & _
            " : " & pubTSGRoupCount.ToString
        End If

        'DropDownList1.Items(DropDownList1.SelectedIndex).Text = DropDownList1.SelectedItem.Text.Trim + "- " & pubTSGRoupCount.ToString

    End Sub

    Private Sub MoveTimeSheet()

        Dim sSql As String
        sSql = ""
        Select Case pubDestin.Trim
            Case "S"
                Select Case pubCLevel
                    Case 7
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'F' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'F', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case 4
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'A' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'A', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case Else
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'M' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'M', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                End Select

            Case "M"
                Select Case pubCLevel
                    Case 2
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'A' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'A', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case 3
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'A' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'A', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case Else
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'F' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'F', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                End Select

            Case "A"
                pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'F' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'F', retflg = ' ' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
        End Select

    End Sub

    Private Sub ReturnTimeSheet()
        Dim sSql As String
        sSql = ""
        Select Case pubDestin.Trim
            Case "A"
                Select Case pubCLevel
                    Case 1
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'V' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'V', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case 4
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'S' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'S', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case Else
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'M' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'M', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                End Select

            Case "M"
                Select Case pubCLevel
                    Case 3
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'S' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'S', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case 5
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'S' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'S', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                    Case Else
                        pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'V' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                        pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'V', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                End Select
            Case Else
                pubSql1 = "update Timesheet.scheme.sir_rlthm set status = 'V' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
                pubSql2 = "update Timesheet.scheme.sir_rldraft set status = 'V', retflg = 'R' where timesheet_ref = '" & pubTimeSheetRef.Trim & "'"
        End Select
    End Sub

    Private Sub UpdateApprove1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = pubSql1

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateApprove2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = pubSql2

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckManagerGroup()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = "Select cgroup from Timesheet.scheme.and_tsmanager_group where userid = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Select Case Mid(mReader("cgroup"), 3, 1)
                        Case "J"
                            pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                            'GridView1.DataSourceID = "ObjectDataSource7"
                            'GridView2.DataSourceID = "ObjectDataSource8"
                            'GridView3.DataSourceID = "ObjectDataSource9"
                        Case "D"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                            'GridView1.DataSourceID = "ObjectDataSource4"
                            'GridView2.DataSourceID = "ObjectDataSource5"
                            'GridView3.DataSourceID = "ObjectDataSource6"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                            'GridView1.DataSourceID = "ObjectDataSource1"
                            'GridView2.DataSourceID = "ObjectDataSource2"
                            'GridView3.DataSourceID = "ObjectDataSource3"
                    End Select
                End While
            Else
                CheckManagerGroupNotExist()
                InsertGroup()
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckManagerGroupNotExist()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
            'pubDestin = Label24.Text
            'End If
            Select Case pubDestin.Trim
                Case "S"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Supervisor) = '" & pubUser.Trim & "' order by cgroup"
                Case "M"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Manager) = '" & pubUser.Trim & "' order by cgroup"
                Case "A"
                    sSql = "Select cgroup from Timesheet.scheme.sir_rlgroup where rtrim(Director) = '" & pubUser.Trim & "' order by cgroup"
            End Select

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    mcounter = mcounter + 1
                    If mcounter = 1 Then
                        pubGroup = mReader("cgroup")
                        Select Case Mid(mReader("cgroup"), 3, 1)
                            Case "J"
                                pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                                'GridView1.DataSourceID = "ObjectDataSource7"
                                'GridView2.DataSourceID = "ObjectDataSource8"
                                'GridView3.DataSourceID = "ObjectDataSource9"
                            Case "D"
                                pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                                'GridView1.DataSourceID = "ObjectDataSource4"
                                'GridView2.DataSourceID = "ObjectDataSource5"
                                'GridView3.DataSourceID = "ObjectDataSource6"
                            Case Else
                                pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                                'GridView1.DataSourceID = "ObjectDataSource1"
                                'GridView2.DataSourceID = "ObjectDataSource2"
                                'GridView3.DataSourceID = "ObjectDataSource3"
                        End Select
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertGroup()

        Dim strConnStr As String
        strConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_tsmanager_group(userid,cgroup) " & _
                            "values('" & pubUser.Trim & "','" & pubGroup & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateGroup()

        Dim strConnStr As String
        strConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.and_tsmanager_group set cgroup = '" & _
                            pubGroup.Trim & "' where userid = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Protected Sub Button4_Click1(ByVal sender As Object, ByVal e As System.EventArgs)

        CheckCLevel()

        Dim dr As GridViewRow
        Dim gIndex As Integer = -1
        For Each dr In GridView1.Rows

            If gIndex = -1 Then
                gIndex = 0
            End If

            Dim RowCheckBox As CheckBox = CType(GridView1.Rows(gIndex).FindControl("CheckBox1"), CheckBox)

            pubTimeSheetRef = GridView1.Rows(gIndex).Cells(3).Text

            If RowCheckBox.Checked = True Then
                ReturnTimeSheet()
                UpdateApprove1()
                UpdateApprove2()
                pubTxtLog = "Timesheet Returned"
                pubRlPeriod = " "
                pubEmpNo = " "
                InsertTimeSheetLog()
                CheckForNoteFlag()
                If pubTimeSheetNotes = False Then
                    pubTxtNotes = "Returned"

                    CheckIfExistInNotesTable()
                    If pubTimeSheetNotesAdded = False Then
                        InsertTimeSheetNotes()
                    End If
                End If
            End If

            gIndex += 1

        Next

        DeleteToTempTSManager()
        InsertIntoTSManager()
        CheckDraftIfEmpty()
        GridView1.DataBind()
        HideDetailsLogControls()

        pubTSGroup = Mid(DropDownList1.SelectedItem.Text.Trim, 1, 3)

        CheckGroupCount()

        If pubTSGRoupCount = 0 Then
            DropDownList1.Items(DropDownList1.SelectedIndex).Text = Mid(DropDownList1.SelectedItem.Text.Trim, 1, InStr(1, DropDownList1.SelectedItem.Text.Trim, ":") - 1)
        Else
            DropDownList1.Items(DropDownList1.SelectedIndex).Text = Mid(DropDownList1.SelectedItem.Text.Trim, 1, InStr(1, DropDownList1.SelectedItem.Text.Trim, ":") - 1) & _
            " : " & pubTSGRoupCount.ToString
        End If

    End Sub

    Private Sub CheckGroupCount()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mcounter As Integer = 0
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try
            sSql = ""
            'If pubUser.Trim = "STEFAN.ULMSTEDT" Then
            'If pubTSGroup.Trim = "CGG" Or pubTSGroup.Trim = "CGP" Then
            '    pubDestin = "M"
            'Else
            '   pubDestin = "A"
            'End If

            'End If
            Select Case pubDestin.Trim
                Case "S"
                    sSql = "Select count(ugroup) as groupcount from Timesheet.scheme.sir_rldraft where ugroup = '" & _
                             pubTSGroup.Trim & "' and status = 'S'"
                Case "M"
                    sSql = "Select count(ugroup) as groupcount from Timesheet.scheme.sir_rldraft where ugroup = '" & _
                             pubTSGroup.Trim & "' and status = 'M'"
                Case "A"
                    sSql = "Select count(ugroup) as groupcount from Timesheet.scheme.sir_rldraft where ugroup = '" & _
                             pubTSGroup.Trim & "' and status = 'A'"
            End Select

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubTSGRoupCount = mReader("groupcount")
                End While
            Else
                pubTSGRoupCount = 0
            End If

        Catch ex As Exception
            MySqlConn.Close()
            Response.Redirect("MgtUnauthorized.aspx")
        Finally
            MySqlConn.Close()
        End Try
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        If e.Row.Cells(5).Text.Trim = "OVERTIME" Then
            e.Row.Cells(0).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(1).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(2).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(3).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(4).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(5).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(6).BackColor = Drawing.Color.LemonChiffon
            e.Row.Cells(7).BackColor = Drawing.Color.LemonChiffon
        End If

    End Sub

End Class
