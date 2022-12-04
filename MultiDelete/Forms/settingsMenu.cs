using System.Text.Json;
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Linq;

namespace MultiDelete
{
    public partial class settingsMenu : Form
    {
        private MultiDelete multiDelete;
        private string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";

        public settingsMenu(MultiDelete multiDelete) {
            this.multiDelete = multiDelete;

            InitializeComponent();
        }

        private void settingsMenu_Load(object sender, EventArgs e)  {
            loadSettings();
        }

        private void loadSettings() {
            if(File.Exists(optionsFile)) {
                try {
                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));

                    instancePathMTB.Texts = options.InstancePaths.ToList();
                    startWithMTB.Texts = options.StartWith.ToList();
                    includeMTB.Texts = options.Include.ToList();
                    endWithMTB.Texts = options.EndWith.ToList();
                    recordingsFTB.Text = options.RecordingsPath;
                    deleteAllWorldsCheckBox.Checked = options.DeleteAllWorlds;
                    deleteAllWorldsCheckBox_CheckedChanged(new object(), new EventArgs());
                    updateScreenNUD.Value = options.UpdateScreenEvery;
                    deleteRecordingsCheckBox.Checked = options.DeleteRecordings;
                    deleteRecordingsCheckBox_CheckedChanged(new object(), new EventArgs());
                    deleteCrashReportsCheckBox.Checked = options.DeleteCrashReports;
                    deleteScreenshotsCheckBox.Checked = options.DeleteScreenshots;
                    threadsTrackBar.TrackBarValue = options.ThreadCount;
                    threadsToUseLabel.Text = "Threads to use: " + threadsTrackBar.TrackBarValue;
                    keepLastWorldsNUD.Value = options.KeepLastWorlds;
                    moveToRecycleBinCheckBox.Checked = options.moveToRecycleBin;
                    MultiDelete.bgColor = ColorTranslator.FromHtml(options.bgColor);
                    MultiDelete.accentColor = ColorTranslator.FromHtml(options.accentColor);
                    MultiDelete.fontColor = ColorTranslator.FromHtml(options.fontColor);
                } catch {
                    if(MessageBox.Show("There was an error importing the settings! Load default settings?", "MultiDelete", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                        loadDefaultSettings();
                    } else {
                        FormClosed -= settingsMenu_FormClosed;
                        Close();
                    }
                }
            } else {
                loadDefaultSettings();
            }

            updateColors();
            multiDelete.updateColors();
        }

        private void loadDefaultSettings() {
            Options options = new Options {
                DeleteAllWorlds = false,
                DeleteCrashReports = false,
                DeleteRecordings = false,
                DeleteScreenshots = false,
                EndWith = new string[0],
                Include = new string[0],
                InstancePaths = new string[0],
                KeepLastWorlds = 0,
                RecordingsPath = "",
                StartWith = new string[] { "Random Speedrun", "Set Speedrun" },
                ThreadCount = 1,
                UpdateScreenEvery = 1,
                bgColor = "#0F0F0F",
                accentColor = "#414141",
                fontColor = "#C2C2C2",
                moveToRecycleBin = false
            };

            saveOptions(options);
            loadSettings();
        }

        private void checkForUpdatesButton_Click(object sender, EventArgs e) {
            Focus();
            MultiDelete.checkForUpdates(true);
        }

        private void settingsMenu_FormClosed(object sender, EventArgs e) {
            saveOptions();
        }

        private void saveOptions(Options options) {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(optionsFile, JsonSerializer.Serialize(options, jsonSerializerOptions));
        }

        private void saveOptions() {
            Options options = new Options {
                InstancePaths = instancePathMTB.Texts.ToArray(),
                DeleteAllWorlds = deleteAllWorldsCheckBox.Checked,
                StartWith = startWithMTB.Texts.ToArray(),
                Include = includeMTB.Texts.ToArray(),
                EndWith = endWithMTB.Texts.ToArray(),
                UpdateScreenEvery = (int)updateScreenNUD.Value,
                DeleteRecordings = deleteRecordingsCheckBox.Checked,
                RecordingsPath = recordingsFTB.Text,
                DeleteCrashReports = deleteCrashReportsCheckBox.Checked,
                DeleteScreenshots = deleteScreenshotsCheckBox.Checked,
                ThreadCount = threadsTrackBar.Value,
                KeepLastWorlds = Decimal.ToInt32(keepLastWorldsNUD.Value),
                bgColor = ColorTranslator.ToHtml(MultiDelete.bgColor),
                accentColor = ColorTranslator.ToHtml(MultiDelete.accentColor),
                fontColor = ColorTranslator.ToHtml(MultiDelete.fontColor),
                moveToRecycleBin = moveToRecycleBinCheckBox.Checked
            };

            saveOptions(options);
        }

