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
    public partial class NewCategory : Form
    {
        SqlConnection session = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\malon\source\repos\InventorySystem\InventorySystem\maindb.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public NewCategory()
        {
            InitializeComponent();
        }

        public void clearFields()
        {
            catName.Clear();
        }

        private void addCatButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm adding new category", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tbCategory(name)VALUES(@name)", session);
                    cmd.Parameters.AddWithValue("@name", catName.Text);
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

        private void backButton_Click(object sender, EventArgs e)
        {
            clearFields();
            this.Dispose();
        }
    }
}
