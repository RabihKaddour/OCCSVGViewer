/*******************************************************************************
*                                                                              
* Author    :  Rabih Kaddour      
* 
* Date      :  15 July 2023       
* 
* Copyright :  Rabih Kaddour  2023-2024
*              Copyright (C) Rabih Kaddour. All rights reserved.
* 
* File      :  OCCViewer.cs     
* 
* Content   :  Represents the Base Class for the OCC 3D-Viewer.
* 
* License:                                                                     
* Use, modification & distribution is subject to Boost Software License Ver 1. 
* http://www.boost.org/LICENSE_1_0.txt                                         
*                                                                              
* Website:  https://inovaitec.com                                              
*******************************************************************************/

using Occt;


namespace OCCSVGViewer
{
    /// <summary>
    /// Base Class for the 3D Viewer.
    /// </summary>
    public class OCCViewer
    {
        #region Protected Fields        

        /// <summary>
        /// Defines services on Viewer type objects.
        /// </summary>
        protected V3d_Viewer myViewer;
        /// <summary>
        /// Defines the application object VIEW for the
        /// VIEWER application.
        /// </summary>
        protected V3d_View myView;
        /// <summary>
        /// This class defines an OpenGl graphic driver.
        /// </summary>
        protected OpenGl_GraphicDriver myGraphicDriver;
        /// <summary>
        /// The Interactive Context allows you to manage graphic behavior and  
        /// selection of Interactive Objects in one or more viewers.
        /// </summary>
        protected AIS_InteractiveContext myAISContext;

        /// <summary>
        /// Defines whether the viewer has already been initialized.
        /// </summary>
        protected bool IsInitialized;

        #endregion

        #region Private Fields

        private AIS_Trihedron myTrihedron;
        private bool mUseTrihedronAxis;
        /// <summary>
        /// The size of the axis.
        /// </summary>
        private double mAxisSize;

        private bool mUseStaticTrihedronAxis;
        private double mTrihedronScale;
        private Aspect_TypeOfTriedronPosition mTriedronPosition;
        private Quantity_Color mTriedronColor;

        // The Background color for the OCC rendering	
        private bool mUseGradientColor;
        System.Drawing.Color mTopColor;
        System.Drawing.Color mBottomColor;
        System.Drawing.Color mBgColor;

        private static object mSynchObj = new object();

        #endregion

        #region Propety Fields

        /// <summary>
        /// Gets or sets the Background gradient color flag.
        /// </summary>
        public bool GradientColor
        {
            get => this.mUseGradientColor;
            set
            {
                if (value != this.mUseGradientColor)
                {
                    this.mUseGradientColor = value;
                    this.SetGradientBackgroundColors();
                }
            }
        }

        /// <summary>
        /// Gets or sets the top gradient background color for the view.
        /// </summary>
        public System.Drawing.Color TopColor
        {
            get => this.mTopColor;
            set
            {
                if (this.mTopColor != value)
                {
                    this.mTopColor = value;
                    this.SetGradientBackgroundColors();
                }
            }
        }

        /// <summary>
        /// Gets or sets the bottom gradient background color for the view.
        /// </summary>
        public System.Drawing.Color BottomColor
        {
            get => this.mBottomColor;
            set
            {
                if (this.mBottomColor != value)
                {
                    this.mBottomColor = value;
                    this.SetGradientBackgroundColors();
                }
            }
        }

        /// <summary>
        /// Sets or gets the axis's visibility flag.
        /// </summary>
        public bool AxisVisibility
        {
            get => this.mUseTrihedronAxis;
            set
            {
                if (value != this.mUseTrihedronAxis)
                {
                    this.mUseTrihedronAxis = value;
                    this.CreateTrihedronAxis(value);
                }
            }
        }

        /// <summary>
        /// Sets or gets the size of the axis.
        /// </summary>
        public double AxisSize
        {
            get => this.GetTrihedronSize();
            set
            {
                if (value != this.mAxisSize)
                {
                    this.mAxisSize = value;
                    this.SetTrihedronSize(value);
                }
            }
        }

        /// <summary>
        /// Sets or gets the trihedron's visibility flag.
        /// </summary>
        public bool TrihedronVisibility
        {
            get => this.mUseStaticTrihedronAxis;
            set
            {
                if (value != this.mUseStaticTrihedronAxis)
                {
                    this.mUseStaticTrihedronAxis = value;
                    this.CreateStaticTrihedronAxis(value, this.mTrihedronScale, this.mTriedronColor, this.mTriedronPosition);
                }
            }
        }

