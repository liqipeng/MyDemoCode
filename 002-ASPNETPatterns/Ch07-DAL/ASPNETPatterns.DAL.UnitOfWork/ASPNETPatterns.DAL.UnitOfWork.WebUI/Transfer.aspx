<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="ASPNETPatterns.DAL.UnitOfWork.WebUI.Transfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        A:
        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        B:
        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
        <br />
        <asp:Button ID="btnAToB" runat="server" OnClick="btnAToB_Click" Text="A-&gt;B" />
&nbsp;<asp:TextBox ID="txtTransferAmount" runat="server" Width="107px"></asp:TextBox>
&nbsp;<asp:Button ID="btnBToA" runat="server" Text="B-&gt;A" OnClick="btnBToA_Click" />
        <br />
    
    </div>
    </form>
</body>
</html>
