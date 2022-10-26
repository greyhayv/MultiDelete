using System;
using System.Windows.Forms;

namespace MultiDelete
{
    partial class settingsMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settingsMenu));
            this.settingsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // settingsPanel
            // 
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(484, 761);
            this.settingsPanel.TabIndex = 10;
            this.settingsPanel.WrapContents = false;
            this.settingsPanel.SizeChanged += new System.EventHandler(this.settingsPanel_SizeChanged);
            this.settingsPanel.HorizontalScroll.Maximum = 0;
            this.settingsPanel.AutoScroll = false;
            this.settingsPanel.VerticalScroll.Visible = false;
            this.settingsPanel.AutoScroll = true;
            // 
            // settingsMenu
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(484, 761);
            this.Controls.Add(this.settingsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "settingsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.settingsMenu_FormClosed);
            this.Load += new System.EventHandler(this.settingsMenu_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.settingsMenu_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.settingsMenu_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel settingsPanel;
    }
}