        /// <summary>
        /// Sets or gets the size of the trihedron.
        /// </summary>
        public double TrihedronSize
        {
            get => this.mTrihedronScale;
            set
            {
                if (value != this.mTrihedronScale)
                {
                    this.mTrihedronScale = value;
                    this.CreateStaticTrihedronAxis(this.mUseStaticTrihedronAxis, value, this.mTriedronColor, this.mTriedronPosition);
                }
            }
        }

        /// <summary>
        /// Sets or gets the label trihedron's color.
        /// </summary>
        public System.Drawing.Color TrihedronColor
        {
            get
            {
                return this.GetStaticTrihedronColor();
            }
            set
            {
                this.SetStaticTrihedronColor(value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OCCViewer"/> class.
        /// </summary>
        public OCCViewer()
        {
            this.myViewer = null;
            this.myView = null;
            this.myAISContext = null;

            this.mUseGradientColor = true;
            this.mUseTrihedronAxis = true;
            this.mAxisSize = 100; // the default value 100 mm for display of trihedra
            this.mUseStaticTrihedronAxis = true;
            this.mTrihedronScale = 0.1;
            this.mTriedronPosition = Aspect_TypeOfTriedronPosition.Aspect_TOTP_RIGHT_LOWER;
            this.mTriedronColor = new Quantity_Color(Quantity_NameOfColor.Quantity_NOC_YELLOW);

            // Black, dark blue gradient
            this.mBottomColor = System.Drawing.Color.White;
            this.mTopColor = System.Drawing.Color.FromArgb(255, 0, 63, 127);

            //// Default
            //this.mTopColor = System.Drawing.Color.FromArgb(47, 47, 116);
            //this.mBottomColor = System.Drawing.Color.FromArgb(218, 218, 246);

            this.mBgColor = this.mTopColor;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the 3D viewer.
        /// </summary>
        /// <param name="hwnd">System.IntPtr that contains the window handle (HWND) of the control.</param>
        /// <remarks>
        /// You must be copy the Occt.Net DLL's (LaserJob3D\bin\Debug\net8.0-windows8.0\occt\x64) into
        /// LaserApplication\bin\Debug\net8.0-windows8.0 folder
        /// </remarks>
        public bool InitViewer(System.IntPtr hwnd)
        {
            try
            {
                LockRendering();

                try
                {
                    // Initialize perform of default OpenGL context on construction
                    var displayConnection = new Aspect_DisplayConnection();

                    // Set up the graphic driver
                    this.myGraphicDriver = new OpenGl_GraphicDriver(displayConnection);
                }
                //catch
                //{
                //    this.myGraphicDriver = new OpenGl_GraphicDriver(displayConnection);
                //}
                catch (System.Exception exc)
                {
                    TraceTool.TTrace.Error.Send("OCCViewer - InitViewer:", exc.StackTrace);
                    return false;
                }

                // Create 3D Viewer
                this.myViewer = new V3d_Viewer(this.myGraphicDriver);

                // Lighting
                this.myViewer.SetDefaultLights();
                this.myViewer.SetLightOn();

                // Creates View
                this.myView = this.myViewer.CreateView();
                //this.myView.SetBackgroundColor(Quantity_NOC_BLACK);

                // Top Color
                int rTop = Convert.ToInt32((this.mBgColor.R).ToString());
                int gTop = Convert.ToInt32((this.mBgColor.G).ToString());
                int bTop = Convert.ToInt32((this.mBgColor.B).ToString());

                // Bottom Color
                int rBtm = Convert.ToInt32((this.mBottomColor.R).ToString());
                int gBtm = Convert.ToInt32((this.mBottomColor.G).ToString());
                int bBtm = Convert.ToInt32((this.mBottomColor.B).ToString());

                // Defines the gradient background color of the view
                Quantity_Color topColor = new Quantity_Color((double)rTop / 255, (double)gTop / 255, (double)bTop / 255,
                                                             Quantity_TypeOfColor.Quantity_TOC_RGB);
                if (this.mUseGradientColor)
                {
                    Quantity_Color bottomColor = new Quantity_Color((double)rBtm / 255, (double)gBtm / 255, (double)bBtm / 255,
                                                                    Quantity_TypeOfColor.Quantity_TOC_RGB);
                    this.myView.SetBgGradientColors(topColor, bottomColor, Aspect_GradientFillMethod.Aspect_GFM_VER, true);
                }
                else
                {
                    this.myView.SetBgGradientColors(topColor, topColor, Aspect_GradientFillMethod.Aspect_GFM_VER, true);
                }

                // Creates a Window based on the existing window handle
                WNT_Window aWNTWindow = new WNT_Window(hwnd);
                this.myView.SetWindow(aWNTWindow);
                if (!aWNTWindow.IsMapped)
                {
                    aWNTWindow.Map();
                }

                // Create Interactive Context
                this.myAISContext = new AIS_InteractiveContext(this.myViewer);
                this.myAISContext.UpdateCurrentViewer();
                // Redraw the view
                this.myView.Redraw();
                this.myView.MustBeResized();

                // Sets depth value automatically depending on the calculated Z size and Aspect parameter
                this.PerformDepth();

                // Create a Trihedron
                this.CreateTrihedronAxis(this.mUseTrihedronAxis);
                this.CreateStaticTrihedronAxis(this.mUseStaticTrihedronAxis, this.mTrihedronScale, this.mTriedronColor, this.mTriedronPosition);

                this.IsInitialized = true;
            }
            finally
            {
                UnlockRendering();
            }

            return true;
        }

        /// <summary>
        /// Gets the Interactive Context.
        /// </summary>
        public AIS_InteractiveContext GetAISContext()
        {
            return this.myAISContext;
        }

        /// <summary>
        /// Update view.
        /// </summary>
        public void UpdateView()
        {
            try
            {
                LockRendering();

                if (this.myView == null)
                {
                    TraceTool.TTrace.Warning.Send("OCCViewer - UpdateView: V3d_View is a null pointer.");
                    return;
                }
                this.myView.MustBeResized();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Redraws the complete view.
        /// </summary>
        public void RedrawView()
        {
            try
            {
                LockRendering();

                if (this.myView == null)
                {
                    TraceTool.TTrace.Warning.Send("OCCViewer - RedrawView: V3d_View is a null pointer.");
                    return;
                }
                this.myView.Redraw();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Update the current viewer.
        /// </summary>
        public void UpdateCurrentViewer()
        {
            if (!this.myAISContext.IsNULL)
            {
                this.myAISContext.UpdateCurrentViewer();
            }
        }

        /// <summary>
        /// Set if the computed mode can be used [false].
        /// </summary>
        public void SetDegenerateModeOn()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.ComputedMode = false;
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Set if the computed mode can be used [true].
        /// </summary>
        public void SetDegenerateModeOff()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.ComputedMode = true;
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Centers the defined PIXEL window so that it occupies
        /// the maximum space while respecting the initial height/width ratio.
        /// </summary>
        /// <param name="Xmin">The Xmin in pixel coordinates of minimal corner on x screen axis.</param>
        /// <param name="Ymin">The Ymin in pixel coordinates of minimal corner on y screen axis.</param>
        /// <param name="Xmax">The Xmax in pixel coordinates of maximal corner on x screen axis.</param>
        /// <param name="Ymax">The Ymax in pixel coordinates of maximal corner on y screen axis.</param>
        /// <remarks>
        /// NOTE than the original Z size of the view is NOT modified.
        /// </remarks>
        public void WindowFitAll(int Xmin, int Ymin, int Xmax, int Ymax)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.WindowFitAll(Xmin, Ymin, Xmax, Ymax);
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Places the point of the view corresponding at the 
        /// pixel position x,y at the center of the window
        /// and updates the view.
        /// </summary>
        /// <param name="x">The pixel position x.</param>
        /// <param name="y">The pixel position y.</param>
        /// <param name="zoomFactor">The current zoom factor.</param>
        public void Place(int x, int y, float zoomFactor)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.Place(x, y, zoomFactor);
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The front side of the view.
        /// </summary>
        public void FrontView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Yneg); 
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The back side of the view.
        /// </summary>
        public void BackView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Ypos);
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The top side of the view.
        /// </summary>
        public void TopView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Zpos);
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The bottom side of the view.
        /// </summary>
        public void BottomView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Zneg);
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The left side of the view.
        /// </summary>
        public void LeftView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Xneg); 
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The right side of the view.
        /// </summary>
        public void RightView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.SetProj(V3d_TypeOfOrientation.V3d_Xpos); 
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Resets the centering and the orientation of the view.
        /// </summary>
        public void ResetView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.Reset();
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Gets the display mode of the shape:
        /// WireFrame = 0;
        /// Shaded = 1;
        /// Other = 10;
        /// </summary>
        /// <returns>Returns the display mode of the shape.</returns>
        public int DisplayMode()
        {
            int mode = -1;

            try
            {
                LockRendering();

                bool OneOrMoreInShading = false;
                bool OneOrMoreInWireframe = false;

                for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
                {
                    if (this.myAISContext.IsDisplayed(this.myAISContext.SelectedInteractive(), 1))
                    {
                        OneOrMoreInShading = true;
                    }
                    if (this.myAISContext.IsDisplayed(this.myAISContext.SelectedInteractive(), 0))
                    {
                        OneOrMoreInWireframe = true;
                    }
                }
                if (OneOrMoreInShading && OneOrMoreInWireframe)
                {
                    mode = 10;
                }
                else if (OneOrMoreInShading)
                {
                    mode = 1;
                }
                else if (OneOrMoreInWireframe)
                {
                    mode = 0;
                }
            }
            finally
            {
                UnlockRendering();
            }

            return mode;
        }

        /// <summary>
        /// Display the topological shape.
        /// </summary>
        /// <param name="aisShape">The AIS shape to display.</param>
        public void DisplayShape(AIS_Shape aisShape)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: AISContext is a null pointer.");
                return;
            }

            if (aisShape != null)
            {
                this.myAISContext.Display(aisShape, true);
            }
            else
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: InteractiveObject is a null pointer.");
            }
        }

