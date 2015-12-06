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
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TxtOwner = new System.Windows.Forms.TextBox();
            this.TxtRepository = new System.Windows.Forms.TextBox();
            this.CmbKeyword = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.Location = new System.Drawing.Point(103, 103);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 3;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(184, 103);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 13);
            label1.TabIndex = 4;
            label1.Text = "Owner:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 41);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(60, 13);
            label2.TabIndex = 5;
            label2.Text = "Repository:";
            // 
            // TxtOwner
            // 
            this.TxtOwner.Location = new System.Drawing.Point(78, 12);
            this.TxtOwner.Name = "TxtOwner";
            this.TxtOwner.Size = new System.Drawing.Size(181, 20);
            this.TxtOwner.TabIndex = 0;
            // 
            // TxtRepository
            // 
            this.TxtRepository.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TxtRepository.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.TxtRepository.Location = new System.Drawing.Point(78, 38);
            this.TxtRepository.Name = "TxtRepository";
            this.TxtRepository.Size = new System.Drawing.Size(181, 20);
            this.TxtRepository.TabIndex = 1;
            this.TxtRepository.Enter += new System.EventHandler(this.TxtRepository_Enter);
            // 
            // CmbKeyword
            // 
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
            "Resolved"});
            this.CmbKeyword.Location = new System.Drawing.Point(81, 64);
            this.CmbKeyword.Name = "CmbKeyword";
            this.CmbKeyword.Size = new System.Drawing.Size(178, 21);
            this.CmbKeyword.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 67);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 13);
            label3.TabIndex = 9;
            label3.Text = "Keyword:";
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(267, 138);
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
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        public System.Windows.Forms.TextBox TxtOwner;
        public System.Windows.Forms.TextBox TxtRepository;
        private System.Windows.Forms.ComboBox CmbKeyword;
	}
}