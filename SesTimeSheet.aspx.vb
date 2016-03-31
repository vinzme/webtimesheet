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

Partial Class SesTimeSheet
    Inherits System.Web.UI.Page

    Dim pubDomain As String
    Dim pubUser As String
    Dim pubServerDate As Date
    Dim pubEmpNo As String
    Dim pubConnStr As String
    Dim pubRlPeriod As String
    Dim pubTimeSheetRef As String
    Dim pubDraftPeriod As String
    Dim pubDraftStatus As String

    Dim pubResourceType As String
    Dim pubResGroup As String
    Dim pubFrequency As String
    Dim pubPmPeriod As String
    Dim pubRlUnit As String
    Dim pubLocation As String
    Dim pubCostCentre As String

    Dim pubDayShift As String
    Dim pubDayRowIndex As Integer
    Dim pubLineNo As String
    Dim pubRateCode As String
    Dim pubProjectCode As String
    Dim pubNoUnits As Double = 0

    Dim pubRateCodeSelection As String
    Dim pubProjectSelection As String

    Dim pubUnitRate As Double = 0

    Dim pubTravelUnits As Double = 0

    Dim pubRlPostCode As String

    Dim pubBranch As String

    Dim pubGridTotalStandard As Double = 0
    Dim pubGridTotalOvertime As Double = 0

    Dim pubGrid3TotalStandard As Double = 0
    Dim pubGrid3TotalOvertime As Double = 0
    Dim pubGrid3TotalTravel As Double = 0

    Dim pubDetailCounter As Integer = 0
    Dim pubDetailAnalysis10 As String
    Dim pubLastLineNo As Integer

    Dim pubButtonInsert As Integer = 0

    'Project variables
    Dim pubProjectValid As Boolean
    Dim pubProjectParent As String
    Dim pubProjectICode As String
    Dim pubProjectIAnal As String
    Dim pubProjectCc As String
    Dim pubProjectDiv As String
    Dim pubProjectType As String
    Dim pubProjectStatus As String
    Dim pubProjectSite As String
    Dim pubProjectCustomer As String

    Dim pubProjectNotExisting As Boolean = False

    Dim pubProjectGlCode As String

    Dim pubDetailErrMess As String
    Dim pubGridview1Status As Boolean = False

    Dim pubGridview1Notes As Boolean = False

    Dim pubGridView1Submit As Boolean = False

    Dim pubValidateWithError As Boolean = False
    Dim pubValidateWithBlank As Boolean = False

    Dim pubValidateNoCounterOne As Boolean = False

    Dim pubRamadanHaj As Boolean = False
    Dim pubMinNoHoursRamadanHaj As Integer = 0

    Dim pubThereIsSCA As Boolean = False
    Dim pubThereIsNotes As Boolean = False

    Dim pubvalMonth As Integer
    Dim pubStrmonth As String

    Dim pubLogDescription As String

    Dim pubCLevel As Integer

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sUser() As String = Split(User.Identity.Name, "\")
        Dim sDomain As String = sUser(0)
        Dim sUserId As String = sUser(1)

        pubDomain = UCase(sDomain)

        pubUser = UCase(sUserId)

        If pubUser.Trim = "IT.SUPPORT" Or pubUser.Trim = "NOLI.JARAPLASAN" Or _
            pubUser.Trim = "ALVIN.DELEON" Then
            pubUser = "AUDIE.TEJADA"
        End If

        'domain checking
        If pubDomain <> "SAUDI-ERICSSON" Then
            Response.Redirect("MgtUnauthorized.aspx")
        End If

        Label1.Text = Mid(pubUser.Trim, 1, InStr(1, pubUser.Trim, ".") - 1) & " " & Mid(pubUser.Trim, InStr(1, pubUser.Trim, ".") + 1, Len(pubUser.Trim) - InStr(1, pubUser.Trim, "."))

        CheckConnectionString()

        'check from sir_rlusers
        GetServerDateTime()

        CheckPubUser()

        CheckForBlankReference()

        DeleteToTempTSDraft()

        SelectToTSDraft()

        Label19.Visible = False

        Label10.Text = pubEmpNo.Trim

        pubProjectSelection = ""

        If Page.IsPostBack = False Then

            ImageButton2.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
            ImageButton2.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")

            ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar3_on.gif'")
            ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar3.gif'")

            ImageButton4.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
            ImageButton4.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")

            ImageButton1.ImageUrl = "~/tsimages/menubar1_sel.gif"

            Button1.Visible = False
            Button1.Enabled = False

            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False

            Panel1.Visible = False
            Panel2.Visible = False
            GridView3.Visible = False

            MultiView1.ActiveViewIndex = 0

        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        ImageButton1.ImageUrl = "~/tsimages/menubar1_sel.gif"
        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_sel.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1_sel.gif'")

        MultiView1.ActiveViewIndex = 0

        ImageButton2.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
        ImageButton2.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")
        ImageButton2.ImageUrl = "~/tsimages/menubar2.gif"

        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar3_on.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar3.gif'")
        ImageButton3.ImageUrl = "~/tsimages/menubar3.gif"

        ImageButton4.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
        ImageButton4.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")
        ImageButton4.ImageUrl = "~/tsimages/menubar4.gif"

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click

        ImageButton2.ImageUrl = "~/tsimages/menubar2_sel.gif"
        ImageButton2.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_sel.gif'")
        ImageButton2.Attributes.Add("onmouseout", "this.src='tsimages/menubar2_sel.gif'")

        MultiView1.ActiveViewIndex = 1

        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_on.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1.gif'")
        ImageButton1.ImageUrl = "~/tsimages/menubar1.gif"

        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar3_on.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar3.gif'")
        ImageButton3.ImageUrl = "~/tsimages/menubar3.gif"

        ImageButton4.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
        ImageButton4.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")
        ImageButton4.ImageUrl = "~/tsimages/menubar4.gif"

    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click

        ImageButton3.ImageUrl = "~/tsimages/menubar3_sel.gif"
        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar3_sel.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar3_sel.gif'")

        MultiView1.ActiveViewIndex = 2

        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_on.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1.gif'")
        ImageButton1.ImageUrl = "~/tsimages/menubar1.gif"

        ImageButton2.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
        ImageButton2.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")
        ImageButton2.ImageUrl = "~/tsimages/menubar2.gif"

        ImageButton4.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_on.gif'")
        ImageButton4.Attributes.Add("onmouseout", "this.src='tsimages/menubar4.gif'")
        ImageButton4.ImageUrl = "~/tsimages/menubar4.gif"

        Label23.Text = Label1.Text
        Label22.Text = pubEmpNo.Trim

        DropDownList4.Items.Clear()
        GetWeekHistoryForDropDownList()


    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click

        ImageButton4.ImageUrl = "~/tsimages/menubar4_sel.gif"
        ImageButton4.Attributes.Add("onmouseover", "this.src='tsimages/menubar4_sel.gif'")
        ImageButton4.Attributes.Add("onmouseout", "this.src='tsimages/menubar4_sel.gif'")

        MultiView1.ActiveViewIndex = 3

        ImageButton1.Attributes.Add("onmouseover", "this.src='tsimages/menubar1_on.gif'")
        ImageButton1.Attributes.Add("onmouseout", "this.src='tsimages/menubar1.gif'")
        ImageButton1.ImageUrl = "~/tsimages/menubar1.gif"

        ImageButton2.Attributes.Add("onmouseover", "this.src='tsimages/menubar2_on.gif'")
        ImageButton2.Attributes.Add("onmouseout", "this.src='tsimages/menubar2.gif'")
        ImageButton2.ImageUrl = "~/tsimages/menubar2.gif"

        ImageButton3.Attributes.Add("onmouseover", "this.src='tsimages/menubar3_on.gif'")
        ImageButton3.Attributes.Add("onmouseout", "this.src='tsimages/menubar3.gif'")
        ImageButton3.ImageUrl = "~/tsimages/menubar3.gif"

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        If Label20.Text.Trim <> "Select" Then
            Exit Sub
        End If

        'reference
        Label8.Text = GridView1.SelectedRow.Cells(4).Text

        'notes
        TextBox6.Text = GridView1.SelectedRow.Cells(8).Text

        If TextBox6.Text.Trim = "&nbsp;" Then
            TextBox6.Text = ""
        End If

        'reference
        Label17.Text = GridView1.SelectedRow.Cells(4).Text.Trim

        'status
        Label18.Text = GridView1.SelectedRow.Cells(3).Text.Trim

        If Label18.Text.Trim = "No Entry" Then
            Button2.Visible = False
        End If

        If Label18.Text.Trim = "Validated" Or Label18.Text.Trim = "Not Validated" Or _
                    Label18.Text.Trim = "Returned" Then
            Button3.Visible = True
        Else
            Button3.Visible = False
        End If

        If Label18.Text.Trim = "Validated" Then
            Button4.Visible = True
        Else
            Button4.Visible = False
        End If

        'week no.
        Label21.Text = GridView1.SelectedRow.Cells(1).Text.Trim

        pubTimeSheetRef = Label8.Text.Trim

        Label8.Visible = True
        TextBox6.Visible = True
        Label14.Visible = True
        Label9.Visible = True
        Label12.Visible = True
        Label16.Visible = True

        Label12.Text = "0"
        Label16.Text = "0"

        Button1.Visible = True
        Button1.Enabled = True
        Panel2.Visible = True

        GetHeaderInfo()

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If pubGridview1Status = True Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "rl_status").ToString) Then
                    If DataBinder.Eval(e.Row.DataItem, "rl_status").ToString.Trim = "No Entry" And _
                            DataBinder.Eval(e.Row.DataItem, "timesheet_ref").ToString.Trim = Label8.Text.Trim Then
                        If e.Row.Cells(3).Text.Trim = "Not Validated" Then
                            e.Row.Cells(3).Text = "No Entry"
                        End If
                    Else
                        If DataBinder.Eval(e.Row.DataItem, "rl_status").ToString.Trim = "Not Validated" And _
                            DataBinder.Eval(e.Row.DataItem, "timesheet_ref").ToString.Trim = Label8.Text.Trim Then
                            If e.Row.Cells(3).Text.Trim = "No Entry" Or e.Row.Cells(3).Text.Trim = "Validated" Then
                                e.Row.Cells(3).Text = "Not Validated"
                            End If
                        Else
                            If DataBinder.Eval(e.Row.DataItem, "rl_status").ToString.Trim = "Validated" And _
                                DataBinder.Eval(e.Row.DataItem, "timesheet_ref").ToString.Trim = Label8.Text.Trim Then
                                If e.Row.Cells(3).Text.Trim = "Not Validated" Then
                                    e.Row.Cells(3).Text = "Validated"
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If pubGridview1Notes = True Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "notes").ToString) Then
                    If e.Row.Cells(4).Text.Trim = Label8.Text.Trim Then
                        e.Row.Cells(8).Text = TextBox6.Text.Trim
                    End If
                End If
            End If
        End If

        If pubGridView1Submit = True Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If Mid(Label18.Text.Trim, 1, 4) = "With" And e.Row.Cells(4).Text.Trim = Label8.Text.Trim Then
                    e.Row.Cells(3).Text = Label18.Text.Trim
                End If
            End If
        End If

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        If Label18.Text.Trim = "Return to Supervisor" Or Label18.Text.Trim = "With Supervisor" Or _
                Label18.Text.Trim = "Return to Manager" Or Label18.Text.Trim = "With Manager" Or _
                Label18.Text.Trim = "Return to Director" Or Label18.Text.Trim = "With Director" Or _
                Label18.Text.Trim = "With Finance" Or Label18.Text.Trim = "Posted" Then
            e.Row.Cells(0).Visible = False
            Button1.Visible = False
            Button1.Enabled = False

            Button2.Visible = False
        Else
            If Label18.Text.Trim <> "No Entry" Then
                Button2.Visible = True
            End If
        End If

        If pubValidateWithError = True Then
            e.Row.Cells(7).Visible = True
        Else
            e.Row.Cells(7).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            ' add the backlogTotal to the running total variables
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "no_units")) Then
                If DataBinder.Eval(e.Row.DataItem, "rate_code").ToString.Trim = "STANDARD" Then
                    pubGridTotalStandard += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "no_units"))
                Else
                    pubGridTotalOvertime += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "no_units"))
                End If

            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Label12.Text = pubGridTotalStandard
            Label16.Text = pubGridTotalOvertime
        End If

    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        Select Case e.CommandName
            Case "Cancel"
                Label20.Text = "Select"
                'enable buttons
                'insert details
                Button1.Enabled = True
                'validate
                Button2.Enabled = True
                'notes
                Button3.Enabled = True

            Case "Edit"

                Label19.Visible = False

                Dim rowindex As Integer = CInt(e.CommandArgument)
                Dim row As GridViewRow = GridView2.Rows(rowindex)
                Dim lbl As Label = DirectCast(row.FindControl("Label1"), Label)

                'rate code
                Dim lblratecode As Label = DirectCast(row.FindControl("Label3"), Label)

                'project
                Dim lblprojectcode As Label = DirectCast(row.FindControl("Label2"), Label)

                pubDayRowIndex = rowindex

                pubDayShift = lbl.Text.Trim

                pubRateCodeSelection = lblratecode.Text.Trim

                pubProjectSelection = lblprojectcode.Text.Trim

                Label20.Text = "Edit"

                UpdateUserSelectedRow()

                'disable buttons
                'insert details
                Button1.Enabled = False
                'validate
                Button2.Enabled = False
                'notes
                Button3.Enabled = False
                'hide submit
                Button4.Visible = False

            Case "Update"
                Dim rowindex As Integer = CInt(e.CommandArgument)
                Dim row As GridViewRow = GridView2.Rows(rowindex)
                'date
                Dim lbl As DropDownList = DirectCast(row.FindControl("Dropdownlist1"), DropDownList)

                'project
                Dim txtproject As TextBox = DirectCast(row.FindControl("TextBox2"), TextBox)

                'no. of units    
                Dim txtnounits As TextBox = DirectCast(row.FindControl("TextBox4"), TextBox)

                'rate code
                Dim ratelbl As DropDownList = DirectCast(row.FindControl("Dropdownlist3"), DropDownList)

                'travel units
                Dim txtnotravel As TextBox = DirectCast(row.FindControl("TextBox5"), TextBox)

                pubDayRowIndex = rowindex
                pubDayShift = lbl.SelectedValue.Trim

                pubProjectCode = UCase(txtproject.Text.Trim)

                pubRateCode = ratelbl.SelectedValue.Trim

                pubNoUnits = Val(txtnounits.Text)

                pubTravelUnits = Val(txtnotravel.Text)

                'validation
                pubDetailErrMess = " "

                Label19.Text = "Message : "

                'day shift
                If pubDayShift.Trim = "" Then
                    Label19.Text = Label19.Text.Trim & "Date should not be empty. "
                    Label19.Visible = True
                    pubDetailErrMess = pubDetailErrMess.Trim & " " & "Date should not be empty. "
                    SaveDetailErrMess()
                End If

                'project
                If pubProjectCode.Trim = "" Then
                    Label19.Text = Label19.Text.Trim & "Project should not be empty. "
                    Label19.Visible = True
                    pubDetailErrMess = pubDetailErrMess.Trim & " " & "Project should not be empty. "
                    pubProjectCustomer = " "
                    pubProjectSite = " "
                    pubProjectCode = " "
                    pubProjectType = " "
                    pubResourceType = " "
                    pubLocation = " "
                    pubProjectGlCode = " "
                    pubRlPostCode = " "
                    pubNoUnits = 0
                    pubUnitRate = 0
                    SaveDetailErrMess()
                Else
                    'initialize project variables
                    pubProjectParent = ""
                    pubProjectICode = ""
                    pubProjectIAnal = ""
                    pubProjectCc = ""
                    pubProjectDiv = ""
                    pubProjectType = ""
                    pubProjectStatus = ""
                    pubProjectSite = ""
                    pubProjectCustomer = ""
                    pubResourceType = ""
                    pubLocation = ""
                    pubProjectGlCode = ""
                    pubRlPostCode = ""

                    pubProjectValid = False

                    Validate_Project()
                    If pubProjectNotExisting = False Then
                        Validate_Project2()
                    End If
                End If

                'no. of units
                If pubNoUnits = 0 Then
                    Label19.Text = Label19.Text.Trim & "Zero Total Worked Hours is not allowed. "
                    Label19.Visible = True
                    pubDetailErrMess = pubDetailErrMess.Trim & " " & "Zero Total Worked Hours is not allowed. "
                    SaveDetailErrMess()
                Else
                    If pubNoUnits > 24 Then
                        Label19.Text = Label19.Text.Trim & "Total Worked Hours should be less than or equal to 24. "
                        Label19.Visible = True
                        pubDetailErrMess = pubDetailErrMess.Trim & " " & "Total Worked Hours should be less than or equal to 24. "
                        SaveDetailErrMess()
                    Else
                        If pubNoUnits - Fix(pubNoUnits) > 0 Then
                            If pubNoUnits - Fix(pubNoUnits) <> 0.5 Then
                                Label19.Text = Label19.Text.Trim & "Number of units is not divisible by the units lowest factor. "
                                Label19.Visible = True
                                pubDetailErrMess = pubDetailErrMess.Trim & " " & "Number of units is not divisible by the units lowest factor. "
                                SaveDetailErrMess()
                            End If
                        End If
                    End If
                End If

                'integration code
                If pubProjectCode.Trim <> "" Then
                    GetInfoForRlthm()
                    Validate_Integration()
                End If

                'rl_post_code
                If pubProjectCode.Trim <> "" Then
                    GetRlPostCode()
                End If

                'travel units
                If pubTravelUnits > 0 Then
                    If pubTravelUnits - Fix(pubTravelUnits) <> 0 Then

                        If pubTravelUnits - Fix(pubTravelUnits) <> 0.5 Then
                            Label19.Text = Label19.Text.Trim & "Travel units is not divisible by the units lowest factor. "
                            Label19.Visible = True
                            pubDetailErrMess = pubDetailErrMess.Trim & " " & "Travel units is not divisible by the units lowest factor. "
                            SaveDetailErrMess()
                        End If
                    End If
                End If
                SaveDetailErrMess()

                'Check if detail is SCA or survey cost
                If pubProjectType.Trim = "SCA" Then
                    If Label19.Text.Trim = "Message :" Then
                        Label19.Text = "Reminder : Please specify the project details of the survey cost using the Add Notes. "
                    Else
                        Label19.Text = Label19.Text.Trim & " Reminder : Please specify the project details of the survey cost using the Add Notes. "
                    End If
                    Label19.Visible = True
                End If

                If pubProjectCode.Trim <> "" Then
                    SaveTimesheet_Details()
                End If

                Label20.Text = "Select"

                If Label18.Text.Trim = "Returned" Or Label18.Text.Trim = "Validated" Then
                    ChangeStatusToNotValidated()
                    ChangeRlStatusToNotValidated()
                    Label18.Text = "Not Validated"
                    pubGridview1Status = True
                    GridView1.DataBind()
                    Button4.Visible = False
                End If
                Button3.Visible = True

                'enable buttons
                'insert details
                Button1.Enabled = True
                'validate
                Button2.Enabled = True
                'notes
                Button3.Enabled = True

        End Select

    End Sub

    Protected Sub GridView2_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridView2.RowUpdated
        UpdateDayShift()
        UpdateDayShiftbyLineNo()
        CheckTimeSheetDuplicateDetail()
        'initialize row counter
        UpdateDetailRowCounter()
        InitializeDetailRowCounter2()
        RefreshDetailAnalysis10()
        CheckTSLineCounter()

        GridView1.DataBind()

    End Sub

    Protected Sub GridView2_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridView2.RowDeleted

        UpdateDetailRowCounter()
        InitializeDetailRowCounter()
        RefreshDetailAnalysis10()

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim row As GridViewRow = GridView2.Rows(pubDayRowIndex)

        Dim lbldropdownShift As DropDownList = DirectCast(row.FindControl("Dropdownlist1"), DropDownList)

        Dim lbldropdown As DropDownList = DirectCast(row.FindControl("Dropdownlist2"), DropDownList)

        Dim ratelbl As DropDownList = DirectCast(row.FindControl("Dropdownlist3"), DropDownList)
        pubRateCodeSelection = ratelbl.SelectedItem.Text

        Dim txtlbl As TextBox = DirectCast(row.FindControl("TextBox2"), TextBox)

        pubDayShift = lbldropdownShift.SelectedValue.Trim

        If Mid(lbldropdown.SelectedValue.Trim, 1, 6) <> "Select" Then
            txtlbl.Text = Mid(lbldropdown.SelectedValue.Trim, 1, InStr(1, lbldropdown.SelectedValue.Trim, "-") - 2)
            pubProjectSelection = Mid(lbldropdown.SelectedValue.Trim, 1, InStr(1, lbldropdown.SelectedValue.Trim, "-") - 2)
            UpdateTextBox2()
        End If

    End Sub

    Protected Sub ObjectDataSource2_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        System.Threading.Thread.Sleep(2000)
    End Sub

    Protected Sub ObjectDataSource2_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs)

    End Sub

    Protected Sub ObjectDataSource5_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        System.Threading.Thread.Sleep(2000)
    End Sub

    Protected Sub ObjectDataSource5_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs)

    End Sub

    Protected Sub ObjectDataSource8_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs)
        System.Threading.Thread.Sleep(2000)
    End Sub

    Protected Sub ObjectDataSource8_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs)

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        pubButtonInsert = pubButtonInsert + 1

        If pubButtonInsert = 1 Then

            GetLastLineNo()
            InsertNewDetail()
            'Refresh Gridview
            GridView2.DataBind()

        End If

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'validation
        Label19.Text = "Message : "

        'remove all blank rows
        CheckIfThereIsBlankRows()
        RemoveBlankRows()
        If pubValidateWithBlank = True Then
            UpdateDetailRowCounter()
            InitializeDetailRowCounter()
            RefreshDetailAnalysis10()
        End If

        'check if there is no counter one
        CheckIfThereIsNoCounterOne()
        If pubValidateNoCounterOne = True Then
            UpdateDetailRowCounterOne()
            InitializeDetailRowCounter()
            RefreshDetailAnalysis10()
        End If


        'check if vacation/haj/ramadan
        CheckIfRamadanHaj()

        'check if there is a day_shift which is more than 9 hours
        CheckNoUnitsPerDay()

        If pubRamadanHaj = False Then
            CheckLessNoUnitsPerDay()
        End If

        CheckMorethan24HoursPerDay()

        If pubRamadanHaj = False Then
            If Val(Label12.Text.Trim) > 45 Then
                Label19.Text = Label19.Text.Trim & " Regular Units or Hours is greater than 45. "
                Label19.Visible = True
            End If
        Else
            If Val(Label12.Text.Trim) < pubMinNoHoursRamadanHaj Then
                Label19.Text = Label19.Text.Trim & " Regular Units or Hours should have a minimum of " & _
                        pubMinNoHoursRamadanHaj.ToString.Trim & " hours. "
                Label19.Visible = True
            End If
        End If

        If pubRamadanHaj = False Then
            CheckLessThan5Days()
        Else
            If pubMinNoHoursRamadanHaj <> 0 Then
                CheckLessThan5Days()
            End If
        End If


        CheckIfThereIsSCA()

        If pubThereIsSCA = True Then
            If pubThereIsNotes = False Then
                Label19.Text = Label19.Text.Trim & " Please specify the project details of the survey cost. "
                Label19.Visible = True
            End If
        End If

        If Label19.Text.Trim = "Message :" Then
            'change status
            ChangeToValidatedStatus()
            ChangeRlStatusToValidated()

            Label18.Text = "Validated"
            Button4.Visible = True
            pubGridview1Status = True
            GridView1.DataBind()
        End If

        CheckIfThereIsErrorDetail()

        If pubValidateWithError = True Then
            ChangeStatusToNotValidated()
            ChangeRlStatusToNotValidated()
            Label18.Text = "Not Validated"
            pubGridview1Status = True
            Button4.Visible = False
            GridView1.DataBind()
            GridView2.DataBind()

            pubLogDescription = "TimeSheet Validated with Error"
        Else
            pubLogDescription = "TimeSheet Validated"
        End If

        pubTimeSheetRef = Label8.Text.Trim
        pubRlPeriod = Label21.Text.Trim

        UpdateTSDraftUnits()

        InsertTimeSheetLog()

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        pubGridview1Notes = True
        If TextBox6.Text.Trim = "" Then
            DeleteTimeSheetNotes()
            UpdateNoteFlag2()
            GridView1.DataBind()
        Else
            CheckNotesForSCA()

            If pubThereIsNotes = True Then
                UpdateTimeSheetNotes()
                GridView1.DataBind()
            Else
                InsertTimeSheetNotes()
                GridView1.DataBind()
            End If
            UpdateNoteFlag1()
        End If

        pubLogDescription = "Notes added, deleted or updated"
        pubTimeSheetRef = Label8.Text.Trim
        pubRlPeriod = Label21.Text.Trim

        InsertTimeSheetLog()

    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedItem.Text.Trim <> "" Then
            Panel1.Visible = True
            GridView3.Visible = True

            lblReference.Text = Right(DropDownList4.SelectedItem.Text.Trim, 8)
            lblPeriod.Text = Left(DropDownList4.SelectedItem.Text.Trim, 5)
            DisplayTimeSheetDetails()
        Else
            Panel1.Visible = False
            GridView3.Visible = False
        End If
    End Sub

    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If Not IsDBNull(DataBinder.Eval(e.Row.DataItem, "no_units")) Then
                If DataBinder.Eval(e.Row.DataItem, "rate_code").ToString.Trim = "STANDARD" Then
                    pubGrid3TotalStandard += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "no_units"))
                Else
                    pubGrid3TotalOvertime += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "no_units"))
                End If
                pubGrid3TotalTravel += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "travel_units"))
            End If
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "Total :"
            e.Row.Cells(1).ForeColor = Drawing.Color.White
            ' for the Footer, display the running totals
            e.Row.Cells(2).Text = (pubGrid3TotalStandard + pubGrid3TotalOvertime).ToString

            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).ForeColor = Drawing.Color.White

            e.Row.Cells(4).Text = pubGrid3TotalTravel.ToString
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(4).ForeColor = Drawing.Color.White

            lblRegular.Text = pubGrid3TotalStandard
            lblOvertime.Text = pubGrid3TotalOvertime

        End If
    End Sub

