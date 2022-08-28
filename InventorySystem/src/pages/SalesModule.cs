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
    public partial class SalesModule : KryptonForm

    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public SalesModule()
        {
            InitializeComponent();
            LoadProducts();
            LoadCustomer();
        }

        public void LoadProducts()
        {
            dvgProducts.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(product_id, name, price, description, category) LIKE '%" + customerSearch.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dvgProducts.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            session.Close();
        }

        public void LoadCustomer()
        {
            dvgCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCustomer WHERE CONCAT(customer_id, name, phone) LIKE '%" + customerSearch.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
    
                dvgCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            session.Close();
        }

        private void dvgCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customerID.Text = dvgCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            customerName.Text = dvgCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dvgProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            productIDText.Text = dvgProducts.Rows[e.RowIndex].Cells[0].Value.ToString();
            productNameText.Text = dvgProducts.Rows[e.RowIndex].Cells[1].Value.ToString();
            productPriceText.Text = dvgProducts.Rows[e.RowIndex].Cells[3].Value.ToString();
            qty = Convert.ToInt16(dvgProducts.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void qtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(qtyUpDown.Value) > qty)
            {
                MessageBox.Show("Quantity specified exceeds items in stock", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtyUpDown.Value = qtyUpDown.Value - 1;
                return;
            }
            if (Convert.ToInt16(qtyUpDown.Value) > 0)
            {
                int total = Convert.ToInt16(productPriceText.Text) * Convert.ToInt16(qtyUpDown.Value);
                productTotalText.Text = total.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (customerID.Text == "")
                {
                    MessageBox.Show("Please select a customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (productIDText.Text == "")
                {
                    MessageBox.Show("Please select a product", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Confirm adding new sale", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbSales(date, product_id, product_name, customer_id, customer_name, quantity, price, total)VALUES(@date, @product_id, @product_name, @customer_id, @customer_name,  @quantity,  @price,  @total)", session);
                    cmd.Parameters.AddWithValue("@date", salesDTP.Value);
                    cmd.Parameters.AddWithValue("@product_id", Convert.ToInt16(productIDText.Text));
                    cmd.Parameters.AddWithValue("@product_name", productNameText.Text);
                    cmd.Parameters.AddWithValue("@customer_id", Convert.ToInt16(customerID.Text));
                    cmd.Parameters.AddWithValue("@customer_name", customerName.Text);
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt16(qtyUpDown.Value));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt16(productPriceText.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt16(productTotalText.Text));
                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Sale added successfully!");


                    cmd = new SqlCommand("UPDATE tbProduct SET quantity=(quantity-@quantity) WHERE product_id LIKE '" + productIDText.Text + "'", session);
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt16(qtyUpDown.Value));

                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                    

                    SalesInvoice invoice = new SalesInvoice();
                    invoice.custID.Text = customerID.Text;
                    invoice.custName.Text = customerName.Text;
                    invoice.prodID.Text = productIDText.Text;
                    invoice.prodName.Text = productNameText.Text;
                    invoice.date.Text = salesDTP.Value.ToString();
                    invoice.price.Text = productPriceText.Text;
                    invoice.qty.Text = qtyUpDown.Value.ToString();
                    invoice.total.Text = productTotalText.Text;
                    invoice.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            LoadProducts();
        }

        public void clearFields()
        {
            productIDText.Clear();
            productNameText.Clear();
            customerID.Clear();
            customerName.Clear();
            qtyUpDown.Value = 1;
            productPriceText.Clear();
            productTotalText.Clear();
            salesDTP.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            clearFields();
            this.Dispose();
        }

        private void customerSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void productSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
