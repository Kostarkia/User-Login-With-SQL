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
    public partial class Login : Form
    {
        LoginModels LoginModels = new LoginModels();

        public Login()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEMail.Text) || string.IsNullOrWhiteSpace(textPassword.Text))
            {
                MessageBox.Show("Please do not leave mandatory fields blank. ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(SqlCommands.SqlConnectionURL))
            {
                connection.Open();

                var command = "Select * from [User] where Email='" + textEMail.Text + "' and Password='" + textPassword.Text + "' ";

                SqlCommand cmd = new SqlCommand(command, connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Login Success","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                        while (reader.Read())
                        {
                            LoginModels.ID = reader[0].ToString();
                            LoginModels.Name = reader[1].ToString();
                            LoginModels.Age = reader[2].ToString();
                            LoginModels.Email = reader[3].ToString();
                            LoginModels.Password = reader[4].ToString();
                            LoginModels.Country = reader[5].ToString();
                        }

                        using (var form = new UserSetting(LoginModels))
                        {
                            form.ShowDialog();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No records found.", "No record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }

                connection.Close();
            }

        }

        private void LinkRegister_LinkClicked(object sender, EventArgs e)
        {
            using (var form = new Register())
            {
                form.ShowDialog();
            }
        }
    }
}

