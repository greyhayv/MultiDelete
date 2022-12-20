using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDelete
{
    internal class TabFlowLayoutPanel : Panel
    {
        private List<string> categorys = new List<string>();
        private Dictionary<string, BButton> tabButtons = new Dictionary<string, BButton>();
        private Dictionary<string, FlowLayoutPanel> panels = new Dictionary<string, FlowLayoutPanel>();
        private Color borderColor;
        private Color buttonForeColor;

        public Color BorderColor { get => borderColor; set { 
            borderColor = value; 
            foreach(BButton button in tabButtons.Values) {
                button.BorderColor = value;
            }
        } }
        public Color ButtonForeColor { get => buttonForeColor; set { 
            buttonForeColor = value; 
            foreach(BButton button in tabButtons.Values) {
                button.ForeColor = value;
            }
        } }
        public override Color BackColor { get => base.BackColor; set { 
            base.BackColor = value; 
            foreach(Panel panel in panels.Values) {
                panel.BackColor = value;
            }
            foreach(BButton button in tabButtons.Values) {
                button.BackgroundColor = value;
            }
        } }

        public TabFlowLayoutPanel(List<string> categorys) {
            this.categorys = categorys;

            for(int i = 0; i < categorys.Count; i++) {
                BButton button = new BButton();
                button.Text = categorys[i];
                button.BorderRadius = 0;
                button.Click += new EventHandler(buttonClicked);
                button.BorderColor = Color.FromArgb(194, 194, 194);
                button.ForeColor = Color.FromArgb(194, 194, 194);
                button.Size = new Size(484 / categorys.Count + (i >= categorys.Count - (484 - ((484 / categorys.Count) * categorys.Count)) ? 1 : 0), 30);
                int locationX = 0;
                for(int i2 = 0; i2 < i; i2++) {
                    locationX += tabButtons[categorys[i2]].Size.Width;
                }
                button.Location = new Point(locationX, 0);
                tabButtons[categorys[i]] = button;
                Controls.Add(button);

                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.AutoScroll = true;
                panel.Dock = System.Windows.Forms.DockStyle.Bottom;
                panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
                panel.Location = new System.Drawing.Point(0, 50);
                panel.Size = new System.Drawing.Size(484, 721 - button.Size.Height);
                panel.TabStop = false;
                panel.WrapContents = false;
                panel.Padding = new Padding(0, 12, 0, 0);
                panels[categorys[i]] = panel;
                panel.BackColor = Color.FromArgb(40, 40, 40);
                Controls.Add(panel);
            }
            setTab(categorys[0]);
        }

        public void clearControls() {
            foreach(string category in categorys) {
                panels[category].Controls.Clear();
            }
        }

        public void addControl(string category, Control control) {
            panels[category].Controls.Add(control);
        }

        private void buttonClicked(object sender, EventArgs e) {
            Focus();

            setTab(((BButton)sender).Text);
        }

        private void setTab(string category) {
            foreach(string cat in categorys) {
                if(cat == category) {
                    panels[cat].Visible = true;
                    tabButtons[cat].TopLineBorderStyle = true;

                    continue;
                } 

                panels[cat].Visible = false;
                tabButtons[cat].TopLineBorderStyle = false;
            }
        }
    }
}
