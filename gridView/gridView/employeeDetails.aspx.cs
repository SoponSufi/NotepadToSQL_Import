using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace gridView
{
    public partial class gridView : System.Web.UI.Page
    {
      

        private string _previousDepartment = string.Empty;
        private double _departmentTotalSalary = 0.0;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGridView1();

                BindGrid2();

                BindGrid3();
            }

        }


        public void getGridView1()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select EmployeeName, DepartmentName, Salary from tbl_Employee", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count >0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }






        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            for (int i = GridView1.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = GridView1.Rows[i];
                GridViewRow previousRow = GridView1.Rows[i - 1];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }


        private void BindGrid2()
        {
            // Update connection string name as needed
            string connectionString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DepartmentName, EmployeeName, Salary FROM tbl_Employee ORDER BY DepartmentName", conn);

                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the current row's department and salary
                string currentDepartment = DataBinder.Eval(e.Row.DataItem, "DepartmentName").ToString();
                double salary = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Salary"));

                // Check if the department has changed (or it's the first row)
                if (_previousDepartment != string.Empty && _previousDepartment != currentDepartment)
                {
                    // Add a summary row for the previous department
                    AddSummaryRow(_previousDepartment, _departmentTotalSalary, e.Row.RowIndex);

                    // Reset the department total for the new department
                    _departmentTotalSalary = 0.0;
                }

                // Add the salary to the running total for the current department
                _departmentTotalSalary += salary;

                // Update _previousDepartment to the current department name
                _previousDepartment = currentDepartment;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Add a summary row for the last department
                AddSummaryRow(_previousDepartment, _departmentTotalSalary, e.Row.RowIndex);
            }
        }

        private void AddSummaryRow(string department, double totalSalary, int rowIndex)
        {
            // Create a new row to display the department total
            GridViewRow summaryRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell cell = new TableCell();
            cell.ColumnSpan = 2; // Adjusting to fit the department column and employee column
            cell.Text = $"Total for {department}:";
            cell.HorizontalAlign = HorizontalAlign.Right;

            TableCell totalCell = new TableCell();
            totalCell.Text = totalSalary.ToString("C");
            totalCell.HorizontalAlign = HorizontalAlign.Right;

            summaryRow.Cells.Add(cell);
            summaryRow.Cells.Add(totalCell);

            // Add the summary row to the GridView
            GridView2.Controls[0].Controls.AddAt(rowIndex, summaryRow);
        }



        private void BindGrid3()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DepartmentName, EmployeeName, Salary FROM tbl_Employee ORDER BY DepartmentName", conn);

                conn.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                }
            }
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string currentDepartment = DataBinder.Eval(e.Row.DataItem, "DepartmentName").ToString();
                double salary = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Salary"));

                // Accumulate salary per department
                _departmentTotalSalary += salary;

                // Insert a summary row when the department changes
                if (_previousDepartment != currentDepartment && !string.IsNullOrEmpty(_previousDepartment))
                {
                    // Add the department total as a footer row
                    AddSummaryRow3(_previousDepartment, _departmentTotalSalary, e.Row.RowIndex);
                    _departmentTotalSalary = salary; // Reset the department total
                }

                // Update the previous department for the next iteration
                _previousDepartment = currentDepartment;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Add the final department total for the last department
                AddSummaryRow3(_previousDepartment, _departmentTotalSalary, e.Row.RowIndex);
            }
        }

        private void AddSummaryRow3(string department, double totalSalary, int rowIndex)
        {
            GridViewRow summaryRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

            // Department Name cell (spans two columns)
            TableCell cell = new TableCell();
            cell.ColumnSpan = 2;
            cell.Text = $"Total for {department}:";
            cell.HorizontalAlign = HorizontalAlign.Right;

            // Total Salary cell
            TableCell totalCell = new TableCell();
            totalCell.Text = totalSalary.ToString("C");
            totalCell.HorizontalAlign = HorizontalAlign.Right;

            // Add cells to the summary row
            summaryRow.Cells.Add(cell);
            summaryRow.Cells.Add(totalCell);

            // Insert the summary row at the specified index
            GridView3.Controls[0].Controls.AddAt(rowIndex, summaryRow);
        }

        // Row editing
        protected void GridView3_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView3.EditIndex = e.NewEditIndex;
            BindGrid3();
        }

        // Row updating
        protected void GridView3_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView3.Rows[e.RowIndex];

            string employeeName = ((Label)row.FindControl("lblEmployeeName")).Text;
            TextBox txtSalary = (TextBox)row.FindControl("txtSalary");
            double salary = Convert.ToDouble(txtSalary.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

            // Update salary in the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_Employee SET Salary = @Salary WHERE EmployeeName = @EmployeeName", conn);
                cmd.Parameters.AddWithValue("@Salary", salary);
                cmd.Parameters.AddWithValue("@EmployeeName", employeeName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            GridView3.EditIndex = -1;
            BindGrid3();
        }

        // Row canceling edit
        protected void GridView3_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView3.EditIndex = -1;
            BindGrid3();
        }

    }
}