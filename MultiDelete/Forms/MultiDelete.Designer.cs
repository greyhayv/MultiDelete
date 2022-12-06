using System.Windows.Forms;

namespace MultiDelete
{
    partial class MultiDelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiDelete));
            this.deleteWorldsButton = new BButton();
            this.settingsButton = new BButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.infoLabel = new System.Windows.Forms.Label();
            this.okButton = new BButton();
            this.cancelButton = new BButton();
            this.SuspendLayout();
            // 
            // deleteWorldsButton
            // 
            this.deleteWorldsButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deleteWorldsButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteWorldsButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.deleteWorldsButton.BorderRadius = 20;
            this.deleteWorldsButton.BorderSize = 1;
            this.deleteWorldsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteWorldsButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteWorldsButton.Location = new System.Drawing.Point(154, 30);
            this.deleteWorldsButton.Name = "deleteWorldsButton";
            this.deleteWorldsButton.Size = new System.Drawing.Size(175, 50);
            this.deleteWorldsButton.Text = "Delete Worlds";
            this.deleteWorldsButton.UseVisualStyleBackColor = false;
            this.deleteWorldsButton.Click += new System.EventHandler(this.deleteWorldsButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.settingsButton.BorderRadius = 18;
            this.settingsButton.BorderSize = 1;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("Nunito", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsButton.Image = global::MultiDelete.Properties.Resources.settingsIcon;
            this.settingsButton.Location = new System.Drawing.Point(447, 7);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.settingsButton.Size = new System.Drawing.Size(30, 30);
            this.settingsButton.TextColor = MultiDelete.accentColor;
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.progressBar.Location = new System.Drawing.Point(17, 44);
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar.Size = new System.Drawing.Size(450, 23);
            this.progressBar.Visible = false;
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.infoLabel.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infoLabel.Location = new System.Drawing.Point(-8, 41);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(500, 25);
            this.infoLabel.Text = "Searching Worlds (0)";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLabel.Visible = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.okButton.BorderRadius = 20;
            this.okButton.BorderSize = 1;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.okButton.Location = new System.Drawing.Point(193, 57);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 40);
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Visible = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundColor = System.Drawing.Color.Transparent;
            this.cancelButton.BorderRadius = 20;
            this.cancelButton.BorderSize = 1;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cancelButton.Location = new System.Drawing.Point(198, 72);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cancelButton.Size = new System.Drawing.Size(90, 35);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // MultiDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(484, 111);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.deleteWorldsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MultiDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MultiDelete";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.updateColors();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void updateColors() {
            deleteWorldsButton.BorderColor = MultiDelete.accentColor;
            deleteWorldsButton.ForeColor = MultiDelete.fontColor;

            settingsButton.BorderColor = MultiDelete.accentColor;
            settingsButton.ForeColor = MultiDelete.fontColor;

            infoLabel.ForeColor = MultiDelete.fontColor;

            okButton.BorderColor = MultiDelete.accentColor;
            okButton.ForeColor = MultiDelete.fontColor;

            cancelButton.BorderColor = MultiDelete.accentColor;
            cancelButton.ForeColor = MultiDelete.fontColor;

            BackColor = MultiDelete.bgColor;

            settingsButton.Image = recolorImage(settingsButton.Image, fontColor);
        }

        #endregion

        private BButton deleteWorldsButton;
        private BButton settingsButton;
        private ProgressBar progressBar;
        private Label infoLabel;
        private BButton okButton;
        private BButton cancelButton;
    }
}