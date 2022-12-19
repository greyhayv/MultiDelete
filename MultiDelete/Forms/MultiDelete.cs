using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MultiDelete
{
    public partial class MultiDelete : Form
    {
        public static string version = "v1.3";
        public static Color bgColor;
        public static Color accentColor;
        public static Color fontColor;

        private readonly string programPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        private readonly string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";
        private bool cancelDeletion = false;
        private bool closeAfterDeletion = false;
        private bool checkUpdates = true;

        public MultiDelete() {
            Theme theme;
            try {
                Options options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
                theme = new Theme(options.Theme);
            } catch {
                theme = new Theme(Themes.Dark);
            }
            bgColor = theme.BgColor;
            accentColor = theme.AccentColor;
            fontColor = theme.FontColor;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if(!Directory.Exists(programPath)) {
                Directory.CreateDirectory(programPath);
            }

            //Check launch arguments
            string[] launchArgs = Environment.GetCommandLineArgs();
            if(launchArgs.Length > 1) {
                foreach(string argument in launchArgs) {
                    switch(argument) {
                        case "-delWorlds":
                            Task.Run(() => deleteWorlds());
                            continue;
                        case "-closeAfterDeletion":
                            closeAfterDeletion = true;
                            continue;
                        case "-dontCheckUpdates":
                            checkUpdates = false;
                            continue;
                    }
                }
            }

            if(checkUpdates) {
                Task.Run(() => checkForUpdates(false));
            }
        }

        public async void checkForUpdates(bool openDialogIfNoNewVersion) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MultiDelete", version));
            byte[] byteArray = new UTF8Encoding().GetBytes(ClientProperties.CLIENT_ID + ":" + ClientProperties.CLIENT_SECRET);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpResponseMessage response = new HttpResponseMessage();
            try {
                response = await client.GetAsync("repos/greyhayv/MultiDelete/releases/latest");
            } catch {
                if(openDialogIfNoNewVersion) {
                    MessageBox.Show("No Internet");
                }
                return;
            }

            if(response.IsSuccessStatusCode) {
                ReleaseInfo latestRelease = JsonSerializer.Deserialize<ReleaseInfo>(response.Content.ReadAsStringAsync().Result);

                if(latestRelease.tag_name != version) {
                    updateScreen updateScreen = new updateScreen(latestRelease);
                    this.Invoke((Action)(() => updateScreen.ShowDialog()));
                } else if(openDialogIfNoNewVersion) {
                    updateScreen updateScreen = new updateScreen(latestRelease);
                    this.Invoke((Action)(() => updateScreen.ShowDialog()));
                }
            } else {
                Console.WriteLine(response.StatusCode);
                if(openDialogIfNoNewVersion) {
                    MessageBox.Show("Api rate limit exceeded.", "MultiDelete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteWorldsButton_Click(object sender, EventArgs e) {
            Focus();
            Task.Run(() => deleteWorlds());
        }

        private void settingsButton_Click(object sender, EventArgs e) {
            Focus();
            settingsMenu settingsMenu = new settingsMenu(this);
            settingsMenu.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e) {
            setLayout(MenuLayout.MainMenu);
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            cancelDeletion = true;
            setLayout(MenuLayout.MainMenu);
        }

        private void deleteWorlds() {
            setLayout(MenuLayout.InfoLabel);

            cancelDeletion = false;
            List<string> checkedPaths = new List<string>();
            long totalFilesSize = 0;
            List<string> worldsToDelete = new List<string>();
            changeText(infoLabel, "Searching Worlds (0)");

            Options options = new Options();
            if(File.Exists(optionsFile)) {
                try {
                    options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
                } catch {
                    MessageBox.Show("There was an error reading the options file! Please open the settings menu and click load default settings!", "MultiDelete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    setLayout(MenuLayout.MainMenu);
                    return;
                }
            }

            //Checks if InstancePaths are configures
            if(options.InstancePaths == null || options.InstancePaths.Length == 0) {
                changeText(infoLabel, "Please add an Instance-Path in the Settingmenu!");
                setLayout(MenuLayout.Error);
                return;
            }

            //Checks if Worlds to delete are configured
            if(options.StartWith.Length == 0 && options.Include.Length == 0 && options.EndWith.Length == 0) {
                changeText(infoLabel, "Please select what worlds to delete in the Settingsmenu!");
                changeFont(infoLabel, new Font("Roboto", 13));
                setLayout(MenuLayout.Error);
                return;
            }

            if(options.UpdateScreenEvery == 0) {
                options.UpdateScreenEvery = 1;
            }

            //Searching worlds to delete
            foreach(string path in savesPathsFromInstancePaths(options.InstancePaths)) {
                if(!Directory.Exists(path)) {
                    changeText(infoLabel, "The Saves-Path '" + path + "' doesnt exist!");
                    changeFont(infoLabel, new Font("Roboto", 13), true);
                    setLayout(MenuLayout.Error);

                    //Make Font smaller if it text is to long to be displayed
                    while(infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width) {
                        changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                    }

                    return;
                }

                if(checkedPaths.Contains(path)) {
                    continue;
                }

                List<string> instanceWorlds = new List<string>();
                List<DirectoryInfo> worldDIs = new List<DirectoryInfo>();
                foreach(string world in Directory.GetDirectories(path)) {
                    if(cancelDeletion) {
                        return;
                    }

                    if(options.DeleteAllWorlds) {
                        worldsToDelete.Add(world);
                        instanceWorlds.Add(world);

                        updateWorldsSearchingScreen(options.UpdateScreenEvery, ref worldsToDelete);

                        continue;
                    }

                    string worldName = world.Substring(path.Length + 1);
                    if(options.StartWith.Length > 0) {
                        foreach(string str in options.StartWith) {
                            if(!worldName.StartsWith(str)) {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldsSearchingScreen(options.UpdateScreenEvery, ref worldsToDelete);
                        }
                    }

                    if(options.Include.Length > 0) {
                        foreach(string str in options.Include) {
                            if(!worldName.Contains(str)) {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldsSearchingScreen(options.UpdateScreenEvery, ref worldsToDelete);
                        }
                    }

                    if(options.EndWith.Length > 0) {
                        foreach(string str in options.EndWith) {
                            if(!worldName.EndsWith(str)) {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldsSearchingScreen(options.UpdateScreenEvery, ref worldsToDelete);
                        }
                    }

                    worldDIs.Add(new DirectoryInfo(world));
                }

                //Remove last x worlds from list
                instanceWorlds = worldDIs.OrderByDescending(f => f.LastWriteTime).Select(f => f.FullName).ToList();

                for(int i = 0; i < options.KeepLastWorlds; i++) {
                    if(instanceWorlds.Count <= i) {
                        break;
                    }
                    worldsToDelete.Remove(instanceWorlds[i]);
                }
                checkedPaths.Add(path);
            }
            
            int deletedWorldCount = 0;

            changeText(infoLabel, "Deleting Worlds (0/" + worldsToDelete.Count.ToString() + ")");
            changeMaximum(progressBar, worldsToDelete.Count);
            changeValue(progressBar, 0);
            setLayout(MenuLayout.ProgressBar);

            CountdownEvent worldDeletionCE = new CountdownEvent(options.ThreadCount);

            //Divide worlds under threads
            for(int i = 0; i < options.ThreadCount; i++) {
                int i2 = i;
                ThreadPool.QueueUserWorkItem(state => delWorldsThread(i2 * (worldsToDelete.Count / options.ThreadCount), (i2 + 1) * (worldsToDelete.Count / options.ThreadCount), ref worldsToDelete, ref deletedWorldCount, ref totalFilesSize, ref worldDeletionCE), worldDeletionCE);
            }
            worldDeletionCE.Wait();

            //Delte leftover worlds
            foreach(string world in worldsToDelete) {
                if(cancelDeletion) {
                    return;
                }

                if(!Directory.Exists(world)) {
                    continue;
                }

                try {
                    delWorld(world, ref totalFilesSize, ref deletedWorldCount, options.UpdateScreenEvery, ref worldsToDelete, ref options);
                } catch {
                    continue;
                }
            }


            if(options.DeleteRecordings) {
                deleteRecordings(ref totalFilesSize, options);
            }
            if(options.DeleteCrashReports) {
                deleteCrashReports(ref totalFilesSize);
            }
            if(options.DeleteScreenshots) {
                deleteScreenshots(ref totalFilesSize);
            }

            showResults(ref worldsToDelete, totalFilesSize);
        }

        private void delWorldsThread(int startIndex, int endIndex, ref List<string> worldsToDelete, ref int totalDeletedWorlds, ref long deletedFileSize, ref CountdownEvent countdownEvent) {
            Options options = new Options();
            if(File.Exists(optionsFile)) {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            long deletedWorldsSize = 0;
            for(int i = startIndex; i < endIndex; i++) {
                if(cancelDeletion) {
                    countdownEvent.Signal();
                    return;
                }

                try {
                    delWorld(worldsToDelete[i], ref deletedWorldsSize, ref totalDeletedWorlds, options.UpdateScreenEvery, ref worldsToDelete, ref options);
                } catch {
                    continue;
                }
            }

            deletedFileSize += deletedWorldsSize;
            countdownEvent.Signal();
        }

        private void deleteRecordings(ref long totalFilesSize, Options options) {
            setLayout(MenuLayout.InfoLabel);

            if(String.IsNullOrWhiteSpace(options.RecordingsPath)) {
                return;
            }

            if(!Directory.Exists(options.RecordingsPath)) {
                MessageBox.Show("The Recordings-Path does not exist!", "MultiDelete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> recordingsToDelete = new List<string>();
            List<FileInfo> recordingFIs = new List<FileInfo>();
            //Search recordings to delete
            foreach(string file in Directory.GetFiles(options.RecordingsPath)) {
                if(cancelDeletion) {
                    return;
                }

                if(!file.EndsWith(".mp4") && !file.EndsWith(".webm") && !file.EndsWith(".mkv") && !file.EndsWith(".flv") && !file.EndsWith(".vob") && !file.EndsWith(".ogv") && !file.EndsWith(".ogg") && !file.EndsWith(".drc") && !file.EndsWith(".gif") && !file.EndsWith(".gifv") && !file.EndsWith(".mng") && !file.EndsWith(".avi") && !file.EndsWith(".MTS") && !file.EndsWith(".M2TS") && !file.EndsWith("TS") && !file.EndsWith(".mov") && !file.EndsWith(".qt") && !file.EndsWith(".wmv") && !file.EndsWith(".yuv") && !file.EndsWith(".rm") && !file.EndsWith(".rmvb") && !file.EndsWith(".viv") && !file.EndsWith(".asf") && !file.EndsWith(".amv") && !file.EndsWith(".m4p") && !file.EndsWith(".m4v") && !file.EndsWith(".mpg") && !file.EndsWith(".mp2") && !file.EndsWith(".mpeg") && !file.EndsWith(".mpe") && !file.EndsWith(".mpv") && !file.EndsWith(".mpg") && !file.EndsWith(".m2v") && !file.EndsWith(".m4v") && !file.EndsWith(".svi") && !file.EndsWith(".3gp") && !file.EndsWith(".3g2") && !file.EndsWith(".mxf") && !file.EndsWith(".roq") && !file.EndsWith(".nsv") && !file.EndsWith(".flv") && !file.EndsWith(".f4v") && !file.EndsWith(".f4p") && !file.EndsWith(".f4a") && !file.EndsWith(".f4b")) {
                    continue;
                }

                recordingsToDelete.Add(file);
                changeText(infoLabel, "Searching Recordings (" + recordingsToDelete.Count.ToString() + ")");
                refreshUI();

                recordingFIs.Add(new FileInfo(file));
            }

            //Remove last x recordings from list
            recordingsToDelete = recordingFIs.OrderByDescending(f => f.LastWriteTime).Select(f => f.FullName).ToList();
            recordingsToDelete.RemoveRange(0, options.KeepLastRecordings > recordingsToDelete.Count ? recordingsToDelete.Count : options.KeepLastRecordings);
            
            int deletedRecordings = 0;

            changeText(infoLabel, "Deleting Recordings (0/" + recordingsToDelete.Count.ToString() + ")");
            changeMaximum(progressBar, recordingsToDelete.Count);
            changeValue(progressBar, 0);
            setLayout(MenuLayout.ProgressBar);

            //Delte recordings
            foreach(string file in recordingsToDelete) {
                if(cancelDeletion) {
                    return;
                }

                if(!File.Exists(file)) {
                    continue;
                }

                try {
                    delFile(file, "Recordings", ref totalFilesSize, ref deletedRecordings, ref options);
                } catch {
                    continue;
                }
            }
        }

        private void deleteCrashReports(ref long totalFilesSize) {
            setLayout(MenuLayout.InfoLabel);
            Options options = new Options();
            if(File.Exists(optionsFile)) {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            int deletedCrashReports = 0;
            changeText(infoLabel, "Deleting Crash-reports (0)");

            foreach(string savesPath in savesPathsFromInstancePaths(options.InstancePaths)) {
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string crashReportsPath = minecraftPath + @"\crash-reports";
                if(!Directory.Exists(crashReportsPath)) {
                    continue;
                }

                foreach(string crashReport in Directory.GetFiles(crashReportsPath)) {
                    delFile(crashReport, "Crash-reports", ref totalFilesSize, ref deletedCrashReports, ref options);
                }

                foreach(string file in Directory.GetFiles(minecraftPath)) {
                    if(!file.Substring(minecraftPath.Length + 1).StartsWith("hs_err_pid")) {
                        continue;
                    }

                    delFile(file, "Crash-reports", ref totalFilesSize, ref deletedCrashReports, ref options);
                }
            }
        }

        private void deleteScreenshots(ref long totalFilesSize) {
            setLayout(MenuLayout.InfoLabel);
            Options options = new Options();
            if(File.Exists(optionsFile)) {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            int deletedScreenshots = 0;
            changeLocation(infoLabel, new Point(-8, 41));
            changeText(infoLabel, "Deleting Screenshots (0)");

            foreach(string savesPath in savesPathsFromInstancePaths(options.InstancePaths)) {
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string screenshotsPath = minecraftPath + @"\screenshots";

                if(!Directory.Exists(screenshotsPath)) {
                    continue;
                }

                foreach(string screenshot in Directory.GetFiles(screenshotsPath)) {
                    delFile(screenshot, "Screenshots", ref totalFilesSize, ref deletedScreenshots, ref options);
                }
            }
        }

        private void showResults(ref List<string> worldsToDelete, long totalFilesSize) {
            //Convert file sizes
            string fileSize;
            if(totalFilesSize < 1024) {
                fileSize = totalFilesSize.ToString() + " Bytes";
            } else if(totalFilesSize < Math.Pow(1024, 2)) {
                fileSize = Math.Round(((double)totalFilesSize / 1024d), 2) + "kB";
            } else if(totalFilesSize < Math.Pow(1024, 3)) {
                fileSize = Math.Round(((double)totalFilesSize / (double)Math.Pow(1024d, 2)), 2) + "MB";
            } else {
                fileSize = Math.Round(((double)totalFilesSize / (double)Math.Pow(1024d, 3)), 2) + "GB";
            }

            changeVisibilaty(progressBar, false);
            if(worldsToDelete.Count == 0) {
                changeText(infoLabel, "No Worlds got found! (" + fileSize + ")");
            } else if(worldsToDelete.Count == 1) {
                changeText(infoLabel, "Deleted 1 World! (" + fileSize + ")");
            } else {
                changeText(infoLabel, "Deleted " + worldsToDelete.Count.ToString() + " Worlds! (" + fileSize + ")");
            }
            setLayout(MenuLayout.Results);

            if(closeAfterDeletion) {
                Application.Exit();
            }
        }

        private void updateWorldsSearchingScreen(int updateScreen, ref List<string> worldsToDelete) {
            if(worldsToDelete.Count % updateScreen == 0) {
                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                refreshUI();
            }
        }

        private void updateWorldDeletionScreen(int updateScreen, int delWorldCount, ref List<string> worldsToDelete) {
            if(delWorldCount % updateScreen == 0) {
                changeText(infoLabel, "Deleting Worlds (" + delWorldCount.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, delWorldCount);
                refreshUI();
            }
        }

        private List<string> savesPathsFromInstancePaths(string[] instancePaths) {
            List<string> savesPaths = new List<string>();
            foreach(string instancePath in instancePaths) {
                if(instancePath.EndsWith(@"\saves")) {
                    savesPaths.Add(instancePath);
                } else if(instancePath.EndsWith(@"\saves\")) {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                } else if(instancePath.EndsWith(@"\.minecraft")) {
                    savesPaths.Add(instancePath + @"\saves");
                } else if(instancePath.EndsWith(@"\.minecraft\")) {
                    savesPaths.Add(instancePath + "saves");
                } else if(instancePath.EndsWith(@"\")) {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                } else {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            return savesPaths;
        }

        private void changeText(Label label, String text) {
            label.BeginInvoke((Action)(() => label.Text = text));
        }

        private void changeText(Button button, String text) {
            button.BeginInvoke((Action)(() => button.Text = text));
        }

        private void changeVisibilaty(Button button, bool visible) {
            button.BeginInvoke((Action)(() => button.Visible = visible));
        }

        private void changeVisibilaty(Label label, bool visible) {
            label.BeginInvoke((Action)(() => label.Visible = visible));
        }

        private void changeVisibilaty(ProgressBar progressBar, bool visible) {
            progressBar.BeginInvoke((Action)(() => progressBar.Visible = visible));
        }

        private void changeFont(Label label, Font font) {
            label.BeginInvoke((Action) (() => label.Font = font));
        }

        private void changeFont(Label label, Font font, bool Invoke){
            if(Invoke) {
                label.Invoke((Action)(() => label.Font = font));
            } else {
                label.BeginInvoke((Action)(() => label.Font = font));
            }
        }

        private void changeLocation(Label label, Point location) {
            label.BeginInvoke((Action)(() => label.Location = location));
        }

        private void changeLocation(Button button, Point location) {
            button.BeginInvoke((Action)(() => button.Location = location));
        }

        private void changeLocation(ProgressBar progressBar, Point location) {
            progressBar.BeginInvoke((Action)(() => progressBar.Location = location));
        }

        private void changeMaximum(ProgressBar progressBar, int max) {
            progressBar.Invoke((Action)(() => progressBar.Maximum = max));
        }

        private void changeValue(ProgressBar progressBar, int value) {
            progressBar.BeginInvoke((Action)(() => progressBar.Value = value));
        }

        private void refreshUI() {
            Invoke((Action)(() => Refresh()));
        }

        private long calcDirSize(DirectoryInfo di) {
            long fileSize = 0;
            FileInfo[] fis = di.GetFiles();
            foreach(FileInfo fi in fis) {
                fileSize += fi.Length;
            }
            foreach(DirectoryInfo di2 in di.GetDirectories()) {
                fileSize += calcDirSize(di2);
            }

            return fileSize;
        }

        private void setLayout(MenuLayout layout) {
            //Reset layout
            changeVisibilaty(cancelButton, false);
            changeVisibilaty(deleteWorldsButton, false);
            changeVisibilaty(infoLabel, false);
            changeVisibilaty(okButton, false);
            changeVisibilaty(progressBar, false);
            changeVisibilaty(settingsButton, false);
            changeLocation(cancelButton, new Point(198, 72));
            changeLocation(deleteWorldsButton, new Point(154, 30));
            changeLocation(infoLabel, new Point(-8, 41));
            changeLocation(okButton, new Point(193, 57));
            changeLocation(progressBar, new Point(17, 44));
            changeLocation(settingsButton, new Point(447, 7));

            switch(layout) {
                case MenuLayout.MainMenu:
                    changeVisibilaty(deleteWorldsButton, true);
                    changeVisibilaty(settingsButton, true);
                    return;
                case MenuLayout.InfoLabel:
                    changeVisibilaty(infoLabel, true);
                    changeVisibilaty(cancelButton, true);
                    changeLocation(infoLabel, new Point(-8, 41));
                    changeFont(infoLabel, new Font("Roboto", 16));
                    return;
                case MenuLayout.Error:
                    changeVisibilaty(okButton, true);
                    changeVisibilaty(infoLabel, true);
                    changeLocation(infoLabel, new Point(-8, 23));
                    changeText(okButton, "OK");
                    return;
                case MenuLayout.ProgressBar:
                    changeVisibilaty(progressBar, true);
                    changeVisibilaty(infoLabel, true);
                    changeVisibilaty(cancelButton, true);
                    changeLocation(infoLabel, new Point(-8, 10));
                    return;
                case MenuLayout.Results:
                    changeText(okButton, "Done");
                    changeLocation(infoLabel, new Point(-8, 23));
                    changeVisibilaty(okButton, true);
                    changeVisibilaty(infoLabel, true);
                    return;
            }
        }

        public static Image recolorImage(Image image, Color color) {
            Bitmap bitmap = (Bitmap)image;
            for(int x = 0; x < bitmap.Width; x++) {
                for(int y = 0; y < bitmap.Height; y++) {
                    bitmap.SetPixel(x, y, Color.FromArgb(bitmap.GetPixel(x, y).A, color));
                }
            }

            return bitmap;
        }

        private void delWorld(string dir, ref long totalFilesSize, ref int deletedWorldCount, int updateScreen, ref List<string> worldsToDelete, ref Options options) {
            DirectoryInfo di = new DirectoryInfo(dir);
            totalFilesSize += calcDirSize(di);

            if(options.moveToRecycleBin) {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(dir, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            } else {
                Directory.Delete(dir, true);
            }
            
            deletedWorldCount += 1;

            updateWorldDeletionScreen(updateScreen, deletedWorldCount, ref worldsToDelete);
        }

        private void delFile(string file, string fileType, ref long totalFilesSize, ref int deletedFileCount, ref Options options) {
            FileInfo fi = new FileInfo(file);
            totalFilesSize += fi.Length;

            if(options.moveToRecycleBin) {
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            } else {
                File.Delete(file);
            }

            deletedFileCount++;
            changeText(infoLabel, "Deleting " + fileType + " (" + deletedFileCount.ToString() + ")");
            refreshUI();
        }
    }

    public enum MenuLayout {
        MainMenu,
        InfoLabel,
        Error,
        ProgressBar,
        Results
    }

    public class ReleaseInfo {
        public string tag_name { get; set; }
        public string body { get; set; }
    }
}