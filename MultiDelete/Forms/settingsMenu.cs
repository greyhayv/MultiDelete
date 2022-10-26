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
using MultiDelete.Controls;
using System.Linq;

namespace MultiDelete
{
    public partial class settingsMenu : Form
    {
        string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";

        //Settings menu elements
        Label settingsHeading = new Label();
        Button exportButton = new Button();
        Button importButton = new Button();
        Panel headingPanel = new Panel();
        RemoveFolderMultiTextBox instancePathMTB = new RemoveFolderMultiTextBox();
        RemoveMultiTextBox startsWithMTB = new RemoveMultiTextBox();
        RemoveMultiTextBox includeMTB = new RemoveMultiTextBox();
        RemoveMultiTextBox endWithMTB = new RemoveMultiTextBox();
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
        Button checkForUpdatesButton = new Button();
        FolderTextBox recordingsFTB = new FolderTextBox();
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
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

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
            toolTip.SetToolTip(exportButton, "Export settings");

            importButton = new Button();
            importButton.Size = new Size(22, 22);
            importButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            importButton.FlatStyle = FlatStyle.Popup;
            importButton.TabStop = false;
            importButton.UseVisualStyleBackColor = false;
            importButton.Image = Properties.Resources.importIcon;
            importButton.Padding = new Padding(0, 0, 1, 1);
            importButton.Click += new EventHandler(importButton_click);
            toolTip.SetToolTip(importButton, "Import settings");

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
            toolTip.SetToolTip(instancePathLabel, "Select in which Instances worlds should get deleted in");

            instancePathMTB = new RemoveFolderMultiTextBox();
            instancePathMTB.setToolTip("Select in which Instances worlds should get deleted in");

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
            toolTip.SetToolTip(startWithLabel, "Select what the name of the world has to start with to be deleted");

            startsWithMTB = new RemoveMultiTextBox();
            startsWithMTB.setToolTip("Select what the name of the world has to start with to be deleted");

            includeLabel = new Label();
            includeLabel.AutoSize = false;
            includeLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
            includeLabel.Size = new Size(76, 23);
            includeLabel.TabStop = false;
            includeLabel.Text = "include:";
            toolTip.SetToolTip(includeLabel, "Select what the name of the world has to include to be deleted");

            includeMTB = new RemoveMultiTextBox();
            includeMTB.setToolTip("Select what the name of the world has to include to be deleted");

            endWithLabel = new Label();
            endWithLabel.AutoSize = false;
            endWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            endWithLabel.Size = new Size(86, 23);
            endWithLabel.TabStop = false;
            endWithLabel.Text = "end with:";
            toolTip.SetToolTip(endWithLabel, "Select what the name of the world has to end with to be deleted");

            endWithMTB = new RemoveMultiTextBox();
            endWithMTB.setToolTip("Select what the name of the world has to end with to be deleted");

            deleteAllWorldsCheckBox = new CheckBox();
            deleteAllWorldsCheckBox.AutoSize = false;
            deleteAllWorldsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteAllWorldsCheckBox.Size = new Size(146, 23);
            deleteAllWorldsCheckBox.TabStop = false;
            deleteAllWorldsCheckBox.Text = "Delete all Worlds";
            deleteAllWorldsCheckBox.UseVisualStyleBackColor = true;
            deleteAllWorldsCheckBox.CheckedChanged += new EventHandler(deleteAllWorldsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteAllWorldsCheckBox, "Select if all worlds should be deleted, no matter the name of it");

            deleteRecordingsCheckBox = new CheckBox();
            deleteRecordingsCheckBox.AutoSize = false;
            deleteRecordingsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRecordingsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteRecordingsCheckBox.Size = new Size(200, 23);
            deleteRecordingsCheckBox.TabStop = false;
            deleteRecordingsCheckBox.Text = "Delete Recordings";
            deleteRecordingsCheckBox.UseVisualStyleBackColor = true;
            deleteRecordingsCheckBox.CheckedChanged += new EventHandler(deleteRecordingsCheckBox_CheckedChanged);
            toolTip.SetToolTip(deleteRecordingsCheckBox, "Select if MultiDelete should delete your Recordings");

