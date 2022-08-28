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

namespace InventorySystem.src.pages
{
    public partial class NewCustomer : Form
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void addCusButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm adding new customer", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbCustomer(name, phone)VALUES(@name, @phone)", session);
                    cmd.Parameters.AddWithValue("@name", customerName.Text);
                    cmd.Parameters.AddWithValue("@phone", customerPhone.Text);
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Category added successfully!");
                    clearFields();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void clearFields()
        {
            customerName.Clear();
            customerPhone.Clear();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            clearFields();
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm updating customer", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbCustomer SET name=@name, phone=@phone WHERE name LIKE '" + customerName.Text + "'", session);
                    cmd.Parameters.AddWithValue("@name", customerName.Text);
                    cmd.Parameters.AddWithValue("@phone", customerPhone.Text);

                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Customer updated successfully!");
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
