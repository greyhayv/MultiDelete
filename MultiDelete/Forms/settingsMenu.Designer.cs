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
            this.SuspendLayout();
            //
            // settingsTabPanel
            //
            settingsTabPanel.Dock = DockStyle.Bottom;
            settingsTabPanel.Size = new System.Drawing.Size(484, 721);
            //
            // importButton
            //
            importButton = new BButton();
            importButton.Size = new System.Drawing.Size(35, 35);
            importButton.TabStop = false;
            importButton.UseVisualStyleBackColor = false;
            importButton.Image = Properties.Resources.importIcon;
            importButton.Click += new EventHandler(importButton_click);
            importButton.BorderSize = 1;
            importButton.BorderRadius = 10;
            importButton.Location = new System.Drawing.Point(409, 2);
            //
            // exportButton
            //
            exportButton = new BButton();
            exportButton.Size = new System.Drawing.Size(35, 35);
            exportButton.TabStop = false;
            exportButton.UseVisualStyleBackColor = false;
            exportButton.Image = Properties.Resources.exportIcon;
            exportButton.Click += new EventHandler(exportButton_click);
            exportButton.BorderSize = 1;
            exportButton.BorderRadius = 10;
            exportButton.Location = new System.Drawing.Point(446, 2);
            //
            // settingsHeading
            //
            settingsHeading = new Label();
            settingsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            settingsHeading.AutoSize = true;
            settingsHeading.Font = new System.Drawing.Font("Roboto", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            settingsHeading.TabStop = false;
            settingsHeading.Text = "Settings";
            settingsHeading.Location = new System.Drawing.Point(179, 1);
            // 
            // settingsMenu
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 761);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "settingsMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Controls.Add(settingsHeading);
            this.Controls.Add(exportButton);
            this.Controls.Add(importButton);
            this.Controls.Add(settingsTabPanel);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.settingsMenu_FormClosed);
            this.Load += new System.EventHandler(this.settingsMenu_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.settingsMenu_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.settingsMenu_DragEnter);
            this.ResumeLayout(false);
        }

        Label settingsHeading = new Label();
        BButton exportButton = new BButton();
        BButton importButton = new BButton();
        TabFlowLayoutPanel settingsTabPanel = new TabFlowLayoutPanel(new System.Collections.Generic.List<string> {"Instances", "Criteria", "Advanced", "Appearance", "About"});

        #endregion
    }
}