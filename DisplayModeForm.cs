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
    /// <summary>
    /// Represents the selected display mode of an object (Shaded or wireframe).
    /// </summary>
    public partial class DisplayModeForm : Form
    {
        #region Properties

        /// <summary>
        /// Gets or sets the wire boolean flag.
        /// </summary>
        public static bool WireMode { get; set; } = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayModeForm"/> class.
        /// </summary>
        public DisplayModeForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Instance Form 

        internal static bool CreateAndShowDialog(Form parent)
        {
            using (DisplayModeForm form = new DisplayModeForm())
            {
                if (form.ShowDialog(parent) == DialogResult.OK)
                {
                    WireMode = form.RBWire.Checked;
                    return true;
                }
                return false;
            }
        }

        #endregion
    }
}
