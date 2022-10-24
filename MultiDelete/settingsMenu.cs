﻿using System.Text.Json;
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

namespace MultiDelete
{
    public partial class settingsMenu : Form
    {
        //Variables
        List<TextBox> instancePathEntrys = new List<TextBox>();
        List<Button> selectInstancePathButtons = new List<Button>();
        List<Button> deleteInstancePathButtons = new List<Button>();
        List<TextBox> startWithEntrys = new List<TextBox>();
        List<TextBox> includesEntrys = new List<TextBox>();
        List<TextBox> endWithEntrys = new List<TextBox>();
        List<Panel> instancePathPanel = new List<Panel>();
        string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";
        string focusEntry = "";

        //Settings menu elements
        Label settingsHeading = new Label();
        Button exportButton = new Button();
        Button importButton = new Button();
        Panel headingPanel = new Panel();
        ComboBox updateScreenComboBox = new ComboBox();
        Label instancePathLabel = new Label();
        Label deleteAllWorldsThatLabel = new Label();
        Label startWithLabel = new Label();
        Label includeLabel = new Label();
        Label endWithLabel = new Label();
        CheckBox deleteAllWorldsCheckBox = new CheckBox();
        CheckBox deleteRecordingsCheckBox = new CheckBox();
        CheckBox deleteCrashReportsCheckBox = new CheckBox();
        CheckBox deleteScreenshotsCheckBox = new CheckBox();
        TextBox recordingsPathTextBox = new TextBox();
        Button checkForUpdatesButton = new Button();
        Button recordingsPathButton = new Button();
        Panel recordingsPathPanel = new Panel();
        Panel updateScreenPanel = new Panel();
        Button addMultipleInstanceButton = new Button();
        Label updateScreenLabel = new Label();
        Label threadsToUseLabel = new Label();
        TrackBar threadsTrackBar = new TrackBar();
        Label keepLastWorldsLabel = new Label();
        Label keepLastWorldsLabel2 = new Label();
        NumericUpDown keepLastWorldsNUD = new NumericUpDown();
        Panel keepLastWorldsPanel = new Panel();

        public settingsMenu()
        {
            InitializeComponent();
        }

