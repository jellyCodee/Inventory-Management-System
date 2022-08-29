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
    public partial class SalesAttendant : KryptonForm
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public SalesAttendant()
        {
            InitializeComponent();
            LoadSales();
        }

        public void LoadSales()
        {
            int total = 0;
            int i = 0;
            dvgSales.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbSales WHERE CONCAT(sales_id, date, product_id, product_name, customer_id, customer_name) LIKE '%" + search.Text + "%'", session);
            session.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                total += Convert.ToInt32(dr[8].ToString());
                i++;
                dvgSales.Rows.Add(dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
            }
            dr.Close();
            session.Close();

            salesCount.Text = i.ToString();
            totalAmount.Text = total.ToString();
        }

        private void newSales_Click_1(object sender, EventArgs e)
        {
                SalesModule sales = new SalesModule();
                sales.ShowDialog();
                LoadSales();
        }

        private void dvgSales_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dvgSales.Columns[e.ColumnIndex].Name;
            if (colName == "del")
            {
                if (MessageBox.Show("Please confirm deleting this sale?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    session.Open();
                    cmd = new SqlCommand("DELETE FROM tbSales WHERE sales_id LIKE '" + dvgSales.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", session);
                    cmd.ExecuteNonQuery();
                    session.Close();
                    MessageBox.Show("Sales deleted successfully!");

                    cmd = new SqlCommand("UPDATE tbProduct SET quantity=(quantity+@quantity) WHERE product_id LIKE '" + dvgSales.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", session);
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt16(dvgSales.Rows[e.RowIndex].Cells[6].Value.ToString()));

                    session.Open();
                    cmd.ExecuteNonQuery();
                    session.Close();
                }
            }
            LoadSales();
        }

        private void search_TextChanged_1(object sender, EventArgs e)
        {
            LoadSales();
        }
    }
}
