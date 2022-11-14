using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User_Login_With_SQL.Connection;
using User_Login_With_SQL.Models;

namespace User_Login_With_SQL
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private void UserSetting_Load(object sender, EventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textName.Text) || string.IsNullOrWhiteSpace(textAge.Text) || string.IsNullOrWhiteSpace(textEmail.Text) || string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Please do not leave mandatory fields blank. ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(SqlCommands.SqlConnectionURL))
            {

                connection.Open();

                SqlCommand filterCmd = new SqlCommand("Select Email From [User] where Email='" + textEmail.Text + "'", connection);

                using (SqlDataReader reader = filterCmd.ExecuteReader())
                {
                    if (reader.HasRows == true)
                    {
                        MessageBox.Show("Email has already been received. Please enter a new email address.", "Email Registered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    reader.Close();
                }

                SqlCommand cmd = new SqlCommand("Insert Into [User](Name,Age,Email,Password,Country) Values(@Name ,@Age ,@Email ,@Password ,@Country)", connection);

                cmd.Parameters.AddWithValue("@Name", textName.Text);
                cmd.Parameters.AddWithValue("@Age", textAge.Text);
                cmd.Parameters.AddWithValue("@Email", textEmail.Text);
                cmd.Parameters.AddWithValue("@Password", textPassword.Text);
                cmd.Parameters.AddWithValue("@Country", textCountry.Text);

                MessageBox.Show("Your information has been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmd.ExecuteNonQuery();

                connection.Close();

                Close();
            }


        }
    }
}
