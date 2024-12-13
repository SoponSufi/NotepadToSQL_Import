<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pattern.aspx.cs" Inherits="Diamond_Interview.Pattern" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Diamond Pattern Generator</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Diamond Pattern Generator</h2>
            <label for="txtInput">Enter a Number:</label>
            <asp:TextBox ID="txtInput" runat="server"></asp:TextBox>
            <asp:Button ID="btnGenerate" runat="server" Text="Generate Pattern" OnClick="btnGenerate_Click" />
        </div>
        <div>
            <h3>Pattern Output:</h3>
            <asp:Literal ID="litOutput" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
