<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoFilesManagementDataEntry.aspx.cs" Inherits="VideoFilesManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Video Files Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Video Files Management</h2>
            <asp:DropDownList ID="ddlFilterVideoQuality" runat="server"></asp:DropDownList>
            <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />
            <asp:GridView ID="gvVideoFiles" runat="server" AutoGenerateColumns="False" DataKeyNames="VideoFileID" OnRowEditing="gvVideoFiles_RowEditing" OnRowUpdating="gvVideoFiles_RowUpdating" OnRowDeleting="gvVideoFiles_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="VideoFileID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="MovieID" HeaderText="Movie ID" />
                    <asp:BoundField DataField="SeriesID" HeaderText="Series ID" />
                    <asp:BoundField DataField="EpisodeID" HeaderText="Episode ID" />
                    <asp:BoundField DataField="VideoQuality" HeaderText="Video Quality" />
                    <asp:BoundField DataField="FilePath" HeaderText="File Path" />
                    <asp:BoundField DataField="FileSize" HeaderText="File Size" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:DropDownList ID="ddlMovieID" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="ddlSeriesID" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="ddlEpisodeID" runat="server"></asp:DropDownList>
            <asp:TextBox ID="txtVideoQuality" runat="server" ToolTip="Video Quality"></asp:TextBox>
            <asp:TextBox ID="txtFilePath" runat="server" ToolTip="File Path"></asp:TextBox>
            <asp:TextBox ID="txtFileSize" runat="server" ToolTip="File Size"></asp:TextBox>
            <asp:Button ID="btnAddVideoFile" runat="server" Text="Add Video File" OnClick="btnAddVideoFile_Click" />
        </div>
    </form>
</body>
</html>
