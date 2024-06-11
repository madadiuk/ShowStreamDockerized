<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManagement.aspx.cs" Inherits="AdminSystem.UserManagement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body>
    <header>
        <div class="container">
            <div id="branding">
                <h1>User Management</h1>
            </div>
            <nav>
                <ul>
                    <li><a href="UserAccountsList.aspx" class="btn">User Accounts List</a></li>
                    <li><a href="UserManagement.aspx" class="btn">User Management</a></li>
                </ul>
            </nav>
        </div>
    </header>

    <div class="container">
        <div class="main-content">
            <form id="form1" runat="server">
                <div class="form-group">
                    <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required." ForeColor="Red" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ForeColor="Red" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ForeColor="Red" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email format." ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblRole" runat="server" Text="Role:"></asp:Label>
                    <asp:DropDownList ID="ddlRole" runat="server">
                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblUserID" runat="server" Text="User ID:"></asp:Label>
                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnFindUser" runat="server" Text="Find User" OnClick="btnFindUser_Click" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSearchUsername" runat="server" Text="Search Username:"></asp:Label>
                    <asp:TextBox ID="txtSearchUsername" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSearchEmail" runat="server" Text="Search Email:"></asp:Label>
                    <asp:TextBox ID="txtSearchEmail" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSearchRole" runat="server" Text="Search Role:"></asp:Label>
                    <asp:DropDownList ID="ddlSearchRole" runat="server">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <div class="form-group">
                   <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowEditing="gvUsers_RowEditing" OnRowDeleting="gvUsers_RowDeleting" OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit">
                       <Columns>
                           <asp:BoundField DataField="UserID" HeaderText="User ID" ReadOnly="True" />
                           <asp:TemplateField HeaderText="Username">
                               <ItemTemplate>
                                   <%# Eval("Username") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtEditUsername" runat="server" Text='<%# Bind("Username") %>' />
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Email">
                               <ItemTemplate>
                                   <%# Eval("Email") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:TextBox ID="txtEditEmail" runat="server" Text='<%# Bind("Email") %>' TextMode="Email" />
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Role">
                               <ItemTemplate>
                                   <%# Eval("Role") %>
                               </ItemTemplate>
                               <EditItemTemplate>
                                   <asp:DropDownList ID="ddlEditRole" runat="server" SelectedValue='<%# Bind("Role") %>'>
                                       <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                       <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                   </asp:DropDownList>
                               </EditItemTemplate>
                           </asp:TemplateField>
                           <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                       </Columns>
                   </asp:GridView>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
