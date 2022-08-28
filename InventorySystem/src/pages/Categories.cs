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
    public partial class Categories : KryptonForm
    {

        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Categories()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dvgCategory.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCategory WHERE CONCAT(category_id, name) LIKE '%" + searchBox.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            session.Close();

            categoryCount.Text = i.ToString();
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            NewCategory newCategory = new NewCategory();
            newCategory.ShowDialog();
            LoadCategory();
        }

        private void dvgProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dvgCategory.Columns[e.ColumnIndex].Name;
            if (colName == "del")
            {
                if (MessageBox.Show("Please confirm deleting this category?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    session.Open();
                    cmd = new SqlCommand("DELETE FROM tbCategory WHERE category_id LIKE '" + dvgCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", session);
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Category deleted successfully!");
                }
            }
            LoadCategory();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }
    }
}