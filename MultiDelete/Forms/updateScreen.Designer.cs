namespace MultiDelete
{
    partial class updateScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(updateScreen));
            this.updatePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.remindMeLaterButton = new BButton();
            this.downloadButton = new BButton();
            this.focusLabel = new System.Windows.Forms.Label();
            this.closeButton = new BButton();
            this.SuspendLayout();
            // 
            // updatePanel
            // 
            this.updatePanel.AutoScroll = true;
            this.updatePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.updatePanel.Location = new System.Drawing.Point(0, 0);
            this.updatePanel.Name = "updatePanel";
            this.updatePanel.Size = new System.Drawing.Size(484, 700);
            this.updatePanel.TabIndex = 0;
            this.updatePanel.WrapContents = false;
            // 
            // remindMeLaterButton
            // 
            this.remindMeLaterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.remindMeLaterButton.BackColor = System.Drawing.Color.Transparent;
            this.remindMeLaterButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.remindMeLaterButton.BorderColor = MultiDelete.accentColor;
            this.remindMeLaterButton.BorderRadius = 20;
            this.remindMeLaterButton.BorderSize = 1;
            this.remindMeLaterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remindMeLaterButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.remindMeLaterButton.ForeColor = MultiDelete.fontColor;
            this.remindMeLaterButton.Location = new System.Drawing.Point(5, 706);
            this.remindMeLaterButton.Name = "remindMeLaterButton";
            this.remindMeLaterButton.Size = new System.Drawing.Size(235, 50);
            this.remindMeLaterButton.TabIndex = 1;
            this.remindMeLaterButton.TabStop = false;
            this.remindMeLaterButton.Text = "Remind me later";
            this.remindMeLaterButton.UseVisualStyleBackColor = false;
            this.remindMeLaterButton.Click += new System.EventHandler(this.remindMeLaterButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadButton.BackColor = System.Drawing.Color.Transparent;
            this.downloadButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.downloadButton.BorderColor = MultiDelete.accentColor;
            this.downloadButton.BorderRadius = 20;
            this.downloadButton.BorderSize = 1;
            this.downloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.downloadButton.ForeColor = MultiDelete.fontColor;
            this.downloadButton.Location = new System.Drawing.Point(244, 706);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(235, 50);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.TabStop = false;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = false;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // focusLabel
            // 
            this.focusLabel.AutoSize = true;
            this.focusLabel.Location = new System.Drawing.Point(-100, -100);
            this.focusLabel.Name = "focusLabel";
            this.focusLabel.Size = new System.Drawing.Size(0, 15);
            this.focusLabel.TabIndex = 3;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.closeButton.BorderColor = MultiDelete.accentColor;
            this.closeButton.BorderRadius = 20;
            this.closeButton.BorderSize = 1;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeButton.ForeColor = MultiDelete.fontColor;
            this.closeButton.Location = new System.Drawing.Point(5, 706);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(474, 50);
            this.closeButton.TabIndex = 4;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Visible = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // updateScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = MultiDelete.bgColor;
            this.ClientSize = new System.Drawing.Size(484, 761);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.focusLabel);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.remindMeLaterButton);
            this.Controls.Add(this.updatePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "updateScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Changelogs";
            this.Load += new System.EventHandler(this.updateScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel updatePanel;
        private BButton remindMeLaterButton;
        private BButton downloadButton;
        private System.Windows.Forms.Label focusLabel;
        private BButton closeButton;
    }
}