            deleteCrashReportsCheckBox = new CheckBox();
            deleteCrashReportsCheckBox.AutoSize = false;
            deleteCrashReportsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCrashReportsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteCrashReportsCheckBox.Size = new Size(200, 23);
            deleteCrashReportsCheckBox.TabStop = false;
            deleteCrashReportsCheckBox.Text = "Delete Crash-Reports";
            deleteCrashReportsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteCrashReportsCheckBox, "Select if MultiDelete should delete your Crash-reports");

            deleteScreenshotsCheckBox = new CheckBox();
            deleteScreenshotsCheckBox.AutoSize = false;
            deleteScreenshotsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteScreenshotsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteScreenshotsCheckBox.Size = new Size(200, 23);
            deleteScreenshotsCheckBox.TabStop = false;
            deleteScreenshotsCheckBox.Text = "Delete Screenshots";
            deleteScreenshotsCheckBox.UseVisualStyleBackColor = true;
            toolTip.SetToolTip(deleteScreenshotsCheckBox, "Select if MultiDelete should delete your Screenshots");

            updateScreenLabel = new Label();
            updateScreenLabel.AutoSize = true;
            updateScreenLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            updateScreenLabel.ForeColor = Color.FromArgb(194, 194, 194);
            updateScreenLabel.TabStop = false;
            updateScreenLabel.Text = "Update screen";
            toolTip.SetToolTip(updateScreenLabel, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");

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
            toolTip.SetToolTip(updateScreenComboBox, "Select how often the screen should update during world deletion (Less updates = way faster world deletion)");

            updateScreenPanel = new Panel();
            updateScreenPanel.Size = new Size(400, 23);
            updateScreenPanel.Controls.Add(updateScreenLabel);
            updateScreenLabel.Location = new Point(0, 0);
            updateScreenPanel.Controls.Add(updateScreenComboBox);
            updateScreenComboBox.Location = new Point(110, 0);

            recordingsFTB = new FolderTextBox();
            recordingsFTB.setFolderDialogDescription("select recordings-path");
            recordingsFTB.setToolTip("Select in which folder MultiDelete should delete your recordings");

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
            toolTip.SetToolTip(checkForUpdatesButton, "Check if a new Update is available");

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
            toolTip.SetToolTip(addMultipleInstanceButton, "Add multiple Instances at once via selecting multiple folders");

            threadsToUseLabel = new Label();
            threadsToUseLabel.AutoSize = true;
            threadsToUseLabel.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            threadsToUseLabel.ForeColor = Color.FromArgb(194, 194, 194);
            threadsToUseLabel.TabStop = false;
            threadsToUseLabel.Text = "Threads to use: 1";
            toolTip.SetToolTip(threadsToUseLabel, "Configure how many threads MultiDelte should use to delete worlds.");

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
            keepLastWorldsLabel.ForeColor = Color.FromArgb(194, 194, 194);
            keepLastWorldsLabel.TabStop = false;
            keepLastWorldsLabel.Text = "Keep last";
            toolTip.SetToolTip(keepLastWorldsLabel, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsNUD = new NumericUpDown();
            keepLastWorldsNUD.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            keepLastWorldsNUD.BorderStyle = BorderStyle.FixedSingle;
            keepLastWorldsNUD.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            keepLastWorldsNUD.Size = new Size(35, 22);
            keepLastWorldsNUD.TabStop = false;
            keepLastWorldsNUD.Value = 10;
            toolTip.SetToolTip(keepLastWorldsNUD, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsLabel2 = new Label();
            keepLastWorldsLabel2.AutoSize = true;
            keepLastWorldsLabel2.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            keepLastWorldsLabel2.ForeColor = Color.FromArgb(194, 194, 194);
            keepLastWorldsLabel2.TabStop = false;
            keepLastWorldsLabel2.Text = "worlds.";
            toolTip.SetToolTip(keepLastWorldsLabel2, "Select how many of the last worlds MultiDelete should keep.");

            keepLastWorldsPanel = new Panel();
            keepLastWorldsPanel.Size = new Size(400, 23);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel);
            keepLastWorldsLabel.Location = new Point(0, 0);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsNUD);
            keepLastWorldsNUD.Location = new Point(75, 0);
            keepLastWorldsPanel.Controls.Add(keepLastWorldsLabel2);
            keepLastWorldsLabel2.Location = new Point(110, 0);

            settingsPanel.Controls.Clear();
            settingsPanel.Controls.Add(headingPanel);
            settingsPanel.Controls.Add(instancePathLabel);
            settingsPanel.Controls.Add(instancePathMTB);
            settingsPanel.Controls.Add(addMultipleInstanceButton);
            settingsPanel.Controls.Add(deleteAllWorldsCheckBox);
            settingsPanel.Controls.Add(deleteAllWorldsThatLabel);
            settingsPanel.Controls.Add(startWithLabel);
            settingsPanel.Controls.Add(startsWithMTB);
            settingsPanel.Controls.Add(includeLabel);
            settingsPanel.Controls.Add(includeMTB);
            settingsPanel.Controls.Add(endWithLabel);
            settingsPanel.Controls.Add(endWithMTB);
            settingsPanel.Controls.Add(updateScreenPanel);
            settingsPanel.Controls.Add(threadsToUseLabel);
            settingsPanel.Controls.Add(threadsTrackBar);
            settingsPanel.Controls.Add(deleteRecordingsCheckBox);
            settingsPanel.Controls.Add(recordingsFTB);
            settingsPanel.Controls.Add(deleteCrashReportsCheckBox);
            settingsPanel.Controls.Add(deleteScreenshotsCheckBox);
            settingsPanel.Controls.Add(keepLastWorldsPanel);
            settingsPanel.Controls.Add(checkForUpdatesButton);           

            loadSettings();
        }