        private void settingsMenu_Load(object sender, EventArgs e)
        {
            //Configures Objects
            settingsHeading = new Label();
            settingsHeading.TextAlign = ContentAlignment.MiddleCenter;
            settingsHeading.AutoSize = true;
            settingsHeading.Font = new Font("Roboto", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            settingsHeading.ForeColor = Color.FromArgb(194, 194, 194);
            settingsHeading.TabStop = false;
            settingsHeading.Text = "Settings";

            exportButton = new Button();
            exportButton.Size = new Size(22, 22);
            exportButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            exportButton.FlatStyle = FlatStyle.Popup;
            exportButton.TabStop = false;
            exportButton.UseVisualStyleBackColor = false;
            exportButton.Image = Properties.Resources.exportIcon;
            exportButton.Padding = new Padding(0, 0, 1, 1);
            exportButton.Click += new EventHandler(exportButton_click);

            importButton = new Button();
            importButton.Size = new Size(22, 22);
            importButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            importButton.FlatStyle = FlatStyle.Popup;
            importButton.TabStop = false;
            importButton.UseVisualStyleBackColor = false;
            importButton.Image = Properties.Resources.importIcon;
            importButton.Padding = new Padding(0, 0, 1, 1);
            importButton.Click += new EventHandler(importButton_click);

            headingPanel = new Panel();
            headingPanel.Size = new Size(478, 35);
            headingPanel.Controls.Add(settingsHeading);
            settingsHeading.Location = new Point(182, 0);
            headingPanel.Controls.Add(exportButton);
            exportButton.Location = new Point(431, 0);
            headingPanel.Controls.Add(importButton);
            importButton.Location = new Point(456, 0);

            instancePathLabel = new Label();
            instancePathLabel.AutoSize = false;
            instancePathLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            instancePathLabel.ForeColor = Color.FromArgb(194, 194, 194);
            instancePathLabel.Size = new Size(150, 23);
            instancePathLabel.TabStop = false;
            instancePathLabel.Text = "Instance-Paths:";

            deleteAllWorldsThatLabel = new Label();
            deleteAllWorldsThatLabel.AutoSize = true;
            deleteAllWorldsThatLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
            deleteAllWorldsThatLabel.Location = new Point(12, 132);
            deleteAllWorldsThatLabel.TabStop = false;
            deleteAllWorldsThatLabel.Text = "Delete all Worlds that";

            startWithLabel = new Label();
            startWithLabel.AutoSize = false;
            startWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            startWithLabel.Size = new Size(93, 23);
            startWithLabel.TabStop = false;
            startWithLabel.Text = "start with:";

            includeLabel = new Label();
            includeLabel.AutoSize = false;
            includeLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
            includeLabel.Size = new Size(76, 23);
            includeLabel.TabStop = false;
            includeLabel.Text = "include:";

            endWithLabel = new Label();
            endWithLabel.AutoSize = false;
            endWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            endWithLabel.Size = new Size(86, 23);
            endWithLabel.TabStop = false;
            endWithLabel.Text = "end with:";

            deleteAllWorldsCheckBox = new CheckBox();
            deleteAllWorldsCheckBox.AutoSize = false;
            deleteAllWorldsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteAllWorldsCheckBox.Size = new Size(146, 23);
            deleteAllWorldsCheckBox.TabStop = false;
            deleteAllWorldsCheckBox.Text = "Delete all Worlds";
            deleteAllWorldsCheckBox.UseVisualStyleBackColor = true;
            deleteAllWorldsCheckBox.CheckedChanged += new EventHandler(this.deleteAllWorldsCheckBox_CheckedChanged);

            deleteRecordingsCheckBox = new CheckBox();
            deleteRecordingsCheckBox.AutoSize = false;
            deleteRecordingsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRecordingsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteRecordingsCheckBox.Size = new Size(200, 23);
            deleteRecordingsCheckBox.TabStop = false;
            deleteRecordingsCheckBox.Text = "Delete Recordings";
            deleteRecordingsCheckBox.UseVisualStyleBackColor = true;
            deleteRecordingsCheckBox.CheckedChanged += new EventHandler(this.deleteRecordingsCheckBox_CheckedChanged);

            deleteCrashReportsCheckBox = new CheckBox();
            deleteCrashReportsCheckBox.AutoSize = false;
            deleteCrashReportsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCrashReportsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteCrashReportsCheckBox.Size = new Size(200, 23);
            deleteCrashReportsCheckBox.TabStop = false;
            deleteCrashReportsCheckBox.Text = "Delete Crash-Reports";
            deleteCrashReportsCheckBox.UseVisualStyleBackColor = true;

            deleteScreenshotsCheckBox = new CheckBox();
            deleteScreenshotsCheckBox.AutoSize = false;
            deleteScreenshotsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteScreenshotsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteScreenshotsCheckBox.Size = new Size(200, 23);
            deleteScreenshotsCheckBox.TabStop = false;
            deleteScreenshotsCheckBox.Text = "Delete Screenshots";
            deleteScreenshotsCheckBox.UseVisualStyleBackColor = true;

            updateScreenLabel = new Label();
            updateScreenLabel.AutoSize = true;
            updateScreenLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel.ForeColor = Color.FromArgb(194, 194, 194);
            updateScreenLabel.TabStop = false;
            updateScreenLabel.Text = "Update screen";

            updateScreenComboBox = new ComboBox();
            updateScreenComboBox.BackColor = Color.FromArgb(76, 76, 76);
            updateScreenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            updateScreenComboBox.FlatStyle = FlatStyle.Popup;
            updateScreenComboBox.ForeColor = Color.FromArgb(194, 194, 194);
            updateScreenComboBox.FormattingEnabled = true;
            updateScreenComboBox.Items.AddRange(new object[] {
            "every world",
            "every 10. world",
            "every 100. world",
            "every 1000. world",
            "never"});
            updateScreenComboBox.Location = new Point(320, 12);
            updateScreenComboBox.Size = new Size(121, 21);
            updateScreenComboBox.TabStop = false;
            updateScreenComboBox.SelectedIndex = 0;
            updateScreenComboBox.SelectedIndexChanged += new EventHandler(updateScreenComboBox_SelectedIndexChanged);

            updateScreenPanel = new Panel();
            updateScreenPanel.Size = new Size(400, 23);
            updateScreenPanel.Controls.Add(updateScreenLabel);
            updateScreenLabel.Location = new Point(0, 0);
            updateScreenPanel.Controls.Add(updateScreenComboBox);
            updateScreenComboBox.Location = new Point(110, 0);

            recordingsPathTextBox = new TextBox();
            recordingsPathTextBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            recordingsPathTextBox.BorderStyle = BorderStyle.FixedSingle;
            recordingsPathTextBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            recordingsPathTextBox.Size = new Size(200, 22);
            recordingsPathTextBox.TabStop = false;
            recordingsPathTextBox.PlaceholderText = "Recordings Path";

            recordingsPathButton = new Button();
            recordingsPathButton.Size = new Size(22, 22);
            recordingsPathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            recordingsPathButton.FlatStyle = FlatStyle.Popup;
            recordingsPathButton.TabStop = false;
            recordingsPathButton.UseVisualStyleBackColor = false;
            recordingsPathButton.Image = Properties.Resources.foldericon;
            recordingsPathButton.Padding = new Padding(0, 0, 1, 0);
            recordingsPathButton.Click += new EventHandler(recordingsPathButton_Click);

            recordingsPathPanel = new Panel();
            recordingsPathPanel.Size = new Size(250, 22);
            recordingsPathPanel.Controls.Add(recordingsPathTextBox);
            recordingsPathTextBox.Location = new Point(0, 0);
            recordingsPathPanel.Controls.Add(recordingsPathButton);
            recordingsPathButton.Location = new Point(205, 0);

            checkForUpdatesButton = new Button();
            checkForUpdatesButton.BackColor = Color.FromArgb(76, 76, 76);
            checkForUpdatesButton.FlatStyle = FlatStyle.Popup;
            checkForUpdatesButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkForUpdatesButton.ForeColor = Color.FromArgb(194, 194, 194);
            checkForUpdatesButton.Size = new Size(150, 50);
            checkForUpdatesButton.TabStop = false;
            checkForUpdatesButton.Text = "Check for Updates";
            checkForUpdatesButton.UseVisualStyleBackColor = false;
            checkForUpdatesButton.Click += new EventHandler(checkForUpdatesButton_Click);

            addMultipleInstanceButton = new Button();
            addMultipleInstanceButton.BackColor = Color.FromArgb(76, 76, 76);
            addMultipleInstanceButton.FlatStyle = FlatStyle.Popup;
            addMultipleInstanceButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            addMultipleInstanceButton.ForeColor = Color.FromArgb(194, 194, 194);
            addMultipleInstanceButton.Size = new Size(200, 35);
            addMultipleInstanceButton.TabStop = false;
            addMultipleInstanceButton.Text = "Add multiple Instances";
            addMultipleInstanceButton.UseVisualStyleBackColor = false;
            addMultipleInstanceButton.Click += new EventHandler(addMultipleInstanceButton_Click);

            threadsToUseLabel = new Label();
            threadsToUseLabel.AutoSize = true;
            threadsToUseLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            threadsToUseLabel.ForeColor = Color.FromArgb(194, 194, 194);
            threadsToUseLabel.TabStop = false;
            threadsToUseLabel.Text = "Threads to use: 1";

            threadsTrackBar.Maximum = Environment.ProcessorCount;
            threadsTrackBar.Minimum = 1;
            threadsTrackBar.Size = new Size(200, 45);
            threadsTrackBar.TabIndex = 0;
            threadsTrackBar.TabStop = false;
            threadsTrackBar.Value = 1;
            threadsTrackBar.ValueChanged += new EventHandler(threadsTrackBar_ValueChanged);

            keepLastWorldsLabel = new Label();
            keepLastWorldsLabel.AutoSize = true;
            keepLastWorldsLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel.ForeColor = Color.FromArgb(194, 194, 194);
            keepLastWorldsLabel.TabStop = false;
            keepLastWorldsLabel.Text = "Keep last";

            keepLastWorldsNUD = new NumericUpDown();
            keepLastWorldsNUD.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            keepLastWorldsNUD.BorderStyle = BorderStyle.FixedSingle;
            keepLastWorldsNUD.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            keepLastWorldsNUD.Size = new Size(35, 22);
            keepLastWorldsNUD.TabStop = false;
            keepLastWorldsNUD.Value = 10;

            keepLastWorldsLabel2 = new Label();
            keepLastWorldsLabel2.AutoSize = true;
            keepLastWorldsLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel2.ForeColor = Color.FromArgb(194, 194, 194);
            keepLastWorldsLabel2.TabStop = false;
            keepLastWorldsLabel2.Text = "worlds.";

            keepLastWorldsPanel = new Panel();
            keepLastWorldsPanel.Size = new Size(400, 23);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel);
            keepLastWorldsLabel.Location = new Point(0, 0);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsNUD);
            keepLastWorldsNUD.Location = new Point(75, 0);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel2);
            keepLastWorldsLabel2.Location = new Point(110, 0);

