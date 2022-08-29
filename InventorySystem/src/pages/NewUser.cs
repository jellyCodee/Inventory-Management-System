using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace InventorySystem.src.pages
{
    public partial class NewUser : Form
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public NewUser()
        {
            InitializeComponent();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Confirm adding new user", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbUser(username, fullname, email, password, phone, is_superuser)VALUES(@username, @fullname, @email, @password, @phone, @is_superuser)", session);
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@fullname", fullname.Text);
                    cmd.Parameters.AddWithValue("@email", email.Text);
                    cmd.Parameters.AddWithValue("@password", password.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    cmd.Parameters.AddWithValue("@is_superuser", 0);
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("User added successfully!");
                    clearFields();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            clearFields();
            this.Dispose();
        }

        public void clearFields()
        {
            username.Clear();
            fullname.Clear();
            email.Clear();
            password.Clear();
            phone.Clear();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm updating user", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbUser SET username, fullname=@fullname, email=@email, password=@password, phone=@phone WHERE phone LIKE '"+phone.Text+"'", session);
                    cmd.Parameters.AddWithValue("@username", username.Text);
                    cmd.Parameters.AddWithValue("@fullname", fullname.Text);
                    cmd.Parameters.AddWithValue("@email", email.Text);
                    cmd.Parameters.AddWithValue("@password", password.Text);
                    cmd.Parameters.AddWithValue("@phone", phone.Text);
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("User updated successfully!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
