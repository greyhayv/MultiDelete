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
        List<TextBox> savesPathEntrys = new List<TextBox>();
        List<TextBox> startWithEntrys = new List<TextBox>();
        List<TextBox> includesEntrys = new List<TextBox>();
        List<TextBox> endWithEntrys = new List<TextBox>();
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        string focusEntry = "";

        public settingsMenu()
        {
            InitializeComponent();
        }

        private void settingsMenu_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < savesPathEntrys.Count; i++)
            {
                this.Controls.Remove(savesPathEntrys[i]);
            }
            savesPathEntrys = new List<TextBox>();
            createNewTextBox("savesPath");
            for (int i = 0; i < startWithEntrys.Count; i++)
            {
                this.Controls.Remove(startWithEntrys[i]);
            }
            startWithEntrys = new List<TextBox>();
            createNewTextBox("startsWith");
            for (int i = 0; i < includesEntrys.Count; i++)
            {
                this.Controls.Remove(includesEntrys[i]);
            }
            includesEntrys = new List<TextBox>();
            createNewTextBox("includes");
            for (int i = 0; i < endWithEntrys.Count; i++)
            {
                this.Controls.Remove(endWithEntrys[i]);
            }
            endWithEntrys = new List<TextBox>();
            createNewTextBox("endWith");

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
                startWithEntrys[0].Text = "RandomSpeedrun";
                startWithEntrys[1].Text = "SetSpeedrun";
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
                    checkBox1.Checked = true;
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
                    checkBox1.Checked = false;
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
            if(checkBox1.Checked == true)
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "true");
            } else
            {
                File.WriteAllText(programsPath + @"\deleteAllWorlds.txt", "false");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textChanged(object sender, EventArgs e)
        {
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
            if(savesPathEntrys[savesPathEntrys.Count - 1].Text != "")
            {
                createNewTextBox("savesPath");
            }

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
            if (startWithEntrys[startWithEntrys.Count - 1].Text != "")
            {
                createNewTextBox("startsWith");
            }

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
            if (includesEntrys[includesEntrys.Count - 1].Text != "")
            {
                createNewTextBox("includes");
            }

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
            if (endWithEntrys[endWithEntrys.Count - 1].Text != "")
            {
                createNewTextBox("endWith");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void createNewTextBox(string type)
        {
            TextBox textBox = new TextBox();
            textBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            textBox.Size = new Size(200, 22);
            textBox.TextChanged += new System.EventHandler(textChanged);
            this.Controls.Add(textBox);
            if(type == "savesPath")
            {
                savesPathEntrys.Add(textBox);
            }
            if(type == "startsWith")
            {
                startWithEntrys.Add(textBox);
            }
            if (type == "includes")
            {
                includesEntrys.Add(textBox);
            }
            if (type == "endWith")
            {
                endWithEntrys.Add(textBox);
            }
            arrangeObjects();
        }

        private void deleteTextBox(int i, string type)
        {
            if(type == "savesPath")
            {
                this.Controls.Remove(savesPathEntrys[i]);
                savesPathEntrys.RemoveAt(i);
                focusEntry = "savesPath";
            }
            if (type == "startsWith")
            {
                this.Controls.Remove(startWithEntrys[i]);
                startWithEntrys.RemoveAt(i);
                focusEntry = "startsWith";
            }
            if (type == "includes")
            {
                this.Controls.Remove(includesEntrys[i]);
                includesEntrys.RemoveAt(i);
                focusEntry = "includes";
            }
            if (type == "endWith")
            {
                this.Controls.Remove(endWithEntrys[i]);
                endWithEntrys.RemoveAt(i);
                focusEntry = "endWith";
            }
            label1.Focus();
            arrangeObjects();
        }

        private void arrangeObjects()
        {
            for (int i = 0; i < savesPathEntrys.Count; i++)
            {
                savesPathEntrys[i].Location = new Point(12, (92 + 28 * i) + this.AutoScrollPosition.Y);
            }
            label3.Location = new Point(12, (132 + 28 * (savesPathEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            label4.Location = new Point(12, (155 + 28 * (savesPathEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            for (int i = 0; i < startWithEntrys.Count; i++)
            {
                startWithEntrys[i].Location = new Point(12, (181 + 28 * i + 28 * (savesPathEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            }
            label5.Location = new Point(12, (210 + 28 * (savesPathEntrys.Count - 1) + 28 * (startWithEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            for (int i = 0; i < includesEntrys.Count; i++)
            {
                includesEntrys[i].Location = new Point(12, (236 + 28 * i + 28 * (savesPathEntrys.Count - 1) + 28 * (startWithEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            }
            label6.Location = new Point(12, (265 + 28 * (savesPathEntrys.Count - 1) + 28 * (startWithEntrys.Count - 1) + 28 * (includesEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            for (int i = 0; i < endWithEntrys.Count; i++)
            {
                endWithEntrys[i].Location = new Point(12, (291 + 28 * i + 28 * (savesPathEntrys.Count - 1) + 28 * (startWithEntrys.Count - 1) + 28 * (includesEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            }
            checkBox1.Location = new Point(12, (320 + 28 * (savesPathEntrys.Count - 1) + 28 * (startWithEntrys.Count - 1) + 28 * (includesEntrys.Count - 1) + 28 * (endWithEntrys.Count - 1)) + this.AutoScrollPosition.Y);
            if(focusEntry == "savesPath")
            {
                this.ActiveControl = savesPathEntrys[savesPathEntrys.Count - 1];
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
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
}
