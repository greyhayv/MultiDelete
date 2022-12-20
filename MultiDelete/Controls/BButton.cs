using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

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
        private bool cancelHoverAnimation = false;

        public int BorderSize { get => borderSize; set { borderSize = value; Invalidate(); } }
        public int BorderRadius { get => borderRadius; set { borderRadius = value; Invalidate(); } }
        public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }
        public bool TopLineBorderStyle { get => topLineBorderStyle; set {topLineBorderStyle = value; Invalidate(); } }
        public bool DisableAnimations { get => disableAnimations; set {
            disableAnimations = value;
            FlatAppearance.MouseDownBackColor = BackColor;
        }}
        public Color BackgroundColor
        { get => BackColor; set { 
                BackColor = value;
                if(disableAnimations) {
                    return;
                }
                if(BackColor.A == 255) {
                    HSLColor color = HSLColor.FromColor(MultiDelete.bgColor);
                    if((Math.Max(MultiDelete.bgColor.R, Math.Max(MultiDelete.bgColor.G, MultiDelete.bgColor.B)) / 255d) > 0.5) {
                        color.L = color.L < 0.1 ? 0 : color.L - 0.1;
                    } else {
                        color.L = color.L > 0.9 ? 1 : color.L + 0.1;
                    }
                    FlatAppearance.MouseDownBackColor = color.ToColor();
                } else {
                    FlatAppearance.MouseDownBackColor = Color.FromArgb(BackColor.A + 40 > 255 ? 255 : BackColor.A + 40, MultiDelete.bgColor.R, MultiDelete.bgColor.G, MultiDelete.bgColor.B);
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
            FlatAppearance.MouseOverBackColor = BackColor;
            FlatAppearance.MouseDownBackColor = BackColor;
            toolTip.ShowAlways = true;
            MouseEnter += new EventHandler(ButtonMouseEnter);
            MouseLeave += new EventHandler(ButtonMouseLeave);
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
            try {
                base.OnPaint(pevent);
            } catch {
                return;
            }
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

        private void ButtonMouseEnter(object sender, EventArgs e) {
            if(!disableAnimations) {
                Task.Run(() => updateMouseHover(sender));
            }
        }

        private void ButtonMouseLeave(object sender, EventArgs e) {
            cancelHoverAnimation = true;
        }

        private void updateMouseHover(object sender) {
            BButton button = (BButton)sender;
            double value = (Math.Max(MultiDelete.bgColor.R, Math.Max(MultiDelete.bgColor.G, MultiDelete.bgColor.B)) / 255d);
            while(!cancelHoverAnimation) {
                Point mouseLocation = new Point();
                this.Invoke((Action)(() => mouseLocation = button.PointToClient(Cursor.Position)));
                Bitmap bitmap = new Bitmap(button.Size.Width, button.Size.Height);
                for(int x = 0; x < bitmap.Width; x++) {
                    for(int y = 0; y < bitmap.Height; y++) {
                        double distance = Math.Sqrt(Math.Pow(mouseLocation.X - x, 2) + Math.Pow(mouseLocation.Y - y, 2));
                        int a = (int)(50 - (distance * 0.5));
                        Color color = new Color();
                        if(value > 0.5) {
                            color = Color.FromArgb((int)(a > 20 ? a : 20), 0, 0, 0);
                        } else {
                            color = Color.FromArgb((int)(a > 20 ? a : 20), 255, 255, 255);
                        }
                        try {
                            bitmap.SetPixel(x, y, color);
                        } catch {
                            cancelHoverAnimation = true;
                            break;
                        }
                    }
                }
                button.BackgroundImage = bitmap;
                Thread.Sleep(5);
            }
            cancelHoverAnimation = false;
            button.BackgroundImage = null;
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
