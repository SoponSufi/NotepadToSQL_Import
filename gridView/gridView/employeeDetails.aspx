<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="employeeDetails.aspx.cs" Inherits="gridView.gridView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     
    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
        runat="server" AutoGenerateColumns="false"
        OnDataBound="GridView1_DataBound" 
         >
        <Columns>
            <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" ItemStyle-Width="150" />
            <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" ItemStyle-Width="150" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" ItemStyle-Width="150" />
        </Columns>
    </asp:GridView>

    <br />
    <h3>2nd Grid View</h3>

    <asp:GridView ID="GridView2" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
        runat="server" AutoGenerateColumns="false"
        OnRowDataBound="GridView2_RowDataBound">
    <Columns>
        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" ItemStyle-Width="150" />
        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="150" />
        <asp:BoundField DataField="Salary" HeaderText="Salary" ItemStyle-Width="150" DataFormatString="{0:C}" />
    </Columns>
</asp:GridView>

     <br />
    <h3>3rd Grid View Salary Edit</h3>

    <%--<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" 
    OnRowEditing="GridView3_RowEditing" OnRowUpdating="GridView3_RowUpdating" 
    OnRowCancelingEdit="GridView3_RowCancelingEdit" 
    OnRowDataBound="GridView3_RowDataBound">
    <Columns>
        <asp:BoundField DataField="DepartmentName" HeaderText="Department" ItemStyle-Width="150" />
        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="150" />

        
        <asp:TemplateField HeaderText="Salary" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Salary", "{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtSalary" runat="server" Text='<%# Eval("Salary") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        
        <asp:CommandField ShowEditButton="true" ShowCancelButton="true" />
    </Columns>
</asp:GridView>--%>

     <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false"
    OnRowEditing="GridView3_RowEditing" OnRowUpdating="GridView3_RowUpdating"
    OnRowCancelingEdit="GridView3_RowCancelingEdit" OnRowDataBound="GridView3_RowDataBound">
    <Columns>
        <asp:BoundField DataField="DepartmentName" HeaderText="Department" ItemStyle-Width="150" />
        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="150" />

       
        <asp:TemplateField HeaderText="Salary" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblSalary" runat="server" Text='<%# Eval("Salary", "{0:C}") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtSalary" runat="server" Text='<%# Eval("Salary") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
         
        <asp:CommandField ShowEditButton="true" ShowCancelButton="true" />
    </Columns>
</asp:GridView>



</asp:Content>
