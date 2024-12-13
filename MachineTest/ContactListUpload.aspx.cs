using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MachineTest
{
    public class ContactList
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public partial class ContactListUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                string fileExtension = Path.GetExtension(FileUploadControl.FileName).ToLower();

                if (fileExtension == ".txt")
                {
                    try
                    {
                        string filePath = Server.MapPath("~/UploadedFiles/") + FileUploadControl.FileName;
                        FileUploadControl.SaveAs(filePath);

                        var Allcontacts = ExtractAllContacts(filePath);
                        int AllcontactsCount = Allcontacts.Count;

                        var contacts = ExtractUniqueContacts(filePath);
                        int totalContactsInFile = contacts.Count;

                        int uploadedCount = SaveUniqueContactsToDatabase(contacts);

                        

                        string alertMessage = $"Total unique emails in file: {totalContactsInFile}, Total Uploaded Email: {AllcontactsCount}";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", $"showAlert('{alertMessage}');", true);

                        lblStatusLabel.Text = $"Total unique emails in file: {totalContactsInFile}, Total Uploaded Email: {AllcontactsCount}";

                    }
                    catch (Exception ex)
                    {

                        lblStatusLabel.Text = "Error Msg:" + ex.Message;
                    }
                }
                else
                {
                    lblStatusLabel.Text = "Only .txt files are allowed!";
                }
            }
            else
            {
                lblStatusLabel.Text = "Please select a file to upload.";
            }

        }


        private List<ContactList> ExtractAllContacts(string filePath)
        {
            var contactList = new List<ContactList>();

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    string name = parts[0].Trim();
                    string email = parts[1].Trim();
                    string phone = parts[2].Trim();

                    contactList.Add(new ContactList
                    {
                        Name = name,
                        Email = email,
                        Phone = phone
                    });
                }
            }
            return contactList;
        }

        private List<ContactList> ExtractUniqueContacts(string filePath)
        {
            var contactList = new List<ContactList>();
            var emailSet = new HashSet<string>();  

            foreach (var line in File.ReadLines(filePath))
            { 
                var parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    string name = parts[0].Trim();
                    string email = parts[1].Trim();
                    string phone = parts[2].Trim();

                     
                    if (!emailSet.Contains(email))
                    {
                        emailSet.Add(email);
                        contactList.Add(new ContactList
                        {
                            Name = name,
                            Email = email,
                            Phone = phone
                        });
                    }
                }
            }
            return contactList;
        }


        private int SaveUniqueContactsToDatabase(List<ContactList> contacts)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            int uploadedCount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var contact in contacts)
                {
                    using (SqlCommand command = new SqlCommand("usp_InsertContact", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Name", contact.Name);
                        command.Parameters.AddWithValue("@Email", contact.Email);
                        command.Parameters.AddWithValue("@Phone", contact.Phone);

                        try
                        {
                            command.ExecuteNonQuery();
                            uploadedCount++;
                        }
                        catch (SqlException ex)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + ex.Message + "');", true);
                            
                        }
                    }
                }
            }

            return uploadedCount;
        }

    }
}