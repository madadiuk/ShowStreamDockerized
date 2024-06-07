<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewGenres.aspx.cs" Inherits="ViewGenres" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Genres</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Genres List</h2>
            <asp:GridView ID="gvGenres" runat="server" AutoGenerateColumns="False" OnRowEditing="gvGenres_RowEditing" OnRowDeleting="gvGenres_RowDeleting" OnRowUpdating="gvGenres_RowUpdating" OnRowCancelingEdit="gvGenres_RowCancelingEdit" DataKeyNames="GenreID">
                <Columns>
                    <asp:BoundField DataField="GenreID" HeaderText="GenreID" ReadOnly="True" SortExpression="GenreID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddGenre" runat="server" Text="Add Genre" OnClick="btnAddGenre_Click" />
            <asp:Button ID="btnMainMenu" runat="server" Text="Main Menu" OnClick="btnMainMenu_Click" />
        </div>
    </form>
</body>
</html>
