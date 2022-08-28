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

namespace InventorySystem
{
    public partial class AdminMainForm : KryptonForm
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        // open subform
        private KryptonForm activeForm = null;
        private void openChildForm(KryptonForm childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(childForm);
            mainPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void adminMainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBoxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxNormal_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            } else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            
        }

        private void sidebarUsers_Click(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Users());
        }

        private void sidebarProducts_Click(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Products());
        }

        private void sidebarCat_Click(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Categories());
        }

        private void sidebarCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Customer());
        }

        private void sidebarSales_Click(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Sales());
        }

        private void sidebarLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm logging out of the system?", "Logging out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                src.pages.LoginForm login = new src.pages.LoginForm();
                this.Hide();
                login.ShowDialog();
                
            }
        }
    }
}
