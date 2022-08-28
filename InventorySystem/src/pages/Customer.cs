using ComponentFactory.Krypton.Toolkit;
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
    public partial class Customer : KryptonForm

    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Customer()
        {
            InitializeComponent();
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dvgCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT(customer_id, name, phone) LIKE '%" + searchBox.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dvgCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            session.Close();

            customerCount.Text = i.ToString();
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            NewCustomer newCustomer = new NewCustomer();
            newCustomer.ShowDialog();
            LoadCustomer();
        }

        private void dvgCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dvgCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                NewCustomer newCustomer = new NewCustomer();
                newCustomer.customerName.Text = dvgCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                newCustomer.customerPhone.Text = dvgCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

                newCustomer.addCusButton.Enabled = false;
                newCustomer.btnUpdate.Enabled = true;
                newCustomer.ShowDialog();
            }
            else if (colName == "del")
            {
                if (MessageBox.Show("Please confirm deleting this customer?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    session.Open();
                    cmd = new SqlCommand("DELETE FROM tbCustomer WHERE customer_id LIKE '" + dvgCustomer.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", session);
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Customer deleted successfully!");
                }
            }
            LoadCustomer();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

    }
}
