using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete.Controls
{
    internal class RemoveFolderMultiTextBox : MultiTextBox
    {
        private List<Button> removeButtons = new List<Button>();
        private List<Button> folderButtons = new List<Button>();
        private List<Panel> textBoxPanel = new List<Panel>();

        public override void createNewTextBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            TextBox textBox = new TextBox();
            textBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            textBox.Size = new Size(200, 22);
            textBox.TabStop = false;
            textBox.TextChanged += new EventHandler(textChanged);
            textBoxes.Add(textBox);

            if (toolTipStr != null)
            {
                toolTip.SetToolTip(textBox, toolTipStr);
            }

            Button removeButton = new Button();
            removeButton.Size = new Size(22, 22);
            removeButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            removeButton.FlatStyle = FlatStyle.Popup;
            removeButton.TabStop = false;
            removeButton.UseVisualStyleBackColor = false;
            removeButton.Image = Properties.Resources.x;
            removeButton.Padding = new Padding(0, 0, 1, 1);
            removeButton.Click += new EventHandler(removeButton_click);
            toolTip.SetToolTip(removeButton, "Remove");
            removeButtons.Add(removeButton);

            Button folderButton = new Button();
            folderButton.Size = new Size(22, 22);
            folderButton.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            folderButton.FlatStyle = FlatStyle.Popup;
            folderButton.TabStop = false;
            folderButton.UseVisualStyleBackColor = false;
            folderButton.Image = Properties.Resources.foldericon;
            folderButton.Padding = new Padding(0, 0, 1, 0);
            folderButton.Click += new EventHandler(folderButton_click);
            toolTip.SetToolTip(folderButton, "Browse");
            folderButtons.Add(folderButton);

            Panel panel = new Panel();
            panel.Size = new Size(275, 22);
            panel.Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            panel.Controls.Add(folderButton);
            folderButton.Location = new Point(205, 0);
            panel.Controls.Add(removeButton);
            removeButton.Location = new Point(230, 0);
            textBoxPanel.Add(panel);

            Controls.Add(panel);

            setRemoveButtonVisibilaty();
        }

        public override void deleteTextBox(int i)
        {
            Controls.Remove(textBoxPanel[i]);
            textBoxes.RemoveAt(i);
            removeButtons.RemoveAt(i);
            folderButtons.RemoveAt(i);
            textBoxPanel.RemoveAt(i);

            setRemoveButtonVisibilaty();
        }

        private void setRemoveButtonVisibilaty()
        {
            for (int i = 0; i < removeButtons.Count; i++)
            {
                if (i < removeButtons.Count - 1)
                {
                    removeButtons[i].Visible = true;
                }
                else
                {
                    removeButtons[i].Visible = false;
                }
            }
        }

        private void removeButton_click(object sender, EventArgs e)
        {
            Focus();
            int index = 0;
            for (int i = 0; i < removeButtons.Count; i++)
            {
                if (removeButtons[i].Equals(sender))
                {
                    index = i;
                }
            }
            deleteTextBox(index);
        }

        private void folderButton_click(object sender, EventArgs e)
        {
            Focus();
            int index = 0;
            for(int i = 0; i < folderButtons.Count; i++)
            {
                if (folderButtons[i].Equals(sender))
                {
                    index = i;
                }
            }

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.UseDescriptionForTitle = true;
                fbd.Description = "Select Instance-path";
                fbd.ShowDialog();
                if (fbd.SelectedPath != "")
                {
                    textBoxes[index].Text = fbd.SelectedPath;
                }
            }
        }
    }
}