            //Create ToolTips
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(exportButton, "Export settings");
            toolTip.SetToolTip(importButton, "Import settings");
            toolTip.SetToolTip(instancePathLabel, "Select in which Instances worlds should get deleted in");
            toolTip.SetToolTip(deleteAllWorldsCheckBox, "Select if all worlds should be deleted, no matter the name of it");
            toolTip.SetToolTip(startWithLabel, "Select what the name of the world has to start with to be deleted");
            toolTip.SetToolTip(includeLabel, "Select what the name of the world has to include to be deleted");
            toolTip.SetToolTip(endWithLabel, "Select what the name of the world has to end with to be deleted");
            toolTip.SetToolTip(deleteRecordingsCheckBox, "Select if MultiDelete should delete your Recordings");
            toolTip.SetToolTip(recordingsPathTextBox, "Select in which folder your Recordings are stored in");
            toolTip.SetToolTip(deleteCrashReportsCheckBox, "Select if MultiDelete should delete your Crash-reports");
            toolTip.SetToolTip(deleteScreenshotsCheckBox, "Select if MultiDelete should delete your Screenshots");
            toolTip.SetToolTip(checkForUpdatesButton, "Check if a new Update is available");
            toolTip.SetToolTip(addMultipleInstanceButton, "Add multiple Instances at once via selecting multiple folders");
            toolTip.SetToolTip(updateScreenLabel, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");
            toolTip.SetToolTip(updateScreenComboBox, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");
            toolTip.SetToolTip(threadsToUseLabel, "Configure how many threads MultiDelte should use to delete worlds.");
            toolTip.SetToolTip(threadsTrackBar, "Configure how many threads MultiDelte should use to delete worlds.");
            toolTip.SetToolTip(keepLastWorldsLabel, "Select how many of the last worlds MultiDelete should keep.");
            toolTip.SetToolTip(keepLastWorldsNUD, "Select how many of the last worlds MultiDelete should keep.");
            toolTip.SetToolTip(keepLastWorldsLabel2, "Select how many of the last worlds MultiDelete should keep.");

            //Resets settingsMenu
            settingsPanel.Controls.Clear();
            settingsPanel.Controls.Add(headingPanel);
            settingsPanel.Controls.Add(instancePathLabel);
            settingsPanel.Controls.Add(addMultipleInstanceButton);
            settingsPanel.Controls.Add(deleteAllWorldsThatLabel);
            settingsPanel.Controls.Add(startWithLabel);
            settingsPanel.Controls.Add(includeLabel);
            settingsPanel.Controls.Add(endWithLabel);
            settingsPanel.Controls.Add(deleteAllWorldsCheckBox);
            settingsPanel.Controls.Add(deleteRecordingsCheckBox);
            settingsPanel.Controls.Add(updateScreenPanel);
            settingsPanel.Controls.Add(recordingsPathPanel);
            settingsPanel.Controls.Add(deleteCrashReportsCheckBox);
            settingsPanel.Controls.Add(deleteScreenshotsCheckBox);
            settingsPanel.Controls.Add(threadsToUseLabel);
            settingsPanel.Controls.Add(threadsTrackBar);
            settingsPanel.Controls.Add(checkForUpdatesButton);
            settingsPanel.Controls.Add(keepLastWorldsPanel);
            instancePathPanel = new List<Panel>();
            selectInstancePathButtons = new List<Button>();
            deleteInstancePathButtons = new List<Button>();
            instancePathEntrys = new List<TextBox>();
            createNewTextBox("instancePath", false);
            startWithEntrys = new List<TextBox>();
            createNewTextBox("startsWith", false);
            includesEntrys = new List<TextBox>();
            createNewTextBox("includes", false);
            endWithEntrys = new List<TextBox>();
            createNewTextBox("endWith", false);
            arrangeObjects();

            loadSettings();
        }

        private void loadSettings()
        {
            if (File.Exists(optionsFile))
            {
                try
                {
                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));

                    for (int i = 0; i < options.InstancePaths.Length; i++)
                    {
                        instancePathEntrys[i].Text = options.InstancePaths[i];
                    }

                    for (int i = 0; i < options.StartWith.Length; i++)
                    {
                        startWithEntrys[i].Text = options.StartWith[i];
                    }

                    for (int i = 0; i < options.Include.Length; i++)
                    {
                        includesEntrys[i].Text = options.Include[i];
                    }

                    for (int i = 0; i < options.EndWith.Length; i++)
                    {
                        endWithEntrys[i].Text = options.EndWith[i];
                    }

                    recordingsPathTextBox.Text = options.RecordingsPath;

                    if (options.DeleteAllWorlds)
                    {
                        deleteAllWorldsCheckBox.Checked = true;
                        foreach (TextBox textBox in startWithEntrys)
                        {
                            textBox.Enabled = false;
                        }
                        foreach (TextBox textBox in includesEntrys)
                        {
                            textBox.Enabled = false;
                        }
                        foreach (TextBox textBox in endWithEntrys)
                        {
                            textBox.Enabled = false;
                        }

                        //Grays out labels if DeleteAllWorlds checkBox is checked
                        deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(94, 94, 94);
                        startWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                        includeLabel.ForeColor = Color.FromArgb(94, 94, 94);
                        endWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                    }
                    else
                    {
                        deleteAllWorldsCheckBox.Checked = false;
                        foreach (TextBox textBox in startWithEntrys)
                        {
                            textBox.Enabled = true;
                        }
                        foreach (TextBox textBox in includesEntrys)
                        {
                            textBox.Enabled = true;
                        }
                        foreach (TextBox textBox in endWithEntrys)
                        {
                            textBox.Enabled = true;
                        }

                        deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
                        startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
                        includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
                        endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
                    }

                    updateScreenComboBox.SelectedItem = options.UpdateScreen;

                    if (options.DeleteRecordings)
                    {
                        deleteRecordingsCheckBox.Checked = true;
                        recordingsPathTextBox.Enabled = true;
                        recordingsPathButton.Enabled = true;
                    }
                    else
                    {
                        deleteRecordingsCheckBox.Checked = false;
                        recordingsPathTextBox.Enabled = false;
                        recordingsPathButton.Enabled = false;
                    }

                    deleteCrashReportsCheckBox.Checked = options.DeleteCrashReports;
                    deleteScreenshotsCheckBox.Checked = options.DeleteScreenshots;

                    if (options.ThreadCount < threadsTrackBar.Minimum)
                    {
                        options.ThreadCount = threadsTrackBar.Minimum;
                    }
                    else if (options.ThreadCount > threadsTrackBar.Maximum)
                    {
                        options.ThreadCount = threadsTrackBar.Maximum;
                    }
                    threadsTrackBar.Value = options.ThreadCount;
                    threadsToUseLabel.Text = "Threads to use: " + options.ThreadCount;
                    keepLastWorldsNUD.Value = options.KeepLastWorlds;
                }
                catch
                {
                    DialogResult dr = MessageBox.Show("There was an error importing the settings! Load default settings?", "MultiDelete", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if(dr == DialogResult.Yes)
                    {
                        loadDefaultSettings();
                    }
                }
            }
            else
            {
                loadDefaultSettings();
            }
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
                KeepLastWorlds = 10,
                RecordingsPath = "",
                StartWith = new string[] { "Random Speedrun", "Set Speedrun" },
                ThreadCount = 1,
                UpdateScreen = "every world"
            };

