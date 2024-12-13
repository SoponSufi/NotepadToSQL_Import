using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diamond_Interview
{
    public partial class Pattern : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int inputNumber;

            // Validate the input
            if (int.TryParse(txtInput.Text, out inputNumber) && inputNumber > 0)
            {
                // Generate the diamond pattern
                string pattern = GenerateDiamondPattern(inputNumber);

                // Display the pattern in the Literal control
                litOutput.Text = $"<pre>{pattern}</pre>";
            }
            else
            {
                litOutput.Text = "Please enter a valid positive number.";
            }
        }

        private string GenerateDiamondPattern(int n)
        {
            StringBuilder sb = new StringBuilder();

            // Generate top half of the diamond including the middle row
            for (int i = 0; i < n; i++)
            {
                sb.AppendLine(GeneratePascalRow(i, n));
            }

            // Generate bottom half of the diamond (reversed)
            for (int i = n - 2; i >= 0; i--)
            {
                sb.AppendLine(GeneratePascalRow(i, n));
            }

            return sb.ToString();
        }

        private string GeneratePascalRow(int row, int totalRows)
        {
            StringBuilder sb = new StringBuilder();

            // Add spaces for alignment
            sb.Append(new string(' ', (totalRows - row - 1) * 2));

            // Generate Pascal's triangle values for the row
            int value = 1;
            for (int j = 0; j <= row; j++)
            {
                sb.Append(value);

                // Add a space between numbers
                if (j < row)
                {
                    sb.Append(" ");
                }

                // Calculate the next value in Pascal's triangle
                value = value * (row - j) / (j + 1);
            }

            return sb.ToString();
        }

    }
}