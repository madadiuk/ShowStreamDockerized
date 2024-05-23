<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenresManagementDataEntry.aspx.cs" Inherits="AdminSystem.GenresManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Genres Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Genres Management</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:GridView ID="gvGenres" runat="server" AutoGenerateColumns="False" DataKeyNames="GenreID" OnRowEditing="gvGenres_RowEditing" OnRowUpdating="gvGenres_RowUpdating" OnRowDeleting="gvGenres_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="GenreID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="txtName" runat="server" ToolTip="Name"></asp:TextBox>
            <asp:TextBox ID="txtDescription" runat="server" ToolTip="Description"></asp:TextBox>
            <asp:Button ID="btnAddGenre" runat="server" Text="Add Genre" OnClick="btnAddGenre_Click" />
        </div>
    </form>
</body>
</html>
