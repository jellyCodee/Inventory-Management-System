using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem.src.pages
{
    public partial class SalesInvoice : KryptonForm
    {
        public SalesInvoice()
        {
            InitializeComponent();
        }

        private void Print(Panel panel)
        {
            PrinterSettings ps = new PrinterSettings();
            mainPanel = panel;
            getprintarea(panel);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memoryImg;

        private void getprintarea(Panel panel)
        {
            memoryImg = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(memoryImg, new Rectangle(0, 0, panel.Width, panel.Height));
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryImg, (pagearea.Width / 2) - (this.mainPanel.Width / 2), this.mainPanel.Location.Y);
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            Print(this.mainPanel);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
