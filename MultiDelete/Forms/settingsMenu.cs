using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.Linq;

namespace MultiDelete
{
    public partial class settingsMenu : Form
    {
        private MultiDelete multiDelete;
        private string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";

        //Settings menu elements
        RemoveFolderMultiTextBox instancePathMTB = new RemoveFolderMultiTextBox();
        RemoveMultiTextBox startWithMTB = new RemoveMultiTextBox();
        RemoveMultiTextBox includeMTB = new RemoveMultiTextBox();
        RemoveMultiTextBox endWithMTB = new RemoveMultiTextBox();
        BNumericUpDown updateScreenNUD = new BNumericUpDown();
        Label instancePathLabel = new Label();
        Label deleteAllWorldsThatLabel = new Label();
        Label startWithLabel = new Label();
        Label includeLabel = new Label();
        Label endWithLabel = new Label();
        CheckBox deleteAllWorldsCheckBox = new CheckBox();
        CheckBox deleteRecordingsCheckBox = new CheckBox();
        CheckBox deleteCrashReportsCheckBox = new CheckBox();
        CheckBox deleteScreenshotsCheckBox = new CheckBox();
        BButton checkForUpdatesButton = new BButton();
        FolderTextBox recordingsFTB = new FolderTextBox();
        Panel updateScreenPanel = new Panel();
        BButton addMultipleInstanceButton = new BButton();
        Label updateScreenLabel = new Label();
        Label updateScreenLabel2 = new Label();
        Label threadsToUseLabel = new Label();
        TrackBar threadsTrackBar = new TrackBar();
        Label keepLastWorldsLabel = new Label();
        Label keepLastWorldsLabel2 = new Label();
        BNumericUpDown keepLastWorldsNUD = new BNumericUpDown();
        Panel keepLastWorldsPanel = new Panel();
        Label multiDeleteHeading = new Label();
        LinkLabel viewRepositoryLabel = new LinkLabel();
        Label bgColorLabel = new Label();
        BButton bgColorButton = new BButton();
        Panel bgColorPanel = new Panel();
        Label accentColorLabel = new Label();
        BButton accentColorButton = new BButton();
        Panel accentColorPanel = new Panel();
        Label fontColorLabel = new Label();
        BButton fontColorButton = new BButton();
        Panel fontColorPanel = new Panel();

