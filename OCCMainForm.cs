using OCCSVG;
using OCCSVG.Enums;
using OCCSVG.Paths;
using OCCSVG.Serializer;
using OCCSVG.Shapes;
using OCCSVGViewer.Helper;
using Occt;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static Occt.AISX_Axis;
using static Occt.Graphic3d_Camera;
using Rectangle = OCCSVG.Shapes.Rectangle;

namespace OCCSVGViewer
{
    public partial class OCCMainForm : Form
    {
        #region Windows DLLs

        /// <summary>
        /// Sets the drawing mode on the specified device (HDC).
        /// </summary>
        /// <param name="hdc">The device handler.</param>
        /// <param name="fnDrawMode">The drawing mode.</param>
        /// <returns>If the function succeeds, the return value is
        /// the previous mix mode. If the function fails, 
        /// the return value is zero.</returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern int SetROP2(IntPtr hdc, int fnDrawMode);

        /// <summary>
        /// Updates the current position to the specified
        /// point and optionally retrieves the previous position.
        /// </summary>
        /// <param name="hdc">The device handler.</param>
        /// <param name="X">The X position to move to.</param>
        /// <param name="Y">The Y position to move to.</param>
        /// <param name="lpPoint">The last point.</param>
        /// <returns>A nonzero value indicates success. 
        /// Zero indicates failure.</returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);

        /// <summary>
        /// Draws a lines to the specified position.
        /// </summary>
        /// <param name="hdc">The device handler.</param>
        /// <param name="nXEnd">The X end position of the line.</param>
        /// <param name="nYEnd">The Y position of the line.</param>
        /// <returns>A non-zero value indicates success. 
        /// Zero indicates failure. </returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        #endregion

        #region Drawing Rectangle

        /// <summary>
        /// Drawing mode - Binary raster operation code.
        /// </summary>
        private const int R2_MERGEPENNOT = 14;

        /// <summary>
        /// Indicates if the erasing rectangle is visible.
        /// </summary>
        protected bool mIsRectVisible;

        /// <summary>
        /// Erasing rectangle lower x coordinate.
        /// </summary>
        protected int theRectDownX;

        /// <summary>
        /// Erasing rectangle lower y coordinate.
        /// </summary>
        protected int theRectDownY;

        /// <summary>
        /// Erasing rectangle upper x coordinate.
        /// </summary>
        protected int theRectUpX;

        /// <summary>
        /// Erasing rectangle upper y coordinate.
        /// </summary>
        protected int theRectUpY;

        #endregion

        #region Enums

        /// <summary>
        /// Modes of the OCC Viewer.
        /// </summary>
        public enum CurrentAction3d
        {
            /// <summary>
            /// Do nothing.
            /// </summary>
            CurAction3d_Nothing,

            /// <summary>
            /// Dynamic zooming.
            /// </summary>
            CurAction3d_DynamicZooming,

            /// <summary>
            /// Zoom to window.
            /// </summary>
            CurAction3d_WindowZooming,

            /// <summary>
            /// Dynamic panning.
            /// </summary>
            CurAction3d_DynamicPanning,

            /// <summary>
            /// Global panning.
            /// </summary>
            CurAction3d_GlobalPanning,

            /// <summary>
            /// Dynamic rotation.
            /// </summary>
            CurAction3d_DynamicRotation
        }

        /// <summary>
        /// Possible keys pressed.
        /// </summary>
        public enum CurrentPressedKey
        {
            /// <summary>
            /// No key pressed.
            /// </summary>
            CurPressedKey_Nothing,

            /// <summary>
            /// Ctrl pressed.
            /// </summary>
            CurPressedKey_Ctrl,

            /// <summary>
            /// Shift pressed.
            /// </summary>
            CurPressedKey_Shift
        }

        #endregion

        #region Protected Fields

        /// <summary>
        /// Current mode of the viewer.
        /// </summary>
        protected CurrentAction3d myCurrentMode;

        /// <summary>
        /// Current key pressed.
        /// </summary>
        protected CurrentPressedKey myCurrentPressedKey;

        /// <summary>
        /// If true - HiddenLineRemoval is on. Otherwise is off.
        /// </summary>
        protected bool mDegenerateModeIsOn;

