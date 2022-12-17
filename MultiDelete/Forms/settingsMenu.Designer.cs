using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

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
            // toolTip
            //
            toolTip.ShowAlways = true;
            //
            // importButton
            //
            importButton.Size = new System.Drawing.Size(35, 35);
            importButton.TabStop = false;
            importButton.UseVisualStyleBackColor = false;
            importButton.Image = Properties.Resources.importIcon;
            importButton.Click += new EventHandler(importButton_click);
            importButton.BorderSize = 1;
            importButton.BorderRadius = 10;
            importButton.Location = new System.Drawing.Point(409, 2);
            toolTip.SetToolTip(importButton, "Import settings");
            //
            // exportButton
            //
            exportButton.Size = new System.Drawing.Size(35, 35);
            exportButton.TabStop = false;
            exportButton.UseVisualStyleBackColor = false;
            exportButton.Image = Properties.Resources.exportIcon;
            exportButton.Click += new EventHandler(exportButton_click);
            exportButton.BorderSize = 1;
            exportButton.BorderRadius = 10;
            exportButton.Location = new System.Drawing.Point(446, 2);
            toolTip.SetToolTip(exportButton, "Export settings");
            //
            // settingsHeading
            //
            settingsHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            settingsHeading.AutoSize = true;
            settingsHeading.Font = new System.Drawing.Font("Roboto", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            settingsHeading.TabStop = false;
            settingsHeading.Text = "Settings";
            settingsHeading.Location = new System.Drawing.Point(179, 1);
            //
            // instancePathLabel
            //
            instancePathLabel.AutoSize = false;
            instancePathLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            instancePathLabel.Size = new Size(150, 23);
            instancePathLabel.TabStop = false;
            instancePathLabel.Text = "Instance-Paths:";
            toolTip.SetToolTip(instancePathLabel, "Select in which Instances worlds should get deleted in");
            //
            // instancePathMTB
            //
            instancePathMTB.FolderDialogDescription = "Select Instance-path";
            instancePathMTB.ToolTip = toolTip.GetToolTip(instancePathLabel);
            //
            // deleteAllWorldsThatLabel
            //
            deleteAllWorldsThatLabel.AutoSize = true;
            deleteAllWorldsThatLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsThatLabel.TabStop = false;
            deleteAllWorldsThatLabel.Text = "Delete all Worlds that";
            //
            // startWithLabel
            //
            startWithLabel.AutoSize = false;
            startWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            startWithLabel.Size = new Size(93, 23);
            startWithLabel.TabStop = false;
            startWithLabel.Text = "start with:";
            toolTip.SetToolTip(startWithLabel, "Select what the name of the world has to start with to be deleted");
            //
            // startWithMTB
            //
            startWithMTB.ToolTip = toolTip.GetToolTip(startWithLabel);
            //
            // includeLabel
            //
            includeLabel.AutoSize = false;
            includeLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            includeLabel.Size = new Size(150, 23);
            includeLabel.TabStop = false;
            includeLabel.Text = "or include:";
            toolTip.SetToolTip(includeLabel, "Select what the name of the world has to include to be deleted");
            //
            // includeMTB
            //
            includeMTB.ToolTip = toolTip.GetToolTip(includeLabel);
            //
            // endWithLabel
            //
            endWithLabel.AutoSize = false;
            endWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            endWithLabel.Size = new Size(150, 23);
            endWithLabel.TabStop = false;
            endWithLabel.Text = "or end with:";
            toolTip.SetToolTip(endWithLabel, "Select what the name of the world has to end with to be deleted");
            //
            // endWithMTB
            //
            endWithMTB.ToolTip = toolTip.GetToolTip(endWithLabel);
            //
            // deleteAllWorldsCheckBox
            //
            deleteAllWorldsCheckBox.AutoSize = false;
            deleteAllWorldsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsCheckBox.Size = new Size(146, 23);
            deleteAllWorldsCheckBox.TabStop = false;
            deleteAllWorldsCheckBox.Text = "Delete all Worlds";
            deleteAllWorldsCheckBox.UseVisualStyleBackColor = true;
            deleteAllWorldsCheckBox.CheckedChanged += new EventHandler(deleteAllWorldsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteAllWorldsCheckBox, "Select if all worlds in your instances should be deleted");
            //
            // deleteRecordingsCheckBox
            //
            deleteRecordingsCheckBox.AutoSize = false;
            deleteRecordingsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRecordingsCheckBox.Size = new Size(200, 23);
            deleteRecordingsCheckBox.TabStop = false;
            deleteRecordingsCheckBox.Text = "Delete Recordings";
            deleteRecordingsCheckBox.UseVisualStyleBackColor = true;
            deleteRecordingsCheckBox.CheckedChanged += new EventHandler(deleteRecordingsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteRecordingsCheckBox, "Select if MultiDelete should delete your Recordings");
            //
            // deleteCrashReportsCheckBox
            //
            deleteCrashReportsCheckBox.AutoSize = false;
            deleteCrashReportsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCrashReportsCheckBox.Size = new Size(200, 23);
            deleteCrashReportsCheckBox.TabStop = false;
            deleteCrashReportsCheckBox.Text = "Delete Crash-Reports";
            deleteCrashReportsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteCrashReportsCheckBox, "Select if MultiDelete should delete your Crash-reports");
            //
            // deleteScreenshotsCheckBox
            //
            deleteScreenshotsCheckBox.AutoSize = false;
            deleteScreenshotsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteScreenshotsCheckBox.Size = new Size(200, 23);
            deleteScreenshotsCheckBox.TabStop = false;
            deleteScreenshotsCheckBox.Text = "Delete Screenshots";
            deleteScreenshotsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteScreenshotsCheckBox, "Select if MultiDelete should delete your Screenshots");
            //
            // updateScreenLabel
            //
            updateScreenLabel.AutoSize = true;
            updateScreenLabel.Padding = new Padding(0, 5, 0, 0);
            updateScreenLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel.TabStop = false;
            updateScreenLabel.Text = "Update screen every";
            toolTip.SetToolTip(updateScreenLabel, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");
            //
            // moveToRecycleBinCheckBox
            //
            moveToRecycleBinCheckBox.AutoSize = true;
            moveToRecycleBinCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            moveToRecycleBinCheckBox.TabStop = false;
            moveToRecycleBinCheckBox.Text = "Move Files to Recycle Bin";
            moveToRecycleBinCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(moveToRecycleBinCheckBox, "Select if the files should be moved to the Recycle Bin instead of being instantly deleted. NOTE: This makes world deletion waaay slower");
            //
            // updateScreenNUD
            //
            updateScreenNUD.Size = new Size(40, 26);
            updateScreenNUD.TabStop = false;
            updateScreenNUD.Value = 1;
            updateScreenNUD.Minimum = 1;
            updateScreenNUD.Maximum = 10000;
            updateScreenNUD.TextAlign = HorizontalAlignment.Center;
            updateScreenNUD.ToolTip = toolTip.GetToolTip(updateScreenLabel);
            //
            // updateScreenLabel2
            //
            updateScreenLabel2.AutoSize = true;
            updateScreenLabel2.Padding = new Padding(0, 5, 0, 0);
            updateScreenLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel2.TabStop = false;
            updateScreenLabel2.Text = "world.";
            toolTip.SetToolTip(updateScreenLabel2, toolTip.GetToolTip(updateScreenLabel));
            //
            // updateScreenPanel
            //
            updateScreenPanel.Size = new Size(400, 28);
            updateScreenPanel.FlowDirection = FlowDirection.LeftToRight;
            updateScreenPanel.Controls.Add(updateScreenLabel);
            updateScreenPanel.Controls.Add(updateScreenNUD);
            updateScreenPanel.Controls.Add(updateScreenLabel2);
            //
            // recordingsFTB
            //
            recordingsFTB.PlaceholderText = "Recordings Path";
            recordingsFTB.FolderDialogDescription = "select recordings-path";
            recordingsFTB.ToolTip = "Select in which folder MultiDelete should delete your recordings";
            //
            // checkForUpdatesButton
            //
            checkForUpdatesButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkForUpdatesButton.Size = new Size(150, 50);
            checkForUpdatesButton.TabStop = false;
            checkForUpdatesButton.Text = "Check for Updates";
            checkForUpdatesButton.UseVisualStyleBackColor = false;
            checkForUpdatesButton.Click += new EventHandler(checkForUpdatesButton_Click);
            checkForUpdatesButton.BorderSize = 1;
            toolTip.SetToolTip(checkForUpdatesButton, "Check if a new Update is available");
            //
            // addMultipleInstanceButton
            //
            addMultipleInstanceButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            addMultipleInstanceButton.Size = new Size(200, 35);
            addMultipleInstanceButton.TabStop = false;
            addMultipleInstanceButton.Text = "Add multiple Instances";
            addMultipleInstanceButton.UseVisualStyleBackColor = false;
            addMultipleInstanceButton.Click += new EventHandler(addMultipleInstanceButton_Click);
            addMultipleInstanceButton.BorderSize = 1;
            toolTip.SetToolTip(addMultipleInstanceButton, "Add multiple Instances at once via selecting multiple folders");
            //
            // threadsToUseLabel
            //
            threadsToUseLabel.AutoSize = true;
            threadsToUseLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            threadsToUseLabel.TabStop = false;
            threadsToUseLabel.Text = "Threads to use: 1";
            toolTip.SetToolTip(threadsToUseLabel, "Configure how many threads MultiDelte should use to delete worlds.");
            //
            // threadsTrackBar
            //
            threadsTrackBar.Maximum = Environment.ProcessorCount;
            threadsTrackBar.Minimum = 1;
            threadsTrackBar.Size = new Size(200, 45);
            threadsTrackBar.TabIndex = 0;
            threadsTrackBar.TabStop = false;
            threadsTrackBar.Value = 1;
            threadsTrackBar.ValueChanged += new EventHandler(threadsTrackBar_ValueChanged);
            toolTip.SetToolTip(threadsTrackBar, toolTip.GetToolTip(threadsToUseLabel));
            //
            // keepLastWorldsLabel
            //
            keepLastWorldsLabel.AutoSize = true;
            keepLastWorldsLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel.TabStop = false;
            keepLastWorldsLabel.Text = "Keep last";
            keepLastWorldsLabel.Padding = new Padding(0, 5, 0, 0);
            toolTip.SetToolTip(keepLastWorldsLabel, "Select how many of the last worlds MultiDelete should keep.");
            //
            // keepLastWorldsNUD
            //
            keepLastWorldsNUD.Size = new Size(37, 26);
            keepLastWorldsNUD.TabStop = false;
            keepLastWorldsNUD.Value = 10;
            keepLastWorldsNUD.Maximum = 1000;
            keepLastWorldsNUD.TextAlign = HorizontalAlignment.Center;
            keepLastWorldsNUD.ToolTip = toolTip.GetToolTip(keepLastWorldsLabel);
            //
            // keepLastWorldsLabel2
            //
            keepLastWorldsLabel2.AutoSize = true;
            keepLastWorldsLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel2.TabStop = false;
            keepLastWorldsLabel2.Text = "worlds.";
            keepLastWorldsLabel2.Padding = new Padding(0, 5, 0, 0);
            toolTip.SetToolTip(keepLastWorldsLabel2, toolTip.GetToolTip(keepLastWorldsLabel));
            //
            // keepLastWorldsPanel
            //
            keepLastWorldsPanel.Size = new Size(400, 34);
            keepLastWorldsPanel.FlowDirection = FlowDirection.LeftToRight;
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsNUD);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel2);
            //
            // multiDeleteHeading
            //
            multiDeleteHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            multiDeleteHeading.AutoSize = true;
            multiDeleteHeading.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            multiDeleteHeading.TabStop = false;
            multiDeleteHeading.Text = "MultiDelete " + MultiDelete.version;
            multiDeleteHeading.Padding = new Padding(0, 10, 0, 5);
            //
            // viewRepositoryLabel
            //
            viewRepositoryLabel.AutoSize = true;
            viewRepositoryLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            viewRepositoryLabel.TabStop = false;
            viewRepositoryLabel.Text = "View GitHub repository";
            viewRepositoryLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(viewRepositoryLabel_LinkClicked);
            viewRepositoryLabel.Padding = new Padding(0, 0, 0, 5);
            toolTip.SetToolTip(viewRepositoryLabel, "https://github.com/greyhayv/MultiDelete");
            //
            // bgColorLabel
            //
            bgColorLabel.AutoSize = true;
            bgColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bgColorLabel.TabStop = false;
            bgColorLabel.Text = "Background Color:";
            bgColorLabel.Padding = new Padding(0, 8, 0, 0);
            toolTip.SetToolTip(bgColorLabel, "Select which color the background of MultiDelete should have.");
            //
            // bgColorButton
            //
            bgColorButton.Size = new Size(30, 30);
            bgColorButton.BorderRadius = 15;
            bgColorButton.TabStop = false;
            bgColorButton.UseVisualStyleBackColor = false;
            bgColorButton.BorderSize = 1;
            bgColorButton.DisableAnimations = true;
            bgColorButton.Click += new EventHandler(bgColorButton_Click);
            toolTip.SetToolTip(bgColorButton, toolTip.GetToolTip(bgColorLabel));
            //
            // bgColorPanel
            //
            bgColorPanel.Size = new Size(400, 35);
            bgColorPanel.FlowDirection = FlowDirection.LeftToRight;
            bgColorPanel.Visible = false;
            bgColorPanel.Controls.Add(bgColorLabel);
            bgColorPanel.Controls.Add(bgColorButton);
            //
            // accentColorLabel
            //
            accentColorLabel.AutoSize = true;
            accentColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            accentColorLabel.TabStop = false;
            accentColorLabel.Text = "Accent Color:";
            accentColorLabel.Padding = new Padding(0, 8, 0, 0);
            toolTip.SetToolTip(accentColorLabel, "Select which Accent color MultiDelete should have.");
            //
            // accentColorButton
            //
            accentColorButton.Size = new Size(30, 30);
            accentColorButton.BorderRadius = 15;
            accentColorButton.TabStop = false;
            accentColorButton.UseVisualStyleBackColor = false;
            accentColorButton.BorderSize = 1;
            accentColorButton.DisableAnimations = true;
            accentColorButton.Click += new EventHandler(accentColorButton_Click);
            toolTip.SetToolTip(accentColorButton, toolTip.GetToolTip(accentColorLabel));
            //
            // accentColorPanel
            //
            accentColorPanel.Size = new Size(400, 35);
            accentColorPanel.FlowDirection = FlowDirection.LeftToRight;
            accentColorPanel.Visible = false;
            accentColorPanel.Controls.Add(accentColorLabel);
            accentColorPanel.Controls.Add(accentColorButton);
            //
            // fontColorLabel
            //
            fontColorLabel.AutoSize = true;
            fontColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fontColorLabel.TabStop = false;
            fontColorLabel.Text = "Font Color:";
            fontColorLabel.Padding = new Padding(0, 8, 0, 0);
            toolTip.SetToolTip(fontColorLabel, "Select which Font color MultiDelete should have.");
            //
            // fontColorButton
            //
            fontColorButton.Size = new Size(30, 30);
            fontColorButton.BorderRadius = 15;
            fontColorButton.TabStop = false;
            fontColorButton.UseVisualStyleBackColor = false;
            fontColorButton.BorderSize = 1;
            fontColorButton.DisableAnimations = true;
            fontColorButton.Click += new EventHandler(fontColorButton_Click);
            toolTip.SetToolTip(fontColorButton, toolTip.GetToolTip(fontColorLabel));
            //
            // fontColorPanel
            //
            fontColorPanel.Size = new Size(400, 35);
            fontColorPanel.FlowDirection = FlowDirection.LeftToRight;
            fontColorPanel.Visible = false;
            fontColorPanel.Controls.Add(fontColorLabel);
            fontColorPanel.Controls.Add(fontColorButton);
            //
            // resetSettingsButton
            //
            resetSettingsButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            resetSettingsButton.Size = new Size(150, 35);
            resetSettingsButton.TabStop = false;
            resetSettingsButton.Text = "Reset Settings";
            resetSettingsButton.UseVisualStyleBackColor = false;
            resetSettingsButton.Click += new EventHandler(resetSettinsButton_Click);
            resetSettingsButton.BorderSize = 1;
            toolTip.SetToolTip(resetSettingsButton, "Reset all of your settings to default");
            //
            // themeLabel
            //
            themeLabel.AutoSize = true;
            themeLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            themeLabel.TabStop = false;
            themeLabel.Text = "Theme:";
            themeLabel.Padding = new Padding(0, 4, 0, 0);
            toolTip.SetToolTip(themeLabel, "Select which Theme MultiDelete should have.");
            //
            // themeComboBox
            //
            themeComboBox.TabStop = false;
            themeComboBox.SelectionChangeCommitted += new EventHandler(themeComboBox_SelectionChangeCommitted);
            themeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            themeComboBox.Items.AddRange(Enum.GetNames(typeof(Themes)));
            toolTip.SetToolTip(themeComboBox, toolTip.GetToolTip(themeLabel));
            //
            // themePanel
            //
            themePanel.TabStop = false;
            themePanel.FlowDirection = FlowDirection.LeftToRight;
            themePanel.Size = new Size(300, 27);
            themePanel.Controls.Add(themeLabel);
            themePanel.Controls.Add(themeComboBox);
            //
            // settingsTabPanel
            //
            settingsTabPanel.Dock = DockStyle.Bottom;
            settingsTabPanel.Size = new System.Drawing.Size(484, 721);
            settingsTabPanel.addControl("Instances", instancePathLabel);
            settingsTabPanel.addControl("Instances", instancePathMTB);
            settingsTabPanel.addControl("Instances", addMultipleInstanceButton);
            settingsTabPanel.addControl("Criteria", deleteAllWorldsCheckBox);
            settingsTabPanel.addControl("Criteria", deleteAllWorldsThatLabel);
            settingsTabPanel.addControl("Criteria", startWithLabel);
            settingsTabPanel.addControl("Criteria", startWithMTB);
            settingsTabPanel.addControl("Criteria", includeLabel);
            settingsTabPanel.addControl("Criteria", includeMTB);
            settingsTabPanel.addControl("Criteria", endWithLabel);
            settingsTabPanel.addControl("Criteria", endWithMTB);
            settingsTabPanel.addControl("Other Files", deleteRecordingsCheckBox);
            settingsTabPanel.addControl("Other Files", recordingsFTB);
            settingsTabPanel.addControl("Other Files", deleteCrashReportsCheckBox);
            settingsTabPanel.addControl("Other Files", deleteScreenshotsCheckBox);
            settingsTabPanel.addControl("Advanced", updateScreenPanel);
            settingsTabPanel.addControl("Advanced", threadsToUseLabel);
            settingsTabPanel.addControl("Advanced", threadsTrackBar);
            settingsTabPanel.addControl("Advanced", keepLastWorldsPanel);
            settingsTabPanel.addControl("Advanced", moveToRecycleBinCheckBox);
            settingsTabPanel.addControl("Advanced", resetSettingsButton);
            settingsTabPanel.addControl("Appearance", themePanel);
            settingsTabPanel.addControl("Appearance", bgColorPanel);
            settingsTabPanel.addControl("Appearance", accentColorPanel);
            settingsTabPanel.addControl("Appearance", fontColorPanel);
            settingsTabPanel.addControl("About", multiDeleteHeading);
            settingsTabPanel.addControl("About", viewRepositoryLabel);
            settingsTabPanel.addControl("About", checkForUpdatesButton);
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
            this.updateColors();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void updateColors() {
            instancePathLabel.ForeColor = MultiDelete.fontColor;

            instancePathMTB.BorderColor = MultiDelete.accentColor;
            instancePathMTB.BackColor = MultiDelete.bgColor;
            instancePathMTB.ForeColor = MultiDelete.fontColor;
        
            deleteAllWorldsThatLabel.ForeColor = MultiDelete.fontColor;

            startWithLabel.ForeColor = MultiDelete.fontColor;

            startWithMTB.BorderColor = MultiDelete.accentColor;
            startWithMTB.BackColor = MultiDelete.bgColor;
            startWithMTB.ForeColor = MultiDelete.fontColor;

            includeLabel.ForeColor = MultiDelete.fontColor;

            includeMTB.BorderColor = MultiDelete.accentColor;
            includeMTB.BackColor = MultiDelete.bgColor;
            includeMTB.ForeColor = MultiDelete.fontColor;

            endWithLabel.ForeColor = MultiDelete.fontColor;

            endWithMTB.BorderColor = MultiDelete.accentColor;
            endWithMTB.BackColor = MultiDelete.bgColor;
            endWithMTB.ForeColor = MultiDelete.fontColor;

            deleteAllWorldsCheckBox.ForeColor = MultiDelete.fontColor;

            deleteRecordingsCheckBox.ForeColor = MultiDelete.fontColor;

            deleteCrashReportsCheckBox.ForeColor = MultiDelete.fontColor;

            deleteScreenshotsCheckBox.ForeColor = MultiDelete.fontColor;

            moveToRecycleBinCheckBox.ForeColor = MultiDelete.fontColor;

            updateScreenLabel.ForeColor = MultiDelete.fontColor;

            updateScreenLabel2.ForeColor = MultiDelete.fontColor;

            updateScreenNUD.BackColor = MultiDelete.bgColor;
            updateScreenNUD.ForeColor = MultiDelete.fontColor;
            updateScreenNUD.BorderColor = MultiDelete.accentColor;

            recordingsFTB.BorderColor = MultiDelete.accentColor;
            recordingsFTB.BackColor = MultiDelete.bgColor;
            recordingsFTB.ForeColor = MultiDelete.fontColor;

            checkForUpdatesButton.ForeColor = MultiDelete.fontColor;
            checkForUpdatesButton.BorderColor = MultiDelete.accentColor;

            addMultipleInstanceButton.ForeColor = MultiDelete.fontColor;
            addMultipleInstanceButton.BorderColor = MultiDelete.accentColor;

            resetSettingsButton.ForeColor = MultiDelete.fontColor;
            resetSettingsButton.BorderColor = MultiDelete.accentColor;

            threadsToUseLabel.ForeColor = MultiDelete.fontColor;

            keepLastWorldsLabel.ForeColor = MultiDelete.fontColor;

            keepLastWorldsNUD.BackColor = MultiDelete.bgColor;
            keepLastWorldsNUD.ForeColor = MultiDelete.fontColor;
            keepLastWorldsNUD.BorderColor = MultiDelete.accentColor;

            keepLastWorldsLabel2.ForeColor = MultiDelete.fontColor;

            multiDeleteHeading.ForeColor = MultiDelete.fontColor;

            viewRepositoryLabel.LinkColor = MultiDelete.fontColor;
            viewRepositoryLabel.ActiveLinkColor = MultiDelete.fontColor;

            bgColorLabel.ForeColor = MultiDelete.fontColor;

            bgColorButton.BackgroundColor = MultiDelete.bgColor;
            bgColorButton.BorderColor = MultiDelete.accentColor;

            accentColorLabel.ForeColor = MultiDelete.fontColor;

            accentColorButton.BackgroundColor = MultiDelete.accentColor;
            accentColorButton.BorderColor = MultiDelete.accentColor;

            fontColorLabel.ForeColor = MultiDelete.fontColor;

            fontColorButton.BackgroundColor = MultiDelete.fontColor;
            fontColorButton.BorderColor = MultiDelete.fontColor;

            settingsTabPanel.BorderColor = MultiDelete.accentColor;
            settingsTabPanel.ButtonForeColor = MultiDelete.fontColor;
            settingsTabPanel.BackColor = MultiDelete.bgColor;

            themeLabel.ForeColor = MultiDelete.fontColor;

            themeComboBox.BackColor = MultiDelete.bgColor;
            themeComboBox.ForeColor = MultiDelete.fontColor;

            importButton.BorderColor = MultiDelete.accentColor;
            importButton.Image = MultiDelete.recolorImage(importButton.Image, MultiDelete.fontColor);

            exportButton.BorderColor = MultiDelete.accentColor;
            exportButton.Image = MultiDelete.recolorImage(exportButton.Image, MultiDelete.fontColor);

            settingsHeading.ForeColor = MultiDelete.fontColor;

            BackColor = MultiDelete.bgColor;
        }

        private ToolTip toolTip = new ToolTip();
        private Label settingsHeading = new Label();
        private BButton exportButton = new BButton();
        private BButton importButton = new BButton();
        private TabFlowLayoutPanel settingsTabPanel = new TabFlowLayoutPanel(new System.Collections.Generic.List<string> {"Instances", "Criteria", "Other Files","Advanced", "Appearance", "About"});
        private RemoveFolderMultiTextBox instancePathMTB = new RemoveFolderMultiTextBox();
        private RemoveMultiTextBox startWithMTB = new RemoveMultiTextBox();
        private RemoveMultiTextBox includeMTB = new RemoveMultiTextBox();
        private RemoveMultiTextBox endWithMTB = new RemoveMultiTextBox();
        private BNumericUpDown updateScreenNUD = new BNumericUpDown();
        private Label instancePathLabel = new Label();
        private Label deleteAllWorldsThatLabel = new Label();
        private Label startWithLabel = new Label();
        private Label includeLabel = new Label();
        private Label endWithLabel = new Label();
        private CheckBox deleteAllWorldsCheckBox = new CheckBox();
        private CheckBox deleteRecordingsCheckBox = new CheckBox();
        private CheckBox deleteCrashReportsCheckBox = new CheckBox();
        private CheckBox deleteScreenshotsCheckBox = new CheckBox();
        private BButton checkForUpdatesButton = new BButton();
        private FolderTextBox recordingsFTB = new FolderTextBox();
        private FlowLayoutPanel updateScreenPanel = new FlowLayoutPanel();
        private BButton addMultipleInstanceButton = new BButton();
        private Label updateScreenLabel = new Label();
        private Label updateScreenLabel2 = new Label();
        private Label threadsToUseLabel = new Label();
        private BTrackBar threadsTrackBar = new BTrackBar();
        private Label keepLastWorldsLabel = new Label();
        private Label keepLastWorldsLabel2 = new Label();
        private BNumericUpDown keepLastWorldsNUD = new BNumericUpDown();
        private FlowLayoutPanel keepLastWorldsPanel = new FlowLayoutPanel();
        private Label multiDeleteHeading = new Label();
        private LinkLabel viewRepositoryLabel = new LinkLabel();
        private Label bgColorLabel = new Label();
        private BButton bgColorButton = new BButton();
        private FlowLayoutPanel bgColorPanel = new FlowLayoutPanel();
        private Label accentColorLabel = new Label();
        private BButton accentColorButton = new BButton();
        private FlowLayoutPanel accentColorPanel = new FlowLayoutPanel();
        private Label fontColorLabel = new Label();
        private BButton fontColorButton = new BButton();
        private FlowLayoutPanel fontColorPanel = new FlowLayoutPanel();
        private CheckBox moveToRecycleBinCheckBox = new CheckBox();
        private BButton resetSettingsButton = new BButton();
        private Label themeLabel = new Label();
        private ComboBox themeComboBox = new ComboBox();
        private FlowLayoutPanel themePanel = new FlowLayoutPanel();

        #endregion
    }
}