using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace MultiDelete 
{
    public class Theme
    {
        private readonly string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";

        private Color bgColor;
        private Color accentColor;
        private Color fontColor;

        public Themes theme;
        public Color BgColor { get => bgColor; }
        public Color AccentColor { get => accentColor; }
        public Color FontColor { get => fontColor; }

        public Theme(Themes theme) {
            this.theme = theme;
            switch(theme) {
                case Themes.Dark:
                    bgColor = Color.FromArgb(15, 15, 15);
                    accentColor = Color.FromArgb(65, 65, 65);
                    fontColor = Color.FromArgb(194, 194, 194);
                    break;
                case Themes.Light:
                    bgColor = Color.FromArgb(250, 250, 250);
                    accentColor = Color.FromArgb(60, 60, 60);
                    fontColor = Color.FromArgb(0, 0, 0);
                    break;
                case Themes.Custom:
                    try {
                        Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
                        if(options.CustomBgColor == null) {
                            throw new Exception();
                        } else if(options.CustomAccentColor == null) {
                            throw new Exception();
                        } else if(options.CustomFontColor == null) {
                            throw new Exception();
                        }
                        bgColor = ColorTranslator.FromHtml(options.CustomBgColor);
                        accentColor = ColorTranslator.FromHtml(options.CustomAccentColor);
                        fontColor = ColorTranslator.FromHtml(options.CustomFontColor);
                    } catch {
                        bgColor = Color.FromArgb(15, 15, 15);
                        accentColor = Color.FromArgb(65, 65, 65);
                        fontColor = Color.FromArgb(194, 194, 194);
                    }
                    break;
            }
        }
    }

    public enum Themes {
        Dark,
        Light,
        Custom
    }
}