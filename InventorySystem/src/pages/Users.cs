using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;

namespace InventorySystem.src.pages
{
    public partial class Users : KryptonForm
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Users()
        {
            InitializeComponent();
            LoadUsers();
        }

        public void LoadUsers()
        {
            int i = 0;
            dvgUsers.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbUser WHERE CONCAT(username, email, fullname, phone) LIKE '%" + searchBox.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgUsers.Rows.Add(i, dr[0].ToString(), dr[2].ToString(), dr[1].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            session.Close();

            userCount.Text = i.ToString();
        }

        private void dvgUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dvgUsers.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                NewUser newUser = new NewUser();
                newUser.username.Text = dvgUsers.Rows[e.RowIndex].Cells[1].Value.ToString();
                newUser.email.Text = dvgUsers.Rows[e.RowIndex].Cells[2].Value.ToString();
                newUser.fullname.Text = dvgUsers.Rows[e.RowIndex].Cells[3].Value.ToString();
                newUser.password.Text = dvgUsers.Rows[e.RowIndex].Cells[4].Value.ToString();
                newUser.phone.Text = dvgUsers.Rows[e.RowIndex].Cells[5].Value.ToString();

                newUser.addButton.Enabled = false;
                newUser.updateButton.Enabled = true;
                newUser.ShowDialog();
            }
            else if (colName == "del")
            {
                if (MessageBox.Show("Please confirm deleting this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    session.Open();
                    cmd = new SqlCommand("DELETE FROM tbUser WHERE phone LIKE '"+ dvgUsers.Rows[e.RowIndex].Cells[5].Value.ToString() +"'", session);
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("User deleted successfully!");
                }
            }
            LoadUsers();
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            newUser.addButton.Enabled = true;
            newUser.updateButton.Enabled = false;
            newUser.ShowDialog();
            LoadUsers();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
