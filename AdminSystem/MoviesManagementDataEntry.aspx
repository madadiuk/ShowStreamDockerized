<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoviesManagementDataEntry.aspx.cs" Inherits="MoviesManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movies Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Movies Management</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:GridView ID="gvMovies" runat="server" AutoGenerateColumns="False" DataKeyNames="MovieID" OnRowEditing="gvMovies_RowEditing" OnRowUpdating="gvMovies_RowUpdating" OnRowDeleting="gvMovies_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="MovieID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="GenreID" HeaderText="Genre ID" />
                    <asp:BoundField DataField="Director" HeaderText="Director" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" />
                    <asp:BoundField DataField="Duration" HeaderText="Duration" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="txtTitle" runat="server" ToolTip="Title"></asp:TextBox>
            <asp:TextBox ID="txtDescription" runat="server" ToolTip="Description"></asp:TextBox>
            <asp:TextBox ID="txtGenreID" runat="server" ToolTip="Genre ID"></asp:TextBox>
            <asp:TextBox ID="txtDirector" runat="server" ToolTip="Director"></asp:TextBox>
            <asp:TextBox ID="txtReleaseDate" runat="server" ToolTip="Release Date"></asp:TextBox>
            <asp:TextBox ID="txtDuration" runat="server" ToolTip="Duration"></asp:TextBox>
            <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" OnClick="btnAddMovie_Click" />
        </div>
    </form>
</body>
</html>
