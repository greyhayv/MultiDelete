using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MultiDelete
{
    public partial class MultiDelete : Form
    {
        static string version = "v1.1.6";

        static settingsMenu settingsMenu = new settingsMenu();
        static updateScreen updateScreen = new updateScreen();
        static string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        List<string> worldsToDelete = new List<string>();
        List<string> recordingsToDelete = new List<string>();
        bool cancelDeletion = false;
        static bool updateAvailable = false;
        static string newestVersion = "";

        public MultiDelete()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Create Programm Folder if it doesnt exist
            if (!Directory.Exists(programsPath))
            {
                Directory.CreateDirectory(programsPath);
            }

            await Task.Run(() => checkForUpdates(false));
        }

        public static void checkForUpdates(bool openDialogIfNoNewVersion)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile("https://raw.githubusercontent.com/greyhayv/MultiDelete/master/version.txt", programsPath + @"\versionCheck.txt");
            updateAvailable = false;

            newestVersion = File.ReadAllText(programsPath + @"\versionCheck.txt");
            if (!newestVersion.Equals(version))
            {
                updateAvailable = true;
                updateScreen.ShowDialog();
            } else if(openDialogIfNoNewVersion)
            {
                updateScreen.ShowDialog();
            }
        }

        public bool isUpdateAvailable()
        {
            return updateAvailable;
        }

        public string getVersion()
        {
            return newestVersion;
        }

        private async void deleteWorldsButton_Click(object sender, EventArgs e)
        {
            deleteWorldsButton.Visible = false;
            settingsButton.Visible = false;
            focusButton.Focus();
            infoLabel.Visible = true;
            await Task.Run(() => searchWorlds());
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            //Opens Settingsmenu
            focusButton.Focus();
            settingsMenu.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Opens Mainmenu
            infoLabel.Visible = false;
            okButton.Visible = false;
            deleteWorldsButton.Visible = true;
            progressBar.Visible = false;
            settingsButton.Visible = true;
            cancelButton.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Opens Mainmenu and cancels WorldDeletion
            infoLabel.Visible = false;
            okButton.Visible = false;
            deleteWorldsButton.Visible = true;
            progressBar.Visible = false;
            settingsButton.Visible = true;
            cancelButton.Visible = false;
            cancelDeletion = true;
        }

        private void searchWorlds()
        {
            cancelDeletion = false;
            worldsToDelete = new List<string>();
            //Get Variables from TextFiles
            string[] instancePaths = File.ReadAllLines(programsPath + @"\instancePaths.txt");
            string[] startWith = File.ReadAllLines(programsPath + @"\startWith.txt");
            string[] include = File.ReadAllLines(programsPath + @"\include.txt");
            string[] endWith = File.ReadAllLines(programsPath + @"\endWith.txt");
            string deleteAllWorlds = File.ReadAllText(programsPath + @"\deleteAllWorlds.txt");

            //Get savespaths from instancePath
            List<string> savesPaths = new List<string>();
            foreach(string instancePath in instancePaths)
            {
                if (instancePath.EndsWith(@"\saves"))
                {
                    savesPaths.Add(instancePath);
                } else if (instancePath.EndsWith(@"\saves\")) {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                } else if (instancePath.EndsWith(@"\.minecraft")) {
                    savesPaths.Add(instancePath + @"\saves");
                } else if (instancePath.EndsWith(@"\.minecraft\"))
                {
                    savesPaths.Add(instancePath + "saves");
                } else if (instancePath.EndsWith(@"\"))
                {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                } else
                {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            //Resets Location and Font of Label
            changeLocation(infoLabel, new Point(-8, 41));
            changeFont(infoLabel, new Font("Roboto", 16));
            if(instancePaths.Length == 0)
            {
                //Checks if Instance-Paths are configured
                changeText(infoLabel, "Please add an Instance-Path in the Settingmenu!");
                changeLocation(infoLabel, new Point(-8, 23));
                changeText(okButton, "OK");
                changeVisibilaty(okButton, true);
            }
            else
            {
                //Checks if Worlds to delete is configured
                if (startWith.Length == 0 && include.Length == 0 && endWith.Length == 0)
                {
                    changeText(infoLabel, "Please select what worlds to delete in the Settingsmenu!");
                    changeFont(infoLabel, new Font("Roboto", 13));
                    changeLocation(infoLabel, new Point(-8, 23));
                    changeText(okButton, "OK");
                    changeVisibilaty(okButton, true);
                }
                else
                {
                    //Checks if same Instancepath is added twice
                    bool areSamePaths = false;
                    for (int i = 0; i < instancePaths.Length; i++)
                    {
                        for (int i2 = 0; i2 < instancePaths.Length; i2++)
                        {
                            if (instancePaths[i] == instancePaths[i2] && i != i2)
                            {
                                areSamePaths = true;
                            }
                        }
                    }
                    if (areSamePaths == true)
                    {
                        changeText(infoLabel, "You cant select the same Instance-Path twice!");
                        changeFont(infoLabel, new Font("Roboto", 13));
                        changeLocation(infoLabel, new Point(-8, 23));
                        changeText(okButton, "OK");
                        changeVisibilaty(okButton, true);
                    }
                    else
                    {
                        //Searches all Worlds To delete
                        changeVisibilaty(cancelButton, true);
                        changeText(infoLabel, "Searching Worlds (0)");
                        worldsToDelete = new List<string>();
                        foreach(string path in savesPaths)
                        {
                            if (!Directory.Exists(path))
                            {
                                changeText(infoLabel, "The Saves-Path '" + path + "' doesnt exist!");
                                changeFont(infoLabel, new Font("Roboto", 13), true);
                                changeLocation(infoLabel, new Point(-8, 23));
                                changeText(okButton, "OK");
                                changeVisibilaty(okButton, true);
                                changeVisibilaty(cancelButton, false);

                                //Make Font smaller if its to long to be displayed
                                while(infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width)
                                {
                                    changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                                }

                                return;
                            }
                            foreach(string world in Directory.GetDirectories(path))
                            {
                                if (cancelDeletion == true)
                                {
                                    cancelDeletion = false;
                                    return;
                                }
                                if (deleteAllWorlds == "true")
                                {
                                    worldsToDelete.Add(world);
                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                    refreshUI();
                                }
                                else
                                {
                                    string worldName = world.Substring(path.Length + 1);
                                    if (startWith.Length > 0)
                                    {
                                        foreach (string str in startWith)
                                        {
                                            if (worldName.StartsWith(str))
                                            {
                                                worldsToDelete.Add(world);
                                                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                refreshUI();
                                            }
                                        }
                                    }
                                    if (include.Length > 0)
                                    {
                                        foreach (string str in include)
                                        {
                                            if (worldName.Contains(str))
                                            {
                                                worldsToDelete.Add(world);
                                                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                refreshUI();
                                            }
                                        }
                                    }
                                    if (endWith.Length > 0)
                                    {
                                        foreach (string str in endWith)
                                        {
                                            if (worldName.EndsWith(str))
                                            {
                                                worldsToDelete.Add(world);
                                                changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                refreshUI();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        deleteWorlds();
                    }
                }
            }
        }

        private void deleteWorlds()
        {
            //deletes All found Worlds
            int deletedWorlds = 0;
            changeLocation(infoLabel, new Point(-8, 10));
            changeText(infoLabel, "Deleting Worlds (0/" + worldsToDelete.Count.ToString() + ")");
            changeMaximum(progressBar, worldsToDelete.Count);
            changeValue(progressBar, 0);
            changeVisibilaty(progressBar, true);
            foreach (string world in worldsToDelete)
            {
                if(cancelDeletion == true)
                {
                    cancelDeletion = false;
                    return;
                }
                Directory.Delete(world, true);
                deletedWorlds += 1;
                changeText(infoLabel, "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                changeValue(progressBar, deletedWorlds);
                refreshUI();
            }

            string deleteRecordingsStr = File.ReadAllText(programsPath + @"\deleteRecordings.txt");
            bool delRecordings = false;

            string deleteCrashReportsStr = File.ReadAllText(programsPath + @"\deleteCrashReports.txt");
            bool delCrashReports = false;

            string deleteRawalleLogsStr = File.ReadAllText(programsPath + @"\deleteRawalleLogs.txt");
            bool delRawalleLogs = false;

            string deleteScreenshotsStr = File.ReadAllText(programsPath + @"\deleteScreenshots.txt");
            bool delScreenshots = false;

            if (deleteCrashReportsStr == "true")
            {
                delCrashReports = true;
            }
            else
            {
                delCrashReports = false;
            }

            if (deleteRecordingsStr == "true")
            {
                delRecordings = true;
            }
            else
            {
                delRecordings = false;
            }

            if (deleteRawalleLogsStr == "true")
            {
                delRawalleLogs = true;
            }
            else
            {
                delRawalleLogs = false;
            }

            if (deleteScreenshotsStr == "true")
            {
                delScreenshots = true;
            }
            else
            {
                delScreenshots = false;
            }

            if (delRecordings)
            {
                searchRecordings();
            }
            if(delCrashReports)
            {
                deleteCrashReports();
            }
            if(delRawalleLogs)
            {
                deleteRawalleLogs();
            }
            if(delScreenshots)
            {
                deleteScreenshots();
            }
            
            showResults();
        }

        private void searchRecordings()
        {
            recordingsToDelete = new List<string>();
            //Gets Variable from TextFiles
            string recordingsPath = File.ReadAllText(programsPath + @"\recordingsPath.txt");
            //Resets Location and Font of Label
            changeLocation(infoLabel, new Point(-8, 41));
            changeFont(infoLabel, new Font("Roboto", 16));
            if (recordingsPath == "")
            {
                //Checks if Recordings-Path is configured
                changeFont(infoLabel, new Font("Roboto", 15));
                changeText(infoLabel, "Please add your Recordings-Path in the Settingmenu!");
                changeLocation(infoLabel, new Point(-8, 23));
                changeText(okButton, "OK");
                changeVisibilaty(okButton, true);
                changeVisibilaty(cancelButton, false);
                changeVisibilaty(progressBar, false);
            }
            else
            {
                //Searches all Recordings to delete
                changeVisibilaty(cancelButton, true);
                changeText(infoLabel, "Searching Recordings (0)");
                recordingsToDelete = new List<string>();
                if (!Directory.Exists(recordingsPath))
                {
                    changeText(infoLabel, "The Recordings-Path '" + recordingsPath + "' doesnt exist!");
                    changeFont(infoLabel, new Font("Roboto", 13), true);
                    changeLocation(infoLabel, new Point(-8, 23));
                    changeText(okButton, "OK");
                    changeVisibilaty(okButton, true);
                    changeVisibilaty(cancelButton, false);
                    changeVisibilaty(progressBar, false);

                    //Make Font smaller if its to long to be displayed
                    while (infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width)
                    {
                        changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                    }

                    return;
                }
                foreach (string recording in Directory.GetFiles(recordingsPath))
                {
                    if (cancelDeletion == true)
                    {
                        cancelDeletion = false;
                        return;
                    }
                    recordingsToDelete.Add(recording);
                    changeText(infoLabel, "Searching Recordings (" + recordingsToDelete.Count.ToString() + ")");
                    refreshUI();
                }
                deleteRecordings();
            }
        }

        private void deleteRecordings()
        {
            //deletes All found Recordings
            int deletedRecordings = 0;
            changeLocation(infoLabel, new Point(-8, 10));
            changeText(infoLabel, "Deleting Recordings (0/" + recordingsToDelete.Count.ToString() + ")");
            changeMaximum(progressBar, recordingsToDelete.Count);
            changeValue(progressBar, 0);
            changeVisibilaty(progressBar, true);
            foreach (string recording in recordingsToDelete)
            {
                if (cancelDeletion == true)
                {
                    cancelDeletion = false;
                    return;
                }
                File.Delete(recording);
                deletedRecordings += 1;
                changeText(infoLabel, "Deleting Recordings (" + deletedRecordings.ToString() + "/" + recordingsToDelete.Count.ToString() + ")");
                changeValue(progressBar, deletedRecordings);
                refreshUI();
            }
            changeVisibilaty(progressBar, false);
        }

        private void deleteCrashReports()
        {
            string[] instancePaths = File.ReadAllLines(programsPath + @"\instancePaths.txt");
            int deletedCrashReports = 0;
            changeLocation(infoLabel, new Point(-8, 41));
            changeText(infoLabel, "Deleting Crash-reports (0)");

            //Get savespaths from instancePath
            List<string> savesPaths = new List<string>();
            foreach (string instancePath in instancePaths)
            {
                if (instancePath.EndsWith(@"\saves"))
                {
                    savesPaths.Add(instancePath);
                }
                else if (instancePath.EndsWith(@"\saves\"))
                {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                }
                else if (instancePath.EndsWith(@"\.minecraft"))
                {
                    savesPaths.Add(instancePath + @"\saves");
                }
                else if (instancePath.EndsWith(@"\.minecraft\"))
                {
                    savesPaths.Add(instancePath + "saves");
                }
                else if (instancePath.EndsWith(@"\"))
                {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                }
                else
                {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            foreach (string savesPath in savesPaths)
            {
                if(savesPath.EndsWith(@"\"))
                {
                    savesPath.Remove(savesPath.Length - 1);
                }
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string crashReportsPath = minecraftPath + @"\crash-reports";
                if(Directory.Exists(crashReportsPath))
                {
                    foreach (string crashReport in Directory.GetFiles(crashReportsPath))
                    {
                        File.Delete(crashReport);
                        deletedCrashReports++;
                        changeText(infoLabel, "Deleting Crash-reports (" + deletedCrashReports.ToString() + ")");
                        refreshUI();
                    }
                }
                foreach(string file in Directory.GetFiles(minecraftPath))
                {
                    if(file.Substring(minecraftPath.Length + 1).StartsWith("hs_err_pid")) {
                        File.Delete(file);
                        deletedCrashReports++;
                        changeText(infoLabel, "Deleting Crash-reports (" + deletedCrashReports.ToString() + ")");
                        refreshUI();
                    }
                }
            }
        }

        private void deleteRawalleLogs()
        {
            string[] instancePaths = File.ReadAllLines(programsPath + @"\instancePaths.txt");
            int deletedRawalleLogs = 0;
            changeLocation(infoLabel, new Point(-8, 41));
            changeText(infoLabel, "Deleting Rawalle-logs (0)");

            //Get savespaths from instancePath
            List<string> savesPaths = new List<string>();
            foreach (string instancePath in instancePaths)
            {
                if (instancePath.EndsWith(@"\saves"))
                {
                    savesPaths.Add(instancePath);
                }
                else if (instancePath.EndsWith(@"\saves\"))
                {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                }
                else if (instancePath.EndsWith(@"\.minecraft"))
                {
                    savesPaths.Add(instancePath + @"\saves");
                }
                else if (instancePath.EndsWith(@"\.minecraft\"))
                {
                    savesPaths.Add(instancePath + "saves");
                }
                else if (instancePath.EndsWith(@"\"))
                {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                }
                else
                {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            foreach (string savesPath in savesPaths)
            {
                if (savesPath.EndsWith(@"\"))
                {
                    savesPath.Remove(savesPath.Length - 1);
                }
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                foreach (string file in Directory.GetFiles(minecraftPath))
                {
                    if(file.Substring(minecraftPath.Length + 1).Equals("log.log"))
                    {
                        File.Delete(file);
                        deletedRawalleLogs++;
                        changeText(infoLabel, "Deleting Rawalle-logs (" + deletedRawalleLogs.ToString() + ")");
                        refreshUI();
                    }
                }
            }
        }

        private void deleteScreenshots()
        {
            string[] instancePaths = File.ReadAllLines(programsPath + @"\instancePaths.txt");
            int deletedScreenshots = 0;
            changeLocation(infoLabel, new Point(-8, 41));
            changeText(infoLabel, "Deleting Screenshots (0)");

            //Get savespaths from instancePath
            List<string> savesPaths = new List<string>();
            foreach (string instancePath in instancePaths)
            {
                if (instancePath.EndsWith(@"\saves"))
                {
                    savesPaths.Add(instancePath);
                }
                else if (instancePath.EndsWith(@"\saves\"))
                {
                    savesPaths.Add(instancePath.Remove(instancePath.Length - 1));
                }
                else if (instancePath.EndsWith(@"\.minecraft"))
                {
                    savesPaths.Add(instancePath + @"\saves");
                }
                else if (instancePath.EndsWith(@"\.minecraft\"))
                {
                    savesPaths.Add(instancePath + "saves");
                }
                else if (instancePath.EndsWith(@"\"))
                {
                    savesPaths.Add(instancePath + @".minecraft\saves");
                }
                else
                {
                    savesPaths.Add(instancePath + @"\.minecraft\saves");
                }
            }

            foreach (string savesPath in savesPaths)
            {
                if (savesPath.EndsWith(@"\"))
                {
                    savesPath.Remove(savesPath.Length - 1);
                }
                string minecraftPath = savesPath.Remove(savesPath.Length - 6);
                string screenshotsPath = minecraftPath + @"\screenshots";
                if (Directory.Exists(screenshotsPath))
                {
                    foreach (string screenshot in Directory.GetFiles(screenshotsPath))
                    {
                        File.Delete(screenshot);
                        deletedScreenshots++;
                        changeText(infoLabel, "Deleting Screenshots (" + deletedScreenshots.ToString() + ")");
                        refreshUI();
                    }
                }
            }
        }

        private void showResults()
        {
            changeVisibilaty(progressBar, false);
            if (worldsToDelete.Count == 0)
            {
                changeText(infoLabel, "No Worlds got found!");
            }
            else if (worldsToDelete.Count == 1)
            {
                changeText(infoLabel, "Deleted 1 World!");
            }
            else
            {
                changeText(infoLabel, "Deleted " + worldsToDelete.Count.ToString() + " Worlds!");
            }
            changeText(okButton, "Done");
            changeLocation(infoLabel, new Point(-8, 23));
            changeVisibilaty(okButton, true);
            changeVisibilaty(cancelButton, false);
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

        private void changeMaximum(ProgressBar progressBar, int max)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Maximum = max));
        }

        private void changeValue(ProgressBar progressBar, int value)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Value = value));
        }

        private void refreshUI()
        {
            Invoke((Action)(() => Refresh()));
        }
    }
}