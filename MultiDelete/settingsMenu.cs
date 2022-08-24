using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

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
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";
        string focusEntry = "";

        Label settingsHeading = new Label();
        Label instancePathLabel = new Label();
        Label deleteAllWorldsThatLabel = new Label();
        Label startWithLabel = new Label();
        Label includeLabel = new Label();
        Label endWithLabel = new Label();
        CheckBox deleteAllWorldsCheckBox = new CheckBox();
        CheckBox deleteRecordingsCheckBox = new CheckBox();
        CheckBox deleteCrashReportsCheckBox = new CheckBox();
        CheckBox deleteRawalleLogsCheckBox = new CheckBox();
        CheckBox deleteScreenshotsCheckBox = new CheckBox();
        TextBox recordingsPathTextBox = new TextBox();
        Button checkForUpdatesButton = new Button();
        Button recordingsPathButton = new Button();
        Panel recordingsPathPanel = new Panel();
        Button addMultipleInstanceButton = new Button();

        public settingsMenu()
        {
            InitializeComponent();
        }

        private void settingsMenu_Load(object sender, EventArgs e)
        {
            //Configures Objects
            settingsHeading.TextAlign = ContentAlignment.MiddleCenter;
            settingsHeading.AutoSize = false;
            settingsHeading.Font = new Font("Roboto", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            settingsHeading.ForeColor = Color.FromArgb(194, 194, 194);
            settingsHeading.Size = new Size(478, 33);
            settingsHeading.TabStop = false;
            settingsHeading.Text = "Settings";

            instancePathLabel.AutoSize = false;
            instancePathLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            instancePathLabel.ForeColor = Color.FromArgb(194, 194, 194);
            instancePathLabel.Size = new Size(150, 23);
            instancePathLabel.TabStop = false;
            instancePathLabel.Text = "Instance-Paths:";

            deleteAllWorldsThatLabel.AutoSize = true;
            deleteAllWorldsThatLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(194, 194, 194);
            deleteAllWorldsThatLabel.Location = new Point(12, 132);
            deleteAllWorldsThatLabel.TabStop = false;
            deleteAllWorldsThatLabel.Text = "Delete all Worlds that";

            startWithLabel.AutoSize = false;
            startWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            startWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            startWithLabel.Size = new Size(93, 23);
            startWithLabel.TabStop = false;
            startWithLabel.Text = "start with:";

            includeLabel.AutoSize = false;
            includeLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            includeLabel.ForeColor = Color.FromArgb(194, 194, 194);
            includeLabel.Size = new Size(76, 23);
            includeLabel.TabStop = false;
            includeLabel.Text = "include:";

            endWithLabel.AutoSize = false;
            endWithLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            endWithLabel.ForeColor = Color.FromArgb(194, 194, 194);
            endWithLabel.Size = new Size(86, 23);
            endWithLabel.TabStop = false;
            endWithLabel.Text = "end with:";

            deleteAllWorldsCheckBox.AutoSize = false;
            deleteAllWorldsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteAllWorldsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteAllWorldsCheckBox.Size = new Size(146, 23);
            deleteAllWorldsCheckBox.TabStop = false;
            deleteAllWorldsCheckBox.Text = "Delete all Worlds";
            deleteAllWorldsCheckBox.UseVisualStyleBackColor = true;
            deleteAllWorldsCheckBox.CheckedChanged += new EventHandler(this.deleteAllWorldsCheckBox_CheckedChanged);

            deleteRecordingsCheckBox.AutoSize = false;
            deleteRecordingsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRecordingsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteRecordingsCheckBox.Size = new Size(200, 23);
            deleteRecordingsCheckBox.TabStop = false;
            deleteRecordingsCheckBox.Text = "Delete Recordings";
            deleteRecordingsCheckBox.UseVisualStyleBackColor = true;
            deleteRecordingsCheckBox.CheckedChanged += new EventHandler(this.deleteRecordingsCheckBox_CheckedChanged);

            deleteCrashReportsCheckBox.AutoSize = false;
            deleteCrashReportsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCrashReportsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteCrashReportsCheckBox.Size = new Size(200, 23);
            deleteCrashReportsCheckBox.TabStop = false;
            deleteCrashReportsCheckBox.Text = "Delete Crash-Reports";
            deleteCrashReportsCheckBox.UseVisualStyleBackColor = true;

            deleteRawalleLogsCheckBox.AutoSize = false;
            deleteRawalleLogsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteRawalleLogsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteRawalleLogsCheckBox.Size = new Size(200, 23);
            deleteRawalleLogsCheckBox.TabStop = false;
            deleteRawalleLogsCheckBox.Text = "Delete Rawalle logs";
            deleteRawalleLogsCheckBox.UseVisualStyleBackColor = true;

            deleteScreenshotsCheckBox.AutoSize = false;
            deleteScreenshotsCheckBox.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteScreenshotsCheckBox.ForeColor = Color.FromArgb(194, 194, 194);
            deleteScreenshotsCheckBox.Size = new Size(200, 23);
            deleteScreenshotsCheckBox.TabStop = false;
            deleteScreenshotsCheckBox.Text = "Delete Screenshots";
            deleteScreenshotsCheckBox.UseVisualStyleBackColor = true;

            recordingsPathTextBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            recordingsPathTextBox.BorderStyle = BorderStyle.FixedSingle;
            recordingsPathTextBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            recordingsPathTextBox.Size = new Size(200, 22);
            recordingsPathTextBox.TabStop = false;
            recordingsPathTextBox.PlaceholderText = "Recordings Path";

            recordingsPathButton.Size = new Size(22, 22);
            recordingsPathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            recordingsPathButton.FlatStyle = FlatStyle.Popup;
            recordingsPathButton.TabStop = false;
            recordingsPathButton.UseVisualStyleBackColor = false;
            recordingsPathButton.Image = Properties.Resources.foldericon;
            recordingsPathButton.Padding = new Padding(0, 0, 1, 0);
            recordingsPathButton.Click += new EventHandler(recordingsPathButton_Click);

            recordingsPathPanel.Size = new Size(250, 22);
            recordingsPathPanel.Controls.Add(recordingsPathTextBox);
            recordingsPathTextBox.Location = new Point(0, 0);
            recordingsPathPanel.Controls.Add(recordingsPathButton);
            recordingsPathButton.Location = new Point(205, 0);

            checkForUpdatesButton.BackColor = Color.FromArgb(76, 76, 76);
            checkForUpdatesButton.FlatStyle = FlatStyle.Popup;
            checkForUpdatesButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkForUpdatesButton.ForeColor = Color.FromArgb(194, 194, 194);
            checkForUpdatesButton.Size = new Size(150, 50);
            checkForUpdatesButton.TabStop = false;
            checkForUpdatesButton.Text = "Check for Updates";
            checkForUpdatesButton.UseVisualStyleBackColor = false;
            checkForUpdatesButton.Click += new EventHandler(checkForUpdatesButton_Click);

            addMultipleInstanceButton.BackColor = Color.FromArgb(76, 76, 76);
            addMultipleInstanceButton.FlatStyle = FlatStyle.Popup;
            addMultipleInstanceButton.Font = new Font("Roboto", 12.25F, FontStyle.Regular, GraphicsUnit.Point);
            addMultipleInstanceButton.ForeColor = Color.FromArgb(194, 194, 194);
            addMultipleInstanceButton.Size = new Size(200, 35);
            addMultipleInstanceButton.TabStop = false;
            addMultipleInstanceButton.Text = "Add multiple Instances";
            addMultipleInstanceButton.UseVisualStyleBackColor = false;
            addMultipleInstanceButton.Click += new EventHandler(addMultipleInstanceButton_Click);

            //Create ToolTips
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(instancePathLabel, "Select in which Instances worlds should get deleted in");
            toolTip.SetToolTip(deleteAllWorldsCheckBox, "Select if all worlds should be deleted, no matter the name of it");
            toolTip.SetToolTip(startWithLabel, "Select what the name of the world has to start with to be deleted");
            toolTip.SetToolTip(includeLabel, "Select what the name of the world has to include with to be deleted");
            toolTip.SetToolTip(endWithLabel, "Select what the name of the world has to end with to be deleted");
            toolTip.SetToolTip(deleteRecordingsCheckBox, "Select if MultiDelete should delete your Recordings. WARNING: THIS DELETES ALL FILES IN THE SELECTED DIRECTORY");
            toolTip.SetToolTip(recordingsPathTextBox, "Select in which folder your Recordings are stored in");
            toolTip.SetToolTip(deleteCrashReportsCheckBox, "Select if MultiDelete should delete your Crash-reports");
            toolTip.SetToolTip(deleteRawalleLogsCheckBox, "Select if MultiDelete should delete your Rawalle logs");
            toolTip.SetToolTip(deleteScreenshotsCheckBox, "Select if MultiDelete should delete your Screenshots");
            toolTip.SetToolTip(checkForUpdatesButton, "Check if a new Update is available");
            toolTip.SetToolTip(addMultipleInstanceButton, "Add multiple Instances at once via selecting multiple folders");

            //Resets settingsMenu
            settingsPanel.Controls.Clear();
            settingsPanel.Controls.Add(settingsHeading);
            settingsPanel.Controls.Add(instancePathLabel);
            settingsPanel.Controls.Add(addMultipleInstanceButton);
            settingsPanel.Controls.Add(deleteAllWorldsThatLabel);
            settingsPanel.Controls.Add(startWithLabel);
            settingsPanel.Controls.Add(includeLabel);
            settingsPanel.Controls.Add(endWithLabel);
            settingsPanel.Controls.Add(deleteAllWorldsCheckBox);
            settingsPanel.Controls.Add(deleteRecordingsCheckBox);
            settingsPanel.Controls.Add(recordingsPathPanel);
            settingsPanel.Controls.Add(deleteCrashReportsCheckBox);
            settingsPanel.Controls.Add(deleteRawalleLogsCheckBox);
            settingsPanel.Controls.Add(deleteScreenshotsCheckBox);
            settingsPanel.Controls.Add(checkForUpdatesButton);
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

            //Load options
            if(File.Exists(optionsFile))
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

                if(options.DeleteRecordings)
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

                deleteRawalleLogsCheckBox.Checked = options.DeleteRawalleLogs;
            } else
            {
                startWithEntrys[0].Text = "Random Speedrun";
                startWithEntrys[1].Text = "Set Speedrun";
            }
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
                instancePaths.Add(instancePathEntry.Text);
            }

            List<string> startWithList = new List<string>();
            foreach (TextBox startWithEntry in startWithEntrys)
            {
                startWithList.Add(startWithEntry.Text);
            }

            List<string> includeList = new List<string>();
            foreach (TextBox includeEntry in includesEntrys)
            {
                includeList.Add(includeEntry.Text);
            }

            List<string> endWithList = new List<string>();
            foreach (TextBox endWithEntry in endWithEntrys)
            {
                endWithList.Add(endWithEntry.Text);
            }

            //Saves settings to options file
            Options options = new Options
            {
                InstancePaths = instancePaths.ToArray(),
                DeleteAllWorlds = deleteAllWorldsCheckBox.Checked,
                StartWith = startWithList.ToArray(),
                Include = includeList.ToArray(),
                EndWith = endWithList.ToArray(),
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsPathTextBox.Text,
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteRawalleLogs = deleteRawalleLogsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked
            };

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(options, jsonSerializerOptions);
            File.WriteAllText(optionsFile, jsonString);
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
            //Adds TextBox to List
            if (type == "instancePath")
            {
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
                startWithEntrys.Add(textBox);
                settingsPanel.Controls.Add(textBox);
            }
            if (type == "includes")
            {
                includesEntrys.Add(textBox);
                settingsPanel.Controls.Add(textBox);
            }
            if (type == "endWith")
            {
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
            settingsPanel.Controls.SetChildIndex(settingsHeading, 0);
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

            settingsPanel.Controls.SetChildIndex(deleteRecordingsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(recordingsPathPanel, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteCrashReportsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteRawalleLogsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(deleteScreenshotsCheckBox, index);
            index++;

            settingsPanel.Controls.SetChildIndex(checkForUpdatesButton, index);
            index++;

            //Changes size of settingsHeading if Vertical scroolbar appears so the horizontal scroolbar doesnt appear
            if(settingsPanel.ClientSize.Width < 484)
            {
                settingsHeading.Size = new Size(461, 33);
            } else
            {
                settingsHeading.Size = new Size(478, 33);
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
            using (var fbd = new FolderBrowserDialog())
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
    }

    public class Options
    {
        public string[] InstancePaths { get; set; }
        public bool DeleteAllWorlds { get; set; }
        public string[] StartWith { get; set; }
        public string[] Include { get; set; }
        public string[] EndWith { get; set; }
        public bool DeleteRecordings { get; set; }
        public string RecordingsPath { get; set; }
        public bool DeleteCrashReports { get; set; }
        public bool DeleteRawalleLogs { get; set; }
        public bool DeleteScreenshots { get; set; }
    }
}