            saveOptions(options);
            loadSettings();
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            MultiDelete.checkForUpdates(true);
        }

        private void settingsMenu_FormClosed(object sender, EventArgs e)
        {
            List<string> instancePaths = new List<string>();
            foreach(TextBox instancePathEntry in instancePathEntrys)
            {
                if(!string.IsNullOrEmpty(instancePathEntry.Text))
                {
                    instancePaths.Add(instancePathEntry.Text);
                }
            }

            List<string> startWithList = new List<string>();
            foreach (TextBox startWithEntry in startWithEntrys)
            {
                if(!string.IsNullOrWhiteSpace(startWithEntry.Text))
                {
                    startWithList.Add(startWithEntry.Text);
                }
            }

            List<string> includeList = new List<string>();
            foreach (TextBox includeEntry in includesEntrys)
            {
                if(!string.IsNullOrWhiteSpace(includeEntry.Text))
                {
                    includeList.Add(includeEntry.Text);
                }
            }

            List<string> endWithList = new List<string>();
            foreach (TextBox endWithEntry in endWithEntrys)
            {
                if(!string.IsNullOrWhiteSpace(endWithEntry.Text))
                {
                    endWithList.Add(endWithEntry.Text);
                }
            }

            Options options = new Options
            {
                InstancePaths = instancePaths.ToArray(),
                DeleteAllWorlds = deleteAllWorldsCheckBox.Checked,
                StartWith = startWithList.ToArray(),
                Include = includeList.ToArray(),
                EndWith = endWithList.ToArray(),
                UpdateScreen = updateScreenComboBox.Text,
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsPathTextBox.Text,
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked,
                ThreadCount = threadsTrackBar.Value,
                KeepLastWorlds = Decimal.ToInt32(keepLastWorldsNUD.Value)
            };

            saveOptions(options);
        }

        private void saveOptions(Options options)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(options, jsonSerializerOptions);
            File.WriteAllText(optionsFile, jsonString);
        }

        private void saveOptions()
        {
            List<string> instancePaths = new List<string>();
            foreach (TextBox instancePathEntry in instancePathEntrys)
            {
                if (!string.IsNullOrEmpty(instancePathEntry.Text))
                {
                    instancePaths.Add(instancePathEntry.Text);
                }
            }

            List<string> startWithList = new List<string>();
            foreach (TextBox startWithEntry in startWithEntrys)
            {
                if (!string.IsNullOrWhiteSpace(startWithEntry.Text))
                {
                    startWithList.Add(startWithEntry.Text);
                }
            }

            List<string> includeList = new List<string>();
            foreach (TextBox includeEntry in includesEntrys)
            {
                if (!string.IsNullOrWhiteSpace(includeEntry.Text))
                {
                    includeList.Add(includeEntry.Text);
                }
            }

            List<string> endWithList = new List<string>();
            foreach (TextBox endWithEntry in endWithEntrys)
            {
                if (!string.IsNullOrWhiteSpace(endWithEntry.Text))
                {
                    endWithList.Add(endWithEntry.Text);
                }
            }

            Options options = new Options
            {
                InstancePaths = instancePaths.ToArray(),
                DeleteAllWorlds = deleteAllWorldsCheckBox.Checked,
                StartWith = startWithList.ToArray(),
                Include = includeList.ToArray(),
                EndWith = endWithList.ToArray(),
                UpdateScreen = updateScreenComboBox.Text,
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsPathTextBox.Text,
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked,
                ThreadCount = threadsTrackBar.Value,
                KeepLastWorlds = Decimal.ToInt32(keepLastWorldsNUD.Value)
            };

            saveOptions(options);
        }

        private void textChanged(object sender, EventArgs e)
        {
            //Deletes TextBoxes if they are empty
            for (int i = 0; i < instancePathEntrys.Count; i++)
            {
                if (instancePathEntrys[i].Text == "")
                {
                    if (i < instancePathEntrys.Count - 2)
                    {
                        deleteTextBox(i, "instancePath");
                        return;
                    }
                    else if (i == instancePathEntrys.Count - 2 && instancePathEntrys.Count > 1)
                    {
                        deleteTextBox(instancePathEntrys.Count - 1, "instancePath");
                    }
                }
            }
            //Creates a new TextBox if last TextBox has Text
            if(instancePathEntrys[instancePathEntrys.Count - 1].Text != "")
            {
                createNewTextBox("instancePath", true);
            }

            //Deletes TextBoxes if they are empty
            for (int i = 0; i < startWithEntrys.Count; i++)
            {
                if (startWithEntrys[i].Text == "")
                {
                    if (i < startWithEntrys.Count - 2)
                    {
                        deleteTextBox(i, "startsWith");
                        return;
                    }
                    else if (i == startWithEntrys.Count - 2 && startWithEntrys.Count > 1)
                    {
                        deleteTextBox(startWithEntrys.Count - 1, "startsWith");
                    }
                }
            }
            //Creates a new TextBox if last TextBox has Text
            if (startWithEntrys[startWithEntrys.Count - 1].Text != "")
            {
                createNewTextBox("startsWith", true);
            }

            //Deletes TextBoxes if they are empty
            for (int i = 0; i < includesEntrys.Count; i++)
            {
                if (includesEntrys[i].Text == "")
                {
                    if (i < includesEntrys.Count - 2)
                    {
                        deleteTextBox(i, "includes");
                        return;
                    }
                    else if (i == includesEntrys.Count - 2 && includesEntrys.Count > 1)
                    {
                        deleteTextBox(includesEntrys.Count - 1, "includes");
                    }
                }
            }
            //Creates a new TextBox if last TextBox has Text
            if (includesEntrys[includesEntrys.Count - 1].Text != "")
            {
                createNewTextBox("includes", true);
            }

            //Deletes TextBoxes if they are empty
            for (int i = 0; i < endWithEntrys.Count; i++)
            {
                if (endWithEntrys[i].Text == "")
                {
                    if (i < endWithEntrys.Count - 2)
                    {
                        deleteTextBox(i, "endWith");
                        return;
                    }
                    else if (i == endWithEntrys.Count - 2 && endWithEntrys.Count > 1)
                    {
                        deleteTextBox(endWithEntrys.Count - 1, "endWith");
                    }
                }
            }
            //Creates a new TextBox if last TextBox has Text
            if (endWithEntrys[endWithEntrys.Count - 1].Text != "")
            {
                createNewTextBox("endWith", true);
            }
        }

        private void createNewTextBox(string type, bool shouldArrangeObjects)
        {
            //Create and configure new TextBox
            TextBox textBox = new TextBox();
            textBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            textBox.Size = new Size(200, 22);
            textBox.TabStop = false;
            textBox.TextChanged += new EventHandler(textChanged);
            ToolTip toolTip = new ToolTip();
            //Adds TextBox to List
            if (type == "instancePath")
            {
                toolTip.SetToolTip(textBox, "Select in which Instances worlds should get deleted in");
                instancePathEntrys.Add(textBox);

                //Creates and configures new Button to select instance Path
                Button selectInstancePathButton = new Button();
                selectInstancePathButton.Size = new Size(22, 22);
                selectInstancePathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
                selectInstancePathButton.FlatStyle = FlatStyle.Popup;
                selectInstancePathButton.TabStop = false;
                selectInstancePathButton.UseVisualStyleBackColor = false;
                selectInstancePathButton.Image = Properties.Resources.foldericon;
                selectInstancePathButton.Padding = new Padding(0, 0, 1, 0);
                selectInstancePathButton.Click += new EventHandler(selectInstancePathButton_click);
                toolTip.SetToolTip(selectInstancePathButton, "Browse for Instance-path");
                selectInstancePathButtons.Add(selectInstancePathButton);

                //Creates and configures new Button to delete instance Path
                Button deleteInstancePathButton = new Button();
                deleteInstancePathButton.Size = new Size(22, 22);
                deleteInstancePathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
                deleteInstancePathButton.FlatStyle = FlatStyle.Popup;
                deleteInstancePathButton.TabStop = false;
                deleteInstancePathButton.UseVisualStyleBackColor = false;
                deleteInstancePathButton.Image = Properties.Resources.x;
                deleteInstancePathButton.Padding = new Padding(0, 0, 1, 1);
                deleteInstancePathButton.Click += new EventHandler(deleteInstancePathButton_click);
                toolTip.SetToolTip(deleteInstancePathButton, "Remove Instance-Path");
                deleteInstancePathButtons.Add(deleteInstancePathButton);

                //Creates and configures Panel with TextBox and Button
                Panel instancePathPanel = new Panel();
                instancePathPanel.Size = new Size(275, 22);
                instancePathPanel.Controls.Add(textBox);
                textBox.Location = new Point(0, 0);
                instancePathPanel.Controls.Add(selectInstancePathButton);
                selectInstancePathButton.Location = new Point(205, 0);
                deleteInstancePathButton.Location = new Point(230, 0);
                instancePathPanel.Controls.Add(deleteInstancePathButton);
                this.instancePathPanel.Add(instancePathPanel);
                settingsPanel.Controls.Add(instancePathPanel);
            }
            if (type == "startsWith")
            {
                toolTip.SetToolTip(textBox, "Select what the name of the world has to start with to be deleted");
                startWithEntrys.Add(textBox);
                settingsPanel.Controls.Add(textBox);
            }
            if (type == "includes")
            {
                toolTip.SetToolTip(textBox, "Select what the name of the world has to include to be deleted");
                includesEntrys.Add(textBox);
                settingsPanel.Controls.Add(textBox);
            }
            if (type == "endWith")
            {
                toolTip.SetToolTip(textBox, "Select what the name of the world has to end with to be deleted");
                endWithEntrys.Add(textBox);
                settingsPanel.Controls.Add(textBox);
            }
            
            if(shouldArrangeObjects)
            {
                arrangeObjects();
            }
        }

        private void deleteTextBox(int i, string type)
        {
            //Deletes TextBox and removes it from List
            if(type == "instancePath")
            {
                settingsPanel.Controls.Remove(instancePathPanel[i]);
                instancePathEntrys.RemoveAt(i);

                selectInstancePathButtons.RemoveAt(i);
                deleteInstancePathButtons.RemoveAt(i);
                instancePathPanel.RemoveAt(i);
                focusEntry = "instancePath";
            }
            if (type == "startsWith")
            {
                settingsPanel.Controls.Remove(startWithEntrys[i]);
                startWithEntrys.RemoveAt(i);
                focusEntry = "startsWith";
            }
            if (type == "includes")
            {
                settingsPanel.Controls.Remove(includesEntrys[i]);
                includesEntrys.RemoveAt(i);
                focusEntry = "includes";
            }
            if (type == "endWith")
            {
                settingsPanel.Controls.Remove(endWithEntrys[i]);
                endWithEntrys.RemoveAt(i);
                focusEntry = "endWith";
            }
            arrangeObjects();
        }

        private void arrangeObjects()
        {
            int index = 0;
            //Arranges objects on Panel
            settingsPanel.Controls.SetChildIndex(headingPanel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(instancePathLabel, index);
            index++;

            for(int i = 0; i < deleteInstancePathButtons.Count; i++)
            {
                if(i < deleteInstancePathButtons.Count - 1)
                {
                    deleteInstancePathButtons[i].Visible = true;
                } else
                {
                    deleteInstancePathButtons[i].Visible = false;
                }
            }

            foreach(Panel panel in instancePathPanel)
            {
                settingsPanel.Controls.SetChildIndex(panel, index);
                index++;
            }

            settingsPanel.Controls.SetChildIndex(addMultipleInstanceButton, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteAllWorldsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteAllWorldsThatLabel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(startWithLabel, index);
            index++;

            foreach (TextBox startsWithEntry in startWithEntrys)
            {
                settingsPanel.Controls.SetChildIndex(startsWithEntry, index);
                index++;
            }

            settingsPanel.Controls.SetChildIndex(includeLabel, index);
            index++;

            foreach(TextBox includesEntry in includesEntrys)
            {
                settingsPanel.Controls.SetChildIndex(includesEntry, index);
                index++;
            }

            settingsPanel.Controls.SetChildIndex(endWithLabel, index);
            index++;

            foreach(TextBox endsWithEntry in endWithEntrys)
            {
                settingsPanel.Controls.SetChildIndex(endsWithEntry, index);
                index++;
            }

            settingsPanel.Controls.SetChildIndex(updateScreenPanel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteRecordingsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(recordingsPathPanel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteCrashReportsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteScreenshotsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(threadsToUseLabel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(threadsTrackBar, index);
            index++;

            settingsPanel.Controls.SetChildIndex(keepLastWorldsPanel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(checkForUpdatesButton, index);
            index++;

            //Change size of headingPanel and position of import and export button depending on if scrollbar has appeared
            if(settingsPanel.ClientSize.Width < 484)
            {
                headingPanel.Size = new Size(461, 35);
                exportButton.Location = new Point(414, 0);
                importButton.Location = new Point(439, 0);
            } else
            {
                headingPanel.Size = new Size(478, 35);
                exportButton.Location = new Point(431, 0);
                importButton.Location = new Point(456, 0);
            }

            //Focuses last TextBox if TextBox was deleted
            if (focusEntry == "instancePath")
            {
                this.ActiveControl = instancePathEntrys[instancePathEntrys.Count - 1];
                instancePathEntrys[instancePathEntrys.Count - 1].Focus();
                focusEntry = "";
            } else if(focusEntry == "startsWith")
            {
                this.ActiveControl = startWithEntrys[startWithEntrys.Count - 1];
                focusEntry = "";
            } else if(focusEntry == "includes")
            {
                this.ActiveControl = includesEntrys[includesEntrys.Count - 1];
                focusEntry = "";
            } else if(focusEntry == "endWith")
            {
                this.ActiveControl = endWithEntrys[endWithEntrys.Count - 1];
                focusEntry = "";
            }
        }

        private void deleteAllWorldsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            if(checkBox.Checked == true)
            {
                //Disables TextBoxes if DeleteAllWorlds checkBox is checked
                foreach(TextBox textBox in startWithEntrys)
                {
                    textBox.Enabled = false;
                }
                foreach(TextBox textBox in includesEntrys)
                {
                    textBox.Enabled = false;
                }
                foreach(TextBox textBox in endWithEntrys)
                {
                    textBox.Enabled = false;
                }

                //Grays out labels if DeleteAllWorlds checkBox is checked
                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(94, 94, 94);
                startWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                includeLabel.ForeColor = Color.FromArgb(94, 94, 94);
                endWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
            } else
            {
                //Enabled TextBoxes if DeleteAllWorlds checkBox is not checked
                foreach (TextBox textBox in startWithEntrys)
                {
                    textBox.Enabled = true;
                }
                foreach (TextBox textBox in includesEntrys)
                {
                    textBox.Enabled = true;
                }
                foreach (TextBox textBox in endWithEntrys)
                {
                    textBox.Enabled = true;
                }

                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
                startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
                includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
                endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            }
        }

        private void deleteRecordingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                recordingsPathTextBox.Enabled = true;
                recordingsPathButton.Enabled = true;
            }
            else
            {
                recordingsPathTextBox.Enabled = false;
                recordingsPathButton.Enabled = false;
            }
        }


        private void selectInstancePathButton_click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            int index = 0;
            for(int i = 0; i < selectInstancePathButtons.Count; i++)
            {
                if (selectInstancePathButtons[i].Equals(sender))
                {
                    index = i;
                }
            }
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.UseDescriptionForTitle = true;
                fbd.Description = "Select Instance-path";
                fbd.ShowDialog();
                if(fbd.SelectedPath != "")
                {
                    instancePathEntrys[index].Text = fbd.SelectedPath;
                }
            }
        }

        private void recordingsPathButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            using(var fbd = new FolderBrowserDialog())
            {
                fbd.UseDescriptionForTitle = true;
                fbd.Description = "Select Recordings-path";
                fbd.ShowDialog();
                if (fbd.SelectedPath != "")
                {
                    recordingsPathTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void addMultipleInstanceButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Focus();

            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.Title = "Select multiple Instance-paths";
            cofd.IsFolderPicker = true;
            cofd.Multiselect = true;

            if(cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (string folder in cofd.FileNames)
                {
                    instancePathEntrys[instancePathEntrys.Count - 1].Text = folder;
                }
            }
        }

        private void deleteInstancePathButton_click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            int index = 0;
            for (int i = 0; i < deleteInstancePathButtons.Count; i++)
            {
                if (deleteInstancePathButtons[i].Equals(sender))
                {
                    index = i;
                }
            }
            deleteTextBox(index, "instancePath");
        }

        private void updateScreenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsPanel.Focus();
        }

        private void threadsTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar) sender;
            threadsToUseLabel.Text = "Threads to use: " + trackBar.Value;
        }
        private void exportButton_click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            exportSettings();
        }

        private void importButton_click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            importSettings();
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
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.FileName.EndsWith(".json"))
                {
                    MessageBox.Show("You need to select a valid settings File!", "MultiDelete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                File.WriteAllText(optionsFile, File.ReadAllText(ofd.FileName));
                loadSettings();
            }
        }
    }

    public class Options
    {
        public string[] InstancePaths { get; set; }
        public bool DeleteAllWorlds { get; set; }
        public string[] StartWith { get; set; }
        public string[] Include { get; set; }
        public string[] EndWith { get; set; }
        public string UpdateScreen { get; set; }
        public bool DeleteRecordings { get; set; }
        public string RecordingsPath { get; set; }
        public bool DeleteCrashReports { get; set; }
        public bool DeleteScreenshots { get; set; }
        public int ThreadCount { get; set; }
        public int KeepLastWorlds { get; set; }
    }
}