        public settingsMenu(MultiDelete md)
        {
            multiDelete = md;

            InitializeComponent();

            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(exportButton, "Export settings");
            toolTip.SetToolTip(importButton, "Import settings");

            instancePathLabel = new Label();
            instancePathLabel.AutoSize = false;
            instancePathLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            instancePathLabel.Size = new Size(150, 23);
            instancePathLabel.TabStop = false;
            instancePathLabel.Text = "Instance-Paths:";
            toolTip.SetToolTip(instancePathLabel, "Select in which Instances worlds should get deleted in");

            instancePathMTB = new RemoveFolderMultiTextBox();
            instancePathMTB.setToolTip("Select in which Instances worlds should get deleted in");

            deleteAllWorldsThatLabel = new Label();
            deleteAllWorldsThatLabel.AutoSize = true;
            deleteAllWorldsThatLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            
            deleteAllWorldsThatLabel.Location = new Point(12, 132);
            deleteAllWorldsThatLabel.TabStop = false;
            deleteAllWorldsThatLabel.Text = "Delete all Worlds that";

            startWithLabel = new Label();
            startWithLabel.AutoSize = false;
            startWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            startWithLabel.Size = new Size(93, 23);
            startWithLabel.TabStop = false;
            startWithLabel.Text = "start with:";
            toolTip.SetToolTip(startWithLabel, "Select what the name of the world has to start with to be deleted");

            startWithMTB = new RemoveMultiTextBox();
            startWithMTB.setToolTip("Select what the name of the world has to start with to be deleted");

            includeLabel = new Label();
            includeLabel.AutoSize = false;
            includeLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            includeLabel.Size = new Size(76, 23);
            includeLabel.TabStop = false;
            includeLabel.Text = "include:";
            toolTip.SetToolTip(includeLabel, "Select what the name of the world has to include to be deleted");

            includeMTB = new RemoveMultiTextBox();
            includeMTB.setToolTip("Select what the name of the world has to include to be deleted");

            endWithLabel = new Label();
            endWithLabel.AutoSize = false;
            endWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            endWithLabel.Size = new Size(86, 23);
            endWithLabel.TabStop = false;
            endWithLabel.Text = "end with:";
            toolTip.SetToolTip(endWithLabel, "Select what the name of the world has to end with to be deleted");

            endWithMTB = new RemoveMultiTextBox();
            endWithMTB.setToolTip("Select what the name of the world has to end with to be deleted");

            deleteAllWorldsCheckBox = new CheckBox();
            deleteAllWorldsCheckBox.AutoSize = false;
            deleteAllWorldsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsCheckBox.Size = new Size(146, 23);
            deleteAllWorldsCheckBox.TabStop = false;
            deleteAllWorldsCheckBox.Text = "Delete all Worlds";
            deleteAllWorldsCheckBox.UseVisualStyleBackColor = true;
            deleteAllWorldsCheckBox.CheckedChanged += new EventHandler(deleteAllWorldsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteAllWorldsCheckBox, "Select if all worlds should be deleted, no matter the name of it");

            deleteRecordingsCheckBox = new CheckBox();
            deleteRecordingsCheckBox.AutoSize = false;
            deleteRecordingsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRecordingsCheckBox.Size = new Size(200, 23);
            deleteRecordingsCheckBox.TabStop = false;
            deleteRecordingsCheckBox.Text = "Delete Recordings";
            deleteRecordingsCheckBox.UseVisualStyleBackColor = true;
            deleteRecordingsCheckBox.CheckedChanged += new EventHandler(deleteRecordingsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteRecordingsCheckBox, "Select if MultiDelete should delete your Recordings");

            deleteCrashReportsCheckBox = new CheckBox();
            deleteCrashReportsCheckBox.AutoSize = false;
            deleteCrashReportsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCrashReportsCheckBox.Size = new Size(200, 23);
            deleteCrashReportsCheckBox.TabStop = false;
            deleteCrashReportsCheckBox.Text = "Delete Crash-Reports";
            deleteCrashReportsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteCrashReportsCheckBox, "Select if MultiDelete should delete your Crash-reports");

            deleteScreenshotsCheckBox = new CheckBox();
            deleteScreenshotsCheckBox.AutoSize = false;
            deleteScreenshotsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteScreenshotsCheckBox.Size = new Size(200, 23);
            deleteScreenshotsCheckBox.TabStop = false;
            deleteScreenshotsCheckBox.Text = "Delete Screenshots";
            deleteScreenshotsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteScreenshotsCheckBox, "Select if MultiDelete should delete your Screenshots");

