namespace OCCSVGViewer
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            MainGroupBox = new GroupBox();
            BtnOk = new Button();
            LinePictureBox = new PictureBox();
            CopyRightLabel = new Label();
            CenterPictureBox = new PictureBox();
            TitleLabel = new Label();
            CornerPictureBox = new PictureBox();
            LbPhone = new Label();
            LblMail = new Label();
            LinkLabelMail = new LinkLabel();
            MainGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LinePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CenterPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CornerPictureBox).BeginInit();
            SuspendLayout();
            // 
            // MainGroupBox
            // 
            MainGroupBox.Controls.Add(LinkLabelMail);
            MainGroupBox.Controls.Add(LblMail);
            MainGroupBox.Controls.Add(LbPhone);
            MainGroupBox.Controls.Add(BtnOk);
            MainGroupBox.Controls.Add(LinePictureBox);
            MainGroupBox.Controls.Add(CopyRightLabel);
            MainGroupBox.Controls.Add(CenterPictureBox);
            MainGroupBox.Controls.Add(TitleLabel);
            MainGroupBox.Controls.Add(CornerPictureBox);
            MainGroupBox.Location = new Point(16, 10);
            MainGroupBox.Name = "MainGroupBox";
            MainGroupBox.Size = new Size(357, 512);
            MainGroupBox.TabIndex = 0;
            MainGroupBox.TabStop = false;
            // 
            // BtnOk
            // 
            BtnOk.Location = new Point(105, 462);
            BtnOk.Name = "BtnOk";
            BtnOk.Size = new Size(135, 36);
            BtnOk.TabIndex = 5;
            BtnOk.Text = "Ok";
            BtnOk.UseVisualStyleBackColor = true;
            BtnOk.Click += BtnOk_Click;
            // 
            // LinePictureBox
            // 
            LinePictureBox.BorderStyle = BorderStyle.FixedSingle;
            LinePictureBox.Location = new Point(12, 449);
            LinePictureBox.Name = "LinePictureBox";
            LinePictureBox.Size = new Size(330, 1);
            LinePictureBox.TabIndex = 4;
            LinePictureBox.TabStop = false;
            // 
            // CopyRightLabel
            // 
            CopyRightLabel.AutoSize = true;
            CopyRightLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CopyRightLabel.Location = new Point(48, 356);
            CopyRightLabel.Name = "CopyRightLabel";
            CopyRightLabel.Size = new Size(259, 17);
            CopyRightLabel.TabIndex = 3;
            CopyRightLabel.Text = "Copyright (C) 2025, Dr.-Ing. Rabih Kaddour";
            CopyRightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CenterPictureBox
            // 
            CenterPictureBox.Image = (Image)resources.GetObject("CenterPictureBox.Image");
            CenterPictureBox.Location = new Point(12, 86);
            CenterPictureBox.Name = "CenterPictureBox";
            CenterPictureBox.Size = new Size(330, 258);
            CenterPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            CenterPictureBox.TabIndex = 2;
            CenterPictureBox.TabStop = false;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TitleLabel.Location = new Point(105, 33);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(168, 30);
            TitleLabel.TabIndex = 1;
            TitleLabel.Text = "OCCSVG Viewer";
            // 
            // CornerPictureBox
            // 
            CornerPictureBox.Image = (Image)resources.GetObject("CornerPictureBox.Image");
            CornerPictureBox.Location = new Point(12, 12);
            CornerPictureBox.Name = "CornerPictureBox";
            CornerPictureBox.Size = new Size(82, 68);
            CornerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            CornerPictureBox.TabIndex = 0;
            CornerPictureBox.TabStop = false;
            // 
            // LbPhone
            // 
            LbPhone.AutoSize = true;
            LbPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LbPhone.Location = new Point(48, 382);
            LbPhone.Name = "LbPhone";
            LbPhone.Size = new Size(155, 17);
            LbPhone.TabIndex = 6;
            LbPhone.Text = "Phone: +49 17631069362";
            LbPhone.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LblMail
            // 
            LblMail.AutoSize = true;
            LblMail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LblMail.Location = new Point(48, 409);
            LblMail.Name = "LblMail";
            LblMail.Size = new Size(36, 17);
            LblMail.TabIndex = 7;
            LblMail.Text = "Mail:";
            LblMail.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LinkLabelMail
            // 
            LinkLabelMail.AutoSize = true;
            LinkLabelMail.Location = new Point(90, 411);
            LinkLabelMail.Name = "LinkLabelMail";
            LinkLabelMail.Size = new Size(134, 15);
            LinkLabelMail.TabIndex = 8;
            LinkLabelMail.TabStop = true;
            LinkLabelMail.Text = "rabih_kaddour@gmx.de";
            // 
            // AboutDialog
            // 
            AcceptButton = BtnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 534);
            ControlBox = false;
            Controls.Add(MainGroupBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AboutDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            MainGroupBox.ResumeLayout(false);
            MainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LinePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)CenterPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)CornerPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox MainGroupBox;
        private PictureBox CornerPictureBox;
        private Label TitleLabel;
        private PictureBox CenterPictureBox;
        private Label CopyRightLabel;
        private PictureBox LinePictureBox;
        private Button BtnOk;
        private Label LbPhone;
        private Label LblMail;
        private LinkLabel LinkLabelMail;
    }
}