namespace TurtleHub
{
	partial class OptionsDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( )
		{
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TxtOwner = new System.Windows.Forms.TextBox();
            this.TxtRepository = new System.Windows.Forms.TextBox();
            this.CmbKeyword = new System.Windows.Forms.ComboBox();
            this.CheckRefFullRepo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtPreview = new System.Windows.Forms.TextBox();
            this.CheckShowPrs = new System.Windows.Forms.CheckBox();
            this.CmbTracker = new System.Windows.Forms.ComboBox();
            this.TxtAPIToken = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 45);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 13);
            label1.TabIndex = 4;
            label1.Text = "Owner:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 71);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 13);
            label2.TabIndex = 5;
            label2.Text = "Repository:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 97);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 13);
            label3.TabIndex = 9;
            label3.Text = "Keyword:";
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Location = new System.Drawing.Point(210, 256);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 3;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(291, 256);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // TxtOwner
            // 
            this.TxtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtOwner.Location = new System.Drawing.Point(78, 42);
            this.TxtOwner.Name = "TxtOwner";
            this.TxtOwner.Size = new System.Drawing.Size(288, 20);
            this.TxtOwner.TabIndex = 0;
            this.TxtOwner.TextChanged += new System.EventHandler(this.TxtOwner_TextChanged);
            // 
            // TxtRepository
            // 
            this.TxtRepository.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtRepository.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TxtRepository.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TxtRepository.Location = new System.Drawing.Point(78, 68);
            this.TxtRepository.Name = "TxtRepository";
            this.TxtRepository.Size = new System.Drawing.Size(288, 20);
            this.TxtRepository.TabIndex = 1;
            this.TxtRepository.TextChanged += new System.EventHandler(this.TxtRepository_TextChanged);
            this.TxtRepository.Enter += new System.EventHandler(this.TxtRepository_Enter);
            // 
            // CmbKeyword
            // 
            this.CmbKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbKeyword.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbKeyword.FormattingEnabled = true;
            this.CmbKeyword.Items.AddRange(new object[] {
            "Close",
            "Closes",
            "Closed",
            "Fix",
            "Fixes",
            "Fixed",
            "Resolve",
            "Resolves",
            "Resolved",
            "<None>"});
            this.CmbKeyword.Location = new System.Drawing.Point(78, 94);
            this.CmbKeyword.Name = "CmbKeyword";
            this.CmbKeyword.Size = new System.Drawing.Size(288, 21);
            this.CmbKeyword.TabIndex = 2;
            this.CmbKeyword.SelectedIndexChanged += new System.EventHandler(this.CmbKeyword_SelectedIndexChanged);
            // 
            // CheckRefFullRepo
            // 
            this.CheckRefFullRepo.AutoSize = true;
            this.CheckRefFullRepo.Location = new System.Drawing.Point(78, 147);
            this.CheckRefFullRepo.Name = "CheckRefFullRepo";
            this.CheckRefFullRepo.Size = new System.Drawing.Size(179, 17);
            this.CheckRefFullRepo.TabIndex = 10;
            this.CheckRefFullRepo.Text = "Reference Full Repository Name";
            this.CheckRefFullRepo.UseVisualStyleBackColor = true;
            this.CheckRefFullRepo.Click += new System.EventHandler(this.CheckRefFullRepo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtPreview);
            this.groupBox1.Location = new System.Drawing.Point(13, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 44);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Example Message";
            // 
            // TxtPreview
            // 
            this.TxtPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPreview.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPreview.Location = new System.Drawing.Point(6, 19);
            this.TxtPreview.Name = "TxtPreview";
            this.TxtPreview.ReadOnly = true;
            this.TxtPreview.Size = new System.Drawing.Size(341, 13);
            this.TxtPreview.TabIndex = 0;
            this.TxtPreview.Text = "Closes #5";
            // 
            // CheckShowPrs
            // 
            this.CheckShowPrs.AutoSize = true;
            this.CheckShowPrs.Location = new System.Drawing.Point(78, 170);
            this.CheckShowPrs.Name = "CheckShowPrs";
            this.CheckShowPrs.Size = new System.Drawing.Size(172, 17);
            this.CheckShowPrs.TabIndex = 12;
            this.CheckShowPrs.Text = "Show Pull Requests by Default";
            this.CheckShowPrs.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 18);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(47, 13);
            label4.TabIndex = 14;
            label4.Text = "Tracker:";
            // 
            // CmbTracker
            // 
            this.CmbTracker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbTracker.FormattingEnabled = true;
            this.CmbTracker.Items.AddRange(new object[] {
            "GitHub",
            "http://gitlab.com",
            "http://"});
            this.CmbTracker.Location = new System.Drawing.Point(78, 15);
            this.CmbTracker.Name = "CmbTracker";
            this.CmbTracker.Size = new System.Drawing.Size(288, 21);
            this.CmbTracker.TabIndex = 13;
            // 
            // TxtAPIToken
            // 
            this.TxtAPIToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtAPIToken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TxtAPIToken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TxtAPIToken.Location = new System.Drawing.Point(78, 121);
            this.TxtAPIToken.Name = "TxtAPIToken";
            this.TxtAPIToken.Size = new System.Drawing.Size(288, 20);
            this.TxtAPIToken.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(12, 124);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(61, 13);
            label5.TabIndex = 16;
            label5.Text = "API Token:";
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(375, 291);
            this.Controls.Add(this.TxtAPIToken);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(this.CmbTracker);
            this.Controls.Add(this.CheckShowPrs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CheckRefFullRepo);
            this.Controls.Add(label3);
            this.Controls.Add(this.CmbKeyword);
            this.Controls.Add(this.TxtRepository);
            this.Controls.Add(this.TxtOwner);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repository Settings";
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.TextBox TxtOwner;
        public System.Windows.Forms.TextBox TxtRepository;
        private System.Windows.Forms.ComboBox CmbKeyword;
        private System.Windows.Forms.CheckBox CheckRefFullRepo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtPreview;
        private System.Windows.Forms.CheckBox CheckShowPrs;
        private System.Windows.Forms.ComboBox CmbTracker;
        public System.Windows.Forms.TextBox TxtAPIToken;
    }
}