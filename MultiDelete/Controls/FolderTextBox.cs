using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete
{
    public class FolderTextBox : Panel
    {
        BTextBox textBox = new BTextBox();
        BButton folderButton = new BButton();
        private string folderDialogDescription;
        private Color borderColor;

        public Color BorderColor { get => borderColor; set { borderColor = value; textBox.BorderColor = value; folderButton.BorderColor = value; } }
        public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox.BackColor = value; } }
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; textBox.ForeColor = value; } }

        public FolderTextBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            textBox.setPlaceholderText("Recordings Path");
            textBox.BorderSize = 1;
            textBox.BorderColor = Color.FromArgb(194, 194, 194);
            textBox.UnderlineStyle = false;
            textBox.BackColor = Color.FromArgb(40, 40, 40);
            textBox.ForeColor = Color.FromArgb(194, 194, 194);

            folderButton.Size = new Size(22, 22);
            folderButton.TabStop = false;
            folderButton.UseVisualStyleBackColor = false;
            folderButton.Image = Properties.Resources.foldericon;
            folderButton.Click += new EventHandler(folderButton_Click);
            folderButton.BorderSize = 1;
            folderButton.BorderRadius = 10;
            folderButton.BorderColor = Color.FromArgb(194, 194, 194);
            toolTip.SetToolTip(folderButton, "Browse");

            Size = new Size(250, 25);
            Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            Controls.Add(folderButton);
            folderButton.Location = new Point(205, 1);
        }

        public void setToolTip(string str)
        {
            textBox.setToolTip(str);
        }

        public void setText(string text)
        {
            textBox.Text = text;
        }

        public string getText()
        {
            return textBox.Text;
        }

        public void setEnabled(bool enabled)
        {
            textBox.setEnabled(enabled);
            folderButton.Enabled = enabled;
        }

        public void setFolderDialogDescription(string descrition)
        {
            folderDialogDescription = descrition;
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            Focus();
            var fbd = new FolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = folderDialogDescription;
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
            {
                textBox.Text = fbd.SelectedPath;
            }
        }
    }
}