#End Region

#Region "Procedures and Functions"

    Private Sub GetServerDateTime()

        Dim ConnStr As String
        Dim sSql As String

        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "select getdate() as logdate, resid as EmpNo from Timesheet.scheme.sir_rlusers where upper(userid) = '" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then

                While mReader.Read()

                    pubServerDate = mReader("logdate")
                    Label2.Text = "Today : " & Format(pubServerDate, "dd MMM yyyy")
                    'Label2.Text = Format(pubServerDate, "dd MMM yyyy hh:mm") & Right(pubServerDate.ToString.Trim, 2)
                    pubEmpNo = mReader("EmpNo").ToString.Trim
                    Label4.Text = pubEmpNo.Trim
                    If pubEmpNo.Trim = "0" Then
                        Response.Redirect("MgtUnauthorized.aspx")
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

    Private Sub CheckPubUser()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select a.resource_code, a.cgroup, b.Supervisor, b.Manager, b.Director, b.clevel from " & _
                    "Timesheet.scheme.sir_rlrsm a inner join Timesheet.scheme.sir_rlgroup b on a.cgroup = b.cgroup where " & _
                    "a.resource_code = '" & pubEmpNo.Trim & "' and delflg <> 'D'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubCLevel = mReader("clevel")

                    Select Case mReader("clevel")
                        Case 1
                            Label3.Text = "Timesheet Approver : " & Trim(mReader("Director"))
                        Case 2
                            Label3.Text = "Timesheet Approver : " & Trim(mReader("Manager")) & _
                                            " - " & Trim(mReader("Director"))
                        Case 3
                            Label3.Text = "Timesheet Approver : " & mReader("Supervisor") & _
                                    " - " & Trim(mReader("Manager")) & " - " & Trim(mReader("Director"))
                        Case 4
                            Label3.Text = "Timesheet Approver : " & mReader("Supervisor") & _
                                    " - " & Trim(mReader("Director"))
                        Case 5
                            Label3.Text = "Timesheet Approver : " & mReader("Supervisor") & _
                                    " - " & Trim(mReader("Manager"))
                        Case 6
                            Label3.Text = "Timesheet Approver : " & Trim(mReader("Manager"))
                        Case 7
                            Label3.Text = "Timesheet Approver : " & Trim(mReader("Supervisor"))
                    End Select

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

    Private Sub CheckConnectionString()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
        'ConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            'sSql = "SELECT Timesheet.scheme.and_ts_connection.connection_string, Timesheet.scheme.sir_rlusers.selected_row, " & _
            '        "Timesheet.scheme.and_ts_connection.branch FROM " & _
            '        "Timesheet.scheme.sir_rlusers INNER JOIN Timesheet.scheme.and_ts_connection ON Timesheet.scheme.sir_rlusers.branch = " & _
            '        "Timesheet.scheme.and_ts_connection.branch WHERE Timesheet.scheme.sir_rlusers.userid='" & pubUser.Trim & "'"

            sSql = "Select Timesheet.scheme.sir_rlrsm.cgroup, Timesheet.scheme.sir_rlusers.selected_row from " & _
                    "Timesheet.scheme.sir_rlrsm INNER JOIN Timesheet.scheme.sir_rlusers " & _
                    "ON Timesheet.scheme.sir_rlrsm.resource_code = Timesheet.scheme.sir_rlusers.resid " & _
                    "WHERE Timesheet.scheme.sir_rlusers.userid='" & pubUser.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    Select Case Mid(mReader("cgroup"), 3, 1)
                        Case "J"
                            pubConnStr = "Data Source=SESISVJBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource4"
                            GridView2.DataSourceID = "ObjectDataSource5"
                            GridView3.DataSourceID = "ObjectDataSource6"
                            pubBranch = "2"
                        Case "D"
                            pubConnStr = "Data Source=SESMSVDBO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource7"
                            GridView2.DataSourceID = "ObjectDataSource8"
                            GridView3.DataSourceID = "ObjectDataSource9"
                            pubBranch = "3"
                        Case Else
                            pubConnStr = "Data Source=SESLSVRHO;User ID=scheme;Password=Er1c550n2"
                            GridView1.DataSourceID = "ObjectDataSource1"
                            GridView2.DataSourceID = "ObjectDataSource2"
                            GridView3.DataSourceID = "ObjectDataSource3"
                            pubBranch = "1"
                    End Select

                    pubDayRowIndex = mReader("selected_row")

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

    Private Sub CheckForBlankReference()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select rl_period, resource_code from Timesheet.scheme.sir_rldraft where resource_code = '" & _
                     pubEmpNo.Trim & "' and ltrim(timesheet_ref) = ''"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubRlPeriod = mReader("rl_period")
                    GetTimeSheetRef()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetUserBranch()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select rl_period, resource_code from Timesheet.scheme.sir_rldraft where resource_code = '" & _
                     pubEmpNo.Trim & "' and ltrim(timesheet_ref) = ''"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubRlPeriod = mReader("rl_period")
                    GetTimeSheetRef()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetTimeSheetRef()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mRefNo As Integer
        Dim i As Integer
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        pubTimeSheetRef = ""

        MySqlConn.Open()
        Try

            sSql = "Select keyval from Timesheet.scheme.sir_rlsyskey where keycode = 'RLLASTREF'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    mRefNo = CLng(mReader("keyval"))
                    mRefNo = mRefNo + 1

                    For i = 1 To 8 - Len(Str(mRefNo).Trim)
                        pubTimeSheetRef = pubTimeSheetRef + "0"
                    Next

                    pubTimeSheetRef = pubTimeSheetRef & CStr(mRefNo)

                    UpdateTimesheetRef()

                    UpdateTimesheetRefDraft()

                    'insert record in sir_rlthm
                    GetInfoForRlthm()
                    GetPmPeriod()
                    InsertToRlthm()

                    pubLogDescription = "Assigned Reference"

                    InsertTimeSheetLog()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateTimesheetRefDraft()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "UPDATE Timesheet.scheme.sir_rldraft SET timesheet_ref = '" & _
                pubTimeSheetRef & "' where rl_period = '" & pubRlPeriod & _
                "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateTimesheetRef()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "UPDATE Timesheet.scheme.sir_rlsyskey SET keyval = '" & _
                pubTimeSheetRef & "' where keycode = 'RLLASTREF'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub SelectToTSDraft()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        MySqlConn.Open()
        Try

            sSql = "Select rl_period, resource_code, status, retflg from Timesheet.scheme.sir_rldraft where resource_code = '" & _
                     pubEmpNo.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubDraftPeriod = mReader("rl_period")

                    Select Case mReader("status".Trim)
                        Case "N"
                            pubDraftStatus = "No Entry"
                        Case "V"
                            If mReader("retflg".Trim) = "R" Then
                                pubDraftStatus = "Returned"
                            Else
                                pubDraftStatus = "Validated"
                            End If
                        Case "S"
                            If mReader("retflg".Trim) = "R" Then
                                pubDraftStatus = "Return to Supervisor"
                            Else
                                pubDraftStatus = "With Supervisor"
                            End If
                        Case "M"
                            If mReader("retflg".Trim) = "R" Then
                                pubDraftStatus = "Return to Manager"
                            Else
                                pubDraftStatus = "With Manager"
                            End If
                        Case "A"
                            If mReader("retflg".Trim) = "R" Then
                                pubDraftStatus = "Return to Director"
                            Else
                                pubDraftStatus = "With Director"
                            End If
                        Case "F"
                            pubDraftStatus = "With Finance"
                        Case "P"
                            pubDraftStatus = "Posted"
                        Case Else
                            pubDraftStatus = "Not Validated"
                    End Select

                    InsertToTempTSDraft()

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DeleteToTempTSDraft()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.and_ts_draft where resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertToTempTSDraft()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.and_ts_draft(rl_period,resource_code,rl_status) " & _
                            "values('" & pubDraftPeriod.Trim & "','" & pubEmpNo.Trim & "','" & _
                            pubDraftStatus.Trim & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InsertToRlthm()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        GetServerDateTime()

        cmdUpdate.CommandText = "insert into Timesheet.scheme.sir_rlthm(timesheet_ref,resource_code,resource_type," & _
                            "res_group,entry_date,frequency,rl_period,pm_period,rl_unit,status,rec_analysis," & _
                            "location,cost_centre,department,analysis01,analysis02,analysis03," & _
                            "analysis04,analysis05,analysis06,analysis07,analysis08,analysis09," & _
                            "analysis10,userid,notflg, retflg) " & _
                            "values('" & pubTimeSheetRef.Trim & "','" & pubEmpNo.Trim & "','" & _
                            pubResourceType.Trim & "','" & pubResGroup.Trim & "','" & Mid(Label2.Text, 9, 11) & "','" & _
                            pubFrequency.Trim & "','" & pubRlPeriod.Trim & _
                            "','" & pubPmPeriod.Trim & "','" & pubRlUnit.Trim & "',' ',' ','" & pubLocation.Trim & _
                            "','" & pubCostCentre.Trim & "',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','" & _
                            pubUser.Trim & "','','')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub GetInfoForRlthm()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select resource_type, res_group, frequency, location, cost_centre from Timesheet.scheme.sir_rlrsm " & _
                        "where resource_code = '" & pubEmpNo.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubResourceType = mReader("resource_type")
                    pubResGroup = mReader("res_group")
                    pubFrequency = mReader("frequency")

                    pubRlUnit = "HRS"

                    pubLocation = mReader("location")
                    pubCostCentre = mReader("cost_centre")
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetPmPeriod()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()

        Try

            sSql = "Select pm_period from Timesheet.scheme.rlclm where period = '" & pubRlPeriod & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubPmPeriod = mReader("pm_period")

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetHeaderInfo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select pm_period from Timesheet.scheme.sir_rlthm where timesheet_ref  = '" & pubTimeSheetRef & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    Label9.Text = mReader("pm_period")
                    Label14.Text = "HRS"
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetDetailInfoStandard()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select sum(no_units) as no_units from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    pubTimeSheetRef & "' and rate_code = 'STANDARD'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("no_units")) Then
                        Label12.Text = mReader("no_units")
                    Else
                        Label12.Text = "0"
                    End If

                End While
            Else
                Label12.Text = "0"
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetDetailInfoOvertime()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select sum(no_units) as no_units from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    pubTimeSheetRef & "' and rate_code = 'OVERTIME'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("no_units")) Then
                        Label16.Text = mReader("no_units")
                    Else
                        Label16.Text = "0"
                    End If

                End While
            Else
                Label16.Text = "0"
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Public Function PopulateControls() As Data.DataSet

        Dim myConnection As SqlConnection = New SqlConnection(pubConnStr)
        Dim ad As SqlDataAdapter = New SqlDataAdapter("select distinct('" & pubDayShift.Trim & _
                "') as prompt from Timesheet.scheme.rlcpm union all " & _
                "Select prompt from Timesheet.scheme.rlcpm where period = '" & _
                GridView1.SelectedRow.Cells(1).Text.Trim & "' and prompt <> '" & pubDayShift.Trim & "'", myConnection)
        Dim ds As Data.DataSet = New Data.DataSet()
        ad.Fill(ds, "tblDates")
        Return ds

    End Function

    Public Function PopulateNonProject() As Data.DataSet

        Dim myConnection As SqlConnection = New SqlConnection(pubConnStr)
        Dim ad As SqlDataAdapter = New SqlDataAdapter("Select rtrim(project_code)+' -- ' + rtrim(project_type) as nonproject from Timesheet.scheme.and_non_project", myConnection)
        Dim ds As Data.DataSet = New Data.DataSet()
        ad.Fill(ds, "tblNonProject")
        Return ds

    End Function

    Public Function PopulateRateCode() As Data.DataSet

        Dim myConnection As SqlConnection = New SqlConnection(pubConnStr)
        Dim mfunctionSql As String
        If pubRateCodeSelection.Trim = "OVERTIME" Then
            mfunctionSql = "Select rate_code as coderate from Timesheet.scheme.and_ts_ratecode order by rate_code_index desc"
        Else
            mfunctionSql = "Select rate_code as coderate from Timesheet.scheme.and_ts_ratecode order by rate_code_index"
        End If

        Dim ad As SqlDataAdapter = New SqlDataAdapter(mfunctionSql, myConnection)
        Dim ds1 As Data.DataSet = New Data.DataSet()
        ad.Fill(ds1, "tblRateCode")
        Return ds1

    End Function

    Private Sub UpdateDayShift()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim i As Integer
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            i = 0
            sSql = "Select line_no from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    Label8.Text.Trim & "' order by line_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If i = pubDayRowIndex Then
                        pubLineNo = Trim(mReader("line_no"))
                    End If
                    i = i + 1
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateDayShiftbyLineNo()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set day_shift = '" & _
                                pubDayShift.Trim & "', rate_code = '" & pubRateCode.Trim & "', project = '" & pubProjectCode.Trim & "' where timesheet_ref = '" & Label8.Text.Trim & _
                                "' and line_no = '" & pubLineNo.Trim & "'"
        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateUserSelectedRow()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlusers set selected_row = '" & _
               pubDayRowIndex & "' where userid = '" & pubUser.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateDetailRowCounter()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set analysis10 = convert(numeric,[line_no]) where " & _
                "timesheet_ref = '" & Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateDetailRowCounterOne()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set analysis10 = convert(numeric,[line_no])-1 where " & _
                "timesheet_ref = '" & Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub InitializeDetailRowCounter()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            pubDetailCounter = 1
            sSql = "Select analysis10 from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    Label8.Text.Trim & "' order by line_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubDetailAnalysis10 = mReader("analysis10")

                    RefreshDetailCounter()

                    pubDetailCounter = pubDetailCounter + 1

                End While
            Else
                'if there is no record, change status to No Entry
                ChangeStatusToNoEntry()
                ChangeRlStatusToNoEntry()
                Label18.Text = "No Entry"
                pubGridview1Status = True
                GridView1.DataBind()
                Button2.Visible = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InitializeDetailRowCounter2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            pubDetailCounter = 1
            sSql = "Select analysis10 from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    Label8.Text.Trim & "' order by substring(day_shift,3,2)+substring(day_shift,1,2),line_no"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubDetailAnalysis10 = mReader("analysis10")

                    If pubDetailCounter = Val(pubDetailAnalysis10) Then
                        RefreshDetailCounter()
                    Else
                        RefreshDetailCounter2()
                    End If

                    pubDetailCounter = pubDetailCounter + 1
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub RefreshDetailCounter()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        If pubDetailCounter > 9 Then
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '00" & _
                    pubDetailCounter.ToString.Trim & "' where timesheet_ref = '" & Label8.Text.Trim & _
                            "' and analysis10 = '" & pubDetailAnalysis10.Trim & "'"
        Else
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '000" & _
                    pubDetailCounter.ToString.Trim & "' where timesheet_ref = '" & Label8.Text.Trim & _
                            "' and analysis10 = '" & pubDetailAnalysis10.Trim & "'"
        End If

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub RefreshDetailCounter2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        If pubDetailCounter > 9 Then
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '10" & _
                    pubDetailCounter.ToString.Trim & "' where timesheet_ref = '" & Label8.Text.Trim & _
                            "' and analysis10 = '" & pubDetailAnalysis10.Trim & "'"
        Else
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '100" & _
                    pubDetailCounter.ToString.Trim & "' where timesheet_ref = '" & Label8.Text.Trim & _
                            "' and analysis10 = '" & pubDetailAnalysis10.Trim & "'"
        End If

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub RefreshDetailAnalysis10()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set analysis10 = space(40) where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckTSLineCounter()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    Label8.Text.Trim & "' and convert(numeric,line_no) > 900"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                UpdateTSLineCounterThousandNine()
                UpdateTSLineCounterThousandTen()
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateTSLineCounterThousandNine()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '000'+" & _
                         "convert(char(1),(convert(numeric,line_no)-1000)) where timesheet_ref = '" & _
                        Label8.Text.Trim & "' and convert(numeric,line_no) > 1000 and convert(numeric,line_no) < 1010"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateTSLineCounterThousandTen()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set line_no = '00'+" & _
                         "convert(char(2),(convert(numeric,line_no)-1000)) where timesheet_ref = '" & _
                        Label8.Text.Trim & "' and convert(numeric,line_no) > 1009"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub GetLastLineNo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            pubDetailCounter = 1
            sSql = "Select max(line_no) as maxlineno from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                    Label8.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Not IsDBNull(mReader("maxlineno")) Then
                        pubLastLineNo = mReader("maxlineno") + 1
                    Else
                        pubLastLineNo = 1
                    End If

                End While
            Else
                pubLastLineNo = 1
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub InsertNewDetail()

        Dim strConnStr As String
        Dim mcounter As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        If pubLastLineNo > 9 Then
            mcounter = "00" & pubLastLineNo.ToString.Trim
        Else
            mcounter = "000" & pubLastLineNo.ToString.Trim
        End If

        cmdUpdate.CommandText = "insert into Timesheet.scheme.sir_rltdm(timesheet_ref,line_no,project,project_type,expense,resource_type,day_shift,rate_code,rec_analysis,location,cost_centre,department," & _
                                    "analysis01,analysis02,analysis03,analysis04,analysis05,analysis06,analysis07,analysis08,analysis09,analysis10," & _
                                    "chargeable_ind,detail_unit,pr_post_code,rl_post_code,no_units,rate,line_value,err_msg,travel_units,codes) " & _
                                "Select timesheet_ref, '" & mcounter & "' as line_no, '' as project, '' as project_type, '' as expense, resource_type,'' as day_shift," & _
                                "'STANDARD' as rate_code,'' as rec_analysis,location,cost_centre,'' as department, '' as analysis01, '' as analysis02," & _
                                "'' as analysis03,'' as analysis04,'' as analysis05,'' as analysis06,'' as analysis07,'' as analysis08,'' as analysis09,'' as analysis10," & _
                                "'n' as chargeable_ind,'HRS' as detail_unit,'' as pr_post_code,'' as rl_post_code,'0' as no_units,'0' as rate, '0' as line_value," & _
                                "'' as err_msg, '0' as travel_units, '' as codes FROM Timesheet.scheme.sir_rlthm where timesheet_ref = '" & Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub Validate_Project()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select project_type FROM Timesheet.scheme.projects where project_code = '" & pubProjectCode.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If Trim(mReader("project_type")) = "ASIN" Then
                        If Len(pubProjectCode.Trim) = 6 Then
                            pubProjectCode = pubProjectCode.Trim & "/I"
                            pubProjectNotExisting = False
                        End If
                    End If
                End While
            Else
                Label19.Text = Label19.Text.Trim & "Project Code is not existing. "
                Label19.Visible = True
                pubDetailErrMess = pubDetailErrMess.Trim & " " & "Project Code is not existing. "
                SaveDetailErrMess()
                pubProjectNotExisting = True
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Validate_Project2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim mbproj As Boolean

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select project_type, parent_project, post_status, project_status, cost_centre, " & _
           "integration_anal1, integration_code1, custname, sitename from Timesheet.scheme.projects where project_code = '" & _
           pubProjectCode.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    If Trim(mReader("post_status")) <> "O" Then
                        Label19.Text = Label19.Text.Trim & "Project is not Open, call Order Control. "
                        Label19.Visible = True
                        pubDetailErrMess = pubDetailErrMess.Trim & " " & "Project is not Open, call Order Control. "
                        SaveDetailErrMess()
                    End If

                    pubProjectParent = Trim(Left(mReader("parent_project"), 20))
                    pubProjectICode = Trim(mReader("integration_code1"))
                    pubProjectIAnal = Trim(mReader("integration_anal1"))
                    pubProjectCc = Trim(mReader("cost_centre"))
                    pubProjectDiv = Left(pubProjectCc, 2)
                    pubProjectType = Trim(mReader("project_type"))
                    pubProjectStatus = Trim(mReader("project_status"))
                    pubProjectSite = Left(Trim(mReader("sitename")), 40)
                    pubProjectCustomer = Left(Trim(mReader("custname")), 40)

                    mbproj = Valid_Project(pubProjectCc, pubProjectStatus, pubProjectICode, pubProjectIAnal)

                    If Not pubProjectValid Then
                        Label19.Text = Label19.Text.Trim & "Project Type " & pubProjectType.Trim & _
                            " " & pubProjectICode.Trim & " " & pubProjectIAnal.Trim & " " & pubProjectStatus.Trim & _
                            " is invalid. "
                        Label19.Visible = True
                        pubDetailErrMess = pubDetailErrMess.Trim & " " & "Project Type " & pubProjectType.Trim & _
                            " " & pubProjectICode.Trim & " " & pubProjectIAnal.Trim & " " & pubProjectStatus.Trim & _
                            " is invalid. "
                        SaveDetailErrMess()
                    End If

                End While
            Else
                pubProjectCustomer = " "
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Public Function Valid_Project(ByVal sCC As String, ByVal sStatus As String, ByVal sIcode As String, ByVal sIanly As String) As Boolean

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        Dim sSelect As String

        Valid_Project = False
        pubProjectGlCode = ""

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select proj_analysis, proj_status, proj_cost_centre, exp_group, " & _
                      "exp_code from Timesheet.scheme.prigm where integration_code = '" & pubProjectICode.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    sSelect = mReader("proj_analysis") & mReader("proj_status") & mReader("proj_cost_centre") & _
                                mReader("exp_group") & mReader("exp_code")
                    Select Case sSelect

                        Case "   1 "
                            Project_CaseOne()
                        Case "  1 2"
                            Project_CaseTwo()
                        Case " 21 2"
                            Project_CaseThree()
                        Case "1    "
                            Project_CaseFour()
                        Case "2 1  "
                            Project_CaseFive()
                        Case "221  "
                            Project_CaseSix()
                    End Select
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Function

    Private Sub Project_CaseOne()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = 'ABS'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseTwo()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectCc.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    Project_CaseTwo_2()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseTwo_2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = 'ABS-BURDEN'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = pubProjectGlCode.Trim & "-" & Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseThree()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectCc.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    Project_CaseThree_2()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseThree_2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectStatus.Trim & "' and item_code_2 = 'ABS-BURDEN'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = pubProjectGlCode.Trim & "-" & Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseFour()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectIAnal.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseFive()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectCc.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    Project_CaseFive_2()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseFive_2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectIAnal.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = pubProjectGlCode.Trim & "-" & Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseSix()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectCc.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = Trim(mReader("part_gl_code"))
                    Project_CaseSix_2()
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Project_CaseSix_2()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.pridm where integration_code = '" & pubProjectICode.Trim & _
                    "' and item_code_1 = '" & pubProjectIAnal.Trim & "' and item_code_2 = '" & _
                    pubProjectStatus.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubProjectGlCode = pubProjectGlCode.Trim & "-" & Trim(mReader("part_gl_code"))
                    pubProjectValid = True
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub Validate_Integration()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select rate_code01, rate_code02, rate01, rate02 from Timesheet.scheme.rlrrm where project_type = '" & _
                        pubProjectType.Trim & "' and resource_type = '" & pubResourceType.Trim & _
                        "' and location = '" & pubLocation.Trim & "' and code = 'STD'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If pubRateCode.Trim = "STANDARD" Then
                        If Trim(mReader("rate_code01")) = "" Then
                            Label19.Text = Label19.Text.Trim & " STANDARD rate not defined. "
                            Label19.Visible = True
                            pubDetailErrMess = pubDetailErrMess.Trim & " " & " STANDARD rate not defined. "
                            SaveDetailErrMess()
                        End If
                        pubUnitRate = mReader("rate01")
                    Else
                        If Trim(mReader("rate_code02")) = "" Then
                            Label19.Text = Label19.Text.Trim & " OVERTIME rate not defined. "
                            Label19.Visible = True
                            pubDetailErrMess = pubDetailErrMess.Trim & " " & " OVERTIME rate not defined. "
                            SaveDetailErrMess()
                        End If
                        pubUnitRate = mReader("rate02")
                    End If
                End While
            Else
                Label19.Text = Label19.Text.Trim & " Invalid Rate Integration, call Payroll. "
                Label19.Visible = True
                pubDetailErrMess = pubDetailErrMess.Trim & " " & " Invalid Rate Integration, call Payroll. "
                SaveDetailErrMess()
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub GetRlPostCode()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select part_gl_code from Timesheet.scheme.rlidm where item_code_1 = '" & pubCostCentre.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubRlPostCode = Trim(mReader("part_gl_code"))
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub SaveDetailErrMess()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand
        Dim strcounter As String
        Dim mcounter As Integer

        mcounter = pubDayRowIndex + 1
        If mcounter > 9 Then
            strcounter = "00" & mcounter.ToString.Trim
        Else
            strcounter = "000" & mcounter.ToString.Trim
        End If

        If pubDetailErrMess.Trim = "" Then
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set err_msg = ' ' " & _
                                        "where timesheet_ref = '" & Label8.Text.Trim & "' and line_no = '" & strcounter & "'"
        Else
            cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set err_msg = '" & pubDetailErrMess.Trim & _
                                        "' where timesheet_ref = '" & Label8.Text.Trim & "' and line_no = '" & strcounter & "'"
        End If

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateTextBox2()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand
        Dim strcounter As String
        Dim mcounter As Integer

        mcounter = pubDayRowIndex + 1
        If mcounter > 9 Then
            strcounter = "00" & mcounter.ToString.Trim
        Else
            strcounter = "000" & mcounter.ToString.Trim
        End If

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set project = '" & pubProjectSelection.Trim & _
                                        "' where timesheet_ref = '" & Label8.Text.Trim & "' and line_no = '" & strcounter & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub SaveTimesheet_Details()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand
        Dim strcounter As String
        Dim mcounter As Integer

        If pubProjectCustomer.Trim = "" Then
            pubProjectCustomer = Space(40)
        End If

        If pubProjectSite.Trim = "" Then
            pubProjectSite = Space(40)
        End If


        mcounter = pubDayRowIndex + 1
        If mcounter > 9 Then
            strcounter = "00" & mcounter.ToString.Trim
        Else
            strcounter = "000" & mcounter.ToString.Trim
        End If

        If InStr(1, pubProjectCustomer, "'") > 0 Then
            pubProjectCustomer = Mid(pubProjectCustomer, 1, InStr(1, pubProjectCustomer, "'") - 1) & _
                    "`" & Mid(pubProjectCustomer, InStr(1, pubProjectCustomer, "'") + 1, Len(pubProjectCustomer) - InStr(1, pubProjectCustomer, "'"))
        End If

        If InStr(1, pubProjectCustomer, "'") > 0 Then
            pubProjectCustomer = Mid(pubProjectCustomer, 1, InStr(1, pubProjectCustomer, "'") - 1) & _
                    "`" & Mid(pubProjectCustomer, InStr(1, pubProjectCustomer, "'") + 1, Len(pubProjectCustomer) - InStr(1, pubProjectCustomer, "'"))
        End If


        If InStr(1, pubProjectSite, "'") > 0 Then
            pubProjectSite = Mid(pubProjectSite, 1, InStr(1, pubProjectSite, "'") - 1) & _
                    "`" & Mid(pubProjectSite, InStr(1, pubProjectSite, "'") + 1, Len(pubProjectSite) - InStr(1, pubProjectSite, "'"))
        End If

        If InStr(1, pubProjectSite, "'") > 0 Then
            pubProjectSite = Mid(pubProjectSite, 1, InStr(1, pubProjectSite, "'") - 1) & _
                    "`" & Mid(pubProjectSite, InStr(1, pubProjectSite, "'") + 1, Len(pubProjectSite) - InStr(1, pubProjectSite, "'"))
        End If


        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rltdm set project = '" & pubProjectCode.Trim & _
                        "', project_type = '" & pubProjectType.Trim & "', expense = 'ABS-BURDEN', resource_type = '" & _
                        pubResourceType & "', rec_analysis = space(10), location = '" & pubLocation.Trim & _
                        "', cost_centre = '" & pubCostCentre.Trim & "', department = space(10), analysis01 = '" & _
                        pubProjectCustomer & "', analysis02 = '" & pubProjectSite & "', analysis03 = space(40), " & _
                        "analysis04 = space(40), analysis05 = space(40), analysis06 = space(40), analysis07 = space(40), " & _
                        "analysis08 = space(40), analysis09 = space(40), analysis10 = space(40), pr_post_code = '" & _
                        pubProjectGlCode.Trim & "', rl_post_code = '" & pubRlPostCode.Trim & "', rate = " & _
                        pubUnitRate & ", line_value = " & pubNoUnits * pubUnitRate & _
                        " where timesheet_ref = '" & Label8.Text.Trim & "' and line_no = '" & strcounter & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

        'changing the TimeSheet Status
        If strcounter.Trim = "0001" And Label18.Text.Trim = "No Entry" Then
            ChangeNoEntryStatus()
            ChangeRlStatusToNotValidated()
            Label18.Text = "Not Validated"
            pubGridview1Status = True
            GridView1.DataBind()
        End If

    End Sub

    Private Sub ChangeNoEntryStatus()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = '' where timesheet_ref = '" & _
                Label8.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeStatusToNoEntry()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'N' where timesheet_ref = '" & _
                Label8.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeRlStatusToNoEntry()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'No Entry' where rl_period = '" & _
                Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeRlStatusToNotValidated()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'Not Validated' where rl_period = '" & _
                Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()
    End Sub

    Private Sub CheckNoUnitsPerDay()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select day_shift, sum(no_units) from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' and rate_code = 'STANDARD' group by day_shift having sum(no_units) > 9"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    Label19.Text = Label19.Text.Trim & Trim(mReader("day_shift")) & " is more than 9 hours. "
                    Label19.Visible = True

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckIfRamadanHaj()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select keyval from Timesheet.scheme.sir_rlsyskey where keycode = 'RLTOTHRS' and keyitem = '" & Label21.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubMinNoHoursRamadanHaj = Val(Trim(mReader("keyval")))
                    pubRamadanHaj = True

                End While
            Else
                pubRamadanHaj = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckLessNoUnitsPerDay()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select day_shift, sum(no_units) from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' and rate_code = 'STANDARD' group by day_shift having sum(no_units) < 9"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    pubvalMonth = Val(Mid(Trim(mReader("day_shift")), 3, 2))
                    GetBuwanDescription()
                    If Format(CDate(Mid(Trim(mReader("day_shift")), 1, 2) + " " + pubStrmonth & " 20" & Mid(Trim(mReader("day_shift")), 5, 2)), "ddd") <> "Thu" And _
                        Format(CDate(Mid(Trim(mReader("day_shift")), 1, 2) + " " + pubStrmonth & " 20" & Mid(Trim(mReader("day_shift")), 5, 2)), "ddd") <> "Fri" Then

                        Label19.Text = Label19.Text.Trim & Trim(mReader("day_shift")) & " is less than 9 hours. "
                        Label19.Visible = True

                    End If

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckMorethan24HoursPerDay()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select day_shift, sum(no_units) from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' group by day_shift having sum(no_units) > 24"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    Label19.Text = Label19.Text.Trim & Trim(mReader("day_shift")) & " is more than 24 hours. "
                    Label19.Visible = True

                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckLessThan5Days()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)
        Dim mcount As Integer

        MySqlConn.Open()
        Try
            sSql = "Select day_shift from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' group by day_shift"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mcount = 0
            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()

                    mcount = mcount + 1

                End While
            End If

            If mcount < 5 Then
                Label19.Text = Label19.Text.Trim & " No. of Days is less than 5. "
                Label19.Visible = True
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub ChangeToValidatedStatus()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'V' where timesheet_ref = '" & _
                Label8.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeRlStatusToValidated()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'Validated' where rl_period = '" & _
                Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()
    End Sub

    Private Sub ChangeStatusToNotValidated()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = '', retflg = '' where timesheet_ref = '" & _
                Label8.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub CheckIfThereIsErrorDetail()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rltdm where err_msg <> '' and timesheet_ref = '" & _
                     Label8.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubValidateWithError = True
                    pubValidateWithBlank = True
                End While
            Else
                pubValidateWithBlank = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckIfThereIsBlankRows()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' and analysis09 ='' and day_shift =''"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubValidateWithError = True
                End While
            Else

            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckIfThereIsNoCounterOne()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try
            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                     Label8.Text.Trim & "' and line_no = '0001'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubValidateNoCounterOne = False
                End While
            Else
                pubValidateNoCounterOne = True
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub RemoveBlankRows()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.sir_rltdm where timesheet_ref = '" & _
                Label8.Text.Trim & "' and analysis09 ='' and day_shift =''"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub GetBuwanDescription()

        Select Case pubvalMonth
            Case 1
                pubStrmonth = "January"
            Case 2
                pubStrmonth = "February"
            Case 3
                pubStrmonth = "March"
            Case 4
                pubStrmonth = "April"
            Case 5
                pubStrmonth = "May"
            Case 6
                pubStrmonth = "June"
            Case 7
                pubStrmonth = "July"
            Case 8
                pubStrmonth = "August"
            Case 9
                pubStrmonth = "September"
            Case 10
                pubStrmonth = "October"
            Case 11
                pubStrmonth = "November"
            Case 12
                pubStrmonth = "December"
        End Select

    End Sub

    Private Sub GetWeekHistoryForDropDownList()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String

        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        'add blank for dropdownlist
        DropDownList4.Items.Add("   ")

        MySqlConn.Open()
        Try
            If pubBranch = "1" Then
                sSql = "Select a.rl_period, substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date))) " & _
                        "+ ' ' + convert(char(4),year(b.from_date))+' to '+substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) " & _
                        "+' ' + convert(char(4),year(b.to_date))+' ' as tsperiod, a.timesheet_ref " & _
                        "from Timesheet.scheme.sir_rlthm_history a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period where a.resource_code = '" & pubEmpNo.Trim & _
                        "' union all " & _
                        "Select a.rl_period, substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date))) " & _
                        "+ ' ' + convert(char(4),year(b.from_date))+' to '+substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) " & _
                        "+' ' + convert(char(4),year(b.to_date))+' ' as tsperiod, a.timesheet_ref " & _
                        "from Timesheet.scheme.sir_rlthm a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period where a.resource_code = '" & pubEmpNo.Trim & "'"
            Else
                sSql = "Select a.rl_period, substring(datename(mm,b.from_date),1,3)+' '+rtrim(convert(char(2),day(b.from_date))) " & _
                        "+ ' ' + convert(char(4),year(b.from_date))+' to '+substring(datename(mm,b.to_date),1,3)+' '+ltrim(convert(char(2),day(b.to_date))) " & _
                        "+' ' + convert(char(4),year(b.to_date))+' ' as tsperiod, a.timesheet_ref " & _
                        "from Timesheet.scheme.sir_rlthm a inner join Timesheet.scheme.rlclm b on a.rl_period = b.period where a.resource_code = '" & pubEmpNo.Trim & "'"
            End If

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    DropDownList4.Items.Add(mReader("rl_period") + " - Period : " + mReader("tsperiod") + "  -  Ref# :" + mReader("timesheet_ref"))
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub DisplayTimeSheetDetails()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select pm_period, cost_centre, entry_date from Timesheet.scheme.sir_rlthm where timesheet_ref  = '" & lblReference.Text.Trim & _
                    "' union " & _
                    "Select pm_period, cost_centre, entry_date from Timesheet.scheme.sir_rlthm_history where timesheet_ref  = '" & _
                        lblReference.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    lblPeriod.Text = lblPeriod.Text.Trim & "  -  " & mReader("pm_period")
                    lblCc.Text = mReader("cost_centre")
                    lblEntry.Text = Format(mReader("entry_date"), "dd MMMM yyyy")
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckTimeSheetDuplicateDetail()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select count(timesheet_ref) as tscount from Timesheet.scheme.sir_rltdm where timesheet_ref  = '" & Label8.Text.Trim & _
                    "' and day_shift = '" & pubDayShift.Trim & "' and rate_code = '" & pubRateCode & "' and project = '" & _
                    pubProjectCode & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    If mReader("tscount") > 1 Then
                        pubDetailErrMess = pubDetailErrMess.Trim & " " & "Duplicate Date, Rate Code and Project. "
                        SaveDetailErrMess()
                        Label19.Text = Label19.Text.Trim & " Duplicate Date, Rate Code and Project. "
                        Label19.Visible = True
                    End If
                End While
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckIfThereIsSCA()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rltdm where timesheet_ref  = '" & Label8.Text.Trim & _
                    "' and project_type = 'SCA'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubThereIsSCA = True
                    CheckNotesForSCA()
                End While
            Else
                pubThereIsSCA = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub CheckNotesForSCA()

        'Handles reentering the the search
        Dim ConnStr As String
        Dim sSql As String
        ConnStr = pubConnStr
        Dim MySqlConn As New SqlConnection(ConnStr)

        MySqlConn.Open()
        Try

            sSql = "Select timesheet_ref from Timesheet.scheme.sir_rlnotes where timesheet_ref  = '" & Label8.Text.Trim & "'"

            Dim MySqlCmd As New SqlCommand(sSql, MySqlConn)
            Dim mReader As SqlDataReader

            mReader = MySqlCmd.ExecuteReader()
            If mReader.HasRows Then
                While mReader.Read()
                    pubThereIsNotes = True
                End While
            Else
                pubThereIsNotes = False
            End If

        Catch ex As Exception
            MySqlConn.Close()
        Finally
            MySqlConn.Close()
        End Try

    End Sub

    Private Sub UpdateTimeSheetNotes()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlnotes set notes = '" & TextBox6.Text.Trim & "' where timesheet_ref = '" & _
                Label8.Text.Trim & "'"

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
                            "values('" & Label8.Text.Trim & "','" & TextBox6.Text.Trim & "','" & _
                            pubUser.Trim & "','" & Mid(Label2.Text, 9, 11) & "','" & pubUser.Trim & _
                            "','" & Mid(Label2.Text, 9, 11) & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateNoteFlag1()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set notflg = 'N' where timesheet_ref = '" & _
                Label8.Text.Trim & "'"

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
                Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub DeleteTimeSheetNotes()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "delete from Timesheet.scheme.sir_rlnotes where timesheet_ref = '" & _
                Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

