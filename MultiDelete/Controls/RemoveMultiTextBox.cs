using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete.Controls
{
    internal class RemoveMultiTextBox : MultiTextBox
    {
        private List<Button> removeButtons = new List<Button>();
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

            if(toolTipStr != null)
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

            Panel panel = new Panel();
            panel.Size = new Size(275, 22);
            panel.Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            panel.Controls.Add(removeButton);
            removeButton.Location = new Point(205, 0);
            textBoxPanel.Add(panel);
            
            Controls.Add(panel);

            setRemoveButtonVisibilaty();
        }

        public override void deleteTextBox(int i)
        {
            Controls.Remove(textBoxPanel[i]);
            textBoxes.RemoveAt(i);
            removeButtons.RemoveAt(i);
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
    }
}
