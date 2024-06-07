<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewMovies.aspx.cs" Inherits="ViewMovies" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Movies</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Movies List</h2>
            <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" OnRowEditing="gvMovies_RowEditing" OnRowDeleting="gvMovies_RowDeleting" OnRowUpdating="gvMovies_RowUpdating" OnRowCancelingEdit="gvMovies_RowCancelingEdit" DataKeyNames="MovieID">
                <Columns>
                    <asp:BoundField DataField="MovieID" HeaderText="MovieID" ReadOnly="True" SortExpression="MovieID" />
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:BoundField DataField="GenreID" HeaderText="GenreID" SortExpression="GenreID" />
                    <asp:BoundField DataField="Director" HeaderText="Director" SortExpression="Director" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" SortExpression="ReleaseDate" />
                    <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" OnClick="btnAddMovie_Click" />
            <asp:Button ID="btnMainMenu" runat="server" Text="Main Menu" OnClick="btnMainMenu_Click" />
        </div>
    </form>
</body>
</html>
