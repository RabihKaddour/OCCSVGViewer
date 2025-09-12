namespace OCCSVGViewer
{
    partial class SVGUnitForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SVGUnitForm));
            MainTableLayoutPanel = new TableLayoutPanel();
            InforPictureBox = new PictureBox();
            ContentTableLayoutPanel = new TableLayoutPanel();
            InfoLabel = new Label();
            BtnOk = new Button();
            MainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InforPictureBox).BeginInit();
            ContentTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            resources.ApplyResources(MainTableLayoutPanel, "MainTableLayoutPanel");
            MainTableLayoutPanel.Controls.Add(InforPictureBox, 0, 0);
            MainTableLayoutPanel.Controls.Add(ContentTableLayoutPanel, 1, 0);
            MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            // 
            // InforPictureBox
            // 
            resources.ApplyResources(InforPictureBox, "InforPictureBox");
            InforPictureBox.Name = "InforPictureBox";
            InforPictureBox.TabStop = false;
            // 
            // ContentTableLayoutPanel
            // 
            resources.ApplyResources(ContentTableLayoutPanel, "ContentTableLayoutPanel");
            ContentTableLayoutPanel.Controls.Add(InfoLabel, 0, 0);
            ContentTableLayoutPanel.Controls.Add(BtnOk, 2, 1);
            ContentTableLayoutPanel.Name = "ContentTableLayoutPanel";
            // 
            // InfoLabel
            // 
            resources.ApplyResources(InfoLabel, "InfoLabel");
            ContentTableLayoutPanel.SetColumnSpan(InfoLabel, 3);
            InfoLabel.Name = "InfoLabel";
            // 
            // BtnOk
            // 
            resources.ApplyResources(BtnOk, "BtnOk");
            BtnOk.Name = "BtnOk";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // SVGUnitForm
            // 
            AcceptButton = BtnOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainTableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SVGUnitForm";
            MainTableLayoutPanel.ResumeLayout(false);
            MainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InforPictureBox).EndInit();
            ContentTableLayoutPanel.ResumeLayout(false);
            ContentTableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private PictureBox InforPictureBox;
        private TableLayoutPanel ContentTableLayoutPanel;
        private Label InfoLabel;
        private Button BtnOk;
    }
}