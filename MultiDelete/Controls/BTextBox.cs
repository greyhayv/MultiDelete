using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MultiDelete
{
    internal class BTextBox : UserControl
    {
        public TextBox textBox = new TextBox();

        private Color borderColor;
        private int borderSize = 1;
        private bool underlineStyle = false;
        private int borderRadius = 12;
        private ToolTip toolTip = new ToolTip();

        public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }
        public int BorderSize { get => borderSize; set { borderSize = value; Invalidate(); } }
        public bool UnderlineStyle { get => underlineStyle; set { underlineStyle = value; Invalidate(); } }
        public int BorderRadius { get => borderRadius; set { if(value >= 0) { borderRadius = value; Invalidate(); } } }
        public string PlaceholderText { get => textBox.PlaceholderText; set => textBox.PlaceholderText = value; }
        public HorizontalAlignment TextAlign { get => textBox.TextAlign; set => textBox.TextAlign = value; }
        public override string Text { get => textBox.Text; set => textBox.Text = value; }
        public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox.BackColor = value; } }
        public override Color ForeColor { get => base.ForeColor; set { base.ForeColor = value; textBox.ForeColor = value; } }
        public bool TextBoxEnabled { get => textBox.Enabled; set => textBox.Enabled = value; }
        public string ToolTip { get => toolTip.GetToolTip(textBox); set => toolTip.SetToolTip(textBox, value);}

        public BTextBox() {
            TabStop = false;
            Size = new Size(200, 22);
            AutoScaleMode = AutoScaleMode.None;
            Padding = new Padding(6, 5, 6, 5);
            borderColor = Color.FromArgb(194, 194, 194);
            toolTip.ShowAlways = true;

            textBox.TabStop = false;
            textBox.Dock = DockStyle.Fill;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            textBox.ContextMenuStrip = new ContextMenuStrip();
            Controls.Add(textBox);

            SetStyle(ControlStyles.UserPaint, true);
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if(borderRadius > 1) {
                Rectangle rectBorderSmooth = ClientRectangle;
                Rectangle rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using(GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius))
                using(GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using(Pen penBorderSmooth = new Pen(Parent.BackColor, smoothSize))
                using(Pen penBorder = new Pen(borderColor, borderSize)) {
                    //Draw Border
                    Region = new Region(pathBorderSmooth);
                    if(borderRadius > 15) {
                        SetTextBoxRoundedRegion();
                    }
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                    if(underlineStyle) {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                    } else {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                    }
                }

                return;
            }

            //Draw Border
            using(Pen penBorder = new Pen(borderColor, borderSize)) {
                Region = new Region(ClientRectangle);
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                if(underlineStyle) {
                    graph.DrawLine(penBorder, 0, Height - 1, Width, Height - 1);
                } else {
                    graph.DrawRectangle(penBorder, 0, 0, Width - 0.5F, Height - 0.5F);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if(DesignMode) {
                UpdateControlHeight();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        private void UpdateControlHeight() {
            if(!textBox.Multiline) {
                int txtHeight = TextRenderer.MeasureText("Text", Font).Height + 1;
                textBox.Multiline = true;
                textBox.MinimumSize = new Size(0, txtHeight);
                textBox.Multiline = false;

                Height = textBox.Height + Padding.Top + Padding.Bottom;
            }
        }

        private void SetTextBoxRoundedRegion() {
            GraphicsPath pathTxt;
            pathTxt = GetFigurePath(textBox.ClientRectangle, borderSize * 2);
            textBox.Region = new Region(pathTxt);
        }
    }
}