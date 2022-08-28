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
using ComponentFactory.Krypton.Toolkit;

namespace InventorySystem.src.pages
{
    public partial class LoginForm : KryptonForm
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public LoginForm()
        {
            InitializeComponent();
            loginPassword.UseSystemPasswordChar = true;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void clearForm()
        {
            loginUsername.Clear();
            loginPassword.Clear();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            try
            {
                cmd = new SqlCommand("SELECT * FROM tbUser WHERE username=@username AND password=@password", session);
                cmd.Parameters.AddWithValue("@username", loginUsername.Text);
                cmd.Parameters.AddWithValue("@password", loginPassword.Text);
                session.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("Welcome " + dr["fullname"].ToString() + " ", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdminMainForm main = new AdminMainForm();
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                session.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void loginBackButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void checkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPassword.Checked == false)
            {
                loginPassword.UseSystemPasswordChar = true;
            }
            else
            {
                loginPassword.UseSystemPasswordChar = false;
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