        /// <summary>
        /// Antialiasing On/Off (true/false).
        /// </summary>
        protected bool mAntialiasMode;

        /// <summary>
        /// The mouse coordinates.
        /// </summary>
        protected int myXmin, myYmin, myXmax, myYmax;

        /// <summary>
        /// The current mouse position.
        /// </summary>
        protected int theButtonDownX, theButtonDownY;

        /// <summary>
        /// Zoom factor of the OCCViewer.
        /// </summary>
        protected float mCurZoom;// ~ Quantity_Factor

        #endregion

        #region Private Fields

        /// <summary>
        /// Create the OCCDocObject.
        /// </summary>
        private OCCDocObject myDocObject = new OCCDocObject();

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The view.</value>
        public OCCViewer View
        {
            get
            {
                return this.myDocObject.View;
            }
            set
            {
                this.myDocObject.View = value;
            }
        }

        /// <summary>
        /// Gets or sets the doc.
        /// </summary>
        /// <value>The doc.</value>
        public OCCDoc Doc
        {
            get
            {
                return this.myDocObject.Doc;
            }
            set
            {
                this.myDocObject.Doc = value;
            }
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public CurrentAction3d Mode
        {
            get
            {
                return this.myCurrentMode;
            }
            set
            {
                this.myCurrentMode = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OCCMainForm"/> class.
        /// </summary>
        public OCCMainForm()
        {
            InitializeComponent();

            //this.BringToFront();

            this.Canvas3DPanel.MouseWheel += this.Canvas3DPanel_MouseWheel;

            // Initializes extra components
            this.InitializeComponentEx();
        }

        #endregion

        #region Initializer

        /// <summary>
        /// Extra initialization code.
        /// </summary>
        private void InitializeComponentEx()
        {
            this.InitializeInputParameters(true);
        }

        /// <summary>
        /// Initializes the input parameters.
        /// </summary>
        /// <param name="localizing">If set to <c>true</c> the control is being localized.</param>
        public void InitializeInputParameters(bool localizing)
        {
            this.myDocObject.View = new OCCViewer();
            this.myDocObject.Doc = new OCCDoc();

            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
            this.myCurrentPressedKey = CurrentPressedKey.CurPressedKey_Nothing;
            this.mDegenerateModeIsOn = true;
            this.mAntialiasMode = false;
            this.mIsRectVisible = false;

            this.InitializeTree(localizing);
        }

        /// <summary>
        /// Initializes the TreeView.
        /// </summary>
        /// <param name="localizing">If set to <c>true</c> the control is being localized.</param>
        private void InitializeTree(bool localizing)
        {
            this.Root = new OCCModel();
            this.Root.SetOCCDocObject(this.myDocObject);
            this.CreateRootNode();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the OCC 3D view.
        /// </summary>
        private void InitV3D()
        {
            if (!myDocObject.View.InitViewer(this.Canvas3DPanel.Handle))
            //if (!myDocObject.View.InitViewer(this.Handle.ToInt32()))
            {
                MessageBox.Show("Fatal Error during the graphic initialization.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                TraceTool.TTrace.Error.Send("OCCViewDocContent - InitV3D: Fatal Error during the graphic initialization.");
            }
        }

        #endregion

        #region Windows Methods

        /// <summary>
        /// Mouse dragging event.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="state">The state (-1: mouse down; 1: mouse up).</param>
        /// <remarks>Multi dragging - pressed shift. Method makes OC add new objects to the selection.</remarks>
        protected void MultiDragEvent(int x, int y, int state)
        {
            if (state == -1)
            {
                this.theButtonDownX = x;
                this.theButtonDownY = y;
            }
            else if (state == 1)
            {
                this.myDocObject.View.ShiftSelect(Math.Min(this.theButtonDownX, x), Math.Min(this.theButtonDownY, y),
                                                  Math.Max(this.theButtonDownX, x), Math.Max(this.theButtonDownY, y));
            }
        }

        /// <summary>
        /// Select by dragging a rectangle.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="state">The state (-1: mouse down; 1: mouse up).</param>
        /// <remarks>Dragging - shift not pressed. Method makes a new selection out of the selected objects.</remarks>
        protected void DragEvent(int x, int y, int state)
        {
            if (state == -1) // mouse is down
            {
                this.theButtonDownX = x;
                this.theButtonDownY = y;
            }
            else if (state == 1) // mouse is up
            {
                this.myDocObject.View.Select(Math.Min(this.theButtonDownX, x), Math.Min(this.theButtonDownY, y),
                                             Math.Max(this.theButtonDownX, x), Math.Max(this.theButtonDownY, y));
            }
        }

        /// <summary>
        /// Draw a rectangle around the object to zoom in on that area of the drawing.
        /// </summary>
        /// <param name="draw">if set to <c>true</c> [draw].</param>
        /// <remarks>Redraws the selection rectangle.</remarks>
        protected void DrawRectangle(bool draw)
        {
            Graphics gr = Graphics.FromHwnd(this.Handle);

            if (this.mIsRectVisible == true) // erase the rect if it's there
            {
                IntPtr hdc = gr.GetHdc();
                SetROP2(hdc, R2_MERGEPENNOT);
                MoveToEx(hdc, theRectDownX, theRectDownY, IntPtr.Zero);
                LineTo(hdc, theRectDownX, theRectUpY);
                LineTo(hdc, theRectUpX, theRectUpY);
                LineTo(hdc, theRectUpX, theRectDownY);
                LineTo(hdc, theRectDownX, theRectDownY);
                gr.ReleaseHdc();

                this.mIsRectVisible = false;
            }

            if (draw)   // draw selection rectangle
            {
                this.theRectDownX = Math.Min(this.myXmin, this.myXmax);
                this.theRectDownX = Math.Min(this.theButtonDownX, this.theRectDownX);

                this.theRectDownY = Math.Min(this.myYmin, this.myYmax);
                this.theRectDownY = Math.Min(this.theButtonDownY, this.theRectDownY);

                this.theRectUpX = Math.Max(this.myXmin, this.myXmax);
                this.theRectUpX = Math.Max(this.theButtonDownX, this.theRectUpX);

                this.theRectUpY = Math.Max(this.myYmin, this.myYmax);
                this.theRectUpY = Math.Max(this.theButtonDownY, this.theRectUpY);

                IntPtr hdc = gr.GetHdc();
                SetROP2(hdc, R2_MERGEPENNOT);
                MoveToEx(hdc, theRectDownX, theRectDownY, IntPtr.Zero);
                LineTo(hdc, theRectDownX, theRectUpY);
                LineTo(hdc, theRectUpX, theRectUpY);
                LineTo(hdc, theRectUpX, theRectDownY);
                LineTo(hdc, theRectDownX, theRectDownY);
                gr.ReleaseHdc();

                this.mIsRectVisible = true;
            }
        }

        /// <summary>
        /// Relays mouse position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        protected void MultiMoveEvent(int x, int y)
        {
            this.myDocObject.View.MoveTo(x, y);
        }

        /// <summary>
        /// Relays mouse position.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        protected void MoveEvent(int x, int y)
        {
            this.myDocObject.View.MoveTo(x, y);
        }

        /// <summary>
        /// Adds a new objects to the selection.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <remarks>Called from within 'MouseUp hadle'.</remarks>
        protected void MultiInputEvent(int x, int y)
        {
            this.myDocObject.View.ShiftSelect();
        }

        /// <summary>
        /// Method makes a new selection out of the selected object.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <remarks>Called from within 'MouseUp hadle'.</remarks>
        protected void InputEvent(int x, int y)
        {
            this.myDocObject.View.Select();
        }

        #endregion

        #region Windows Events

        /// <summary>
        /// Initialize the rendering when the window is shown for the first time.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OCCViewDocContent_Shown(object sender, EventArgs e)
        {
            this.InitV3D();
        }

        /// <summary>
        /// Handles the Paint event of the Canvas3DPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void Canvas3DPanel_Paint(object sender, PaintEventArgs e)
        {
            if (this.myDocObject.View != null)
            {
                this.myDocObject.View.RedrawView();
                this.myDocObject.View.UpdateView();
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the OCCViewerDocContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OCCViewDocContent_SizeChanged(object sender, EventArgs e)
        {
            if (this.myDocObject.View != null)
            {
                this.myDocObject.View.UpdateView();
            }
        }

        #endregion

        #region ToolStrip Handling

        /// <summary>
        /// Zoom in all view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FitAllTSB_Click(object sender, EventArgs e)
        {
            this.View?.ZoomAllView();
        }

        /// <summary>
        /// Zoom In (+10%) - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ZoomInTSB_Click(object sender, EventArgs e)
        {
            int zoomFactor = 10;
            this.View?.Zoom(0, 0, zoomFactor, zoomFactor);
        }

        /// <summary>
        /// Zoom Out (-10%) - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ZoomOutTSB_Click(object sender, EventArgs e)
        {
            int zoomFactor = -10;
            this.View?.Zoom(0, 0, zoomFactor, zoomFactor);
        }

        /// <summary>
        /// Front side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewFrontTSB_Click(object sender, EventArgs e)
        {
            this.View?.FrontView();
        }

        /// <summary>
        /// Back side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewBackTSB_Click(object sender, EventArgs e)
        {
            this.View?.BackView();
        }

        /// <summary>
        /// Top side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewTopTSB_Click(object sender, EventArgs e)
        {
            this.View?.TopView();
        }

        /// <summary>
        /// Bottom side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewBottomTSB_Click(object sender, EventArgs e)
        {
            this.View?.BottomView();
        }

        /// <summary>
        /// Left side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewLeftTSB_Click(object sender, EventArgs e)
        {
            this.View?.LeftView();
        }

        /// <summary>
        /// Right side of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ViewRightTSB_Click(object sender, EventArgs e)
        {
            this.View?.RightView();
        }

        /// <summary>
        /// Resets the centering and the orientation of the view - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ResetViewTSB_Click(object sender, EventArgs e)
        {
            this.View?.ResetView();
        }

        /// <summary>
        /// Translates the center of the view according to the mouse position (X,Y) - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MoveViewTSB_Click(object sender, EventArgs e)
        {
            this.Mode = CurrentAction3d.CurAction3d_DynamicPanning;
        }

        /// <summary>
        /// Begin the rotation of the view according to the mouse position (X,Y) - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RotateViewTSB_Click(object sender, EventArgs e)
        {
            this.Mode = CurrentAction3d.CurAction3d_DynamicRotation;
        }

        /// <summary>
        /// Draw a rectangle around the object to zoom in on that area of the drawing - click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ZoomWindowTSB_Click(object sender, EventArgs e)
        {
            this.Mode = CurrentAction3d.CurAction3d_WindowZooming;
        }

        #endregion

        #region Mouse Events

        /// <summary>
        /// Handles the MouseDown event of the Canvas3DPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Canvas3DPanel_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    this.myXmin = e.X; this.myYmin = e.Y;
                    this.myXmax = e.X; this.myYmax = e.Y;
                    this.theButtonDownX = e.X;
                    this.theButtonDownY = e.Y;

                    // Start the dynamic zooming....
                    if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                    {
                        this.myCurrentMode = CurrentAction3d.CurAction3d_DynamicZooming;
                    }
                    else
                    {
                        switch (this.myCurrentMode)
                        {
                            case CurrentAction3d.CurAction3d_Nothing:
                                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                {
                                    this.MultiDragEvent(myXmax, myYmax, -1);
                                }
                                else
                                {
                                    this.DragEvent(myXmax, myYmax, -1);
                                }
                                break;
                            case CurrentAction3d.CurAction3d_DynamicRotation:
                                if (!this.mDegenerateModeIsOn)
                                {
                                    this.myDocObject.View.SetDegenerateModeOn();
                                }
                                // Start the rotation according to the mouse position (X,Y)
                                this.myDocObject.View.StartRotation(e.X, e.Y);
                                break;
                            case CurrentAction3d.CurAction3d_WindowZooming:
                                this.Cursor = System.Windows.Forms.Cursors.Hand;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case MouseButtons.Right:
                    //MessageBox.Show("right mouse button is down");
                    if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                    {
                        if (!this.mDegenerateModeIsOn)
                        {
                            this.myDocObject.View.SetDegenerateModeOn();
                        }
                        this.myDocObject.View.StartRotation(e.X, e.Y);
                    }
                    else if ((MouseButtons.Middle | MouseButtons.Right) == MouseButtons)
                    {
                        // do nothing - middle button pressed - panning                        
                    }
                    else
                    {
                        //this.Popup(e.X, e.Y); // TODO?
                    }
                    break;
                case MouseButtons.Middle:
                    if (!this.mDegenerateModeIsOn)
                    {
                        this.myDocObject.View.SetDegenerateModeOn();
                    }
                    this.myDocObject.View.StartRotation(e.X, e.Y);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the Canvas3DPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Canvas3DPanel_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                    {
                        return;
                    }
                    switch (this.myCurrentMode)
                    {
                        case CurrentAction3d.CurAction3d_Nothing:
                            if (e.X == this.myXmin && e.Y == this.myYmin)
                            {
                                this.myXmax = e.X; this.myYmax = e.Y;
                                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                                {
                                    this.MultiInputEvent(myXmax, myYmax);
                                }
                                else
                                {
                                    this.InputEvent(myXmax, myYmax);
                                }
                            }
                            //else
                            //{
                            //    myXmax = e.X; myYmax = e.Y;
                            //    DrawRectangle(false);
                            //    if (myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                            //        MultiDragEvent(myXmax, myYmax, 1);
                            //    else
                            //        DragEvent(myXmax, myYmax, 1);
                            //    myDocObject.View.RedrawView();
                            //}
                            break;
                        case CurrentAction3d.CurAction3d_DynamicZooming:
                            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                            break;
                        case CurrentAction3d.CurAction3d_WindowZooming:
                            this.myXmax = e.X; this.myYmax = e.Y;
                            this.DrawRectangle(false);
                            int ValZWMin = 1;
                            if (Math.Abs(this.myXmax - this.myXmin) > ValZWMin &&
                                Math.Abs(this.myXmax - this.myYmax) > ValZWMin)
                            {
                                this.myDocObject.View.WindowFitAll(this.myXmin, this.myYmin, this.myXmax, this.myYmax);
                            }
                            this.Cursor = Cursors.Default;
                            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                            break;
                        case CurrentAction3d.CurAction3d_DynamicPanning:
                            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                            break;
                        case CurrentAction3d.CurAction3d_GlobalPanning:
                            this.myDocObject.View.Place(e.X, e.Y, this.mCurZoom);
                            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                            break;
                        case CurrentAction3d.CurAction3d_DynamicRotation:
                            this.myCurrentMode = CurrentAction3d.CurAction3d_Nothing;
                            if (!this.mDegenerateModeIsOn)
                            {
                                this.myDocObject.View.SetDegenerateModeOff();
                                this.mDegenerateModeIsOn = false;
                            }
                            else
                            {
                                this.myDocObject.View.SetDegenerateModeOn();
                                this.mDegenerateModeIsOn = true;
                            }
                            break;
                        default:
                            break;

                    }
                    break;
                case MouseButtons.Right:
                    if (!this.mDegenerateModeIsOn)
                    {
                        this.myDocObject.View.SetDegenerateModeOff();
                        this.mDegenerateModeIsOn = false;
                    }
                    else
                    {
                        this.myDocObject.View.SetDegenerateModeOn();
                        this.mDegenerateModeIsOn = true;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the Canvas3DPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Canvas3DPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == (MouseButtons.Left | MouseButtons.Middle))
            {
                // do panning when middle and right mouse button is pressed
                this.myDocObject.View.Zoom(this.myXmax, this.myYmax, e.X, e.Y);
                this.myXmax = e.X; myYmax = e.Y;
            }
            else if (e.Button == MouseButtons.Left) // left button is pressed
            {
                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                {
                    this.myDocObject.View.Zoom(this.myXmax, this.myYmax, e.X, e.Y);
                    this.myXmax = e.X; myYmax = e.Y;
                }
                else
                {
                    switch (myCurrentMode)
                    {
                        case CurrentAction3d.CurAction3d_Nothing:
                            //this.DrawRectangle(false);
                            //this.myXmax = e.X; myYmax = e.Y;
                            //this.DrawRectangle(true);                            
                            break;
                        case CurrentAction3d.CurAction3d_DynamicZooming:
                            this.myDocObject.View.Zoom(this.myXmax, this.myYmax, e.X, e.Y);
                            this.myXmax = e.X; myYmax = e.Y;
                            break;
                        case CurrentAction3d.CurAction3d_WindowZooming:
                            this.DrawRectangle(false);
                            this.myXmax = e.X; myYmax = e.Y;
                            this.DrawRectangle(true); // add brush here
                            break;
                        case CurrentAction3d.CurAction3d_DynamicPanning:
                            this.myDocObject.View.Pan(e.X - myXmax, myYmax - e.Y);
                            this.myXmax = e.X; myYmax = e.Y;
                            break;
                        case CurrentAction3d.CurAction3d_GlobalPanning:
                            break;
                        case CurrentAction3d.CurAction3d_DynamicRotation:
                            this.myDocObject.View.Rotation(e.X, e.Y);
                            this.myDocObject.View.RedrawView();
                            break;
                        default:
                            break;
                    }
                }
            } // e.Button == MouseButtons.Left  
            else if (e.Button == (MouseButtons.Middle | MouseButtons.Right))
            {
                // do panning when middle and right mouse button is pressed
                this.myDocObject.View.Pan(e.X - this.myXmax, this.myYmax - e.Y);
                this.myXmax = e.X; this.myYmax = e.Y;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                {
                    this.myDocObject.View.Pan(e.X - this.myXmax, this.myYmax - e.Y);
                    this.myXmax = e.X; this.myYmax = e.Y;
                }
                else // rotate
                {
                    this.myDocObject.View.Rotation(e.X, e.Y);
                }

            }//e.Button=MouseButtons.Middle
            else if (e.Button == MouseButtons.Right) // right button is pressed
            {
                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Ctrl)
                {
                    this.myDocObject.View.Rotation(e.X, e.Y);
                }
            }
            else // no buttons are pressed
            {
                this.myXmax = e.X; this.myYmax = e.Y;
                if (this.myCurrentPressedKey == CurrentPressedKey.CurPressedKey_Shift)
                {
                    this.MultiMoveEvent(e.X, e.Y);
                }
                else
                {
                    this.MoveEvent(e.X, e.Y);
                }
            }
        }

        /// <summary>
        /// Handles the MouseWheel event of the Canvas3DPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Canvas3DPanel_MouseWheel(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Opencascade ist ursprünglich vorbereitet einen zoom durch mausbewegung durchzuführen. 
            // Die Entscheidung daüber in welcher Richtung gezoomt werden soll scheint vom X Wert
            // des durch die Mausbewegung beschriebenen Rechtecks.
            // Mit anderen Worten, wenn der Benutzer mit der Maus einen Rechteck von Links nach Rechts zeichnet 
            // (die maus in positiver Richtung auf der X Achse bewegt) ist es eine Vergrößerung des Modells (Zoom In)
            // in der anderen Richtung ist es dann einen Zoom out
            // Wir erstellen hier einen dummy Rechteck von 0,0 bis zoomDirechtion,0
            // Dabei ist die zoomDirection durch die Drehung des Mausrads bestimmt
            // Der Wert der Variable beschreibt die Schrittweite (grössere Werte --> schnelleres zooming)
            int zoomFactor = -10; // (%)
            if (e.Delta > 0)
            {
                this.myDocObject.View.Zoom(0, 0, zoomFactor, zoomFactor);
            }
            else if (e.Delta < 0)
            {
                this.myDocObject.View.Zoom(0, 0, -zoomFactor, -zoomFactor);
            }
        }

        #endregion

        #region Tree Handling

        private OCCModel Root { get; set; }// = new OCCModel();
        private TreeNode RootNode { get; set; }

        /// <summary>
        /// Create the tree view parent node.
        /// </summary>
        private void CreateRootNode()
        {
            // and refill the treeview with the model
            if (this.Root != null)
            {
                // Remove all nodes on the first call
                this.ModelTreeView.Nodes.Clear();

                // Create the parent root
                TreeNode rootNode = new TreeNode(this.Root.Name);
                rootNode.Tag = Root;

                // Add node to parent
                this.ModelTreeView.Nodes.Add(rootNode);

                // Set the root node
                this.RootNode = rootNode;
            }
        }

        /// <summary>
        /// Refill the tree view with the updated entities.
        /// </summary>
        /// <param name="doc">The OCC document.</param>
        private void FillTree(AOCCBaseElement doc)
        {
            // and refill the treeview with the model
            if (doc != null)
            {
                //// Remove all nodes on the first call
                //this.ModelTreeView.Nodes.Clear();

                this.FillTree(doc, this.ModelTreeView.Nodes);
            }
        }

        /// <summary>
        /// Recursively add a base object and all its children to the tree view.
        /// </summary>
        /// <param name="doc">The OCC document.</param>
        /// <param name="Nodes">The nodes collection to add the parentObj to.</param>
        private void FillTree(AOCCBaseElement doc, TreeNodeCollection Nodes)
        {
            // Create a new tree node with the parent
            TreeNode parentNode = Nodes.Add(doc.Name);

            // The Tag property of the node contains the actuall base object, so we can find it later
            parentNode.Tag = doc;

            // Update the icons nodes in the tree
            this.RefreshTreeNode(parentNode);

            // Iterator for all children
            foreach (AOCCBaseElement child in doc.Children)
            {
                this.FillTree(child, parentNode.Nodes);
            }
        }

        /// <summary>
        /// Update the icons nodes in the tree.
        /// </summary>
        /// <param name="node">The node to updated.</param>
        private void RefreshTreeNode(TreeNode node)
        {
            AOCCBaseElement tagDoc = (AOCCBaseElement)node.Tag;
            if (tagDoc != null)
            {
                string imgKey = tagDoc.GetImageKey();
                if (node.ImageKey != imgKey)
                {
                    node.ImageKey = imgKey;
                }
                imgKey = tagDoc.GetSelectedImageKey();
                if (node.SelectedImageKey != imgKey)
                {
                    node.SelectedImageKey = imgKey;
                }
                if (node.Text != tagDoc.Name)
                {
                    node.Text = tagDoc.Name;
                }
                if (node.Name != tagDoc.Name)
                {
                    node.Name = tagDoc.Name;
                }
            }
        }

        /// <summary>
        /// Determines the node associated with the an AFaceBaseObject node.
        /// </summary>
        /// <param name="obj">The object, which is searched.</param>
        /// <returns></returns>
        public TreeNode GetTreeNode(AOCCBaseElement obj)
        {
            // The top nodes
            foreach (TreeNode node in this.ModelTreeView.Nodes)
            {
                if (node.Tag == obj)
                {
                    return node;
                }
                // If it's not the node you are looking for, then ask the children
                else
                {
                    return this.GetTreeNode(obj, node);
                }
            }
            return null;
        }

        /// <summary>
        /// Determines the node associated with the an AFaceBaseObject node.
        /// </summary>
        /// <param name="obj">The object, which is searched.</param>
        /// <param name="node">The reference node.</param>
        /// <returns>Returns the searched node.</returns>
        private TreeNode GetTreeNode(AOCCBaseElement obj, TreeNode node)
        {
            // Searches a node and all subnodes
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Tag == obj)
                {
                    return child;
                }
                else
                {
                    TreeNode result = this.GetTreeNode(obj, child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        #endregion

        #region TreeView Events

        private void ModelTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }

        /// <summary>
        /// After select - TreeView event handler.
        /// Occurs when the tree node has been selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void ModelTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Node.Tag == null) return;

            TreeNode node = this.ModelTreeView.SelectedNode;
            if (node.Tag.GetType() == typeof(OCCModel))
            {
                // Show the selected Object's parameters in the Property Grid
                this.SetObjectPropertyGrid((OCCModel)node.Tag);
                return;
            }

            AOCCBaseElement? element = node.Tag as AOCCBaseElement;
            if (element == null) return;
            element.IsObjectSelected = true; // Sets selectable object

            // Show the selected Object's parameters in the Property Grid
            this.SetObjectPropertyGrid(element);

            // The parent's visibility must be equal to all children's visibility
            if (element is OCCElement)
            {
                ((OCCElement)element).ChildrenVisibility();
            }
        }

        /// <summary>
        /// Node Mouse click - TreeView  event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TreeNodeMouseClickEventArgs"/> instance containing the event data.</param>
        private void ModelTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Select the node so that it's highlighted
            this.ModelTreeView.SelectedNode = e.Node;
        }

        #endregion

        #region MenuStrip Handling

        /// <summary>
        /// About dialog window.
        /// </summary>
        protected AboutDialog aboutDialog;

        /// <summary>
        /// Shows the about dialog.
        /// </summary>
        private void ShowAboutDialog()
        {
            if (this.aboutDialog == null)
            {
                this.aboutDialog = new AboutDialog();
            }
            this.aboutDialog.ShowDialog(this);
        }

        /// <summary>
        /// Handles the events from MenuFile -> About.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowAboutDialog();
            }
            catch 
            {

            }
        }

        /// <summary>
        /// Handles the events from MenuFile -> Load Vector File.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LoadVectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filter = "Vector Files|*.svg"; // *.dxf";

            // Open Dialog Initialization
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = filter;// + "|All files (*.*)|*.*";
            openDialog.Title = "Open Vector File";

            if (FileHelper.ProjectDataDir != string.Empty)
            {
                openDialog.InitialDirectory = FileHelper.ProjectDataDir;
            }

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openDialog.FileName;
                //if (fileName == "") return;

                // Check if the file actually exists
                if (fileName == "" || !File.Exists(fileName))
                {
                    string msg = string.Format("The file [{0}] cannot be found.", fileName);
                    MessageBox.Show(msg, "Open Vector File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Check if the file actually exists
                    //FileInfo fileInfo = new FileInfo(fileName);
                    //string extension = fileInfo.Extension;
                    //extension = extension.TrimStart('.');
                    string extension = System.IO.Path.GetExtension(fileName);
                    extension = extension.ToLowerInvariant();

                    if (extension == ".svg") // Load SVG Vector Image
                    {
                        // Ask for display mode
                        if (DisplayModeForm.CreateAndShowDialog(this) != true)
                        {
                            // Nothing selected - abort
                            return;
                        }

                        DisplayMode mode = DisplayModeForm.WireMode == true ? DisplayMode.Wireframe : DisplayMode.Shaded;

                        // Info about unit type
                        UnitType unit = UnitType.None;
                        if (SVGUnitForm.CreateAndShowDialog(this) == true)
                        {
                            unit = UnitType.Pixel;
                        }

                        // Read the SVG file
                        SvgReader reader = new SvgReader(unit, mode);
                        OCCElement doc = reader.Open(fileName);
                        if (doc != null)
                        {
                            if (doc.NbrChildren > 0)
                            {
                                this.View.TopView();

                                doc.SetOCCContext(this.View.GetAISContext());

                                //// Fill the tree
                                //this.FillTree(doc);

                                // Add the OCCModel as parent node in the TreeView
                                this.FillTree(doc, this.RootNode.Nodes);
                                this.RootNode.Expand();

                                // Display all elements.
                                doc.Display(true);

                                // Fit all
                                this.View?.ZoomAllView();

                                return;

                                // Alternative: Handling of shape outside of an OCCDocument
                                foreach (var element in doc.Children)
                                {
                                    if (element.TopoShape == null) continue;

                                    // Create AIS_Shape for visualization
                                    AIS_Shape aisShape = new AIS_Shape(element.TopoShape);
                                    this.View.DisplayShape(aisShape);
                                }

                                // Get element by class name
                                List<PathSegment> paths = doc.GetAllElements<PathSegment>();
                                List<Rectangle> Rectangles = doc.GetAllElements<Rectangle>();

                                // Get element by name
                                TreeNode node = this.ModelTreeView.SelectedNode;
                                if (node != null)
                                {
                                    AOCCBaseElement element = doc.GetElementByName(node.Name);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Can't open the SVG file", "Error!",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This file type is not supported.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception exc)
                {
                    TraceTool.TTrace.Debug.UnIndent("Could not open vector file.");

                    MessageBox.Show(string.Format("Failed to load vector file. {0}", exc.GetBaseException().Message),
                        "Open Vector File", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    TraceTool.TTrace.Error.Send(string.Format("OCCMainForm - LoadVector: Could not open file. {0}", exc.GetBaseException().Message));
                    this.Cursor = Cursors.Default;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Lists the system log file - Click event handler.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            if (exePath != null)
            {
                string filePath = System.IO.Path.Combine(exePath, "OCCSession.log");
                if (File.Exists(filePath))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = filePath;
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }
            }
        }

        #endregion

        #region PropertyGrid External

        /// <summary>
        /// The method is used to display the properties of an object in PropertyGrid.
        /// </summary>
        /// <param name="obj">The parameter object.</param>
        public void SetObjectPropertyGrid(object obj)
        {
            //if(obj != null)
            {
                this.ModelPropertyGrid.SelectedObject = obj;
            }
        }

        #endregion

    }
}