            updateScreenLabel = new Label();
            updateScreenLabel.AutoSize = true;
            updateScreenLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel.TabStop = false;
            updateScreenLabel.Text = "Update screen every";
            toolTip.SetToolTip(updateScreenLabel, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");

            updateScreenNUD = new BNumericUpDown();
            updateScreenNUD.Size = new Size(40, 26);
            updateScreenNUD.TabStop = false;
            updateScreenNUD.Value = 1;
            updateScreenNUD.Minimum = 1;
            updateScreenNUD.Maximum = 10000;
            updateScreenNUD.TextAlign = HorizontalAlignment.Center;
            toolTip.SetToolTip(updateScreenNUD, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");

            updateScreenLabel2 = new Label();
            updateScreenLabel2.AutoSize = true;
            updateScreenLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel2.TabStop = false;
            updateScreenLabel2.Text = "world.";
            toolTip.SetToolTip(updateScreenLabel2, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");

            updateScreenPanel = new Panel();
            updateScreenPanel.Size = new Size(400, 25);
            updateScreenPanel.Controls.Add(updateScreenLabel);
            updateScreenLabel.Location = new Point(0, 0);
            updateScreenPanel.Controls.Add(updateScreenNUD);
            updateScreenNUD.Location = new Point(150, 0);
            updateScreenPanel.Controls.Add(updateScreenLabel2);
            updateScreenLabel2.Location = new Point(190, 0);

            recordingsFTB = new FolderTextBox();
            recordingsFTB.setFolderDialogDescription("select recordings-path");
            recordingsFTB.setToolTip("Select in which folder MultiDelete should delete your recordings");

            checkForUpdatesButton = new BButton();
            checkForUpdatesButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkForUpdatesButton.Size = new Size(150, 50);
            checkForUpdatesButton.TabStop = false;
            checkForUpdatesButton.Text = "Check for Updates";
            checkForUpdatesButton.UseVisualStyleBackColor = false;
            checkForUpdatesButton.Click += new EventHandler(checkForUpdatesButton_Click);
            checkForUpdatesButton.BorderSize = 1;
            toolTip.SetToolTip(checkForUpdatesButton, "Check if a new Update is available");

            addMultipleInstanceButton = new BButton();
            addMultipleInstanceButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            addMultipleInstanceButton.Size = new Size(200, 35);
            addMultipleInstanceButton.TabStop = false;
            addMultipleInstanceButton.Text = "Add multiple Instances";
            addMultipleInstanceButton.UseVisualStyleBackColor = false;
            addMultipleInstanceButton.Click += new EventHandler(addMultipleInstanceButton_Click);
            addMultipleInstanceButton.BorderSize = 1;
            toolTip.SetToolTip(addMultipleInstanceButton, "Add multiple Instances at once via selecting multiple folders");

            threadsToUseLabel = new Label();
            threadsToUseLabel.AutoSize = true;
            threadsToUseLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            threadsToUseLabel.TabStop = false;
            threadsToUseLabel.Text = "Threads to use: 1";
            toolTip.SetToolTip(threadsToUseLabel, "Configure how many threads MultiDelte should use to delete worlds.");

            threadsTrackBar = new TrackBar();
            threadsTrackBar.Maximum = Environment.ProcessorCount;
            threadsTrackBar.Minimum = 1;
            threadsTrackBar.Size = new Size(200, 45);
            threadsTrackBar.TabIndex = 0;
            threadsTrackBar.TabStop = false;
            threadsTrackBar.Value = 1;
            threadsTrackBar.ValueChanged += new EventHandler(threadsTrackBar_ValueChanged);
            toolTip.SetToolTip(threadsTrackBar, "Configure how many threads MultiDelte should use to delete worlds.");

            keepLastWorldsLabel = new Label();
            keepLastWorldsLabel.AutoSize = true;
            keepLastWorldsLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel.TabStop = false;
            keepLastWorldsLabel.Text = "Keep last";
            toolTip.SetToolTip(keepLastWorldsLabel, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsNUD = new BNumericUpDown();
            keepLastWorldsNUD.Size = new Size(37, 26);
            keepLastWorldsNUD.TabStop = false;
            keepLastWorldsNUD.Value = 10;
            keepLastWorldsNUD.Maximum = 1000;
            keepLastWorldsNUD.TextAlign = HorizontalAlignment.Center;
            toolTip.SetToolTip(keepLastWorldsNUD, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsLabel2 = new Label();
            keepLastWorldsLabel2.AutoSize = true;
            keepLastWorldsLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel2.TabStop = false;
            keepLastWorldsLabel2.Text = "worlds.";
            toolTip.SetToolTip(keepLastWorldsLabel2, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsPanel = new Panel();
            keepLastWorldsPanel.Size = new Size(400, 34);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel);
            keepLastWorldsLabel.Location = new Point(0, 3);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsNUD);
            keepLastWorldsNUD.Location = new Point(75, 0);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel2);
            keepLastWorldsLabel2.Location = new Point(112, 3);

            multiDeleteHeading = new Label();
            multiDeleteHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            multiDeleteHeading.AutoSize = true;
            multiDeleteHeading.Font = new System.Drawing.Font("Roboto", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            multiDeleteHeading.TabStop = false;
            multiDeleteHeading.Text = "MultiDelete " + MultiDelete.version;
            multiDeleteHeading.Padding = new Padding(0, 10, 0, 5);

            viewRepositoryLabel = new LinkLabel();
            viewRepositoryLabel.AutoSize = true;
            viewRepositoryLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            viewRepositoryLabel.TabStop = false;
            viewRepositoryLabel.Text = "View GitHub repository";
            viewRepositoryLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(viewRepositoryLabel_LinkClicked);
            viewRepositoryLabel.Padding = new Padding(0, 0, 0, 5);
            toolTip.SetToolTip(viewRepositoryLabel, "https://github.com/greyhayv/MultiDelete");

            bgColorLabel = new Label();
            bgColorLabel.AutoSize = true;
            bgColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bgColorLabel.TabStop = false;
            bgColorLabel.Text = "Background Color:";
            toolTip.SetToolTip(bgColorLabel, "Select which color the background of MultiDelete should have.");

            bgColorButton = new BButton();
            bgColorButton.Size = new Size(30, 30);
            bgColorButton.BorderRadius = 15;
            bgColorButton.TabStop = false;
            bgColorButton.UseVisualStyleBackColor = false;
            bgColorButton.BorderSize = 1;
            bgColorButton.DisableAnimations = true;
            bgColorButton.Click += new EventHandler(bgColorButton_Click);
            toolTip.SetToolTip(bgColorButton, "Select which color the background of MultiDelete should have.");

            bgColorPanel = new Panel();
            bgColorPanel.Size = new Size(400, 32);
            bgColorPanel.Controls.Add(bgColorLabel);
            bgColorLabel.Location = new Point(0, 5);
            bgColorPanel.Controls.Add(bgColorButton);
            bgColorButton.Location = new Point(140, 0);

            accentColorLabel = new Label();
            accentColorLabel.AutoSize = true;
            accentColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            accentColorLabel.TabStop = false;
            accentColorLabel.Text = "Accent Color:";
            toolTip.SetToolTip(accentColorLabel, "Select which Accent color MultiDelete should have.");

            accentColorButton = new BButton();
            accentColorButton.Size = new Size(30, 30);
            accentColorButton.BorderRadius = 15;
            accentColorButton.TabStop = false;
            accentColorButton.UseVisualStyleBackColor = false;
            accentColorButton.BorderSize = 1;
            accentColorButton.DisableAnimations = true;
            accentColorButton.Click += new EventHandler(accentColorButton_Click);
            toolTip.SetToolTip(accentColorButton, "Select which Accent color MultiDelete should have.");

            accentColorPanel = new Panel();
            accentColorPanel.Size = new Size(400, 32);
            accentColorPanel.Controls.Add(accentColorLabel);
            accentColorLabel.Location = new Point(0, 5);
            accentColorPanel.Controls.Add(accentColorButton);
            accentColorButton.Location = new Point(105, 0);

            fontColorLabel = new Label();
            fontColorLabel.AutoSize = true;
            fontColorLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            fontColorLabel.TabStop = false;
            fontColorLabel.Text = "Font Color:";
            toolTip.SetToolTip(fontColorLabel, "Select which Font color MultiDelete should have.");

            fontColorButton = new BButton();
            fontColorButton.Size = new Size(30, 30);
            fontColorButton.BorderRadius = 15;
            fontColorButton.TabStop = false;
            fontColorButton.UseVisualStyleBackColor = false;
            fontColorButton.BorderSize = 1;
            fontColorButton.DisableAnimations = true;
            fontColorButton.Click += new EventHandler(fontColorButton_Click);
            toolTip.SetToolTip(fontColorButton, "Select which Font color MultiDelete should have.");

            fontColorPanel = new Panel();
            fontColorPanel.Size = new Size(400, 32);
            fontColorPanel.Controls.Add(fontColorLabel);
            fontColorLabel.Location = new Point(0, 5);
            fontColorPanel.Controls.Add(fontColorButton);
            fontColorButton.Location = new Point(85, 0);

            settingsTabPanel.clearControls();
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
            settingsTabPanel.addControl("Advanced", updateScreenPanel);
            settingsTabPanel.addControl("Advanced", threadsToUseLabel);
            settingsTabPanel.addControl("Advanced", threadsTrackBar);
            settingsTabPanel.addControl("Advanced", keepLastWorldsPanel);
            settingsTabPanel.addControl("Advanced", deleteRecordingsCheckBox);
            settingsTabPanel.addControl("Advanced", recordingsFTB);
            settingsTabPanel.addControl("Advanced", deleteCrashReportsCheckBox);
            settingsTabPanel.addControl("Advanced", deleteScreenshotsCheckBox);
            settingsTabPanel.addControl("Appearance", bgColorPanel);
            settingsTabPanel.addControl("Appearance", accentColorPanel);
            settingsTabPanel.addControl("Appearance", fontColorPanel);
            settingsTabPanel.addControl("About", multiDeleteHeading);
            settingsTabPanel.addControl("About", viewRepositoryLabel);
            settingsTabPanel.addControl("About", checkForUpdatesButton);

            updateColors();

            loadSettings();
        }

        private void settingsMenu_Load(object sender, EventArgs e)
        {
            
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

            importButton.BorderColor = MultiDelete.accentColor;

            exportButton.BorderColor = MultiDelete.accentColor;

            settingsHeading.ForeColor = MultiDelete.fontColor;

            BackColor = MultiDelete.bgColor;

            exportButton.Image = MultiDelete.recolorImage(exportButton.Image, MultiDelete.fontColor);

            importButton.Image = MultiDelete.recolorImage(importButton.Image, MultiDelete.fontColor);
        }

        private void loadSettings()
        {
            if(File.Exists(optionsFile))
            {
                try
                {
                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));

                    instancePathMTB.setTexts(options.InstancePaths);
                    startWithMTB.setTexts(options.StartWith);
                    includeMTB.setTexts(options.Include);
                    endWithMTB.setTexts(options.EndWith);
                    recordingsFTB.setText(options.RecordingsPath);
                    deleteAllWorldsCheckBox.Checked = options.DeleteAllWorlds;
                    deleteAllWorldsCheckBox_CheckedChanged(new object(), new EventArgs());
                    updateScreenNUD.Value = options.UpdateScreenEvery;
                    deleteRecordingsCheckBox.Checked = options.DeleteRecordings;
                    deleteRecordingsCheckBox_CheckedChanged(new object(), new EventArgs());
                    deleteCrashReportsCheckBox.Checked = options.DeleteCrashReports;
                    deleteScreenshotsCheckBox.Checked = options.DeleteScreenshots;
                    if(options.ThreadCount < threadsTrackBar.Minimum)
                    {
                        options.ThreadCount = threadsTrackBar.Minimum;
                    } else if(options.ThreadCount > threadsTrackBar.Maximum)
                    {
                        options.ThreadCount = threadsTrackBar.Maximum;
                    }
                    threadsTrackBar.Value = options.ThreadCount;
                    threadsToUseLabel.Text = "Threads to use: " + options.ThreadCount;
                    keepLastWorldsNUD.Value = options.KeepLastWorlds;
                } catch
                {
                    DialogResult dr = MessageBox.Show("There was an error importing the settings! Load default settings?", "MultiDelete", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if(dr == DialogResult.Yes)
                    {
                        loadDefaultSettings();
                    }
                }

                return;
            }

            loadDefaultSettings();
        }

        private void loadDefaultSettings()
        {
            Options options = new Options
            {
                DeleteAllWorlds = false,
                DeleteCrashReports = false,
                DeleteRecordings = false,
                DeleteScreenshots = false,
                EndWith = new string[0],
                Include = new string[0],
                InstancePaths = new string[0],
                KeepLastWorlds = 0,
                RecordingsPath = "",
                StartWith = new string[] { "Random Speedrun", "Set Speedrun" },
                ThreadCount = 1,
                UpdateScreenEvery = 1
            };

            saveOptions(options);
            loadSettings();
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e)
        {
            Focus();
            MultiDelete.checkForUpdates(true);
        }

        private void settingsMenu_FormClosed(object sender, EventArgs e)
        {
            saveOptions();
        }

        private void saveOptions(Options options)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(options, jsonSerializerOptions);
            File.WriteAllText(optionsFile, jsonString);
        }

        private void saveOptions()
        {
            Options options = new Options
            {
                InstancePaths = instancePathMTB.getTexts().ToArray(),
                DeleteAllWorlds = deleteAllWorldsCheckBox.Checked,
                StartWith = startWithMTB.getTexts().ToArray(),
                Include = includeMTB.getTexts().ToArray(),
                EndWith = endWithMTB.getTexts().ToArray(),
                UpdateScreenEvery = (int)updateScreenNUD.Value,
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsFTB.getText(),
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked,
                ThreadCount = threadsTrackBar.Value,
                KeepLastWorlds = Decimal.ToInt32(keepLastWorldsNUD.Value),
                bgColor = ColorTranslator.ToHtml(MultiDelete.bgColor),
                accentColor = ColorTranslator.ToHtml(MultiDelete.accentColor),
                fontColor = ColorTranslator.ToHtml(MultiDelete.fontColor)
            };

            saveOptions(options);
        }

        private void deleteAllWorldsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(deleteAllWorldsCheckBox.Checked)
            {
                startWithMTB.disableTextBoxes();
                includeMTB.disableTextBoxes();
                endWithMTB.disableTextBoxes();

                //Grays out labels if DeleteAllWorlds checkBox is checked
                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(94, 94, 94);
                startWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                includeLabel.ForeColor = Color.FromArgb(94, 94, 94);
                endWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
            } else
            {
                startWithMTB.enableTextBoxes();
                includeMTB.enableTextBoxes();
                endWithMTB.enableTextBoxes();

                deleteAllWorldsThatLabel.ForeColor = MultiDelete.fontColor;
                startWithLabel.ForeColor = MultiDelete.fontColor;
                includeLabel.ForeColor = MultiDelete.fontColor;
                endWithLabel.ForeColor = MultiDelete.fontColor;

                deleteAllWorldsThatLabel.ForeColor = MultiDelete.fontColor;
                startWithLabel.ForeColor = MultiDelete.fontColor;
                includeLabel.ForeColor = MultiDelete.fontColor;
                endWithLabel.ForeColor = MultiDelete.fontColor;
            }
        }