        /// <summary>
        /// Display the topological shape.
        /// </summary>
        /// <param name="shape">The shape to display.</param>
        public void DisplayShape(TopoDS_Shape shape)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: AISContext is a null pointer.");
                return;
            }

            AIS_Shape aisShape = new AIS_Shape(shape);
            if (aisShape != null)
            {
                //TraceTool.TTrace.Debug.Send("Displaying...");
                this.myAISContext.Display(aisShape, true);
            }
            else
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: InteractiveObject is a null pointer.");
            }
        }

        /// <summary>
        /// Display the topological shape.
        /// </summary>
        /// <param name="shape">The shape to display.</param>
        /// <param name="color">The shape's color.</param>
        public void DisplayShape(TopoDS_Shape shape, Color color)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: AISContext is a null pointer.");
                return;
            }

            AIS_Shape aisShape = new AIS_Shape(shape);
            if (aisShape != null)
            {
                // Bottom Color
                int r = Convert.ToInt32((color.R).ToString());
                int g = Convert.ToInt32((color.G).ToString());
                int b = Convert.ToInt32((color.B).ToString());

                // Defines the gradient background color of the view
                Quantity_Color qColor = new Quantity_Color((double)r / 255, (double)g / 255, (double)b / 255,
                                                             Quantity_TypeOfColor.Quantity_TOC_RGB);
                aisShape.SetColor(qColor);
                aisShape.DisplayMode = 1;

                //TraceTool.TTrace.Debug.Send("Displaying...");
                this.myAISContext.Display(aisShape, true);
            }
            else
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - DisplayShape: InteractiveObject is a null pointer.");
            }
        }

        /// <summary>
        /// Sets the display mode of the shape (e.g. shaded or wireframe).
        /// </summary>
        /// <param name="mode">The parameter display mode.</param>
        public void SetDisplayMode(int mode)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetDisplayMode: AISContext is a null pointer.");
                return;
            }

            try
            {
                LockRendering();

                AIS_DisplayMode CurrentMode = AIS_DisplayMode.AIS_WireFrame;
                if (mode == 0)
                {
                    CurrentMode = AIS_DisplayMode.AIS_WireFrame;
                }
                else
                {
                    CurrentMode = AIS_DisplayMode.AIS_Shaded;
                }

                if (this.myAISContext.NbSelected == 0)
                {
                    this.myAISContext.SetDisplayMode(CurrentMode, true);
                }
                else
                {
                    for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
                    {
                        this.myAISContext.SetDisplayMode(this.myAISContext.SelectedInteractive(), mode, false);
                    }
                }
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Enables or disables face boundary drawing for shading presentations. 
        /// </summary>
        /// <param name="enable">
        /// The parameter enable is a boolean flag indicating whether the face boundaries should be drawn or not.
        /// </param>
        public void SetFaceBoundary(bool enable)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetFaceBoundary: AISContext is a null pointer.");
                return;
            }

            try
            {
                LockRendering();

                // Indicates whether the boundary line for each face of the object is visible or not.
                // set attributes of boundaries
                Prs3d_Drawer drawer = new Prs3d_Drawer();
                drawer.FaceBoundaryDraw = enable; // Show/Hide edges of the face

                for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
                {
                    this.myAISContext.SetLocalAttributes(this.myAISContext.SelectedInteractive(), drawer, false);
                }
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Gets the RGB color from selected object.
        /// </summary>
        /// <returns>Returns the color of the selected object.</returns>
        public System.Drawing.Color GetObjectColor()
        {
            System.Drawing.Color color = System.Drawing.Color.FromArgb(0, 0, 1);

            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL)
                {
                    this.myAISContext.InitSelected();
                    if (!this.myAISContext.MoreSelected())
                    {
                        return color;
                    }

                    AIS_InteractiveObject IO = this.myAISContext.SelectedInteractive();
                    if (IO.HasColor)
                    {
                        Quantity_Color qColor;

                        this.myAISContext.Color(this.myAISContext.SelectedInteractive(), out qColor);

                        double c1, c2, c3;
                        qColor.Values(out c1, out c2, out c3, Quantity_TypeOfColor.Quantity_TOC_RGB);
                        int r = (int)(c1 * 255);
                        int g = (int)(c2 * 255);
                        int b = (int)(c3 * 255);

                        color = System.Drawing.Color.FromArgb(r, g, b);
                        return color;
                    }
                }
                else
                {
                    TraceTool.TTrace.Warning.Send("OCCViewer - GetObjectColor: AISContext is a nullptr pointer.");
                }
            }
            finally
            {
                UnlockRendering();
            }

            return color;
        }

        /// <summary>
        /// Apply the RGB color of the selected object.
        /// </summary>
        /// <param name="color">The color to apply.</param>
        public void SetObjectColor(System.Drawing.Color color)
        {
            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL)
                {
                    int r = (int)color.R;
                    int g = (int)color.G;
                    int b = (int)color.B;

                    this.SetColor(r, g, b);
                }
                else
                {
                    TraceTool.TTrace.Warning.Send("OCCViewer - SetObjectColor: AISContext is a nullptr pointer.");
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Sets the transparency value of the shape.
        /// </summary>
        /// <param name="transparency">The tansparency parameter.</param>
        public void SetTransparency(int transparency)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetTransparency: AISContext is a null pointer.");
                return;
            }

            try
            {
                LockRendering();

                for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
                {
                    this.myAISContext.SetTransparency(this.myAISContext.SelectedInteractive(), (double)transparency / 10.0, false);
                }
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Sets the material value of the shape.
        /// </summary>
        /// <param name="mat">The material parameter.</param>
        public void SetMaterial(int mat)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetMaterial: AISContext is a null pointer.");
                return;
            }

            try
            {
                LockRendering();

                //Graphic3d_NameOfMaterial material = new Graphic3d_NameOfMaterial();
                //object boxedEnumValue = Enum.ToObject(material, mat);

                // Predefined name
                var material = Graphic3d_NameOfMaterial.Graphic3d_NameOfMaterial_Aluminum;
                if (Enum.IsDefined(typeof(Graphic3d_NameOfMaterial), mat))
                {
                    material = (Graphic3d_NameOfMaterial)mat;
                }

                var objMat = new Graphic3d_MaterialAspect(material);

                for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
                {
                    this.myAISContext.SetMaterial(this.myAISContext.SelectedInteractive(),
                                                  objMat, false);
                }
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        #endregion

        #region Add/Remove Handling

        /// <summary>
        /// Removes all selected objects from the viewer.
        /// </summary>
        public void EraseObjects()
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - EraseObjects: AISContext is a null pointer.");
                return;
            }

            try
            {
                LockRendering();

                this.myAISContext.InitSelected();
                while (myAISContext.MoreSelected())
                {
                    // Remove the object from context
                    this.myAISContext.Erase(myAISContext.SelectedInteractive(), true);
                    this.myAISContext.InitSelected();
                }

                this.myAISContext.ClearSelected(true);
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        #endregion

        #region Mouse Handling

        /// <summary>
        /// Select object by mouse click.
        /// </summary>
        /// <remarks>
        /// See https://dev.opencascade.org/doc/overview/html/occt_user_guides__visualization.html
        /// </remarks>
        public void Select()
        {
            try
            {
                LockRendering();

                if (this.myAISContext != null)
                {
                    // TEST
                    // // Select the detected owners
                    //this.myAISContext.SelectDetected(AIS_SelectionScheme.AIS_SelectionScheme_Replace);
                    this.myAISContext.SelectDetected(); // TODO: bad!
                    this.UpdateCurrentViewer();
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Select: AISContext is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Select the specified object.
        /// </summary>
        /// <param name="objectToSelect">The object to 
        /// be selected.</param>	
        public void Select(AIS_InteractiveObject objectToSelect)
        {
            if (objectToSelect.IsNULL == true)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - Select: Object to select is not valid.");
                return;
            }

            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL)
                {
                    AIS_ListOfInteractive LOI = new AIS_ListOfInteractive();
                    this.myAISContext.DisplayedObjects(out LOI);
                    this.myAISContext.Deactivate();
                    this.myAISContext.Activate(0);

                    AIS_InteractiveObject IO;
                    //AIS_ListIteratorOfListOfInteractive aListIterator = new AIS_ListIteratorOfListOfInteractive();
                    //for (aListIterator.Initialize(aList); aListIterator.More(); aListIterator.Next())
                    for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected()) // occ version 7.3.0
                    {
                        //if (aListIterator.Value() == objectToSelect)
                        //{ 
                        //    this.myAISContext.SetSelected(objectToSelect, false); 
                        //}

                        IO = this.myAISContext.SelectedInteractive();
                        if (IO == objectToSelect)
                        {
                            this.myAISContext.SetSelected(objectToSelect, false);
                        }
                    }
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Select: AISContext is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Select object by dragging a rectangle while holding the Shift key.
        /// </summary>
        public void Select(int x1, int y1, int x2, int y2)
        {
            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL)
                {
                    this.myAISContext.SelectRectangle(new Graphic3d_Vec2i(x1, y1),
                                                      new Graphic3d_Vec2i(x2, y2),
                                                      this.myView);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Select: AISContext is a null pointer.");
                //}
                this.UpdateCurrentViewer();
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Selects objects within the bounding rectangle while the 'Shift' key is pressed.
        /// </summary>
        /// <param name="x1">The rectangle's lower x-coordinate.</param>
        /// <param name="y1">The rectangle's lower y-coordinate.</param>
        /// <param name="x2">The rectangle's upper x-coordinate.</param>
        /// <param name="y2">The rectangle's upper y-coordinate.</param>
        public void ShiftSelect(int x1, int y1, int x2, int y2)
        {
            try
            {
                if ((!this.myAISContext.IsNULL) && (!this.myView.IsNULL))
                {
                    //myAISContext.ShiftSelect(x1, y1, x2, y2, myView, true);

                    this.myAISContext.SelectRectangle(new Graphic3d_Vec2i(x1, y1), new Graphic3d_Vec2i(x2, y2),
                                                      this.myView, AIS_SelectionScheme.AIS_SelectionScheme_XOR);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - ShiftSelect: AISContext is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Adds the last detected object to the list of previously picked objects 
        /// while the 'Shift' key is pressed.
        /// </summary>
        public void ShiftSelect()
        {
            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL)
                {
                    this.myAISContext.SelectDetected(AIS_SelectionScheme.AIS_SelectionScheme_XOR);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - ShiftSelect: AISContext is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Begin the rotation of the view around the screen axis
        /// according to the mouse position (X,Y).
        /// </summary>
        /// <param name="x">The mouse's x-coordinate.</param>
        /// <param name="y">The mouse's y-coordinate.</param>
        public void StartRotation(int x, int y)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.StartRotation(x, y);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - StartRotation: V3d_View is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the object is selected.
        /// </summary>
        /// <returns>Returns true if the object is selected; Otherwise return false.</returns>
        public bool IsObjectSelected()
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - IsObjectSelected: AISContext is a null pointer.");
                return false;
            }

            this.myAISContext.InitSelected();
            return this.myAISContext.MoreSelected() != false;
        }

        /// <summary>
        /// Zoom the view according to a zoom factor computed 
        /// from the distance between the last and new mouse position. 
        /// </summary>
        /// <param name="x1">The last mouse position in x.</param>
        /// <param name="y1">The last mouse position in y.</param>
        /// <param name="x2">The new mouse position in x.</param>
        /// <param name="y2">The new mouse position in y.</param>
        public void Zoom(int x1, int y1, int x2, int y2)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.Zoom(x1, y1, x2, y2);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Zoom: V3d_View is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Zoom in all view.
        /// </summary>
        public void ZoomAllView()
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.FitAll();
                    this.myView.ZFitAll();
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - ZoomAllView: V3d_View is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Translates the center of the view
        /// and updates the view.
        /// </summary>
        /// <param name="x">The translation in x.</param>
        /// <param name="y">The translation in y.</param>
        public void Pan(int x, int y)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.Pan(x, y);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Pan: V3d_View is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// The rotation of the view according to the mouse position (X,Y).
        /// </summary>
        /// <param name="x">The new mouse position in x.</param>
        /// <param name="y">The new mouse position in y.</param> 
        public void Rotation(int x, int y)
        {
            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.myView.Rotation(x, y);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - Rotation: V3d_View is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        /// <summary>
        /// Relays mouse position in pixels x and y to the interactive context selectors.
        /// </summary>
        /// <param name="x">The x-location.</param>
        /// <param name="y">The y-location.</param>
        public void MoveTo(int x, int y)
        {
            try
            {
                LockRendering();

                if (!this.myAISContext.IsNULL && !this.myView.IsNULL)
                {
                    this.myAISContext.MoveTo(x, y, this.myView, true);
                }
                //else
                //{
                //    TraceTool.TTrace.Warning.Send("OCCViewer - MoveTo: AISContext is a null pointer.");
                //}
            }
            finally
            {
                UnlockRendering();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adjusts the viewing volume so as not to clip the displayed objects by front and back
        /// and back clipping planes. Also sets depth value automatically depending on the
        /// calculated Z size and Aspect parameter.
        /// </summary>
        /// <remarks>
        /// NOTE than the original XY size of the view is NOT modified.
        /// </remarks>
        private void PerformDepth()
        {
            if (myView.IsNULL == false)
            {
                myView.ZFitAll();
                myView.DepthFitAll();
            }
        }

        /// <summary>
        /// Sets the color of the selected object in viewer.
        /// </summary>
        /// <param name="r">The red color.</param>
        /// <param name="g">The green color.</param>
        /// <param name="b">The blue color.</param>
        private void SetColor(int r, int g, int b)
        {
            Quantity_Color col = new Quantity_Color((double)r / 255, (double)g / 255, (double)b / 255,
                                                    Quantity_TypeOfColor.Quantity_TOC_RGB);
            for (this.myAISContext.InitSelected(); this.myAISContext.MoreSelected(); this.myAISContext.NextSelected())
            {
                this.myAISContext.SetColor(this.myAISContext.SelectedInteractive(), col, false);
            }
        }

        #endregion

        #region Background

        /// <summary>
        /// Gets the background color.
        /// </summary>
        /// <returns>Returns the color of the background.</returns>
        public System.Drawing.Color GetBackgroundColor()
        {
            return this.mBgColor;
        }

        /// <summary>
        /// Sets the background RGB color.
        /// </summary>
        /// <param name="color">The color to apply.</param>
        public void SetBackgroundColor(System.Drawing.Color col)
        {
            if (this.myView == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetBackgroundColor: V3d_View is a nullptr pointer.");
                return;
            }

            try
            {
                LockRendering();

                if (!this.myView.IsNULL)
                {
                    this.mBgColor = col;
                    int r = (int)col.R;
                    int g = (int)col.G;
                    int b = (int)col.B;

                    System.Drawing.Color color = this.mUseGradientColor == true ? this.mBottomColor : this.mBgColor;

                    Quantity_Color color1 = new Quantity_Color((double)r / 255, (double)g / 255, (double)b / 255,
                                                                Quantity_TypeOfColor.Quantity_TOC_RGB);

                    Quantity_Color color2 = new Quantity_Color((double)color.R / 255, (double)color.G / 255, (double)color.B / 255,
                                            Quantity_TypeOfColor.Quantity_TOC_RGB);

                    this.myView.SetBgGradientColors(color1, color2, Aspect_GradientFillMethod.Aspect_GFM_VER, true);

                    this.mTopColor = this.mBgColor;
                }
            }
            finally
            {
                UnlockRendering();
            }
        }

        private void SetGradientBackgroundColors()
        {
            if (this.myView == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetGradientBackgroundColors: V3d_View is a null pointer.");
                return;
            }

            // Gardient color
            if (this.mUseGradientColor == true)
            {
                this.mBgColor = this.mTopColor;
                this.UpdateGradientColor();
            }
            // Single color
            else
            {
                this.SetBackgroundColor(this.mTopColor);
            }
        }

        private void UpdateGradientColor()
        {
            if (!this.myView.IsNULL)
            {
                // Top Color
                int rTop = Convert.ToInt32((this.mTopColor.R).ToString());
                int gTop = Convert.ToInt32((this.mTopColor.G).ToString());
                int bTop = Convert.ToInt32((this.mTopColor.B).ToString());

                // Bottom Color
                int rBtm = Convert.ToInt32((this.mBottomColor.R).ToString());
                int gBtm = Convert.ToInt32((this.mBottomColor.G).ToString());
                int bBtm = Convert.ToInt32((this.mBottomColor.B).ToString());

                // Defines the gradient background color of the view
                Quantity_Color topColor = new Quantity_Color((double)rTop / 255, (double)gTop / 255, (double)bTop / 255,
                                                             Quantity_TypeOfColor.Quantity_TOC_RGB);
                if (this.mUseGradientColor)
                {
                    Quantity_Color bottomColor = new Quantity_Color((double)rBtm / 255, (double)gBtm / 255, (double)bBtm / 255,
                                                                    Quantity_TypeOfColor.Quantity_TOC_RGB);
                    this.myView.SetBgGradientColors(topColor, bottomColor, Aspect_GradientFillMethod.Aspect_GFM_VER, true);
                }
            }
            else
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - UpdateGradientColor: V3d_View is a null pointer.");
            }
        }

        #endregion

        #region Trihedron Handling

        #region Dynamic 

        /// <summary>
        /// Sets the size of the trihedron.
        /// </summary>
        /// <param name="size">The trihedron size.</param>
        private void SetTrihedronSize(double size)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - SetTrihedronSize: AISContext is a null pointer.");
                return;
            }
            if (this.myAISContext.IsDisplayed(this.myTrihedron))
            {
                this.myAISContext.SetTrihedronSize(size, true);
            }
            else
            {
                if (!this.myTrihedron.IsNULL)
                {
                    this.myAISContext.SetTrihedronSize(size, false);
                }
            }
        }

        /// <summary>
        /// Gets the value of trihedron size.
        /// </summary>
        /// <returns>Returns the current value of trihedron size.</returns>
        private double GetTrihedronSize()
        {
            double size = this.mAxisSize; // the default value 100 mm for display of trihedra
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - GetTrihedronSize: AISContext is a null pointer.");
                return size;
            }
            if (this.myAISContext.IsDisplayed(this.myTrihedron))
            {
                size = this.myAISContext.TrihedronSize;
            }
            return size;
        }

        /// <summary>
        /// Create a trihedron axis.
        /// </summary>
        /// <param name="visible">The parameter visibility.</param>
        private void CreateTrihedronAxis(bool visible)
        {
            if (this.myAISContext == null)
            {
                TraceTool.TTrace.Warning.Send("OCCViewer - CreateTrihedronAxis: AISContext is a null pointer.");
                return;
            }

            if (visible)
            {
                Geom_Axis2Placement trihedronAxis = new Geom_Axis2Placement(gp_Ax2.XOY);
                this.myTrihedron = new AIS_Trihedron(trihedronAxis);
                this.SetTrihedronSize(this.mAxisSize);
                //this.myTrihedron.UnsetSelectionMode();
                this.myAISContext.Display(this.myTrihedron, false);
                this.UpdateCurrentViewer();
            }
            else
            {
                if (this.myAISContext.IsDisplayed(this.myTrihedron))
                {
                    this.myAISContext.Remove(this.myTrihedron, false);
                    this.UpdateCurrentViewer();
                }
            }
        }

        #endregion

        #region Static

        /// <summary>
        /// Create a static trihedron.
        /// </summary>
        /// <param name="visible">The parameter visibility.</param>
        /// <param name="trihedronScale">
        /// Initialize the length of Triedron axes.
        /// NOTE: the scale is a percent of the window width.
        /// </param>
        /// <param name="triedronColor">Initialize the color of Triedron axes.</param>
        /// <param name="position">Initialize position of Triedron axes.</param>
        private void CreateStaticTrihedronAxis(bool visible, double trihedronScale,
                                          Quantity_Color triedronColor,
                                          Aspect_TypeOfTriedronPosition position)
        {
            // Static TRIHEDRON - not sizable, not movable
            if (this.myView == null)
            {
                return;
            }

            if (visible)
            {
                //myView.TriedronDisplay( position, triedronColor, trihedronScale );	
                this.myView.TriedronDisplay(position, triedronColor, trihedronScale, V3d_TypeOfVisualization.V3d_ZBUFFER);
            }
            else
            {
                this.myView.TriedronErase();
            }
            this.myView.Update();
        }

        /// <summary>
        /// Gets the label trihedron's color.
        /// </summary>
        /// <returns></returns>
        private System.Drawing.Color GetStaticTrihedronColor()
        {
            Quantity_Color curcolor = this.mTriedronColor;
            double R1 = curcolor.Red;
            double G1 = curcolor.Green;
            double B1 = curcolor.Blue;

            int r = (int)(R1 * 255);
            int g = (int)(G1 * 255);
            int b = (int)(B1 * 255);

            return System.Drawing.Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Sets the label trihedron's color.
        /// </summary>
        /// <param name="color">The color to set.</param>
        private void SetStaticTrihedronColor(System.Drawing.Color color)
        {
            //if (this.myView == null)
            //{
            //    TraceTool.TTrace.Warning.Send("OCCViewer - SetStaticTrihedronColor: V3d_View is a nullptr pointer.");
            //    return;
            //}

            Quantity_Color curcolor = new Quantity_Color(color.R / 255, color.G / 255, color.B / 255,
                                             Quantity_TypeOfColor.Quantity_TOC_RGB);
            this.mTriedronColor = curcolor;
            this.CreateStaticTrihedronAxis(this.mUseStaticTrihedronAxis, this.mTrihedronScale, this.mTriedronColor, this.mTriedronPosition);
        }

        #endregion

        #endregion

        #region Lock Windows

        private void LockRendering()
        {
            System.Threading.Monitor.Enter(mSynchObj);
        }

        private void UnlockRendering()
        {
            if (System.Threading.Monitor.IsEntered(mSynchObj))
            {
                System.Threading.Monitor.Exit(mSynchObj);
            }
        }

        #endregion
    }
}
