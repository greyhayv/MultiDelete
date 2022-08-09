using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace MultiDelete
{
    public partial class settingsMenu : Form
    {
        //Variables
        List<TextBox> instancePathEntrys = new List<TextBox>();
        List<Button> selectInstancePathButtons = new List<Button>();
        List<TextBox> startWithEntrys = new List<TextBox>();
        List<TextBox> includesEntrys = new List<TextBox>();
        List<TextBox> endWithEntrys = new List<TextBox>();
        List<Panel> instancePathPanel = new List<Panel>();
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
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

            recordingsPathPanel.Size = new Size(232, 22);
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

            //Resets settingsMenu
            settingsPanel.Controls.Clear();
            settingsPanel.Controls.Add(settingsHeading);
            settingsPanel.Controls.Add(instancePathLabel);
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
            instancePathEntrys = new List<TextBox>();
            createNewTextBox("instancePath", false);
            startWithEntrys = new List<TextBox>();
            createNewTextBox("startsWith", false);
            includesEntrys = new List<TextBox>();
            createNewTextBox("includes", false);
            endWithEntrys = new List<TextBox>();
            createNewTextBox("endWith", false);
            arrangeObjects();

            //Create Program Files if they dont already exist and set options
            if (!File.Exists(programsPath + @"\instancePaths.txt"))
            {
                File.CreateText(programsPath + @"\instancePaths.txt").Dispose();
            } else
            {
                string[] text = File.ReadAllLines(programsPath + @"\instancePaths.txt");
                for(int i = 0; i < text.Length; i++)
                {
                    instancePathEntrys[i].Text = text[i];
                }
            }

            if (!File.Exists(programsPath + @"\startWith.txt"))
            {
                File.CreateText(programsPath + @"\startWith.txt").Dispose();
                startWithEntrys[0].Text = "Random Speedrun";
                startWithEntrys[1].Text = "Set Speedrun";
            } else
            {
                string[] text = File.ReadAllLines(programsPath + @"\startWith.txt");
                for (int i = 0; i < text.Length; i++)
                {
                    startWithEntrys[i].Text = text[i];
                }
            }

            if (!File.Exists(programsPath + @"\include.txt"))
            {
                File.CreateText(programsPath + @"\include.txt").Dispose();
            } else
            {
                string[] text = File.ReadAllLines(programsPath + @"\include.txt");
                for (int i = 0; i < text.Length; i++)
                {
                    includesEntrys[i].Text = text[i];
                }
            }

            if (!File.Exists(programsPath + @"\endWith.txt"))
            {
                File.CreateText(programsPath + @"\endWith.txt").Dispose();
            } else
            {
                string[] text = File.ReadAllLines(programsPath + @"\endWith.txt");
                for (int i = 0; i < text.Length; i++)
                {
                    endWithEntrys[i].Text = text[i];
                }
            }

            if (!File.Exists(programsPath + @"\recordingsPath.txt"))
            {
                File.CreateText(programsPath + @"\recordingsPath.txt").Dispose();
            }
            else
            {
                recordingsPathTextBox.Text = File.ReadAllText(programsPath + @"\recordingsPath.txt");
            }

            if (!File.Exists(programsPath + @"\deleteAllWorlds.txt"))
            {
                File.CreateText(programsPath + @"\deleteAllWorlds.txt").Dispose();
            } else
            {
                string text = File.ReadAllText(programsPath + @"\deleteAllWorlds.txt");
                if(text == "true")
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

                } else
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
            }

            if (!File.Exists(programsPath + @"\deleteRecordings.txt"))
            {
                File.CreateText(programsPath + @"\deleteRecordings.txt").Dispose();
            }
            else
            {
                string text = File.ReadAllText(programsPath + @"\deleteRecordings.txt");
                if (text == "true")
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
            }

            if (!File.Exists(programsPath + @"\deleteCrashReports.txt"))
            {
                File.CreateText(programsPath + @"\deleteCrashReports.txt").Dispose();
            }
            else
            {
                string text = File.ReadAllText(programsPath + @"\deleteCrashReports.txt");
                if (text == "true")
                {
                    deleteCrashReportsCheckBox.Checked = true;
                }
                else
                {
                    deleteCrashReportsCheckBox.Checked = false;
                }
            }

            if (!File.Exists(programsPath + @"\deleteScreenshots.txt"))
            {
                File.CreateText(programsPath + @"\deleteScreenshots.txt").Dispose();
            }
            else
            {
                string text = File.ReadAllText(programsPath + @"\deleteScreenshots.txt");
                if (text == "true")
                {
                    deleteScreenshotsCheckBox.Checked = true;
                }
                else
                {
                    deleteScreenshotsCheckBox.Checked = false;
                }
            }

            if (!File.Exists(programsPath + @"\deleteRawalleLogs.txt"))
            {
                File.CreateText(programsPath + @"\deleteRawalleLogs.txt").Dispose();
            }
            else
            {
                string text = File.ReadAllText(programsPath + @"\deleteRawalleLogs.txt");
                if (text == "true")
                {
                    deleteRawalleLogsCheckBox.Checked = true;
                }
                else
                {
                    deleteRawalleLogsCheckBox.Checked = false;
                }
            }
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            MultiDelete.checkForUpdates(true);
        }

        private void settingsMenu_FormClosed(object sender, EventArgs e)
        {
            //Saves settings to Files
            string text = "";
            for(int i = 0; i < instancePathEntrys.Count; i++)
            {
                if (instancePathEntrys[i].Text != "")
                {
                    text =  text + instancePathEntrys[i].Text + Environment.NewLine;
                }
            }
            File.WriteAllText(programsPath + @"\instancePaths.txt", text);

            text = "";
            for (int i = 0; i < startWithEntrys.Count; i++)
            {
                if (startWithEntrys[i].Text != "")
                {
                    text = text + startWithEntrys[i].Text + Environment.NewLine;
                }
            }
            File.WriteAllText(programsPath + @"\startWith.txt", text);

            text = "";
            for (int i = 0; i < includesEntrys.Count; i++)
            {
                if (includesEntrys[i].Text != "")
                {
                    text = text + includesEntrys[i].Text + Environment.NewLine;
                }
            }
            File.WriteAllText(programsPath + @"\include.txt", text);

            text = "";
            for (int i = 0; i < endWithEntrys.Count; i++)
            {
                if (endWithEntrys[i].Text != "")
                {
                    text = text + endWithEntrys[i].Text + Environment.NewLine;
                }
            }
            File.WriteAllText(programsPath + @"\endWith.txt", text);

            File.WriteAllText(programsPath + @"\recordingsPath.txt", recordingsPathTextBox.Text);

            if (deleteAllWorldsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "true");
            } else
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "false");
            }

            if (deleteRecordingsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteRecordings.txt", "true");
            }
            else
            {
                File.WriteAllText(programsPath + @"\deleteRecordings.txt", "false");
            }

            if (deleteCrashReportsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteCrashReports.txt", "true");
            }
            else
            {
                File.WriteAllText(programsPath + @"\deleteCrashReports.txt", "false");
            }

            if (deleteRawalleLogsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteRawalleLogs.txt", "true");
            }
            else
            {
                File.WriteAllText(programsPath + @"\deleteRawalleLogs.txt", "false");
            }

            if (deleteScreenshotsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteScreenshots.txt", "true");
            }
            else
            {
                File.WriteAllText(programsPath + @"\deleteScreenshots.txt", "false");
            }
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
                createNewTextBox("instancePath");
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
                createNewTextBox("startsWith");
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
                createNewTextBox("includes");
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
                createNewTextBox("endWith");
            }
        }

        private void createNewTextBox(string type)
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
            if(type == "instancePath")
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

                //Creates and configures Panel with TextBox and Button
                Panel instancePathPanel = new Panel();
                instancePathPanel.Size = new Size(232, 22);
                instancePathPanel.Controls.Add(textBox);
                textBox.Location = new Point(0, 0);
                instancePathPanel.Controls.Add(selectInstancePathButton);
                selectInstancePathButton.Location = new Point(205, 0);
                this.instancePathPanel.Add(instancePathPanel);
                settingsPanel.Controls.Add(instancePathPanel);
            }
            if(type == "startsWith")
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
            arrangeObjects();
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

                //Creates and configures Panel with TextBox and Button
                Panel instancePathPanel = new Panel();
                instancePathPanel.Size = new Size(232, 22);
                instancePathPanel.Controls.Add(textBox);
                textBox.Location = new Point(0, 0);
                instancePathPanel.Controls.Add(selectInstancePathButton);
                selectInstancePathButton.Location = new Point(205, 0);
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

            foreach (Panel panel in instancePathPanel)
            {
                settingsPanel.Controls.SetChildIndex(panel, index);
                index++;
            }

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
                fbd.ShowDialog();
                if (fbd.SelectedPath != "")
                {
                    recordingsPathTextBox.Text = fbd.SelectedPath;
                }
            }
        }
    }
}