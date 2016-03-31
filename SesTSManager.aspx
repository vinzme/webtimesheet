<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SesTSManager.aspx.vb" Inherits="SesTSManager" %>

<%@ Register Assembly="AtlasControlToolkit" Namespace="AtlasControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Saudi Ericsson Web TimeSheet - For Approval</title>
    <link href="MgtStyleSheet.css" rel="stylesheet" type="text/css" />
<script> 
if(!window.opener){ 
newWin=window.open (this.location,'newwindow','toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,fullscreen=no,directories=no,status=no') 
newWin.moveTo(0,0); 
newWin.resizeTo(screen.availWidth,screen.availHeight) 
window.opener='a'; 
window.close(); 
} 
</script>

</head>
<body style="background-color: white">
    <form id="form1" runat="server">

    <atlas:ScriptManager ID="sl" EnablePartialRendering="true" runat="server"/>
    <atlas:UpdatePanel ID="pl1" runat="server">
    <ContentTemplate>
      
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 100px; vertical-align: middle;">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/tsimages/timesheet.gif" /></td>
                <td style="width: 100%; text-align: right;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/tsimages/ses_logo.png" />
                    </td>
            </tr>
        </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color:  royalblue; height: 1px; border-bottom: gray 1px solid; vertical-align: bottom;">
        <tr>
            <td style="width: 100px; vertical-align: middle; height: 30px;">
                <table style="width: 712px">
                    <tr>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/tsimages/menubar1.gif" /></td>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/tsimages/menubar2.gif" /></td>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/tsimages/menubar4.gif" /></td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
        <atlas:UpdateProgress ID="progress1" runat="server">
            <ProgressTemplate>
                <div class="progress" style="font-weight: bold; font-size: 8pt; left: 860px; color: #0033cc; font-family: Verdana; position: absolute; top: 50px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="tsimages/Computer004.gif" />In Progress....
                </div>
            
            </ProgressTemplate>        
        
        </atlas:UpdateProgress>    
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: lemonchiffon">
                    <tr>
                        <td style="width: 103px;">
                            <table style="width: 349px">
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                                            ForeColor="MediumBlue" Text="SAYED WARETH" Width="221px"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label2" runat="server" ForeColor="RoyalBlue" Text="Show me TimeSheet from Group :"
                                            Width="168px"></asp:Label></td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" BackColor="AliceBlue"
                                            Width="268px" Font-Names="Verdana" ForeColor="MediumBlue">
                                        </asp:DropDownList></td>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label5" runat="server" Text="CRP" Width="77px" ForeColor="LemonChiffon"></asp:Label>
                                    </td>                                        
                                    <td style="width: 100px">
                                        <asp:Label ID="Label3" runat="server" Text="Label" ForeColor="Red" Width="189px"></asp:Label>
                                    </td>                                        
                                    <td style="width: 400px">
                                        <asp:Label ID="Label4" runat="server" Text="CSR" ForeColor="LemonChiffon"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="Label11" runat="server" Text="Label" Width="84px" ForeColor="#FFFFC0"></asp:Label></td>
                    </tr>
                </table>            
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ObjectDataSource1" Width="100%" DataKeyNames="timesheet_ref" GridLines="None" ShowFooter="True">
                    <FooterStyle BackColor="CornflowerBlue" ForeColor="#000066" />
                    <Columns>
                        <asp:TemplateField HeaderText="Check">
                            <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="24px" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Image" InsertVisible="False" SelectImageUrl="~/tsimages/select.png"
                            SelectText="Click to Select and Display Details below." ShowCancelButton="False"
                            ShowSelectButton="True">
                            <ItemStyle Width="20px" />
                        </asp:CommandField>
                        <asp:CommandField ShowEditButton="True" ButtonType="Image" CancelImageUrl="~/tsimages/cancel.png" EditImageUrl="~/tsimages/edit.png" EditText="Edit Notes" UpdateImageUrl="~/tsimages/update.png" SelectImageUrl="~/tsimages/select.png" SelectText="Click to Select and Display Details and Log" UpdateText="Update Notes" >
                            <ItemStyle HorizontalAlign="Center" Width="46px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="timesheet_ref" HeaderText="Reference" ReadOnly="True"
                            SortExpression="timesheet_ref" >
                            <ItemStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="HotPink" Font-Names="Verdana" Font-Size="Small" />
                        </asp:BoundField>
                        <asp:BoundField DataField="resource_code" HeaderText="Emp No" SortExpression="resource_code" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="resource_name" HeaderText="Name" SortExpression="resource_name" ReadOnly="True" />
                        <asp:BoundField DataField="cost_centre" HeaderText="Cost Centre" SortExpression="cost_centre" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="entry_date" HeaderText="Date" SortExpression="entry_date" ReadOnly="True" />
                        <asp:BoundField DataField="regular_unit" HeaderText="Regular" SortExpression="regular_unit" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ot_unit" HeaderText="Overtime" SortExpression="ot_unit" ReadOnly="True" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Red" />
                        </asp:BoundField>
                        <asp:BoundField DataField="week_period" HeaderText="Period" SortExpression="week_period" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Notes" SortExpression="tsnotes">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("tsnotes") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle ForeColor="MidnightBlue" Font-Names="Verdana" Font-Size="X-Small" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("tsnotes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Image" CommandName="Log" ImageUrl="~/tsimages/log.png"
                            Text="Click to Display Log Details below." />
                    </Columns>
                    <RowStyle ForeColor="#000066" BackColor="#EFF3FB" Height="18px" />
                    <SelectedRowStyle BackColor="BlanchedAlmond" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Height="20px" />
                    <AlternatingRowStyle BackColor="#EFF3FB" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetDataTimeSheetDirector2" TypeName="TimeSheetDirector2TableAdapters.and_tsmanagerTableAdapter" DeleteMethod="Delete" UpdateMethod="Update" InsertMethod="Insert">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label5" Name="TSManager" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="Label4" Name="TSGrouping" PropertyName="Text" Type="String" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="tsnotes" Type="String" />
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="resource_name" Type="String" />
                        <asp:Parameter Name="cost_centre" Type="String" />
                        <asp:Parameter Name="regular_unit" Type="Double" />
                        <asp:Parameter Name="week_period" Type="String" />
                        <asp:Parameter Name="tsnotes" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDirectorDammam"
                    TypeName="TimeSheetDirectorDammamTableAdapters.and_tsmanagerTableAdapter" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="tsnotes" Type="String" />
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label5" Name="TSManager" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="Label4" Name="TSGrouping" PropertyName="Text" Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="resource_name" Type="String" />
                        <asp:Parameter Name="cost_centre" Type="String" />
                        <asp:Parameter Name="regular_unit" Type="Double" />
                        <asp:Parameter Name="week_period" Type="String" />
                        <asp:Parameter Name="tsnotes" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDirectorJeddah"
                    TypeName="TimeSheetDirectorJeddahTableAdapters.and_tsmanagerTableAdapter" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="tsnotes" Type="String" />
                        <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label5" Name="TSManager" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="Label4" Name="TSGrouping" PropertyName="Text" Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="resource_name" Type="String" />
                        <asp:Parameter Name="cost_centre" Type="String" />
                        <asp:Parameter Name="regular_unit" Type="Double" />
                        <asp:Parameter Name="week_period" Type="String" />
                        <asp:Parameter Name="tsnotes" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <br />
                <table style="width: 744px">
                    <tr>
                        <td style="width: 100px">
                            <asp:Button ID="Button1" runat="server" Text="Check All" Width="103px" /></td>
                        <td style="width: 100px">
                            <asp:Button ID="Button2" runat="server" Text="UnCheck All" Width="103px" /></td>
                        <td style="width: 100px">
                            <asp:Button ID="Button3" runat="server" Text="Approve Checked Items" Width="167px" OnClick="Button3_Click" /></td>
                        <td style="width: 100px">
                            <asp:Button ID="Button4" runat="server" Text="Return Checked Items" Width="167px" OnClick="Button4_Click1" /></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label24" runat="server" ForeColor="White" Text="M" Width="58px"></asp:Label></td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: honeydew; border-top: gray 1px solid; height: 76px;">
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label6" runat="server" Style="text-align: right" Text="Reference :"
                                Width="94px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label10" runat="server" ForeColor="Red" Text="00019421" Width="88px"></asp:Label></td>
                        <td style="width: 72px">
                            <asp:Label ID="Label15" runat="server" Style="text-align: right" Text="Technician :"
                                Width="74px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label19" runat="server" ForeColor="RoyalBlue" Text="Label" Width="207px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label7" runat="server" Style="text-align: right" Text="Date of Entry :"
                                Width="94px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label12" runat="server" ForeColor="RoyalBlue" Text="Label" Width="95px"></asp:Label></td>
                        <td style="width: 72px">
                            <asp:Label ID="Label16" runat="server" Style="text-align: right" Text="Regular Unit :"
                                Width="74px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label20" runat="server" ForeColor="RoyalBlue" Text="Label" Width="58px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label8" runat="server" Style="text-align: right" Text="Timesheet Period :"
                                Width="94px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label13" runat="server" ForeColor="RoyalBlue" Text="Label" Width="233px"></asp:Label></td>
                        <td style="width: 72px">
                            <asp:Label ID="Label17" runat="server" Style="text-align: right" Text="Overtime :"
                                Width="74px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label21" runat="server" ForeColor="Red" Text="Label" Width="58px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 79px">
                            <asp:Label ID="Label9" runat="server" Style="text-align: right" Text="Project Period :"
                                Width="94px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label14" runat="server" ForeColor="RoyalBlue" Text="00019421" Width="86px"></asp:Label></td>
                        <td style="width: 72px">
                            <asp:Label ID="Label18" runat="server" Style="text-align: right" Text="Unit :" Width="74px"></asp:Label></td>
                        <td style="width: 100px">
                            <asp:Label ID="Label22" runat="server" ForeColor="RoyalBlue" Text="HRS" Width="45px"></asp:Label></td>
                    </tr>
                </table>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    DataKeyNames="timesheet_ref,line_no" DataSourceID="ObjectDataSource2" ForeColor="#333333"
                    GridLines="None" Width="100%">
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="day_shift" HeaderText="Date" SortExpression="day_shift" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="project" HeaderText="Project" SortExpression="project" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="custname" HeaderText="Customer" SortExpression="custname" />
                        <asp:BoundField DataField="sitename" HeaderText="Site" SortExpression="sitename" />
                        <asp:BoundField DataField="no_units" HeaderText="Total Worked" SortExpression="no_units" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rate_code" HeaderText="Rate Code" SortExpression="rate_code" />
                        <asp:BoundField DataField="travel_units" HeaderText="Travel" SortExpression="travel_units" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codes" HeaderText="Codes" SortExpression="codes" />
                    </Columns>
                    <RowStyle BackColor="AliceBlue" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="AliceBlue" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTsDetailsCustSite"
                    TypeName="TSDetailsWCustomerTableAdapters.DataTable1TableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="tsreference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTsDetailsCustSiteDam"
                    TypeName="TSDetailsWCustomerDamTableAdapters.DataTable1TableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="tsreference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTsDetailsCustSiteJed"
                    TypeName="TSDetailsWCustomerJedTableAdapters.DataTable1TableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="tsreference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="RoyalBlue" Text="User Log :"
                    Width="113px" BackColor="Pink"></asp:Label>
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ObjectDataSource3" ForeColor="#333333" GridLines="None" Width="100%">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ldate" HeaderText="Date and Time" SortExpression="ldate">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="userid" HeaderText="User" SortExpression="userid" />
                        <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description" />
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" InsertMethod="Insert"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSLog" TypeName="TSLogTableAdapters.sir_rlsyslogTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="TSReference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="userid" Type="String" />
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="rl_period" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="ldate" Type="DateTime" />
                        <asp:Parameter Name="ltime" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" InsertMethod="Insert"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSLogDammam"
                    TypeName="TSLogDammamTableAdapters.sir_rlsyslogTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="TSReference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="userid" Type="String" />
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="rl_period" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="ldate" Type="DateTime" />
                        <asp:Parameter Name="ltime" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" InsertMethod="Insert"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSLogJeddah"
                    TypeName="TSLogJeddahTableAdapters.sir_rlsyslogTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Label11" Name="TSReference" PropertyName="Text"
                            Type="String" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="userid" Type="String" />
                        <asp:Parameter Name="timesheet_ref" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="rl_period" Type="String" />
                        <asp:Parameter Name="resource_code" Type="String" />
                        <asp:Parameter Name="ldate" Type="DateTime" />
                        <asp:Parameter Name="ltime" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                
            </asp:View>
            <asp:View ID="View2" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="background-color: #99ccff;">
	<tr>
	<td width="1%" nowrap="nowrap">&nbsp;</td>
	<td width="89%" height="30" colspan="2" class="logo" nowrap="nowrap">
	2013 Weekly Calendar</td>
	<td width="10%">&nbsp;</td>
	</tr>

	<tr bgcolor="#ffffff">
	<td colspan="4"><img src="mm_spacer.gif" alt="" width="1" height="1" border="0" /></td>
	</tr>
	<tr bgcolor="#a4c2c2">
	<td nowrap="nowrap">&nbsp;</td>
	<td height="20" id="navigation" class="navText"></td>
	<td>&nbsp;</td>
	<td>&nbsp;</td>
	</tr>

	<tr bgcolor="#ffffff">
	<td colspan="4"><img src="mm_spacer.gif" alt="" width="1" height="1" border="0" /></td>
	</tr>

	<tr bgcolor="#ffffff">
	<td valign="top" width="1%"><img src="mm_spacer.gif" alt="" width="15" height="1" border="0" /></td>
	<td valign="top" width="5%"><img src="mm_spacer.gif" alt="" width="35" height="1" border="0" /></td>
	<td width="84%" valign="top"><br />
	<table border="0" cellspacing="0" cellpadding="2" width="100%">
        <tr>
          <td class="pageName"></td>
        </tr>
	</table>
	<table width="100%" cellpadding="2" cellspacing="1" border="0" id="calendar">
        <tr>
          <td colspan="6" class="subHeader"></td>
        </tr>
		<tr id="weekdays" bgcolor="#0000cc">
          <th align="center" width="10%" class="bodyText">WEEK#</th>
          <th align="center" width="23%" class="bodyText">INCLUSIVE DATES</th>
          <th align="center" width="10%" class="bodyText">WEEK#</th>
          <th align="center" width="23%" class="bodyText">INCLUSIVE DATES</th>
          <th align="center" width="10%" class="bodyText">WEEK#</th>
          <th align="center" width="23%" class="bodyText">INCLUSIVE DATES</th>
        </tr>
		<tr bgcolor="#0066FF" id="calheader">
          <td colspan="2" align="center" valign="top" class="calendarText">January</td>
          <td colspan="2" align="center" valign="top" class="calendarText">May</td>
          <td colspan="2" align="center" valign="top" class="calendarText">September</td>
        </tr>
        <tr bgcolor="#CCFFFF">
          <td valign="top" height="50" class="calendarText">
		  	W0112&nbsp;<br /><br />
          		W0212&nbsp;<br /><br />
          		W0312&nbsp;<br /><br />
          		W0412&nbsp;</td>
          <td valign="top" class="calendarText2">
          		01-Jan to 04-Jan<br /><br />
          		05-Jan to 11-Jan<br /><br />
          		12-Jan to 18-Jan<br /><br />
          		19-Jan to 25-Jan</td>
          <td valign="top" class="calendarText">
          		W1812&nbsp;<br />
          		<br />
          		W1912&nbsp;<br />
          		<br />
          		W2012&nbsp;<br />
          		<br />
          		W2112&nbsp;</td>
          <td valign="top" class="calendarText2">
          		27-Apr to 03-May<br /><br />
          		04-May to 10-May<br /><br />
          		11-May to 17-May<br /><br />
          		18-May to 24-May</td>
          <td valign="top" class="calendarText">
		  	W3512&nbsp;<br />
		  	<br />
          		W3612&nbsp;<br />
          		<br />
          		W3712&nbsp;<br />
          		<br />
          		W3812&nbsp;<br />
          		<br />          		
          		W3912&nbsp;</td>
          <td valign="top" class="calendarText2">
          		24-Aug to 30-Aug<br /><br />
          		31-Aug to 06-Sep<br /><br />
          		07-Sep to 13-Sep<br /><br />
          		14-Sep to 20-Sep<br /><br />
          		21-Sep to 27-Sep</td>
        </tr>
        <tr bgcolor="#0066FF" id="calheader">
          <td colspan="2" align="center" valign="top" class="calendarText">February</td>
          <td colspan="2" align="center" valign="top" class="calendarText">June</td>
          <td colspan="2" align="center" valign="top" class="calendarText">October</td>
        </tr>
        <tr bgcolor="#CCFFFF">
          <td valign="top" height="50" class="calendarText">
		  	W0512&nbsp;<br /><br />
          		W0612&nbsp;<br /><br />
          		W0712&nbsp;<br /><br />
          		W0812&nbsp;</td>
          <td valign="top" class="calendarText2">
          		26-Jan to 01-Feb<br /><br />
          		02-Feb to 08-Feb<br /><br />
          		09-Feb to 15-Feb<br /><br />
          		16-Feb to 22-Feb</td>
          <td valign="top" class="calendarText">
		  	W2212&nbsp;<br />
		  	<br />
          		W2312&nbsp;<br />
          		<br />
          		W2412&nbsp;<br />
          		<br />
          		W2512&nbsp;<br />
          		<br />          		
          		W2612&nbsp;</td>
          <td valign="top" class="calendarText2">
			25-May to 31-May<br /><br />
          		01-Jun to 07-Jun<br /><br />
          		08-Jun to 14-Jun<br /><br />
          		15-Jun to 21-Jun<br /><br />
          		22-Jun to 28-Jun</td>
          <td valign="top" class="calendarText">
          		W4012&nbsp;<br />
          		<br />
          		W4112&nbsp;<br />
          		<br />
          		W4212&nbsp;<br />
          		<br />
          		W4312&nbsp;</td>
          <td valign="top" class="calendarText2">
          		28-Sep to 04-Oct<br /><br />
          		05-Oct to 11-Oct<br /><br />
          		12-Oct to 18-Oct<br /><br />
			19-Oct to 25-Oct</td>
        </tr>
        <tr bgcolor="#0066FF" id="calheader">
          <td colspan="2" align="center" valign="top" class="calendarText">March</td>
          <td colspan="2" align="center" valign="top" class="calendarText">July</td>
          <td colspan="2" align="center" valign="top" class="calendarText">November</td>
        </tr>
        <tr bgcolor="#CCFFFF">
          <td valign="top" height="50" class="calendarText">
		  	W0912&nbsp;<br />
		  	<br />
          		W1012&nbsp;<br />
          		<br />
          		W1112&nbsp;<br />
          		<br />
          		W1212&nbsp;</td>
          <td valign="top" class="calendarText2">
          		23-Feb to 01-Mar<br /><br />
          		02-Mar to 08-Mar<br /><br />
          		09-Mar to 15-Mar<br /><br />
          		16-Mar to 22-Mar</td>
          <td valign="top" class="calendarText">
          		W2712&nbsp;<br />
          		<br />
          		W2812&nbsp;<br />
          		<br />
          		W2912&nbsp;<br />
			<br />
			W3012&nbsp;</td>
          <td valign="top" class="calendarText2">
          		29-Jun to 05-Jul<br /><br />
          		06-Jul to 12-Jul<br /><br />
          		13-Jul to 19-Jul<br /><br />
			20-Jul to 26-Jul</td>
          <td valign="top" class="calendarText">
		  	W4412&nbsp;<br />
		  	<br />
          		W4512&nbsp;<br />
          		<br />
          		W4612&nbsp;<br />
          		<br />
          		W4712&nbsp;<br />
          		<br />          		
          		W4812&nbsp;</td>
          <td valign="top" class="calendarText2">
          		26-Oct to 01-Nov<br /><br />
          		02-Nov to 08-Nov<br /><br />
          		09-Nov to 15-Nov<br /><br />
          		16-Nov to 22-Nov<br /><br />
          		23-Nov to 29-Nov</td>
        </tr>
        <tr bgcolor="#0066FF" id="calheader">
          <td colspan="2" align="center" valign="top" class="calendarText">April</td>
          <td colspan="2" align="center" valign="top" class="calendarText">August</td>
          <td colspan="2" align="center" valign="top" class="calendarText">December</td>
        </tr>
        <tr bgcolor="#CCFFFF">
          <td valign="top" height="50" class="calendarText">
		  	W1312&nbsp;<br />
		  	<br />
          		W1412&nbsp;<br />
          		<br />
          		W1512&nbsp;<br />
          		<br />
		  	    W1612&nbsp;<br />
		  	    <br />
          		W1712&nbsp;</td>
          <td valign="top" class="calendarText2">
          		23-Mar to 29-Mar<br /><br />
          		30-Mar to 05-Apr<br /><br />
          		06-Apr to 12-Apr<br /><br />
          		13-Apr to 19-Apr<br /><br />
          		20-Apr to 26-Apr</td>
          <td valign="top" class="calendarText">
		  	W3112&nbsp;<br />
		  	<br />
          		W3212&nbsp;<br />
          		<br />
          		W3312&nbsp;<br />
          		<br />
          		W3412&nbsp;</td>
          <td valign="top" class="calendarText2">
          		27-Jul to 02-Aug<br /><br />
          		03-Aug to 09-Aug<br /><br />
          		10-Aug to 16-Aug<br /><br />
          		17-Aug to 23-Aug</td>
          <td valign="top" class="calendarText">
          		W4912&nbsp;<br />
          		<br />
          		W5012&nbsp;<br />
          		<br />
          		W5112&nbsp;<br />
          		<br />
          		W5212&nbsp;<br />
                </td>
          <td valign="top" class="calendarText2">
          		30-Nov to 06-Dec<br /><br />
          		07-Dec to 13-Dec<br /><br />
          		14-Dec to 20-Dec<br /><br />
          		21-Dec to 31-Dec</td>
        </tr>
      </table>
        &nbsp;<br />
	&nbsp;<br />	</td>
	<td>&nbsp;</td>
	</tr>

	<tr bgcolor="#ffffff">
	<td colspan="4"><img src="mm_spacer.gif" alt="" width="1" height="1" border="0" /></td>
	</tr>
	<tr bgcolor="#a4c2c2">
	<td nowrap="nowrap">&nbsp;</td>
	<td height="20" id="Td1" class="navText"></td>
	<td>&nbsp;</td>
	<td>&nbsp;</td>
	</tr>

	<tr bgcolor="#ffffff">
	<td colspan="4"><img src="mm_spacer.gif" alt="" width="1" height="1" border="0" /></td>
	</tr>

	<tr>
	<td width="15">&nbsp;</td>
    <td width="35">&nbsp;</td>
    <td width="757">&nbsp;</td>
	<td width="86">&nbsp;</td>
	</tr>
</table>
        </asp:View>
            <asp:View ID="View3" runat="server">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/tsimages/tsuser1.png" />
                <asp:Image ID="Image3" runat="server" ImageUrl="~/tsimages/tsuser2.png" />
                </asp:View>
        </asp:MultiView>
</ContentTemplate>   
</atlas:UpdatePanel>        
    </form>
</body>
</html>
