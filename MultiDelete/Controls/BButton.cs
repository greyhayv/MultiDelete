using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MultiDelete
{
    internal class BButton : Button
    {
        private int borderSize = 1;
        private int borderRadius = 20;
        private Color borderColor = Color.FromArgb(194, 194, 194);
        private bool topLineBorderStyle = false;
        private bool disableAnimations = false;
        private ToolTip toolTip = new ToolTip();

        public int BorderSize { get => borderSize; set { borderSize = value; Invalidate(); } }
        public int BorderRadius { get => borderRadius; set { borderRadius = value; Invalidate(); } }
        public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }
        public bool TopLineBorderStyle { get => topLineBorderStyle; set {topLineBorderStyle = value; Invalidate(); } }
        public bool DisableAnimations { get => disableAnimations; set {
            disableAnimations = value;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
            FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
        }}
        public Color BackgroundColor
        { get => BackColor; set { 
                BackColor = value; 
                if(disableAnimations) {
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
                    FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);

                    return;
                }
                if(BackColor.A == 255) {
                    try {
                        FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.R + 5, BackColor.G + 5, BackColor.B + 5);
                        FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.R + 8, BackColor.G + 8, BackColor.B + 8);
                    } catch {
                        FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
                        FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A, BackColor.R, BackColor.G, BackColor.B);
                    }
                } else {
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.A + 20 > 255 ? 255 : BackColor.A + 20, BackColor.R, BackColor.G, BackColor.B);
                    FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A + 40 > 255 ? 255 : BackColor.A + 40, BackColor.R, BackColor.G, BackColor.B);
                } 
        }}
        public Color TextColor { get => ForeColor; set => ForeColor = value; }
        public string ToolTip { get => toolTip.GetToolTip(this); set => toolTip.SetToolTip(this, value); }

        public BButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = Color.Transparent;
            TabStop = false; 
            ForeColor = Color.FromArgb(194, 194, 194);
            FlatAppearance.MouseOverBackColor = Color.FromArgb(BackColor.A + 20 > 255 ? 255 : BackColor.A + 20, BackColor.R, BackColor.G, BackColor.B);
            FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A + 40 > 255 ? 255 : BackColor.A + 40, BackColor.R, BackColor.G, BackColor.B);
            toolTip.ShowAlways = true;
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectBorder = new RectangleF(1, 1, Width - 0.8F, Height - 1);

            if(topLineBorderStyle) {
                Region = new Region(rectSurface);
                if(borderSize < 1) {
                    return;
                }

                using(Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawLine(penBorder, 0, 0, Width - 1, 0);
                }

                return;
            }

            if(borderRadius > 2)
            {
                using(GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using(GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1F))
                using(Pen penSurface = new Pen(Parent.BackColor, 2))
                using(Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if(borderSize < 1) {
                        return;
                    }

                    pevent.Graphics.DrawPath(penBorder, pathBorder);
                }

                return;
            }

            Region = new Region(rectSurface);
            if(borderSize >= 1)
            {
                using(Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if(DesignMode)
            {
                Invalidate();
            }
        }
    }
}
