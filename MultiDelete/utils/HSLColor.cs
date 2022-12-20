using System;
using System.Drawing;

namespace MultiDelete 
{
    internal class HSLColor
    {
        private double h = 0;
        private double s = 0;
        private double l = 0;

        public double H { get => h; set => h = value; }
        public double S { get => s; set => s = value; }
        public double L { get => l; set => l = value; }

        public HSLColor() {

        }

        public HSLColor(double h, double s, double l) {
            this.h = h;
            this.s = s;
            this.l = l;
        }

        public override string ToString() {
            return "H: " + H.ToString() + " S: " + S.ToString() + " L: " + L.ToString();
        }

        public static HSLColor FromColor(Color color) {
            HSLColor hslColor = new HSLColor();

	        double r = (color.R / 255.0d);
	        double g = (color.G / 255.0d);
	        double b = (color.B / 255.0d);

	        double min = Math.Min(Math.Min(r, g), b);
	        double max = Math.Max(Math.Max(r, g), b);
	        double delta = max - min;

	        hslColor.L = (max + min) / 2;

	        if(delta == 0) {
		        hslColor.H = 0;
		        hslColor.S = 0.0d;
	        } else {
		        hslColor.S = (hslColor.L <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

		        double hue;

		        if(r == max) {
			        hue = ((g - b) / 6) / delta;
		        } else if (g == max) {
			        hue = (1.0d / 3) + ((b - r) / 6) / delta;
		        } else {
			    hue = (2.0d / 3) + ((r - g) / 6) / delta;
		        }

		        if(hue < 0) {
                    hue += 1;
                }
		        if (hue > 1) {
			        hue -= 1;
                }

		        hslColor.H = (int)(hue * 360);
	        }

	        return hslColor;
        }

        public Color ToColor() {
            byte r, g, b;
            if (S == 0)
            {
                r = (byte)Math.Round(L * 255d);
                g = (byte)Math.Round(L * 255d);
                b = (byte)Math.Round(L * 255d);
            }
            else
            {
                double t1, t2;
                double th = H / 360.0d;
                if (L < 0.5d)
                {
                    t2 = L * (1d + S);
                }
                else
                {
                    t2 = (L + S) - (L * S);
                }
                t1 = 2d * L - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)Math.Round(tr * 255d);
                g = (byte)Math.Round(tg * 255d);
                b = (byte)Math.Round(tb * 255d);
            }
            return Color.FromArgb(r, g, b);
        }

        private static double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }
    }
}