        private void deleteRecordingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            recordingsFTB.setEnabled(deleteRecordingsCheckBox.Checked);
        }

        private void addMultipleInstanceButton_Click(object sender, EventArgs e)
        {
            Focus();

            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.Title = "Select multiple Instance-paths";
            cofd.IsFolderPicker = true;
            cofd.Multiselect = true;

            if(cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                instancePathMTB.addTexts(cofd.FileNames.ToList());
            }
        }

        private void updateScreenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Focus();
        }

        private void threadsTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar) sender;
            threadsToUseLabel.Text = "Threads to use: " + trackBar.Value;
        }
        private void exportButton_click(object sender, EventArgs e)
        {
            Focus();
            exportSettings();
        }

        private void importButton_click(object sender, EventArgs e)
        {
            Focus();
            importSettings();
        }

        private void bgColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickBGColor();
        }

        private void pickBGColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.bgColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.bgColor = Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B);
                multiDelete.updateColors();
                updateColors();
            }
        }

        private void accentColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickAccentColor();
        }

        private void pickAccentColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.accentColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.accentColor = Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B);
                multiDelete.updateColors();
                updateColors();
            }
        }

        private void fontColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickFontColor();
        }

        private void pickFontColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.fontColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.fontColor = Color.FromArgb(255, cd.Color.R, cd.Color.G, cd.Color.B);
                multiDelete.updateColors();
                updateColors();
            }
        }

        private void exportSettings()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export settings";
            sfd.Filter = "JSON File (*.json)|*.json";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                saveOptions();
                File.Create(sfd.FileName).Close();
                File.WriteAllText(sfd.FileName, File.ReadAllText(optionsFile));
            }
        }

        private void importSettings()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Import settings";
            ofd.Filter = "JSON File (*.json) |*.json";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(optionsFile, File.ReadAllText(ofd.FileName));
                loadSettings();
            }
        }

        private void settingsMenu_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                
                if(!files.Any(file => file.EndsWith(".json", StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }

                e.Effect = DragDropEffects.Copy;
            }
        }

        private void settingsMenu_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach(string file in files)
                {
                    if(!file.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (MessageBox.Show("Import settings from " + file + "?", "MultiDelete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }

                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(file));

                    saveOptions(options);
                    loadSettings();

                    return;
                }
            }
        }

        private void viewRepositoryLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("cmd", "/c start https://www.github.com/greyhayv/MultiDelete");
        }
    }

    public class Options
    {
        public string[] InstancePaths { get; set; }
        public bool DeleteAllWorlds { get; set; }
        public string[] StartWith { get; set; }
        public string[] Include { get; set; }
        public string[] EndWith { get; set; }
        public int UpdateScreenEvery { get; set; }
        public bool DeleteRecordings { get; set; }
        public string RecordingsPath { get; set; }
        public bool DeleteCrashReports { get; set; }
        public bool DeleteScreenshots { get; set; }
        public int ThreadCount { get; set; }
        public int KeepLastWorlds { get; set; }
        public string bgColor { get; set; }
        public string accentColor { get; set; }
        public string fontColor { get; set; }
    }
}