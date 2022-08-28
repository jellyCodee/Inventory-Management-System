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
    public partial class NewProduct : Form
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public NewProduct()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            productCatComboBox.Items.Clear();
            cmd = new SqlCommand("SELECT name FROM tbCategory", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                productCatComboBox.Items.Add(dr[0].ToString());
            }
            dr.Close();
            session.Close();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            clearFields();
            this.Dispose();
        }

        private void clearFields()
        {
            productName.Clear();
            productQty.Clear();
            productPrice.Clear();
            productDesc.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm adding new product", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbProduct(name, quantity, price, description, category)VALUES(@name, @quantity, @price, @description, @category)", session);
                    cmd.Parameters.AddWithValue("@name", productName.Text);
                    cmd.Parameters.AddWithValue("@quantity", productQty.Text);
                    cmd.Parameters.AddWithValue("@price", productPrice.Text);
                    cmd.Parameters.AddWithValue("@description", productDesc.Text);
                    cmd.Parameters.AddWithValue("@category", productCatComboBox.Text);
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Product added successfully!");
                    clearFields();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm updating product", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE tbProduct SET name=@name, quantity=@quantity, price=@price, description=@description, category=@category WHERE name LIKE '" + productName.Text + "'", session);
                    cmd.Parameters.AddWithValue("@name", productName.Text);
                    cmd.Parameters.AddWithValue("@quantity", productQty.Text);
                    cmd.Parameters.AddWithValue("@price", productPrice.Text);
                    cmd.Parameters.AddWithValue("@description", productDesc.Text);
                    cmd.Parameters.AddWithValue("@category", productCatComboBox.Text);
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Product updated successfully!");
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
