using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class AttendantMainForm : KryptonForm
    {
        public AttendantMainForm()
        {
            InitializeComponent();
            openChildForm(new src.pages.SalesAttendant());
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

        private void pictureBoxClose_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBoxMinimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxNormal_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }


        private void attendantSidebarSales_Click_1(object sender, EventArgs e)
        {
            openChildForm(new src.pages.SalesAttendant());
        }

        private void attendantSidebarCustomer_Click_1(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Customer());
        }

        private void attendantSidebarCat_Click_1(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Categories());
        }

        private void attendantSidebarProducts_Click_1(object sender, EventArgs e)
        {
            openChildForm(new src.pages.Products());
        }

        private void attendantSidebarLogout_Click_1(object sender, EventArgs e)
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
