using Occt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCCSVGViewer
{
    /// <summary>
    /// The class represented the OCC properties.
    /// </summary>
    [Serializable()]
    public class OCCModel
    {
        #region Private Fields

        /// <summary>
        /// The top gradient background color for the view.
        /// </summary>
        private System.Drawing.Color mTopColor;

        /// <summary>
        /// The bottom gradient background color for the view.
        /// </summary>
        private System.Drawing.Color mBottomColor;

        /// <summary>
        /// the Background gradient color flag.
        /// </summary>
        private bool mGradientColor;

        /// <summary>
        /// The axis's visibility flag.
        /// </summary>
        private bool mAxisVisibility;

        /// <summary>
        /// The Triedron's visibility flag.
        /// </summary>
        private bool mTrihedronVisibility;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the OCCViewer.
        /// </summary>
        [Browsable(false)]
        public OCCViewer OCCView { get; set; }

        /// <summary>
        /// Gets or sets the OCCDoc.
        /// </summary>
        [Browsable(false)]
        public OCCDocObject DocObject { get; set; }

        [Category("General")]
        [ReadOnly(true)]
        public string Name { get; set; }

        #region Background

        /// <summary>
        /// Gets or sets the top gradient background color for the view.
        /// </summary>
        [Browsable(true)]
        [Category("Representation")]
        [Description("Defines the top gradient background color for the view.")]
        public System.Drawing.Color TopColor
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.TopColor;
                }
                return this.mTopColor;
            }
            set
            {
                if (this.mTopColor != value)
                {
                    this.mTopColor = value;
                    if (this.OCCView != null)
                    {
                        this.OCCView.TopColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the bottom gradient background color for the view.
        /// </summary>
        [Browsable(true)]
        [Category("Representation")]
        [Description("Defines the bottom gradient background color for the view.")]
        public System.Drawing.Color BottomColor
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.BottomColor;
                }
                return this.mBottomColor;
            }
            set
            {
                if (this.mBottomColor != value)
                {
                    this.mBottomColor = value;
                    if (this.OCCView != null)
                    {
                        this.OCCView.BottomColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the Background gradient color flag.
        /// </summary>
        [Category("Representation")]
        [Description("Choose a single or gradient background color.")]
        public bool GradientColor
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.GradientColor;
                }
                return this.mGradientColor;
            }
            set
            {
                if (this.mGradientColor != value)
                {
                    this.mGradientColor = value;
                    if (this.OCCView != null)
                    {
                        this.OCCView.GradientColor = value;
                    }
                }
            }
        }

        #endregion

        #region Axis

        /// <summary>
        /// Gets or sets the axis's visibility flag.
        /// </summary>
        [Browsable(true)]
        [Category("Axis")]
        [Description("Toggle the visibility of the axis.")]
        public bool AxisVisibility
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.AxisVisibility;
                }
                return this.mAxisVisibility;
            }
            set
            {
                if (value != this.mAxisVisibility)
                {
                    this.mAxisVisibility = value;
                    if (this.OCCView != null)
                    {
                        this.OCCView.AxisVisibility = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the axis.
        /// </summary>
        [Browsable(true)]
        [Category("Axis")]
        [Description("The size of the axis.")]
        public double AxisSize
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.AxisSize;
                }
                return 100; // the default value 100 mm for display of trihedra
            }
            set
            {
                if (value <= 0) value = 10;
                if (this.OCCView != null)
                {
                    this.OCCView.AxisSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the static trihedron's visibility flag.
        /// </summary>
        [Browsable(true)]
        [Category("Trihedron")]
        [Description("Toggle the visibility of the trihedron.")]
        public bool TrihedronVisibility
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.TrihedronVisibility;
                }
                return this.mTrihedronVisibility;
            }
            set
            {
                if (value != this.mTrihedronVisibility)
                {
                    this.mTrihedronVisibility = value;
                    if (this.OCCView != null)
                    {
                        this.OCCView.TrihedronVisibility = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the trihedron.
        /// </summary>
        [Browsable(true)]
        [Category("Trihedron")]
        [Description("The size of the trihedron.\n NOTE: the size is a percent of the window width.")]
        public double TrihedronSize
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.TrihedronSize;
                }
                return 0.1; // the default value 0.1 mm for display of trihedra
            }
            set
            {
                if (value <= 0) value = 0.1;
                if (value > 1) value = 1;
                if (this.OCCView != null)
                {
                    this.OCCView.TrihedronSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the label trihedron's color.
        /// </summary>
        [Browsable(true)]
        [Category("Trihedron")]
        [Description("Defines the trihedron's label color.")]
        public System.Drawing.Color TrihedronColor
        {
            get
            {
                if (this.OCCView != null)
                {
                    return this.OCCView.TrihedronColor;
                }
                return System.Drawing.Color.FromArgb(0, 0, 0);
            }
            set
            {
                if (this.OCCView != null)
                {
                    this.OCCView.TrihedronColor = value;
                }
            }
        }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OCCModel"/> class.
        /// </summary>
        public OCCModel() : base()
        {
            this.Name = "Application";

            // Black, dark blue gradient
            this.mGradientColor = true;
            this.mTopColor = System.Drawing.Color.FromArgb(255, 0, 0, 0);
            this.mBottomColor = System.Drawing.Color.FromArgb(255, 0, 63, 127);

            // Axis
            this.AxisVisibility = true;
            this.mTrihedronVisibility = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the OCCDocObject.
        /// </summary>
        /// <param name="docObject">The parameter doc object.</param>
        public void SetOCCDocObject(OCCDocObject docObject)
        {
            this.DocObject = docObject;
            this.SetOCCView(docObject.View);
        }

        /// <summary>
        /// Sets the OCCViewer.
        /// </summary>
        /// <param name="occView">The parameter OCCViewer.</param>
        public void SetOCCView(OCCViewer occView)
        {
            this.OCCView = occView;
        }

        #endregion
    }
}
