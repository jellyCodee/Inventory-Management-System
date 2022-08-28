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
    public partial class Products : KryptonForm
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public Products()
        {
            InitializeComponent();
            LoadProducts();
        }

        public void LoadProducts()
        {
            int i = 0;
            dvgProducts.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(product_id, name, price, description, category) LIKE '%"+searchBox.Text+"%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgProducts.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            session.Close();

            productCount.Text = i.ToString();
        }

        private void addProduct_Click(object sender, EventArgs e)
        {
            NewProduct newProduct = new NewProduct();
            newProduct.btnAdd.Enabled = true;
            newProduct.btnUpdate.Enabled = false;
            newProduct.ShowDialog();
            LoadProducts();
        }

        private void dvgProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dvgProducts.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                NewProduct newProduct = new NewProduct();
                newProduct.productName.Text = dvgProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
                newProduct.productQty.Text = dvgProducts.Rows[e.RowIndex].Cells[2].Value.ToString();
                newProduct.productPrice.Text = dvgProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
                newProduct.productDesc.Text = dvgProducts.Rows[e.RowIndex].Cells[4].Value.ToString();
                newProduct.productCatComboBox.Text = dvgProducts.Rows[e.RowIndex].Cells[5].Value.ToString();

                newProduct.btnAdd.Enabled = false;
                newProduct.btnUpdate.Enabled = true;
                newProduct.ShowDialog();
            }
            else if (colName == "del")
            {
                if (MessageBox.Show("Please confirm deleting this product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    session.Open();
                    cmd = new SqlCommand("DELETE FROM tbProduct WHERE product_id LIKE '" + dvgProducts.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", session);
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Product deleted successfully!");
                }
            }
            LoadProducts();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
