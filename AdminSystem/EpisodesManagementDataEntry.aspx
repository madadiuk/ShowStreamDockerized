<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EpisodesManagementDataEntry.aspx.cs" Inherits="EpisodesManagementDataEntry" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Episodes Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Episodes Management</h2>
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />

            <!-- Filtering Section -->
            <h3>Filter Episodes</h3>
            <asp:TextBox ID="txtFilterSeriesID" runat="server" ToolTip="Series ID" Placeholder="Series ID"></asp:TextBox>
            <asp:TextBox ID="txtFilterSeasonNumber" runat="server" ToolTip="Season Number" Placeholder="Season Number"></asp:TextBox>
            <asp:TextBox ID="txtFilterEpisodeNumber" runat="server" ToolTip="Episode Number" Placeholder="Episode Number"></asp:TextBox>
            <asp:TextBox ID="txtFilterReleaseDate" runat="server" ToolTip="Release Date" Placeholder="Release Date"></asp:TextBox>
            <asp:Button ID="btnFilterEpisodes" runat="server" Text="Filter Episodes" OnClick="btnFilterEpisodes_Click" />
            <br /><br />

            <!-- Episodes Grid -->
            <asp:GridView ID="gvEpisodes" runat="server" AutoGenerateColumns="False" DataKeyNames="EpisodeID" OnRowEditing="gvEpisodes_RowEditing" OnRowUpdating="gvEpisodes_RowUpdating" OnRowDeleting="gvEpisodes_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="EpisodeID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="SeriesID" HeaderText="Series ID" />
                    <asp:BoundField DataField="SeasonNumber" HeaderText="Season Number" />
                    <asp:BoundField DataField="EpisodeNumber" HeaderText="Episode Number" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="Release Date" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="txtSeriesID" runat="server" ToolTip="Series ID" Placeholder="Series ID"></asp:TextBox>
            <asp:TextBox ID="txtSeasonNumber" runat="server" ToolTip="Season Number" Placeholder="Season Number"></asp:TextBox>
            <asp:TextBox ID="txtEpisodeNumber" runat="server" ToolTip="Episode Number" Placeholder="Episode Number"></asp:TextBox>
            <asp:TextBox ID="txtTitle" runat="server" ToolTip="Title" Placeholder="Title"></asp:TextBox>
            <asp:TextBox ID="txtDescription" runat="server" ToolTip="Description" Placeholder="Description"></asp:TextBox>
            <asp:TextBox ID="txtReleaseDate" runat="server" ToolTip="Release Date" Placeholder="Release Date"></asp:TextBox>
            <asp:Button ID="btnAddEpisode" runat="server" Text="Add Episode" OnClick="btnAddEpisode_Click" />
        </div>
    </form>
</body>
</html>
