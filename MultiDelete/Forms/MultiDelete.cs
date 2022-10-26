using System;
using System.Text.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using MultiDelete.Controls;

namespace MultiDelete
{
    public partial class MultiDelete : Form
    {
        static string version = "v1.3";

        private static settingsMenu settingsMenu = new settingsMenu();
        private static updateScreen updateScreen = new updateScreen();
        static string programPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";
        private List<string> worldsToDelete = new List<string>();
        private List<string> recordingsToDelete = new List<string>();
        private bool cancelDeletion = false;
        public static bool updateAvailable = false;
        public static string newestVersion = "";
        private long totalFilesSize = 0;
        private bool closeAfterDeletion = false;
        private int deletedWorldCount = 0;
        private CountdownEvent worldDeletionCE = new CountdownEvent(0);
        private int[] threadDeletedWorlds = new int[0];
        private bool checkUpdates = true;

        public MultiDelete()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!Directory.Exists(programPath))
            {
                Directory.CreateDirectory(programPath);
            }

            //Check launch arguments
            string[] launchArgs = Environment.GetCommandLineArgs();
            if(launchArgs.Length > 1)
            {
                foreach(string argument in launchArgs)
                {
                    if(argument == "-delWorlds")
                    {
                        Task.Run(() => searchWorlds());
                    } else if(argument == "-closeAfterDeletion")
                    {
                        closeAfterDeletion = true;
                    } else if(argument == "-dontCheckUpdates")
                    {
                        checkUpdates = false;
                    }
                }
            }