        private void deleteAllWorldsCheckBox_CheckedChanged(object sender, EventArgs e) {
            if(deleteAllWorldsCheckBox.Checked) {
                startWithMTB.MTBEnabled = false;
                includeMTB.MTBEnabled = false;
                endWithMTB.MTBEnabled = false;

                //Grays out labels if DeleteAllWorlds checkBox is checked
                deleteAllWorldsThatLabel.ForeColor = Color.FromArgb(94, 94, 94);
                startWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
                includeLabel.ForeColor = Color.FromArgb(94, 94, 94);
                endWithLabel.ForeColor = Color.FromArgb(94, 94, 94);
            } else {
                startWithMTB.MTBEnabled = true;
                includeMTB.MTBEnabled = true;
                endWithMTB.MTBEnabled = true;

                deleteAllWorldsThatLabel.ForeColor = MultiDelete.fontColor;
                startWithLabel.ForeColor = MultiDelete.fontColor;
                includeLabel.ForeColor = MultiDelete.fontColor;
                endWithLabel.ForeColor = MultiDelete.fontColor;

                deleteAllWorldsThatLabel.ForeColor = MultiDelete.fontColor;
                startWithLabel.ForeColor = MultiDelete.fontColor;
                includeLabel.ForeColor = MultiDelete.fontColor;
                endWithLabel.ForeColor = MultiDelete.fontColor;
            }
        }

        private void deleteRecordingsCheckBox_CheckedChanged(object sender, EventArgs e) {
            recordingsFTB.FTBEnabled = deleteRecordingsCheckBox.Checked;
        }

        private void addMultipleInstanceButton_Click(object sender, EventArgs e) {
            Focus();

            CommonOpenFileDialog cofd = new CommonOpenFileDialog();
            cofd.Title = "Select multiple Instance-paths";
            cofd.IsFolderPicker = true;
            cofd.Multiselect = true;

            if(cofd.ShowDialog() == CommonFileDialogResult.Ok) {
                instancePathMTB.addTexts(cofd.FileNames.ToList());
            }
        }

        private void updateScreenComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            Focus();
        }

        private void threadsTrackBar_ValueChanged(object sender, EventArgs e) {
            threadsToUseLabel.Text = "Threads to use: " + ((TrackBar)sender).Value;
        }

        private void exportButton_click(object sender, EventArgs e) {
            Focus();
            exportSettings();
        }

        private void importButton_click(object sender, EventArgs e) {
            Focus();
            importSettings();
        }

        private void bgColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickBGColor();
        }

        private void pickBGColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.bgColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.bgColor = Color.FromArgb(255, cd.Color);
                updateColors();
                multiDelete.updateColors();
            }
        }

        private void accentColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickAccentColor();
        }

        private void pickAccentColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.accentColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.accentColor = Color.FromArgb(255, cd.Color);
                updateColors();
                multiDelete.updateColors();
            }
        }

        private void fontColorButton_Click(object sender, EventArgs e) {
            Focus();
            pickFontColor();
        }

        private void pickFontColor() {
            ColorDialog cd = new ColorDialog();
            cd.Color = MultiDelete.fontColor;
            cd.FullOpen = true;
            if(cd.ShowDialog() == DialogResult.OK) {
                MultiDelete.fontColor = Color.FromArgb(255, cd.Color);
                updateColors();
                multiDelete.updateColors();
            }
        }

        private void exportSettings() {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export settings";
            sfd.Filter = "JSON File (*.json)|*.json";
            if(sfd.ShowDialog() == DialogResult.OK) {
                saveOptions();
                File.Create(sfd.FileName).Close();
                File.WriteAllText(sfd.FileName, File.ReadAllText(optionsFile));
            }
        }

        private void importSettings() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Import settings";
            ofd.Filter = "JSON File (*.json) |*.json";
            if(ofd.ShowDialog() == DialogResult.OK) {
                File.WriteAllText(optionsFile, File.ReadAllText(ofd.FileName));
                loadSettings();
            }
        }

        private void settingsMenu_DragEnter(object sender, DragEventArgs e) {
            if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                
                if(!files.Any(file => file.EndsWith(".json", StringComparison.OrdinalIgnoreCase))) {
                    return;
                }

                e.Effect = DragDropEffects.Copy;
            }
        }

        private void settingsMenu_DragDrop(object sender, DragEventArgs e) {
            if(e.Data.GetDataPresent(DataFormats.FileDrop)) {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach(string file in files) {
                    if(!file.EndsWith(".json", StringComparison.OrdinalIgnoreCase)) {
                        continue;
                    }

                    if(MessageBox.Show("Import settings from " + file + "?", "MultiDelete", MessageBoxButtons.YesNo) != DialogResult.Yes) {
                        return;
                    }

                    Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(file));

                    saveOptions(options);
                    loadSettings();

                    return;
                }
            }
        }

        private void viewRepositoryLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("cmd", "/c start https://www.github.com/greyhayv/MultiDelete");
        }

        private void resetSettinsButton_Click(object sender, EventArgs e) {
            if(MessageBox.Show("Are you sure you want to reset your settings?", "MultiDelete", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                loadDefaultSettings();
            }
        }
    }

    public struct Options
    {
        public string[] InstancePaths { get; set; }
        public bool DeleteAllWorlds { get; set; }
        public string[] StartWith { get; set; }
        public string[] Include { get; set; }
        public string[] EndWith { get; set; }
        public int UpdateScreenEvery { get; set; }
        public bool DeleteRecordings { get; set; }
        public string RecordingsPath { get; set; }
        public bool DeleteCrashReports { get; set; }
        public bool DeleteScreenshots { get; set; }
        public int ThreadCount { get; set; }
        public int KeepLastWorlds { get; set; }
        public string bgColor { get; set; }
        public string accentColor { get; set; }
        public string fontColor { get; set; }
        public bool moveToRecycleBin { get; set; }
    }
}