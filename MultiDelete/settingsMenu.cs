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
        List<TextBox> savesPathEntrys = new List<TextBox>();
        List<Button> selectSavesPathButtons = new List<Button>();
        List<TextBox> startWithEntrys = new List<TextBox>();
        List<TextBox> includesEntrys = new List<TextBox>();
        List<TextBox> endWithEntrys = new List<TextBox>();
        List<Panel> savesPathPanels = new List<Panel>();
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        string focusEntry = "";

        Label settingsHeading = new Label();
        Label savesPathLabel = new Label();
        Label deleteAllWorldsThatLabel = new Label();
        Label startWithLabel = new Label();
        Label includeLabel = new Label();
        Label endWithLabel = new Label();
        CheckBox deleteAllWorldsCheckBox = new CheckBox();

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

            savesPathLabel.AutoSize = false;
            savesPathLabel.Font = new Font("Roboto", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            savesPathLabel.ForeColor = Color.FromArgb(194, 194, 194);
            savesPathLabel.Size = new Size(118, 23);
            savesPathLabel.TabStop = false;
            savesPathLabel.Text = "Saves-Paths:";

            deleteAllWorldsThatLabel.AutoSize = false;
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

            //Resets settingsMenu
            settingsPanel.Controls.Clear();
            settingsPanel.Controls.Add(settingsHeading);
            settingsPanel.Controls.Add(savesPathLabel);
            settingsPanel.Controls.Add(deleteAllWorldsThatLabel);
            settingsPanel.Controls.Add(startWithLabel);
            settingsPanel.Controls.Add(includeLabel);
            settingsPanel.Controls.Add(endWithLabel);
            settingsPanel.Controls.Add(deleteAllWorldsCheckBox);
            savesPathPanels = new List<Panel>();
            selectSavesPathButtons = new List<Button>();
            savesPathEntrys = new List<TextBox>();
            createNewTextBox("savesPath", false);
            startWithEntrys = new List<TextBox>();
            createNewTextBox("startsWith", false);
            includesEntrys = new List<TextBox>();
            createNewTextBox("includes", false);
            endWithEntrys = new List<TextBox>();
            createNewTextBox("endWith", false);
            arrangeObjects();

            //Create Program Files if they dont already exist
            if (!Directory.Exists(programsPath))
            {
                Directory.CreateDirectory(programsPath);
            }
            if (!File.Exists(programsPath + @"\savesPaths.txt"))
            {
                File.CreateText(programsPath + @"\savesPaths.txt").Dispose();
            } else
            {
                string[] text = File.ReadAllLines(programsPath + @"\savesPaths.txt");
                for(int i = 0; i < text.Length; i++)
                {
                    savesPathEntrys[i].Text = text[i];
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
            if(!File.Exists(programsPath + @"\deleteAllWorlds.txt"))
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
                }
            }
        }

        private void settingsMenu_FormClosed(object sender, EventArgs e)
        {
            //Saves settings to Files
            string text = "";
            for(int i = 0; i < savesPathEntrys.Count; i++)
            {
                if (savesPathEntrys[i].Text != "")
                {
                    text =  text + savesPathEntrys[i].Text + Environment.NewLine;
                }
            }
            File.WriteAllText(programsPath + @"\savesPaths.txt", text);
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
            if(deleteAllWorldsCheckBox.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "true");
            } else
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "false");
            }
        }

        private void textChanged(object sender, EventArgs e)
        {
            //Deletes TextBoxes if they are empty
            for (int i = 0; i < savesPathEntrys.Count; i++)
            {
                if (savesPathEntrys[i].Text == "")
                {
                    if (i < savesPathEntrys.Count - 2)
                    {
                        deleteTextBox(i, "savesPath");
                        return;
                    }
                    else if (i == savesPathEntrys.Count - 2 && savesPathEntrys.Count > 1)
                    {
                        deleteTextBox(savesPathEntrys.Count - 1, "savesPath");
                    }
                }
            }
            //Creates a new TextBox if last TextBox has Text
            if(savesPathEntrys[savesPathEntrys.Count - 1].Text != "")
            {
                createNewTextBox("savesPath");
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
            if(type == "savesPath")
            {
                savesPathEntrys.Add(textBox);

                //Creates and configures new Button to select saves Path
                Button selectSavesPathButton = new Button();
                selectSavesPathButton.Size = new Size(22, 22);
                selectSavesPathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
                selectSavesPathButton.FlatStyle = FlatStyle.Popup;
                selectSavesPathButton.TabStop = false;
                selectSavesPathButton.UseVisualStyleBackColor = false;
                selectSavesPathButton.Image = Properties.Resources.foldericon;
                selectSavesPathButton.Padding = new Padding(0, 0, 1, 0);
                selectSavesPathButton.Click += new EventHandler(selectSavesPathButton_Click);
                selectSavesPathButtons.Add(selectSavesPathButton);

                //Creates and configures Panel with TextBox and Button
                Panel savePathPanel = new Panel();
                savePathPanel.Size = new Size(232, 22);
                savePathPanel.Controls.Add(textBox);
                textBox.Location = new Point(0, 0);
                savePathPanel.Controls.Add(selectSavesPathButton);
                selectSavesPathButton.Location = new Point(205, 0);
                savesPathPanels.Add(savePathPanel);
                settingsPanel.Controls.Add(savePathPanel);
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
            if (type == "savesPath")
            {
                savesPathEntrys.Add(textBox);

                //Creates and configures new Button to select saves Path
                Button selectSavesPathButton = new Button();
                selectSavesPathButton.Size = new Size(22, 22);
                selectSavesPathButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
                selectSavesPathButton.FlatStyle = FlatStyle.Popup;
                selectSavesPathButton.TabStop = false;
                selectSavesPathButton.UseVisualStyleBackColor = false;
                selectSavesPathButton.Image = Properties.Resources.foldericon;
                selectSavesPathButton.Padding = new Padding(0, 0, 1, 0);
                selectSavesPathButton.Click += new EventHandler(selectSavesPathButton_Click);
                selectSavesPathButtons.Add(selectSavesPathButton);

                //Creates and configures Panel with TextBox and Button
                Panel savePathPanel = new Panel();
                savePathPanel.Size = new Size(232, 22);
                savePathPanel.Controls.Add(textBox);
                textBox.Location = new Point(0, 0);
                savePathPanel.Controls.Add(selectSavesPathButton);
                selectSavesPathButton.Location = new Point(205, 0);
                savesPathPanels.Add(savePathPanel);
                settingsPanel.Controls.Add(savePathPanel);
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
            if(type == "savesPath")
            {
                settingsPanel.Controls.Remove(savesPathPanels[i]);
                savesPathEntrys.RemoveAt(i);

                selectSavesPathButtons.RemoveAt(i);
                savesPathPanels.RemoveAt(i);
                focusEntry = "savesPath";
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

            settingsPanel.Controls.SetChildIndex(savesPathLabel, index);
            index++;

            foreach (Panel savesPathPanel in savesPathPanels)
            {
                settingsPanel.Controls.SetChildIndex(savesPathPanel, index);
                index++;
            }

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
            
            settingsPanel.Controls.SetChildIndex(deleteAllWorldsCheckBox, index);

            //Changes size of settingsHeading if Vertical scroolbar appears so the horizontal scroolbar doesnt appear
            if(settingsPanel.ClientSize.Width < 484)
            {
                settingsHeading.Size = new Size(461, 33);
            } else
            {
                settingsHeading.Size = new Size(478, 33);
            }

            //Focuses last TextBox if TextBox was deleted
            if (focusEntry == "savesPath")
            {
                this.ActiveControl = savesPathEntrys[savesPathEntrys.Count - 1];
                savesPathEntrys[savesPathEntrys.Count - 1].Focus();
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
            }
        }

        private void selectSavesPathButton_Click(object sender, EventArgs e)
        {
            settingsPanel.Focus();
            int index = 0;
            for(int i = 0; i < selectSavesPathButtons.Count; i++)
            {
                if (selectSavesPathButtons[i].Equals(sender))
                {
                    index = i;
                }
            }
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.ShowDialog();
                if(fbd.SelectedPath != "")
                {
                    savesPathEntrys[index].Text = fbd.SelectedPath;
                }
            }
        }
    }
}