            if(checkUpdates)
            {
                Task.Run(() => checkForUpdates(false));
            }
        }

        public static void checkForUpdates(bool openDialogIfNoNewVersion)
        {
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead("https://raw.githubusercontent.com/greyhayv/MultiDelete/master/version.txt");
            StreamReader sr = new StreamReader(stream);
            updateAvailable = false;
            newestVersion = sr.ReadToEnd();

            if(newestVersion != version)
            {
                updateAvailable = true;
                updateScreen.ShowDialog();
            } else if(openDialogIfNoNewVersion)
            {
                updateScreen.ShowDialog();
            }
        }

        private void deleteWorldsButton_Click(object sender, EventArgs e)
        {
            focusButton.Focus();
            Task.Run(() => searchWorlds());
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            focusButton.Focus();
            settingsMenu.ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            setLayout(MenuLayout.MainMenu);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            setLayout(MenuLayout.MainMenu);
            cancelDeletion = true;
        }

        private void searchWorlds()
        {
            setLayout(MenuLayout.InfoLabel);

            List<string> checkedPaths = new List<string>();
            totalFilesSize = 0;
            cancelDeletion = false;
            worldsToDelete = new List<string>();

            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            List<string> savesPaths = convertInstancePathToSavesPath(options.InstancePaths);

            //Checks if InstancePaths are configures
            if(options.InstancePaths.Length == 0)
            {
                changeText(infoLabel, "Please add an Instance-Path in the Settingmenu!");
                setLayout(MenuLayout.Error);
                return;
            }

            //Checks if Worlds to delete are configured
            if(options.StartWith.Length == 0 && options.Include.Length == 0 && options.EndWith.Length == 0)
            {
                changeText(infoLabel, "Please select what worlds to delete in the Settingsmenu!");
                changeFont(infoLabel, new Font("Roboto", 13));
                setLayout(MenuLayout.Error);
                return;
            }

            //Searches all Worlds To delete
            if(options.UpdateScreen == "never")
            {
                changeText(infoLabel, "Searching Worlds");
            } else
            {
                changeText(infoLabel, "Searching Worlds (0)");
            }

            worldsToDelete = new List<string>();
            foreach(string path in savesPaths)
            {
                if(!Directory.Exists(path))
                {
                    changeText(infoLabel, "The Saves-Path '" + path + "' doesnt exist!");
                    changeFont(infoLabel, new Font("Roboto", 13), true);
                    setLayout(MenuLayout.Error);

                    //Make Font smaller if it text to long to be displayed
                    while (infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width)
                    {
                        changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                    }

                    return;
                }

                if(checkedPaths.Contains(path))
                {
                    continue;
                }

                List<string> instanceWorlds = new List<string>();
                foreach(string world in Directory.GetDirectories(path))
                {
                    if(cancelDeletion)
                    {
                        cancelDeletion = false;
                        return;
                    }

                    if(options.DeleteAllWorlds)
                    {
                        worldsToDelete.Add(world);
                        instanceWorlds.Add(world);

                        updateWorldSearchingScreen(options.UpdateScreen);

                        continue;
                    }

                    string worldName = world.Substring(path.Length + 1);
                    if(options.StartWith.Length > 0)
                    {
                        foreach(string str in options.StartWith)
                        {
                            if(!worldName.StartsWith(str))
                            {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldSearchingScreen(options.UpdateScreen);
                        }
                    }

                    if(options.Include.Length > 0)
                    {
                        foreach(string str in options.Include)
                        {
                            if(!worldName.Contains(str))
                            {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldSearchingScreen(options.UpdateScreen);
                        }
                    }
                    if(options.EndWith.Length > 0)
                    {
                        foreach(string str in options.EndWith)
                        {
                            if(!worldName.EndsWith(str))
                            {
                                continue;
                            }

                            worldsToDelete.Add(world);
                            instanceWorlds.Add(world);

                            updateWorldSearchingScreen(options.UpdateScreen);
                        }
                    }
                }

                //Remove last x worlds form list
                List<DirectoryInfo> worldDIs = new List<DirectoryInfo>();
                foreach(string instanceWorld in instanceWorlds)
                {
                    worldDIs.Add(new DirectoryInfo(instanceWorld));
                }
                instanceWorlds = worldDIs.OrderByDescending(f => f.LastWriteTime).Select(f => f.FullName).ToList();

                for(int i = 0; i < options.KeepLastWorlds; i++)
                {
                    if(instanceWorlds.Count <= i)
                    {
                        continue;
                    }
                    worldsToDelete.Remove(instanceWorlds[i]);
                }
                checkedPaths.Add(path);
            }
            deleteWorlds();
        }

        private void deleteWorlds()
        {
            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            deletedWorldCount = 0;

            if(options.UpdateScreen == "never")
            {
                changeText(infoLabel, "Deleting Worlds");
            }
            else
            {
                changeText(infoLabel, "Deleting Worlds (0/" + worldsToDelete.Count.ToString() + ")");
                changeMaximum(progressBar, worldsToDelete.Count);
                changeValue(progressBar, 0);
                setLayout(MenuLayout.ProgressBar);
            }

            threadDeletedWorlds = new int[options.ThreadCount + 1];

            worldDeletionCE = new CountdownEvent(options.ThreadCount);

            //Divide worlds under threads
            for(int i = 0; i < options.ThreadCount; i++)
            {
                int i2 = i;
                ThreadPool.QueueUserWorkItem(state => delWorlds(i2 * (worldsToDelete.Count / options.ThreadCount), (i2 + 1) * (worldsToDelete.Count / options.ThreadCount), i), worldDeletionCE);
            }
            worldDeletionCE.Wait();

            foreach(int delWorlds in threadDeletedWorlds)
            {
                deletedWorldCount += delWorlds;
            }

            //Delte leftover worlds
            foreach(string world in worldsToDelete)
            {
                if(cancelDeletion)
                {
                    cancelDeletion = false;
                    return;
                }

                if(!Directory.Exists(world))
                {
                    continue;
                }

                try
                {
                    DirectoryInfo di = new DirectoryInfo(world);
                    addDirSize(di);

                    Directory.Delete(world, true);
                    deletedWorldCount += 1;

                    updateWorldDeletionScreen(options.UpdateScreen, deletedWorldCount);
                }
                catch
                {
                    continue;
                }
            }

            bool delRecordings = options.DeleteRecordings;
            bool delCrashReports = options.DeleteCrashReports;
            bool delScreenshots = options.DeleteScreenshots;

            if(delRecordings)
            {
                searchRecordings();
            }
            if(delCrashReports)
            {
                deleteCrashReports();
            }
            if(delScreenshots)
            {
                deleteScreenshots();
            }

            showResults();
        }

        private void delWorlds(int startIndex, int endIndex, int threadNumber)
        {
            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            //deletes All found Worlds
            changeMaximum(progressBar, worldsToDelete.Count);
            for(int i = startIndex; i < endIndex; i++)
            {
                if(cancelDeletion)
                {
                    worldDeletionCE.Signal();
                    return;
                }

                try
                {
                    DirectoryInfo di = new DirectoryInfo(worldsToDelete[i]);
                    addDirSize(di);

                    Directory.Delete(worldsToDelete[i], true);
                    threadDeletedWorlds[threadNumber] += 1;

                    int totalDeletedWorlds = deletedWorldCount;
                    foreach(int threadDeletedWorld in threadDeletedWorlds)
                    {
                        totalDeletedWorlds += threadDeletedWorld;
                    }

                    updateWorldDeletionScreen(options.UpdateScreen, totalDeletedWorlds);
                } catch
                {
                    continue;
                }
            }

            worldDeletionCE.Signal();
            return;
        }

        private void searchRecordings()
        {
            recordingsToDelete = new List<string>();

            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            setLayout(MenuLayout.InfoLabel);
            if(String.IsNullOrWhiteSpace(options.RecordingsPath))
            {
                //Checks if Recordings-Path is configured
                changeFont(infoLabel, new Font("Roboto", 15));
                changeText(infoLabel, "Please add your Recordings-Path in the Settingmenu!");
                changeLocation(infoLabel, new Point(-8, 23));
                changeText(okButton, "OK");
                setLayout(MenuLayout.Error);
                return;
            }

            changeText(infoLabel, "Searching Recordings (0)");
            recordingsToDelete = new List<string>();
            if(!Directory.Exists(options.RecordingsPath))
            {
                changeText(infoLabel, "The Recordings-Path '" + options.RecordingsPath + "' doesnt exist!");
                changeFont(infoLabel, new Font("Roboto", 13), true);
                changeText(okButton, "OK");
                setLayout(MenuLayout.Error);

                //Make Font smaller if its to long to be displayed
                while (infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width)
                {
                    changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                }

                return;
            }

            //Search Recordings to delete
            foreach(string recording in Directory.GetFiles(options.RecordingsPath))
            {
                if(cancelDeletion)
                {
                    cancelDeletion = false;
                    return;
                }

                if(!recording.EndsWith(".mp4") && !recording.EndsWith(".webm") && !recording.EndsWith(".mkv") && !recording.EndsWith(".flv") && !recording.EndsWith(".vob") && !recording.EndsWith(".ogv") && !recording.EndsWith(".ogg") && !recording.EndsWith(".drc") && !recording.EndsWith(".gif") && !recording.EndsWith(".gifv") && !recording.EndsWith(".mng") && !recording.EndsWith(".avi") && !recording.EndsWith(".MTS") && !recording.EndsWith(".M2TS") && !recording.EndsWith("TS") && !recording.EndsWith(".mov") && !recording.EndsWith(".qt") && !recording.EndsWith(".wmv") && !recording.EndsWith(".yuv") && !recording.EndsWith(".rm") && !recording.EndsWith(".rmvb") && !recording.EndsWith(".viv") && !recording.EndsWith(".asf") && !recording.EndsWith(".amv") && !recording.EndsWith(".m4p") && !recording.EndsWith(".m4v") && !recording.EndsWith(".mpg") && !recording.EndsWith(".mp2") && !recording.EndsWith(".mpeg") && !recording.EndsWith(".mpe") && !recording.EndsWith(".mpv") && !recording.EndsWith(".mpg") && !recording.EndsWith(".m2v") && !recording.EndsWith(".m4v") && !recording.EndsWith(".svi") && !recording.EndsWith(".3gp") && !recording.EndsWith(".3g2") && !recording.EndsWith(".mxf") && !recording.EndsWith(".roq") && !recording.EndsWith(".nsv") && !recording.EndsWith(".flv") && !recording.EndsWith(".f4v") && !recording.EndsWith(".f4p") && !recording.EndsWith(".f4a") && !recording.EndsWith(".f4b"))
                {
                    continue;
                }

                recordingsToDelete.Add(recording);
                changeText(infoLabel, "Searching Recordings (" + recordingsToDelete.Count.ToString() + ")");
                refreshUI();
            }

            deleteRecordings();
        }

        private void deleteRecordings()
        {
            int deletedRecordings = 0;
            changeText(infoLabel, "Deleting Recordings (0/" + recordingsToDelete.Count.ToString() + ")");
            changeMaximum(progressBar, recordingsToDelete.Count);
            setLayout(MenuLayout.ProgressBar);
            foreach(string recording in recordingsToDelete)
            {
                if(cancelDeletion)
                {
                    cancelDeletion = false;
                    return;
                }

                FileInfo fi = new FileInfo(recording);
                totalFilesSize += fi.Length;

                File.Delete(recording);
                deletedRecordings += 1;
                changeText(infoLabel, "Deleting Recordings (" + deletedRecordings.ToString() + "/" + recordingsToDelete.Count.ToString() + ")");
                changeValue(progressBar, deletedRecordings);
                refreshUI();
            }
        }

        private void deleteCrashReports()
        {
            setLayout(MenuLayout.InfoLabel);
            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            int deletedCrashReports = 0;
            changeText(infoLabel, "Deleting Crash-reports (0)");

            List<string> savesPaths = convertInstancePathToSavesPath(options.InstancePaths);

            foreach(string savesPath in savesPaths)
            {
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string crashReportsPath = minecraftPath + @"\crash-reports";
                if(!Directory.Exists(crashReportsPath))
                {
                    continue;
                }

                foreach(string crashReport in Directory.GetFiles(crashReportsPath))
                {
                    FileInfo fi = new FileInfo(crashReport);
                    totalFilesSize += fi.Length;

                    File.Delete(crashReport);
                    deletedCrashReports++;
                    changeText(infoLabel, "Deleting Crash-reports (" + deletedCrashReports.ToString() + ")");
                    refreshUI();
                }

                foreach(string file in Directory.GetFiles(minecraftPath))
                {
                    if(!file.Substring(minecraftPath.Length + 1).StartsWith("hs_err_pid"))
                    {
                        continue;
                    }

                    FileInfo fi = new FileInfo(file);
                    totalFilesSize += fi.Length;

                    File.Delete(file);
                    deletedCrashReports++;
                    changeText(infoLabel, "Deleting Crash-reports (" + deletedCrashReports.ToString() + ")");
                    refreshUI();
                }
            }
        }

        private void deleteScreenshots()
        {
            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }

            int deletedScreenshots = 0;
            changeLocation(infoLabel, new Point(-8, 41));
            changeText(infoLabel, "Deleting Screenshots (0)");

            List<string> savesPaths = convertInstancePathToSavesPath(options.InstancePaths);

            foreach(string savesPath in savesPaths)
            {
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string screenshotsPath = minecraftPath + @"\screenshots";

                if(!Directory.Exists(screenshotsPath))
                {
                    continue;
                }

                foreach(string screenshot in Directory.GetFiles(screenshotsPath))
                {
                    FileInfo fi = new FileInfo(screenshot);
                    totalFilesSize += fi.Length;

                    File.Delete(screenshot);
                    deletedScreenshots++;
                    changeText(infoLabel, "Deleting Screenshots (" + deletedScreenshots.ToString() + ")");
                    refreshUI();
                }
            }
        }

        private void showResults()
        {
            //Convert file sizes
            string fileSize = "";
            if(totalFilesSize < 1024)
            {
                fileSize = totalFilesSize.ToString() + " Bytes";
            }
            else if(totalFilesSize < 1048576)
            {
                fileSize = Math.Round(Decimal.Divide(totalFilesSize, 1024), 2) + "kB";
            }
            else if(totalFilesSize < 1073741824)
            {
                fileSize = Math.Round(Decimal.Divide(Decimal.Divide(totalFilesSize, 1024), 1024), 2) + "MB";
            }
            else
            {
                fileSize = Math.Round(Decimal.Divide(Decimal.Divide(Decimal.Divide(totalFilesSize, 1024), 1024), 1024), 2) + "GB";
            }

            changeVisibilaty(progressBar, false);
            if(worldsToDelete.Count == 0)
            {
                changeText(infoLabel, "No Worlds got found! (" + fileSize + ")");
            }
            else if(worldsToDelete.Count == 1)
            {
                changeText(infoLabel, "Deleted 1 World! (" + fileSize + ")");
            }
            else
            {
                changeText(infoLabel, "Deleted " + worldsToDelete.Count.ToString() + " Worlds! (" + fileSize + ")");
            }
            setLayout(MenuLayout.Results);

            if(closeAfterDeletion)
            {
                Application.Exit();
            }
        }

        private void updateWorldSearchingScreen(string updateScreen)
        {
            if(updateScreen == "every world")
            {
                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                refreshUI();
            } else if(updateScreen == "every 10. world" && worldsToDelete.Count % 10 == 0)
            {
                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                refreshUI();
            } else if(updateScreen == "every 100. world" && worldsToDelete.Count % 100 == 0)
            {
                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                refreshUI();
            } else if(updateScreen == "every 1000. world" && worldsToDelete.Count % 1000 == 0)
            {
                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                refreshUI();
            }
        }

        private void updateWorldDeletionScreen(string updateScreen, int delWorldCount)
        {
            if(updateScreen == "every world")
            {
                changeText(infoLabel, "Deleting Worlds (" + delWorldCount.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, delWorldCount);
                refreshUI();
            } else if(updateScreen == "every 10. world" && delWorldCount % 10 == 0)
            {
                changeText(infoLabel, "Deleting Worlds (" + delWorldCount.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, delWorldCount);
                refreshUI();
            } else if(updateScreen == "every 100. world" && delWorldCount % 100 == 0)
            {
                changeText(infoLabel, "Deleting Worlds (" + delWorldCount.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, delWorldCount);
                refreshUI();
            } else if(updateScreen == "every 1000. world" && delWorldCount % 1000 == 0)
            {
                changeText(infoLabel, "Deleting Worlds (" + delWorldCount.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, delWorldCount);
                refreshUI();
            }
        }

        private List<string> convertInstancePathToSavesPath(string[] instancePaths)
        {
            List<string> savesPaths = new List<string>();
            foreach(string instancePath in instancePaths)
            {
                if(instancePath.EndsWith(@"\saves"))
                {
                    savesPaths.Add(instancePath);
                } else if(instancePath.EndsWith(@"\saves\"))
                {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                } else if(instancePath.EndsWith(@"\.minecraft"))
                {
                    savesPaths.Add(instancePath + @"\saves");
                } else if(instancePath.EndsWith(@"\.minecraft\"))
                {
                    savesPaths.Add(instancePath + "saves");
                } else if(instancePath.EndsWith(@"\"))
                {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                } else
                {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            return savesPaths;
        }

        private void changeText(Label label, String text)
        {
            label.BeginInvoke((Action)(() => label.Text = text));
        }

        private void changeText(Button button, String text)
        {
            button.BeginInvoke((Action)(() => button.Text = text));
        }

        private void changeVisibilaty(Button button, bool visible)
        {
            button.BeginInvoke((Action)(() => button.Visible = visible));
        }

        private void changeVisibilaty(Label label, bool visible)
        {
            label.BeginInvoke((Action)(() => label.Visible = visible));
        }

        private void changeVisibilaty(ProgressBar progressBar, bool visible)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Visible = visible));
        }

        private void changeFont(Label label, Font font)
        {
            label.BeginInvoke((Action) (() => label.Font = font));
        }

        private void changeFont(Label label, Font font, bool Invoke)
        {
            if(Invoke)
            {
                label.Invoke((Action)(() => label.Font = font));
            } else
            {
                label.BeginInvoke((Action)(() => label.Font = font));
            }
        }

        private void changeLocation(Label label, Point location)
        {
            label.BeginInvoke((Action)(() => label.Location = location));
        }

        private void changeLocation(Button button, Point location)
        {
            button.BeginInvoke((Action)(() => button.Location = location));
        }

        private void changeLocation(ProgressBar progressBar, Point location)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Location = location));
        }

        private void changeMaximum(ProgressBar progressBar, int max)
        {
            progressBar.Invoke((Action)(() => progressBar.Maximum = max));
        }

        private void changeValue(ProgressBar progressBar, int value)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Value = value));
        }

        private void refreshUI()
        {
            Invoke((Action)(() => Refresh()));
        }

        private void addDirSize(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles();
            foreach(FileInfo fi in fis)
            {
                totalFilesSize += fi.Length;
            }
            DirectoryInfo[] subDirectorys = di.GetDirectories();
            foreach(DirectoryInfo di2 in subDirectorys)
            {
                addDirSize(di2);
            }
        }

        private void setLayout(MenuLayout layout)
        {
            //Reset menu
            changeVisibilaty(cancelButton, false);
            changeVisibilaty(deleteWorldsButton, false);
            changeVisibilaty(focusButton, false);
            changeVisibilaty(infoLabel, false);
            changeVisibilaty(okButton, false);
            changeVisibilaty(progressBar, false);
            changeVisibilaty(settingsButton, false);
            changeLocation(cancelButton, new Point(198, 72));
            changeLocation(deleteWorldsButton, new Point(167, 31));
            changeLocation(focusButton, new Point(431, 48));
            changeLocation(infoLabel, new Point(-8, 41));
            changeLocation(okButton, new Point(193, 57));
            changeLocation(progressBar, new Point(17, 44));
            changeLocation(settingsButton, new Point(447, 7));

            if(layout == MenuLayout.MainMenu)
            {
                changeVisibilaty(deleteWorldsButton, true);
                changeVisibilaty(settingsButton, true);
            } else if(layout == MenuLayout.InfoLabel)
            {
                changeVisibilaty(infoLabel, true);
                changeVisibilaty(cancelButton, true);
                changeLocation(infoLabel, new Point(-8, 41));
                changeFont(infoLabel, new Font("Roboto", 16));
            } else if(layout == MenuLayout.Error)
            {
                changeVisibilaty(okButton, true);
                changeVisibilaty(infoLabel, true);
                changeLocation(infoLabel, new Point(-8, 23));
                changeText(okButton, "OK");
            } else if(layout == MenuLayout.ProgressBar)
            {
                changeVisibilaty(progressBar, true);
                changeVisibilaty(infoLabel, true);
                changeVisibilaty(cancelButton, true);
                changeLocation(infoLabel, new Point(-8, 10));
            } else if(layout == MenuLayout.Results)
            {
                changeText(okButton, "Done");
                changeLocation(infoLabel, new Point(-8, 23));
                changeVisibilaty(okButton, true);
                changeVisibilaty(infoLabel, true);
            }
        }
    }

    public enum MenuLayout
    {
        MainMenu,
        InfoLabel,
        Error,
        ProgressBar,
        Results
    }
}