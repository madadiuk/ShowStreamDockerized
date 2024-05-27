<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeriesManagementDataEntry.aspx.cs" Inherits="SeriesManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Series Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Series Management</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:GridView ID="gvSeries" runat="server" AutoGenerateColumns="False" DataKeyNames="SeriesID" OnRowEditing="gvSeries_RowEditing" OnRowUpdating="gvSeries_RowUpdating" OnRowDeleting="gvSeries_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="SeriesID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="GenreID" HeaderText="Genre ID" />
                    <asp:BoundField DataField="StartYear" HeaderText="Start Year" />
                    <asp:BoundField DataField="EndYear" HeaderText="End Year" />
                    <asp:BoundField DataField="Country" HeaderText="Country" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="txtTitle" runat="server" ToolTip="Title"></asp:TextBox>
            <asp:TextBox ID="txtGenreID" runat="server" ToolTip="Genre ID"></asp:TextBox>
            <asp:TextBox ID="txtStartYear" runat="server" ToolTip="Start Year"></asp:TextBox>
            <asp:TextBox ID="txtEndYear" runat="server" ToolTip="End Year"></asp:TextBox>
            <asp:TextBox ID="txtCountry" runat="server" ToolTip="Country"></asp:TextBox>
            <asp:Button ID="btnAddSeries" runat="server" Text="Add Series" OnClick="btnAddSeries_Click" />
        </div>
    </form>
</body>
</html>