#End Region

    Private Sub MoveTimeSheetTSHeader()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        Select Case pubCLevel
            Case 1
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlthm set status = 'A' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case 2
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlthm set status = 'M' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case 6
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlthm set status = 'M' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case Else
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rlthm set status = 'S' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
        End Select

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub MoveTimeSheetDraft()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        Select Case pubCLevel
            Case 1
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'A', retflg = '' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case 2
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'M', retflg = '' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case 6
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'M', retflg = '' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
            Case Else
                cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set status = 'S', retflg = '' where timesheet_ref = '" & _
                        Label8.Text.Trim & "'"
        End Select

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub ChangeRlStatusBasedCLevel()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        Select Case pubCLevel
            Case 1
                cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'With Director' where rl_period = '" & _
                        Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"
                Label18.Text = "With Director"
            Case 2
                cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'With Manager' where rl_period = '" & _
                        Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"
                Label18.Text = "With Manager"
            Case 6
                cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'With Manager' where rl_period = '" & _
                        Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"
                Label18.Text = "With Manager"
            Case Else
                cmdUpdate.CommandText = "update Timesheet.scheme.and_ts_draft set rl_status = 'With Supervisor' where rl_period = '" & _
                        Label21.Text.Trim & "' and resource_code = '" & pubEmpNo.Trim & "'"
                Label18.Text = "With Supervisor"
        End Select

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Label18.Text.Trim = "Validated" Then
            MoveTimeSheetTSHeader()
            MoveTimeSheetDraft()
            ChangeRlStatusBasedCLevel()
            pubGridView1Submit = True
            GridView1.DataBind()
            GridView2.DataBind()
            Button3.Visible = False
            Button4.Visible = False

            pubLogDescription = "TimeSheet Sent for Approval"
            pubTimeSheetRef = Label8.Text.Trim
            pubRlPeriod = Label21.Text.Trim

            InsertTimeSheetLog()
        End If

    End Sub

    Protected Sub InsertTimeSheetLog()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        cmdUpdate.CommandText = "insert into Timesheet.scheme.sir_rlsyslog(userid,timesheet_ref,description,rl_period,resource_code,ldate,ltime) " & _
                            "values('" & pubUser.Trim & "','" & pubTimeSheetRef.Trim & "','" & pubLogDescription.Trim & "','" & _
                            pubRlPeriod.Trim & "','" & pubEmpNo.Trim & " ','" & Format(pubServerDate, "dd MMM yyyy hh:mm") & _
                            Right(pubServerDate.ToString.Trim, 2) & "','" & _
                            Format(pubServerDate, "dd MMM yyyy hh:mm") & Right(pubServerDate.ToString.Trim, 2) & "')"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

    Private Sub UpdateTSDraftUnits()

        Dim strConnStr As String
        strConnStr = pubConnStr

        Dim MySqlConn As New SqlConnection(strConnStr)

        Dim cmdUpdate As New SqlCommand

        Dim mvalRegular As Double
        Dim mvalOvertime As Double

        If Label12.Text.Trim = "" Then
            mvalRegular = 0
        Else
            mvalRegular = Val(Label12.Text)
        End If

        If Label16.Text.Trim = "" Then
            mvalOvertime = 0
        Else
            mvalOvertime = Val(Label16.Text)
        End If

        cmdUpdate.CommandText = "update Timesheet.scheme.sir_rldraft set regular_unit = " & mvalRegular & _
                                    ", ot_unit = " & mvalOvertime & " where timesheet_ref = '" & _
                                    Label8.Text.Trim & "'"

        cmdUpdate.Connection = MySqlConn
        cmdUpdate.Connection.Open()
        cmdUpdate.ExecuteNonQuery()
        MySqlConn.Close()

    End Sub

End Class
