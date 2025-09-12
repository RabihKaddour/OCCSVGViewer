using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCCSVGViewer
{
    public partial class SVGUnitForm : Form
    {
        public SVGUnitForm()
        {
            InitializeComponent();
        }

        internal static bool CreateAndShowDialog(Form parent)
        {
            using (SVGUnitForm form = new SVGUnitForm())
            {
                if (form.ShowDialog(parent) == DialogResult.OK)
                {
                    return true;
                }
                return true;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