        private void loadSettings()
        {
            if(File.Exists(optionsFile))
            {
                try
                {
                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));

                    instancePathMTB.setTexts(options.InstancePaths);
                    startsWithMTB.setTexts(options.StartWith);
                    includeMTB.setTexts(options.Include);
                    endWithMTB.setTexts(options.EndWith);
                    recordingsFTB.setText(options.RecordingsPath);
                    deleteAllWorldsCheckBox.Checked = options.DeleteAllWorlds;
                    deleteAllWorldsCheckBox_CheckedChanged(new object(), new EventArgs());
                    updateScreenComboBox.SelectedItem = options.UpdateScreen;
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
                StartWith = startsWithMTB.getTexts().ToArray(),
                Include = includeMTB.getTexts().ToArray(),
                EndWith = endWithMTB.getTexts().ToArray(),
                UpdateScreen = updateScreenComboBox.Text,
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsFTB.getText(),
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked,
                ThreadCount = threadsTrackBar.Value,
                KeepLastWorlds = Decimal.ToInt32(keepLastWorldsNUD.Value)
            };

            saveOptions(options);
        }

        private void deleteAllWorldsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(deleteAllWorldsCheckBox.Checked)
            {
                startsWithMTB.disableTextBoxes();
                includeMTB.disableTextBoxes();
                endWithMTB.disableTextBoxes();

                //Grays out labels if DeleteAllWorlds checkBox is checked
                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(94, 94, 94);
                startWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                includeLabel.ForeColor = Color.FromArgb(94, 94, 94);
                endWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
            } else
            {
                startsWithMTB.enableTextBoxes();
                includeMTB.enableTextBoxes();
                endWithMTB.enableTextBoxes();

                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
                startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
                includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
                endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);

                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
                startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
                includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
                endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            }
        }

        private void deleteRecordingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            recordingsFTB.setEnabled(deleteRecordingsCheckBox.Checked);
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
                instancePathMTB.addTexts(cofd.FileNames.ToList());
            }
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
                if(!ofd.FileName.EndsWith(".json"))
                {
                    MessageBox.Show("You need to select a valid settings File!", "MultiDelete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                File.WriteAllText(optionsFile, File.ReadAllText(ofd.FileName));
                loadSettings();
            }
        }

        private void settingsPanel_SizeChanged(object sender, EventArgs e)
        {
            //Change size of headingPanel and position of import and export button depending on if scrollbar has appeared
            if(settingsPanel.ClientSize.Width < 484)
            {
                headingPanel.Size = new Size(461, 35);
                exportButton.Location = new Point(414, 0);
                importButton.Location = new Point(439, 0);
            }
            else
            {
                headingPanel.Size = new Size(478, 35);
                exportButton.Location = new Point(431, 0);
                importButton.Location = new Point(456, 0);
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