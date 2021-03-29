<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DemoApplication.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 80px">

            <div style="text-align:center">
                <asp:DropDownList ID="ddlStates" runat="server" OnSelectedIndexChanged="ddlStates_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>

            <br />

            <asp:GridView ID="gvCdcData" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" CellPadding="10" OnRowDataBound="gvCdcData_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="state" HeaderText="State" ReadOnly="True" SortExpression="state" />
                    <asp:BoundField DataField="submission_date" HeaderText="Date" SortExpression="submission_date" />
                    <asp:BoundField DataField="tot_death" HeaderText="State Death" SortExpression="tot_death" />
                    <asp:TemplateField HeaderText="% State Death">
                        <ItemTemplate>
                            <asp:Label ID="lblPercent" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <br />

            <div style="text-align: center">
                <asp:Label ID="lblTotalDeath" runat="server" Text="Total Death in API call: " Font-Bold="true"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
