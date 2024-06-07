<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoviesManagementDataEntry.aspx.cs" Inherits="MoviesManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movies Management Data Entry</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Add New Movie</h2>
            <label for="txtTitle">Title:</label>
            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            <br />
            <label for="txtDescription">Description:</label>
            <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
            <br />
            <label for="txtGenreID">Genre ID:</label>
            <asp:TextBox ID="txtGenreID" runat="server"></asp:TextBox>
            <br />
            <label for="txtDirector">Director:</label>
            <asp:TextBox ID="txtDirector" runat="server"></asp:TextBox>
            <br />
            <label for="txtReleaseDate">Release Date:</label>
            <asp:TextBox ID="txtReleaseDate" runat="server"></asp:TextBox>
            <br />
            <label for="txtDuration">Duration:</label>
            <asp:TextBox ID="txtDuration" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnAddMovie" runat="server" Text="Add Movie" OnClick="btnAddMovie_Click" />
            <br />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
