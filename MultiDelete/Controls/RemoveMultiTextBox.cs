using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class RemoveMultiTextBox : MultiTextBox
    {
        private List<BButton> removeButtons = new List<BButton>();
        private List<Panel> textBoxPanel = new List<Panel>();

        public override Color BorderColor { get => base.BorderColor; set { 
            base.BorderColor = value;
            foreach(BTextBox textBox in textBoxes) {
                textBox.BorderColor = value;
            }
            foreach(BButton button in removeButtons) {
                button.BorderColor = value;
            }
        } }

        public override Color BackColor { get => base.BackColor; set { 
            base.BackColor = value;
            foreach(BTextBox textBox in textBoxes) {
                textBox.BackColor = value;
            }
            foreach(BButton button in removeButtons) {
                button.BackColor = value;
            }
        } }

        public override void createNewTextBox()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            BTextBox textBox = new BTextBox();
            textBox.BorderSize = 1;
            textBox.BorderColor = BorderColor;
            textBox.UnderlineStyle = false;
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            textBox.textBox.TextChanged += new EventHandler(textChanged);
            textBoxes.Add(textBox);

            if(toolTipStr != null)
            {
                toolTip.SetToolTip(textBox, toolTipStr);
            }

            BButton removeButton = new BButton();
            removeButton.Size = new Size(22, 22);
            removeButton.TabStop = false;
            removeButton.UseVisualStyleBackColor = false;
            removeButton.Image = Properties.Resources.x;
            removeButton.Click += new EventHandler(removeButton_click);
            removeButton.BorderSize = 1;
            removeButton.BorderRadius = 10;
            removeButton.BorderColor = BorderColor;
            toolTip.SetToolTip(removeButton, "Remove");
            removeButtons.Add(removeButton);

            Panel panel = new Panel();
            panel.Size = new Size(275, 25);
            panel.Controls.Add(textBox);
            textBox.Location = new Point(0, 0);
            panel.Controls.Add(removeButton);
            removeButton.Location = new Point(205, 1);
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

        public override void enableTextBoxes()
        {
            base.enableTextBoxes();
            foreach(BButton button in removeButtons) {
                button.Enabled = true;
            }
        }

        public override void disableTextBoxes()
        {
            base.disableTextBoxes();
            foreach(BButton button in removeButtons) {
                button.Enabled = false;
            }
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
