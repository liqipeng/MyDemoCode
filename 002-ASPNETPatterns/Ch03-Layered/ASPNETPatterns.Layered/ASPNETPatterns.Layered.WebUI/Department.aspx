<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="ASPNETPatterns.Layered.WebUI.Department" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        table {
            border-top:1px solid black;
            border-left:1px solid black;
            border-collapse:collapse;
        }
        table td {
            border-bottom:1px solid black;
            border-right:1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>

            <asp:Repeater ID="rptDepartments" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td>Id</td>
                            <td>Name</td>
                            <td>Description</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Id") %></td>
                        <td><%# Eval("Name")%></td>
                        <td><%# Eval("Description") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
