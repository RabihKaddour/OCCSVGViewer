namespace OCCSVGViewer
{
    partial class OCCMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OCCMainForm));
            MainToolStripContainer = new ToolStripContainer();
            Canvas3DPanel = new Panel();
            ModelPanel = new Panel();
            ModelTableLayoutPanel = new TableLayoutPanel();
            ModelTreeView = new TreeView();
            ModelImageList = new ImageList(components);
            ModelPropertyGrid = new PropertyGrid();
            ToolBar = new ToolStrip();
            FitAllTSB = new ToolStripButton();
            ZoomInTSB = new ToolStripButton();
            ZoomOutTSB = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            ViewFrontTSB = new ToolStripButton();
            ViewBackTSB = new ToolStripButton();
            ViewTopTSB = new ToolStripButton();
            ViewBottomTSB = new ToolStripButton();
            ViewLeftTSB = new ToolStripButton();
            ViewRightTSB = new ToolStripButton();
            ResetViewTSB = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            MoveViewTSB = new ToolStripButton();
            RotateViewTSB = new ToolStripButton();
            ZoomWindowTSB = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            MainMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            LoadVectorToolStripMenuItem = new ToolStripMenuItem();
            HelpToolStripMenuItem = new ToolStripMenuItem();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            ReportToolStripMenuItem = new ToolStripMenuItem();
            StatusStrip = new StatusStrip();
            MainToolStripContainer.ContentPanel.SuspendLayout();
            MainToolStripContainer.TopToolStripPanel.SuspendLayout();
            MainToolStripContainer.SuspendLayout();
            ModelPanel.SuspendLayout();
            ModelTableLayoutPanel.SuspendLayout();
            ToolBar.SuspendLayout();
            MainMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainToolStripContainer
            // 
            // 
            // MainToolStripContainer.ContentPanel
            // 
            MainToolStripContainer.ContentPanel.Controls.Add(Canvas3DPanel);
            MainToolStripContainer.ContentPanel.Controls.Add(ModelPanel);
            resources.ApplyResources(MainToolStripContainer.ContentPanel, "MainToolStripContainer.ContentPanel");
            resources.ApplyResources(MainToolStripContainer, "MainToolStripContainer");
            MainToolStripContainer.Name = "MainToolStripContainer";
            // 
            // MainToolStripContainer.TopToolStripPanel
            // 
            MainToolStripContainer.TopToolStripPanel.Controls.Add(ToolBar);
            // 
            // Canvas3DPanel
            // 
            Canvas3DPanel.BackColor = SystemColors.ActiveCaption;
            resources.ApplyResources(Canvas3DPanel, "Canvas3DPanel");
            Canvas3DPanel.BorderStyle = BorderStyle.Fixed3D;
            Canvas3DPanel.Name = "Canvas3DPanel";
            Canvas3DPanel.Paint += Canvas3DPanel_Paint;
            Canvas3DPanel.MouseDown += Canvas3DPanel_MouseDown;
            Canvas3DPanel.MouseMove += Canvas3DPanel_MouseMove;
            Canvas3DPanel.MouseUp += Canvas3DPanel_MouseUp;
            // 
            // ModelPanel
            // 
            resources.ApplyResources(ModelPanel, "ModelPanel");
            ModelPanel.Controls.Add(ModelTableLayoutPanel);
            ModelPanel.Name = "ModelPanel";
            // 
            // ModelTableLayoutPanel
            // 
            resources.ApplyResources(ModelTableLayoutPanel, "ModelTableLayoutPanel");
            ModelTableLayoutPanel.Controls.Add(ModelTreeView, 0, 0);
            ModelTableLayoutPanel.Controls.Add(ModelPropertyGrid, 0, 1);
            ModelTableLayoutPanel.Name = "ModelTableLayoutPanel";
            // 
            // ModelTreeView
            // 
            resources.ApplyResources(ModelTreeView, "ModelTreeView");
            ModelTreeView.HideSelection = false;
            ModelTreeView.ImageList = ModelImageList;
            ModelTreeView.Name = "ModelTreeView";
            ModelTreeView.AfterCheck += ModelTreeView_AfterCheck;
            ModelTreeView.AfterSelect += ModelTreeView_AfterSelect;
            ModelTreeView.NodeMouseClick += ModelTreeView_NodeMouseClick;
            // 
            // ModelImageList
            // 
            ModelImageList.ColorDepth = ColorDepth.Depth32Bit;
            ModelImageList.ImageStream = (ImageListStreamer)resources.GetObject("ModelImageList.ImageStream");
            ModelImageList.TransparentColor = Color.Transparent;
            ModelImageList.Images.SetKeyName(0, "Application");
            ModelImageList.Images.SetKeyName(1, "Element");
            ModelImageList.Images.SetKeyName(2, "Rectangle");
            ModelImageList.Images.SetKeyName(3, "Circle");
            ModelImageList.Images.SetKeyName(4, "Ellipse");
            ModelImageList.Images.SetKeyName(5, "Line");
            ModelImageList.Images.SetKeyName(6, "Polyline");
            ModelImageList.Images.SetKeyName(7, "Triangle");
            ModelImageList.Images.SetKeyName(8, "Text");
            // 
            // ModelPropertyGrid
            // 
            resources.ApplyResources(ModelPropertyGrid, "ModelPropertyGrid");
            ModelPropertyGrid.Name = "ModelPropertyGrid";
            // 
            // ToolBar
            // 
            resources.ApplyResources(ToolBar, "ToolBar");
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { FitAllTSB, ZoomInTSB, ZoomOutTSB, toolStripSeparator1, ViewFrontTSB, ViewBackTSB, ViewTopTSB, ViewBottomTSB, ViewLeftTSB, ViewRightTSB, ResetViewTSB, toolStripSeparator2, MoveViewTSB, RotateViewTSB, ZoomWindowTSB, toolStripSeparator3 });
            ToolBar.Name = "ToolBar";
            // 
            // FitAllTSB
            // 
            FitAllTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(FitAllTSB, "FitAllTSB");
            FitAllTSB.Margin = new Padding(0, 1, 6, 2);
            FitAllTSB.Name = "FitAllTSB";
            FitAllTSB.Click += FitAllTSB_Click;
            // 
            // ZoomInTSB
            // 
            ZoomInTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ZoomInTSB, "ZoomInTSB");
            ZoomInTSB.Margin = new Padding(0, 1, 6, 2);
            ZoomInTSB.Name = "ZoomInTSB";
            ZoomInTSB.Click += ZoomInTSB_Click;
            // 
            // ZoomOutTSB
            // 
            ZoomOutTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ZoomOutTSB, "ZoomOutTSB");
            ZoomOutTSB.Name = "ZoomOutTSB";
            ZoomOutTSB.Click += ZoomOutTSB_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new Padding(0, 0, 3, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // ViewFrontTSB
            // 
            ViewFrontTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewFrontTSB, "ViewFrontTSB");
            ViewFrontTSB.Margin = new Padding(3, 1, 6, 2);
            ViewFrontTSB.Name = "ViewFrontTSB";
            ViewFrontTSB.Click += ViewFrontTSB_Click;
            // 
            // ViewBackTSB
            // 
            ViewBackTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewBackTSB, "ViewBackTSB");
            ViewBackTSB.Margin = new Padding(0, 1, 6, 2);
            ViewBackTSB.Name = "ViewBackTSB";
            ViewBackTSB.Click += ViewBackTSB_Click;
            // 
            // ViewTopTSB
            // 
            ViewTopTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewTopTSB, "ViewTopTSB");
            ViewTopTSB.Margin = new Padding(0, 1, 6, 2);
            ViewTopTSB.Name = "ViewTopTSB";
            ViewTopTSB.Click += ViewTopTSB_Click;
            // 
            // ViewBottomTSB
            // 
            ViewBottomTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewBottomTSB, "ViewBottomTSB");
            ViewBottomTSB.Margin = new Padding(0, 1, 6, 2);
            ViewBottomTSB.Name = "ViewBottomTSB";
            ViewBottomTSB.Click += ViewBottomTSB_Click;
            // 
            // ViewLeftTSB
            // 
            ViewLeftTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewLeftTSB, "ViewLeftTSB");
            ViewLeftTSB.Margin = new Padding(0, 1, 6, 2);
            ViewLeftTSB.Name = "ViewLeftTSB";
            ViewLeftTSB.Click += ViewLeftTSB_Click;
            // 
            // ViewRightTSB
            // 
            ViewRightTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ViewRightTSB, "ViewRightTSB");
            ViewRightTSB.Margin = new Padding(0, 1, 6, 2);
            ViewRightTSB.Name = "ViewRightTSB";
            ViewRightTSB.Click += ViewRightTSB_Click;
            // 
            // ResetViewTSB
            // 
            ResetViewTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ResetViewTSB, "ResetViewTSB");
            ResetViewTSB.Margin = new Padding(0, 1, 6, 2);
            ResetViewTSB.Name = "ResetViewTSB";
            ResetViewTSB.Click += ResetViewTSB_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new Padding(0, 0, 3, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // MoveViewTSB
            // 
            MoveViewTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(MoveViewTSB, "MoveViewTSB");
            MoveViewTSB.Margin = new Padding(3, 1, 6, 2);
            MoveViewTSB.Name = "MoveViewTSB";
            MoveViewTSB.Click += MoveViewTSB_Click;
            // 
            // RotateViewTSB
            // 
            RotateViewTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(RotateViewTSB, "RotateViewTSB");
            RotateViewTSB.Margin = new Padding(0, 1, 6, 2);
            RotateViewTSB.Name = "RotateViewTSB";
            RotateViewTSB.Click += RotateViewTSB_Click;
            // 
            // ZoomWindowTSB
            // 
            ZoomWindowTSB.DisplayStyle = ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(ZoomWindowTSB, "ZoomWindowTSB");
            ZoomWindowTSB.Margin = new Padding(0, 1, 6, 2);
            ZoomWindowTSB.Name = "ZoomWindowTSB";
            ZoomWindowTSB.Click += ZoomWindowTSB_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Margin = new Padding(0, 0, 3, 0);
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // MainMenuStrip
            // 
            MainMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, HelpToolStripMenuItem });
            resources.ApplyResources(MainMenuStrip, "MainMenuStrip");
            MainMenuStrip.Name = "MainMenuStrip";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LoadVectorToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            resources.ApplyResources(FileToolStripMenuItem, "FileToolStripMenuItem");
            // 
            // LoadVectorToolStripMenuItem
            // 
            resources.ApplyResources(LoadVectorToolStripMenuItem, "LoadVectorToolStripMenuItem");
            LoadVectorToolStripMenuItem.Name = "LoadVectorToolStripMenuItem";
            LoadVectorToolStripMenuItem.Click += LoadVectorToolStripMenuItem_Click;
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AboutToolStripMenuItem, ReportToolStripMenuItem });
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            resources.ApplyResources(HelpToolStripMenuItem, "HelpToolStripMenuItem");
            // 
            // AboutToolStripMenuItem
            // 
            resources.ApplyResources(AboutToolStripMenuItem, "AboutToolStripMenuItem");
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            AboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // ReportToolStripMenuItem
            // 
            resources.ApplyResources(ReportToolStripMenuItem, "ReportToolStripMenuItem");
            ReportToolStripMenuItem.Name = "ReportToolStripMenuItem";
            ReportToolStripMenuItem.Click += ReportToolStripMenuItem_Click;
            // 
            // StatusStrip
            // 
            resources.ApplyResources(StatusStrip, "StatusStrip");
            StatusStrip.Name = "StatusStrip";
            // 
            // OCCMainForm
            // 
            AccessibleRole = AccessibleRole.Application;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(StatusStrip);
            Controls.Add(MainToolStripContainer);
            Controls.Add(MainMenuStrip);
            Name = "OCCMainForm";
            Shown += OCCViewDocContent_Shown;
            SizeChanged += OCCViewDocContent_SizeChanged;
            MainToolStripContainer.ContentPanel.ResumeLayout(false);
            MainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            MainToolStripContainer.TopToolStripPanel.PerformLayout();
            MainToolStripContainer.ResumeLayout(false);
            MainToolStripContainer.PerformLayout();
            ModelPanel.ResumeLayout(false);
            ModelPanel.PerformLayout();
            ModelTableLayoutPanel.ResumeLayout(false);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            MainMenuStrip.ResumeLayout(false);
            MainMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem LoadVectorToolStripMenuItem;
        private ToolStrip ToolBar;
        private ToolStripButton FitAllTSB;
        private ToolStripButton ZoomInTSB;
        private ToolStripButton ZoomOutTSB;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton ViewFrontTSB;
        private ToolStripButton ViewBackTSB;
        private ToolStripButton ViewTopTSB;
        private ToolStripButton ViewBottomTSB;
        private ToolStripButton ViewLeftTSB;
        private ToolStripButton ViewRightTSB;
        private ToolStripButton ResetViewTSB;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton MoveViewTSB;
        private ToolStripButton RotateViewTSB;
        private ToolStripButton ZoomWindowTSB;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripContainer MainToolStripContainer;
        private Panel Canvas3DPanel;
        private StatusStrip StatusStrip;
        private Panel ModelPanel;
        public TreeView ModelTreeView;
        private ImageList ModelImageList;
        private PropertyGrid ModelPropertyGrid;
        private TableLayoutPanel ModelTableLayoutPanel;
        private ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem ReportToolStripMenuItem;
        private ToolStripMenuItem AboutToolStripMenuItem;
    }
}
