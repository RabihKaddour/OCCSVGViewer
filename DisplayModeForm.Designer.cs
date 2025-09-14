namespace OCCSVGViewer
{
    partial class DisplayModeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayModeForm));
            MainTableLayoutPanel = new TableLayoutPanel();
            ModeTableLayoutPanel = new TableLayoutPanel();
            LblShaded = new Label();
            LblWire = new Label();
            RBWire = new RadioButton();
            RBShaded = new RadioButton();
            ConfirmTableLayoutPanel = new TableLayoutPanel();
            BtnSelect = new Button();
            MainTableLayoutPanel.SuspendLayout();
            ModeTableLayoutPanel.SuspendLayout();
            ConfirmTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainTableLayoutPanel
            // 
            resources.ApplyResources(MainTableLayoutPanel, "MainTableLayoutPanel");
            MainTableLayoutPanel.Controls.Add(ModeTableLayoutPanel, 0, 0);
            MainTableLayoutPanel.Controls.Add(ConfirmTableLayoutPanel, 0, 1);
            MainTableLayoutPanel.Name = "MainTableLayoutPanel";
            // 
            // ModeTableLayoutPanel
            // 
            resources.ApplyResources(ModeTableLayoutPanel, "ModeTableLayoutPanel");
            ModeTableLayoutPanel.Controls.Add(LblShaded, 1, 1);
            ModeTableLayoutPanel.Controls.Add(LblWire, 1, 0);
            ModeTableLayoutPanel.Controls.Add(RBWire, 0, 0);
            ModeTableLayoutPanel.Controls.Add(RBShaded, 0, 1);
            ModeTableLayoutPanel.Name = "ModeTableLayoutPanel";
            // 
            // LblShaded
            // 
            resources.ApplyResources(LblShaded, "LblShaded");
            LblShaded.ForeColor = Color.Black;
            LblShaded.Name = "LblShaded";
            // 
            // LblWire
            // 
            resources.ApplyResources(LblWire, "LblWire");
            LblWire.ForeColor = Color.Black;
            LblWire.Name = "LblWire";
            // 
            // RBWire
            // 
            resources.ApplyResources(RBWire, "RBWire");
            RBWire.Checked = true;
            RBWire.Name = "RBWire";
            RBWire.TabStop = true;
            RBWire.UseVisualStyleBackColor = true;
            // 
            // RBShaded
            // 
            resources.ApplyResources(RBShaded, "RBShaded");
            RBShaded.Name = "RBShaded";
            RBShaded.UseVisualStyleBackColor = true;
            // 
            // ConfirmTableLayoutPanel
            // 
            resources.ApplyResources(ConfirmTableLayoutPanel, "ConfirmTableLayoutPanel");
            ConfirmTableLayoutPanel.Controls.Add(BtnSelect, 2, 1);
            ConfirmTableLayoutPanel.Name = "ConfirmTableLayoutPanel";
            // 
            // BtnSelect
            // 
            BtnSelect.DialogResult = DialogResult.OK;
            resources.ApplyResources(BtnSelect, "BtnSelect");
            BtnSelect.Name = "BtnSelect";
            BtnSelect.UseVisualStyleBackColor = true;
            // 
            // DisplayModeForm
            // 
            AcceptButton = BtnSelect;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MainTableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DisplayModeForm";
            MainTableLayoutPanel.ResumeLayout(false);
            MainTableLayoutPanel.PerformLayout();
            ModeTableLayoutPanel.ResumeLayout(false);
            ModeTableLayoutPanel.PerformLayout();
            ConfirmTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTableLayoutPanel;
        private TableLayoutPanel ModeTableLayoutPanel;
        private Label LblWire;
        private Label LblShaded;
        private RadioButton RBWire;
        private RadioButton RBShaded;
        private TableLayoutPanel ConfirmTableLayoutPanel;
        private Button BtnSelect;
    }
}