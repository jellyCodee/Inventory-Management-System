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
    public partial class WelcomeForm : KryptonForm
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Close Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            src.pages.LoginForm login = new src.pages.LoginForm();
            this.Hide();
            login.ShowDialog();
        }

        private void attendantButton_Click(object sender, EventArgs e)
        {
            src.pages.LoginForm login = new src.pages.LoginForm();
            this.Hide();
            login.ShowDialog();
        }

    }
}
