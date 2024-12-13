<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactListUpload.aspx.cs" Inherits="MachineTest.ContactListUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact List Upload</title>

    <link type="text/css" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function showAlert(message) {
            alert(message);
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <div class="container">

            <div class="card">

                <div class="card-header center">
                 <h5>Machine Test --  Notepad to SQL insert data</h5> 
             </div>

                <div class="card-body">
                    <asp:FileUpload ID="FileUploadControl" runat="server" />
                    <asp:Button ID="btnUploadButton" runat="server" Text="Submit" OnClick="btnUploadButton_Click" />
                    <br />
                    <asp:Label ID="lblStatusLabel" runat="server" Text="" ForeColor="Green"></asp:Label>
                </div>
            </div>


        </div>

    </form>


</body>
</html>
