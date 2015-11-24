// This file is part of TurtleHub.
// 
// Copyright (C)2013 Justin Dailey <dail8859@yahoo.com>
// 
// TurtleHub is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either
// version 2 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.

namespace TurtleHub
{
    partial class IssueBrowserDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.StatusStrip statusStrip;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueBrowserDialog));
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.workStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnReload = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.checkColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numberColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.summaryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openedbyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assignedtoColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.updateNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            statusStrip = new System.Windows.Forms.StatusStrip();
            statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.workStatus});
            statusStrip.Location = new System.Drawing.Point(0, 465);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            statusStrip.Size = new System.Drawing.Size(849, 22);
            statusStrip.TabIndex = 12;
            // 
            // statusLabel
            // 
            this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(835, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Ready";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // workStatus
            // 
            this.workStatus.AutoSize = false;
            this.workStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.workStatus.Image = ((System.Drawing.Image)(resources.GetObject("workStatus.Image")));
            this.workStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.workStatus.Name = "workStatus";
            this.workStatus.Size = new System.Drawing.Size(45, 17);
            this.workStatus.Visible = false;
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnOk.Location = new System.Drawing.Point(681, 432);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnCancel.Location = new System.Drawing.Point(762, 432);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // BtnReload
            // 
            this.BtnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnReload.Location = new System.Drawing.Point(12, 432);
            this.BtnReload.Name = "BtnReload";
            this.BtnReload.Size = new System.Drawing.Size(77, 23);
            this.BtnReload.TabIndex = 3;
            this.BtnReload.Text = "Reload";
            this.BtnReload.UseVisualStyleBackColor = true;
            this.BtnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.checkColumn,
            this.numberColumn,
            this.summaryColumn,
            this.openedbyColumn,
            this.assignedtoColumn});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(849, 421);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // checkColumn
            // 
            this.checkColumn.Text = "";
            this.checkColumn.Width = 17;
            // 
            // numberColumn
            // 
            this.numberColumn.Text = "#";
            this.numberColumn.Width = 34;
            // 
            // summaryColumn
            // 
            this.summaryColumn.Text = "Summary";
            this.summaryColumn.Width = 606;
            // 
            // openedbyColumn
            // 
            this.openedbyColumn.Text = "Opened By";
            this.openedbyColumn.Width = 115;
            // 
            // assignedtoColumn
            // 
            this.assignedtoColumn.Text = "Assigned To";
            this.assignedtoColumn.Width = 90;
            // 
            // updateNotifyIcon
            // 
            this.updateNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.updateNotifyIcon.BalloonTipText = "There is a new version of TurtleHub available.";
            this.updateNotifyIcon.BalloonTipTitle = "TurtleHub Update Available";
            this.updateNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("updateNotifyIcon.Icon")));
            this.updateNotifyIcon.Text = "TurtleHub Update Available";
            // 
            // IssueBrowserDialog
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(849, 487);
            this.Controls.Add(statusStrip);
            this.Controls.Add(this.BtnReload);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MinimizeBox = false;
            this.Name = "IssueBrowserDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Issues for {0}";
            this.Load += new System.EventHandler(this.MyIssuesForm_Load);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnReload;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel workStatus;
        private System.Windows.Forms.ColumnHeader numberColumn;
        private System.Windows.Forms.ColumnHeader summaryColumn;
        private System.Windows.Forms.ColumnHeader openedbyColumn;
        private System.Windows.Forms.ColumnHeader assignedtoColumn;
        private System.Windows.Forms.ColumnHeader checkColumn;
        private System.Windows.Forms.NotifyIcon updateNotifyIcon;
    }
}