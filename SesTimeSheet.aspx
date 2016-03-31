<%@ Page Language="VB" masterpagefile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SesTimeSheet.aspx.vb" Inherits="SesTimeSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <atlas:ScriptManager ID="sl" EnablePartialRendering="true" runat="server"/>
    <atlas:UpdatePanel ID="pl1" runat="server">
    <ContentTemplate>

<div class="menuheader">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color:  royalblue; height: 1px; border-bottom: gray 1px solid; vertical-align: bottom;">
        <tr>
            <td style="width: 100px; vertical-align: middle; height: 30px;">
                <table style="width: 712px">
                    <tr>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/tsimages/menubar1.gif" /></td>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/tsimages/menubar2.gif" /></td>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/tsimages/menubar3.gif" /></td>
                        <td style="width: 100px">
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/tsimages/menubar4.gif" /></td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
        <atlas:UpdateProgress ID="progress1" runat="server">
            <ProgressTemplate>
                <div class="progress" style="font-weight: bold; font-size: 8pt; left: 350px; color: #0033cc; font-family: Verdana; position: absolute; top: 62px;">
                    <asp:Image ID="Image1" runat="server" ImageUrl="tsimages/Computer004.gif" />In Progress......
                </div>
            
            </ProgressTemplate>        
        
        </atlas:UpdateProgress>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: lemonchiffon;">
                <tr>
                    <td style="width: 100px; text-align: center">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                            ForeColor="MediumBlue" Text="7969" Width="71px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Text="ALVIN NAMIN DE LEON"
                            Width="443px" Font-Bold="True" Font-Size="Medium" ForeColor="MediumBlue"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Text="19 September 2006" Width="212px" ForeColor="Red"></asp:Label></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="Timesheet Approver :  " Width="544px" Font-Bold="True"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label4" runat="server" Text="Label" Visible="False" Width="100px"></asp:Label></td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/tsimages/select.png" />
                                <asp:BoundField DataField="rl_period" HeaderText="Week" SortExpression="rl_period" />
                                <asp:BoundField DataField="tsperiod" HeaderText="Period" ReadOnly="True" SortExpression="tsperiod" />
                                <asp:BoundField DataField="rl_status" HeaderText="Status" SortExpression="rl_status" >
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="HotPink" />
                                </asp:BoundField>
                                <asp:BoundField DataField="timesheet_ref" HeaderText="Reference" SortExpression="timesheet_ref" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ugroup" HeaderText="Group" SortExpression="ugroup" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cost_centre" HeaderText="Cost Centre" SortExpression="cost_centre" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="entry_date" HeaderText="Date" SortExpression="entry_date" />
                                <asp:BoundField DataField="notes" HeaderText="Remarks" SortExpression="notes" />
                            </Columns>
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="Moccasin" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#EFF3FB" />
                        </asp:GridView>

                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetDataNewTSDraft" TypeName="TimeSheetDraftTableAdapters.DataTable1TableAdapter">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Label4" Name="WhatEmpNo" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>            
            <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataTSDraftJeddah2" TypeName="TSDraftJeddah2TableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label4" Name="WhatEmpNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource7" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataTSDraftDammam" TypeName="TimeSheetDraftDammamTableAdapters.DataTable1TableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label4" Name="WhatEmpNo" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
                        <br />
            <asp:Panel ID="Panel2" runat="server" Height="50px" Width="100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-bottom: gray 1px solid; height: 124px; border-top: gray 1px solid;">
                <tr>
                    <td style="width: 40px; height: 13px; text-align: right;">
                        <asp:Label ID="Label5" runat="server" Text="Reference :" Width="89px" ForeColor="DarkBlue" Height="19px"></asp:Label></td>
                    <td style="width: 100px; height: 13px;">
                        <asp:Label ID="Label8" runat="server" Font-Names="Verdana" ForeColor="Red" Text="00018556"
                            Width="68px" Visible="False" Font-Bold="True"></asp:Label></td>
                    <td style="width: 51px; height: 13px; text-align: right;">
                        <asp:Label ID="Label11" runat="server" ForeColor="DarkBlue" Text="Regular Units :" Width="105px"></asp:Label></td>
                    <td style="width: 100px; height: 13px; text-align: right;">
                        <asp:Label ID="Label12" runat="server" ForeColor="RoyalBlue" Text="0" Width="52px" Visible="False" Font-Bold="True"></asp:Label></td>
                    <td style="width: 100px; height: 13px; text-align: right;">
                        <asp:Label ID="Label13" runat="server" ForeColor="MidnightBlue" Text="Unit :" Width="112px"></asp:Label></td>
                    <td style="width: 100px; height: 13px;">
                        <asp:Label ID="Label14" runat="server" ForeColor="RoyalBlue" Text="HRS" Width="55px" Visible="False" Font-Bold="True"></asp:Label></td>
                    <td style="width: 100px; height: 13px">
                        <asp:Label ID="Label20" runat="server" Text="Select" Width="72px" Visible="False"></asp:Label></td>
                    <td style="width: 100px; height: 13px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 40px; height: 13px; text-align: right;">
                        <asp:Label ID="Label6" runat="server" Text="Project Period :" Width="90px" ForeColor="DarkBlue" Height="18px"></asp:Label></td>
                    <td style="width: 100px; height: 13px;">
                        <asp:Label ID="Label9" runat="server" ForeColor="RoyalBlue" Text="0" Width="70px" Visible="False" Font-Bold="True"></asp:Label></td>
                    <td style="width: 51px; height: 13px; text-align: right;">
                        <asp:Label ID="Label15" runat="server" Text="Overtime Units :" Width="105px" ForeColor="DarkBlue"></asp:Label></td>
                    <td style="width: 100px; height: 13px; text-align: right;">
                        <asp:Label ID="Label16" runat="server" ForeColor="RoyalBlue" Text="0" Visible="False"
                            Width="50px" Font-Bold="True"></asp:Label></td>
                    <td style="width: 100px; height: 13px;">
                        <asp:Label ID="Label17" runat="server" Text="Label" Width="111px" Visible="False"></asp:Label></td>
                    <td style="width: 100px; height: 13px; text-align: right;">
                        <asp:Label ID="Label180" runat="server" Text="Week No. :" Width="146px" ForeColor="DarkBlue"></asp:Label></td>
                    <td style="width: 100px; height: 13px">
                        <asp:Label ID="Label21" runat="server" Width="82px" Font-Bold="True" ForeColor="MidnightBlue"></asp:Label></td>
                    <td style="width: 100px; height: 13px">
                        <asp:Label ID="Label33" runat="server" Text="Label" Visible="False" Width="63px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: center; height: 28px; background-color: honeydew;">
                        <asp:Label ID="Label7" runat="server" Text="Notes :" Width="90px" ForeColor="DarkBlue" Height="18px"></asp:Label></td>
                    <td colspan="5" style="height: 28px; background-color: honeydew;">
                        <asp:TextBox ID="TextBox6" runat="server" Font-Names="Verdana" Font-Size="X-Small"
                            Height="40px" Width="628px" Visible="False" BackColor="CornflowerBlue" ForeColor="White" TextMode="MultiLine">The quick brown fox jumps</asp:TextBox></td>
                    <td colspan="1" style="height: 28px; background-color: honeydew;">
                        <asp:Button ID="Button3" runat="server" Font-Size="X-Small" Text="Save Notes"
                            Width="77px" OnClick="Button3_Click" /></td>
                    <td colspan="1" style="height: 28px; background-color: honeydew;">
                        <asp:Label ID="Label18" runat="server" Text="Label" Width="59px" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 28px; background-color: honeydew; text-align: center">
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="timesheet_ref,line_no"
                DataSourceID="ObjectDataSource2" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Image" CancelImageUrl="~/tsimages/cancel.png" DeleteImageUrl="~/tsimages/trash.png" EditImageUrl="~/tsimages/edit.png" UpdateImageUrl="~/tsimages/update.png" >
                        <ItemStyle Width="55px" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Date" SortExpression="day_shift">
                        <EditItemTemplate>
                        
                <asp:TextBox ID="TextBox1" runat="server"  Text='<%# Bind("day_shift") %>' Visible="false"></asp:TextBox>                        
        
            <asp:DropDownList ID="DropDownList1" DataTextField="prompt" 
                DataValueField = "prompt" DataSource= '<%# PopulateControls() %>' runat="server">
            </asp:DropDownList>
                                       
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("day_shift") %>'></asp:Label>
                            
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project" SortExpression="project">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("project") %>' AutoPostBack="false"></asp:TextBox>

            <asp:DropDownList ID="DropDownList2" DataTextField="nonproject" 
                DataValueField = "nonproject" DataSource= '<%# PopulateNonProject() %>' runat="server" Font-Size="X-Small" Font-Names="Arial" 
                OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true" Width="150px">
            </asp:DropDownList>                            
                            
                        </EditItemTemplate>
                        <ControlStyle Width="170px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("project") %>'></asp:Label>

                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Worked" SortExpression="no_units">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("no_units") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ControlStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("no_units") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate Code" SortExpression="rate_code">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("rate_code") %>' Visible="false"></asp:TextBox>

                        <asp:DropDownList ID="DropDownList3" DataTextField="coderate" 
                DataValueField = "coderate" DataSource= '<%# PopulateRateCode() %>' runat="server">
                            </asp:DropDownList>


                        </EditItemTemplate>
                        <ControlStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("rate_code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Travel" SortExpression="travel_units">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("travel_units") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ControlStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("travel_units") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="codes" HeaderText="Codes" SortExpression="codes" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="err_msg" HeaderText="Error Message" SortExpression="err_msg" ReadOnly="True" >
                        <ItemStyle HorizontalAlign="Left" ForeColor="Red" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <EditRowStyle Font-Names="Verdana" Font-Size="Small" BackColor="AliceBlue" />
            </asp:GridView>

            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Delete"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataNewTSDetail"
                TypeName="TimeSheetDetailsTableAdapters.sir_rltdmTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label17" Name="tsreference" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="timesheet_ref" Type="String" />
                    <asp:Parameter Name="line_no" Type="String" />
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="err_msg" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" DeleteMethod="Delete"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDetailsJeddah"
                TypeName="TimeSheetDetailsJeddahTableAdapters.sir_rltdmTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label17" Name="tsreference" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="timesheet_ref" Type="String" />
                    <asp:Parameter Name="line_no" Type="String" />
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="err_msg" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" DeleteMethod="Delete"
                InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDetailsDammam"
                TypeName="TimeSheetDetailsDammamTableAdapters.sir_rltdmTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="Label17" Name="tsreference" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="timesheet_ref" Type="String" />
                    <asp:Parameter Name="line_no" Type="String" />
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Single" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Single" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="err_msg" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>                    
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="height: 28px; background-color: honeydew; text-align: center">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; background-color: skyblue;">
                <tr>
                    <td style="width: 92px">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Insert Detail"
                            Width="147px" /></td>
                    <td style="width: 100px">
                        <asp:Button ID="Button2" runat="server" Text="Validate" Width="147px" OnClick="Button2_Click" /></td>
                    <td style="width: 100px">
                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Submit" ToolTip="Click to Submit TimeSheet to Approver"
                            Width="147px" /></td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                        </td>
                </tr>
            </table>
            <table style="width: 902px">
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="Label19" runat="server" ForeColor="Red" Text="Message :" Width="862px"></asp:Label></td>
                </tr>
            </table>                    
                    </td>
                </tr>
            </table>            
            </asp:Panel>
            
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
            <table>
                <tr>
                    <td style="width: 8px">
                    </td>
                    <td style="width: 62px">
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            ForeColor="MediumBlue" Text="7969" Width="39px"></asp:Label></td>
                    <td style="width: 188px">
                        <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
                            ForeColor="MediumBlue" Text="ALVIN NAMIN DE LEON" Width="403px"></asp:Label></td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-top: gray 1px solid; height: 16px;">
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
            <table style="width: 650px">
                <tr>
                    <td style="width: 12px">
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="Label25" runat="server" Text="Select Week :" Font-Names="Verdana" Font-Size="Small" ForeColor="DarkBlue" Width="111px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="482px" BackColor="CornflowerBlue" Font-Names="Verdana" ForeColor="White">
                        </asp:DropDownList></td>
                    <td style="width: 100px">
                        <asp:Button ID="Button5" runat="server" Text="View" /></td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 61px; border-top: gray 1px solid; border-bottom: gray 1px solid; background-color: lemonchiffon;">
                <tr>
                    <td style="width: 100px; text-align: right">
                        <asp:Label ID="Label24" runat="server" Text="Reference :" Width="87px"></asp:Label></td>
                    <td style="width: 7px">
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="lblReference" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="Small" ForeColor="Red" Text="00019041" Width="101px"></asp:Label></td>
                    <td style="width: 100px; text-align: right;">
                        <asp:Label ID="Label31" runat="server" Text="Date of Entry :" Width="89px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="lblEntry" runat="server" Text="Date" ForeColor="MediumBlue" Width="217px"></asp:Label></td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right">
                        <asp:Label ID="Label27" runat="server" Text="Period :" Width="83px"></asp:Label></td>
                    <td style="width: 7px">
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="lblPeriod" runat="server" ForeColor="MediumBlue" Text=" W4106              0610"
                            Width="93px"></asp:Label></td>
                    <td style="width: 100px; text-align: right;">
                        <asp:Label ID="Label26" runat="server" Text="Regular Units :" Width="87px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="lblRegular" runat="server" ForeColor="MediumBlue" Text="Label" Width="61px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label28" runat="server" Text="Overtime Units :" Width="87px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="lblOvertime" runat="server" ForeColor="MediumBlue" Text="Label" Width="54px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label30" runat="server" Text="Unit :" Width="30px"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:Label ID="Label32" runat="server" ForeColor="MediumBlue" Text="Hours" Width="35px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px; text-align: right">
                        <asp:Label ID="Label29" runat="server" Text="Cost Centre :" Width="80px"></asp:Label></td>
                    <td style="width: 7px">
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="lblCc" runat="server" ForeColor="MediumBlue" Text="28" Width="89px"></asp:Label></td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>            
            
            </asp:Panel>
            <br />
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="timesheet_ref,line_no" DataSourceID="ObjectDataSource3" ForeColor="White"
                GridLines="None" Width="100%" ShowFooter="True">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundField DataField="day_shift" HeaderText="Date" SortExpression="day_shift">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="project" HeaderText="Project" SortExpression="project">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="no_units" HeaderText="Units" SortExpression="no_units">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rate_code" HeaderText="Rate" SortExpression="rate_code">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="travel_units" HeaderText="Travel" SortExpression="travel_units">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="codes" HeaderText="Codes" SortExpression="codes">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" Height="20px" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDetailInquire"
                TypeName="TSDetailInquireTableAdapters.sir_rltdmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblReference" Name="tsrefdet" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" DeleteMethod="Delete"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataTSDetailInquireJeddah"
                TypeName="TSDetailInquireJeddahTableAdapters.sir_rltdmTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="timesheet_ref" Type="String" />
                    <asp:Parameter Name="line_no" Type="String" />
                    <asp:Parameter Name="day_shift" Type="String" />
                    <asp:Parameter Name="project" Type="String" />
                    <asp:Parameter Name="no_units" Type="Double" />
                    <asp:Parameter Name="rate_code" Type="String" />
                    <asp:Parameter Name="travel_units" Type="Double" />
                    <asp:Parameter Name="codes" Type="String" />
                    <asp:Parameter Name="Original_timesheet_ref" Type="String" />
                    <asp:Parameter Name="Original_line_no" Type="String" />
                </UpdateParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblReference" Name="tsrefdet" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDataTSDetailInquireDammam" TypeName="TSDetailInquireDammamTableAdapters.sir_rltdmTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblReference" Name="tsrefdet" PropertyName="Text"
                        Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </asp:View>
        <asp:View ID="View4" runat="server">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/tsimages/tsuserguide1.jpg" />
            <asp:Image ID="Image3" runat="server" ImageUrl="~/tsimages/tsuserguide2.jpg" />
            </asp:View>
    </asp:MultiView>

</div>
   
</ContentTemplate>   
</atlas:UpdatePanel>    
    
</asp:Content>