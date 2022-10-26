using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDelete.Controls
{
    internal class MultiTextBox : FlowLayoutPanel
    {
        public readonly List<TextBox> textBoxes = new List<TextBox>();

        public string toolTipStr;

        public MultiTextBox()
        {
            AutoScroll = false;
            FlowDirection = FlowDirection.TopDown;
            TabStop = false;
            AutoSize = true;

            createNewTextBox();
        }

        public List<string> getTexts()
        {
            List<string> entrys = new List<string>();
            for (int i = 0; i < textBoxes.Count - 1; i++)
            {
                entrys.Add(textBoxes[i].Text);
            }

            return entrys;
        }

        public virtual void createNewTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.BackColor = ColorTranslator.FromHtml("#4C4C4C");
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ForeColor = ColorTranslator.FromHtml("#C2C2C2");
            textBox.Size = new Size(200, 22);
            textBox.TabStop = false;
            textBox.TextChanged += new EventHandler(textChanged);
            if(toolTipStr != null)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.ShowAlways = true;
                toolTip.SetToolTip(textBox, toolTipStr);
            }

            textBoxes.Add(textBox);
            Controls.Add(textBox);
        }

        public void setToolTip(string str)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ShowAlways = true;

            foreach(TextBox textBox in textBoxes)
            {
                toolTip.SetToolTip(textBox, str);
            }

            toolTipStr = str;
        }

        public virtual void deleteTextBox(int i)
        {
            Controls.Remove(textBoxes[i]);
            textBoxes.RemoveAt(i);
        }

        public void textChanged(object sender, EventArgs e)
        {
            //Deletes TextBoxes if they are empty
            for (int i = 0; i < textBoxes.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(textBoxes[i].Text))
                {
                    continue;
                }

                if (i < textBoxes.Count - 2)
                {
                    deleteTextBox(i);
                    return;
                } else if (i == textBoxes.Count - 2 && textBoxes.Count > 1)
                {
                    deleteTextBox(textBoxes.Count - 1);
                }
            }

            //Creates a new TextBox if last TextBox has Text
            if (!string.IsNullOrWhiteSpace(textBoxes[textBoxes.Count - 1].Text))
            {
                createNewTextBox();
            }
        }

        public void setTexts(string[] texts)
        {
            for(int i = 0; i < texts.Length; i++)
            {
                textBoxes[i].Text = texts[i];
            }
        }

        public void addTexts(List<string> texts)
        {
            foreach(string text in texts)
            {
                textBoxes[textBoxes.Count - 1].Text = text;
            }
        }

        public void disableTextBoxes()
        {
            foreach(TextBox textBox in textBoxes)
            {
                textBox.Enabled = false;
            }
        }

        public void enableTextBoxes()
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Enabled = true;
            }
        }
    }
}
