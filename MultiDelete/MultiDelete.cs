using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using System.Diagnostics;
using System.Linq;

namespace MultiDelete
{
    public partial class MultiDelete : Form
    {
        static string version = "v1.3";

        static settingsMenu settingsMenu = new settingsMenu();
        static updateScreen updateScreen = new updateScreen();
        static string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";
        string optionsFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete\options.json";
        List<string> worldsToDelete = new List<string>();
        List<string> recordingsToDelete = new List<string>();
        bool cancelDeletion = false;
        static bool updateAvailable = false;
        static string newestVersion = "";
        long size = 0;
        bool closeAfterDeletion = false;
        List<string> checkedPaths = new List<string>();
        int deletedWorlds = 0;
        public CountdownEvent wdCountdownEvent = new CountdownEvent(0);
        public int[] threadDeletedWorlds = new int[0];
        private bool checkUpdates = true;

        public MultiDelete()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Create Programm Folder if it doesnt exist
            if (!Directory.Exists(programsPath))
            {
                Directory.CreateDirectory(programsPath);
            }

            //Check launch arguments
            string[] launchArgs = Environment.GetCommandLineArgs();
            if(launchArgs.Length > 1)
            {
                foreach (string argument in launchArgs)
                {
                    if(argument == "-delWorlds")
                    {
                        deleteWorldsButton.Visible = false;
                        settingsButton.Visible = false;
                        infoLabel.Visible = true;
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
            //Reset Variables
            checkedPaths = new List<string>();
            size = 0;
            cancelDeletion = false;
            worldsToDelete = new List<string>();
            //Get Variables
            Options options = new Options();
            if(File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            } else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false
                };
            }
            string[] instancePaths = options.InstancePaths;
            string[] startWith = options.StartWith;
            string[] include = options.Include;
            string[] endWith = options.EndWith;
            bool deleteAllWorlds = options.DeleteAllWorlds;

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
                    //Searches all Worlds To delete
                    changeVisibilaty(cancelButton, true);
                    if (options.UpdateScreen == "never")
                    {
                        changeText(infoLabel, "Searching Worlds");
                    }
                    else
                    {
                        changeText(infoLabel, "Searching Worlds (0)");
                    }
                    worldsToDelete = new List<string>();
                    foreach (string path in savesPaths)
                    {
                        if (!Directory.Exists(path))
                        {
                            changeText(infoLabel, "The Saves-Path '" + path + "' doesnt exist!");
                            changeFont(infoLabel, new Font("Roboto", 13), true);
                            changeLocation(infoLabel, new Point(-8, 23));
                            changeText(okButton, "OK");
                            changeVisibilaty(okButton, true);
                            changeVisibilaty(cancelButton, false);

                            //Make Font smaller if it text to long to be displayed
                            while (infoLabel.Width < TextRenderer.MeasureText(infoLabel.Text, infoLabel.Font).Width)
                            {
                                changeFont(infoLabel, new Font("Roboto", infoLabel.Font.Size - 0.5f), true);
                            }

                            return;
                        }
                        bool hasPathAlreadyBeenChecked = false;
                        foreach(string checkedPath in checkedPaths)
                        {
                            if(path == checkedPath)
                            {
                                hasPathAlreadyBeenChecked = true;
                            }
                        }
                        if(!hasPathAlreadyBeenChecked)
                        {
                            List<string> instanceWorlds = new List<string>();
                            foreach (string world in Directory.GetDirectories(path))
                            {
                                if (cancelDeletion == true)
                                {
                                    cancelDeletion = false;
                                    return;
                                }
                                if (deleteAllWorlds)
                                {
                                    worldsToDelete.Add(world);
                                    instanceWorlds.Add(world);

                                    if (options.UpdateScreen == "every world")
                                    {
                                        changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                        refreshUI();
                                    }
                                    else if (options.UpdateScreen == "every 10. world" && worldsToDelete.Count % 10 == 0)
                                    {
                                        changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                        refreshUI();
                                    }
                                    else if (options.UpdateScreen == "every 100. world" && worldsToDelete.Count % 100 == 0)
                                    {
                                        changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                        refreshUI();
                                    }
                                    else if (options.UpdateScreen == "every 1000. world" && worldsToDelete.Count % 1000 == 0)
                                    {
                                        changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                        refreshUI();
                                    }
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
                                                instanceWorlds.Add(world);
                                                if (options.UpdateScreen == "every world")
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 10. world" && worldsToDelete.Count % 10 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 100. world" && worldsToDelete.Count % 100 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 1000. world" && worldsToDelete.Count % 1000 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
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
                                                instanceWorlds.Add(world);
                                                if (options.UpdateScreen == "every world")
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 10. world" && worldsToDelete.Count % 10 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 100. world" && worldsToDelete.Count % 100 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 1000. world" && worldsToDelete.Count % 1000 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
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
                                                instanceWorlds.Add(world);
                                                if (options.UpdateScreen == "every world")
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 10. world" && worldsToDelete.Count % 10 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 100. world" && worldsToDelete.Count % 100 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                                else if (options.UpdateScreen == "every 1000. world" && worldsToDelete.Count % 1000 == 0)
                                                {
                                                    changeText(infoLabel, "Searching Worlds (" + worldsToDelete.Count.ToString() + ")");
                                                    refreshUI();
                                                }
                                            }
                                        }
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
                        }
                        checkedPaths.Add(path);
                    }
                    deleteWorlds();
                }
            }
        }

        private void deleteWorlds()
        {
            //Get options
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false,
                    ThreadCount = 1
                };
            }

            deletedWorlds = 0;

            if (options.UpdateScreen == "never")
            {
                changeText(infoLabel, "Deleting Worlds");
            }
            else
            {
                changeLocation(infoLabel, new Point(-8, 10));
                changeText(infoLabel, "Deleting Worlds (0/" + worldsToDelete.Count.ToString() + ")");
                changeMaximum(progressBar, worldsToDelete.Count);
                changeValue(progressBar, 0);
                changeVisibilaty(progressBar, true);
            }

            threadDeletedWorlds = new int[options.ThreadCount + 1];

            wdCountdownEvent = new CountdownEvent(options.ThreadCount);
            //Divide worlds under threads
            for(int i = 0; i < options.ThreadCount; i++)
            {
                int i2 = i;
                ThreadPool.QueueUserWorkItem(state => 
                delWorlds(i2 * (worldsToDelete.Count / options.ThreadCount), (i2 + 1) * (worldsToDelete.Count / options.ThreadCount), i), wdCountdownEvent);
            }
            wdCountdownEvent.Wait();

            foreach(int delWorlds in threadDeletedWorlds)
            {
                deletedWorlds += delWorlds;
            }
            
            //Delte leftover worlds
            foreach(string world in worldsToDelete)
            {
                if (cancelDeletion == true)
                {
                    cancelDeletion = false;
                    return;
                }
                if (Directory.Exists(world))
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(world);
                        calcDirSize(di);

                        Directory.Delete(world, true);
                        deletedWorlds += 1;
                        changeMaximum(progressBar, worldsToDelete.Count);
                        if (options.UpdateScreen == "every world")
                        {
                            changeText(infoLabel, "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                            changeValue(progressBar, deletedWorlds);
                            refreshUI();
                        }
                        else if (options.UpdateScreen == "every 10. world" && deletedWorlds % 10 == 0)
                        {
                            changeText(infoLabel, "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                            changeValue(progressBar, deletedWorlds);
                            refreshUI();
                        }
                        else if (options.UpdateScreen == "every 100. world" && deletedWorlds % 100 == 0)
                        {
                            changeText(infoLabel, "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                            changeValue(progressBar, deletedWorlds);
                            refreshUI();
                        }
                        else if (options.UpdateScreen == "every 1000. world" && deletedWorlds % 1000 == 0)
                        {
                            changeText(infoLabel, "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                            changeValue(progressBar, deletedWorlds);
                            refreshUI();
                        }
                    } catch
                    {
                        continue;
                    }
                }
            }

            bool delRecordings = options.DeleteRecordings;
            bool delCrashReports = options.DeleteCrashReports;
            bool delRawalleLogs = options.DeleteRawalleLogs;
            bool delScreenshots = options.DeleteScreenshots;

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

        private void delWorlds(int startIndex, int endIndex, int threadNumber)
        {
            //Get options
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false,
                    ThreadCount = 1
                };
            }

            //deletes All found Worlds
            changeMaximum(progressBar, worldsToDelete.Count);
            for (int i = startIndex; i < endIndex; i++)
            {
                if(cancelDeletion)
                {
                    wdCountdownEvent.Signal();
                    return;
                }
                try
                {
                    DirectoryInfo di = new DirectoryInfo(worldsToDelete[i]);
                    calcDirSize(di);

                    Directory.Delete(worldsToDelete[i], true);
                    threadDeletedWorlds[threadNumber] += 1;

                    int totalDeletedWorlds = deletedWorlds;
                    foreach (int threadDeletedWorld in threadDeletedWorlds)
                    {
                        totalDeletedWorlds += threadDeletedWorld;
                    }
                    if (options.UpdateScreen == "every world")
                    {
                        changeText(infoLabel, "Deleting Worlds (" + totalDeletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                        changeValue(progressBar, totalDeletedWorlds);
                        refreshUI();
                    }
                    else if (options.UpdateScreen == "every 10. world" && totalDeletedWorlds % 10 == 0)
                    {
                        changeText(infoLabel, "Deleting Worlds (" + totalDeletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                        changeValue(progressBar, totalDeletedWorlds);
                        refreshUI();
                    }
                    else if (options.UpdateScreen == "every 100. world" && totalDeletedWorlds % 100 == 0)
                    {
                        changeText(infoLabel, "Deleting Worlds (" + totalDeletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                        changeValue(progressBar, totalDeletedWorlds);
                        refreshUI();
                    }
                    else if (options.UpdateScreen == "every 1000. world" && totalDeletedWorlds % 1000 == 0)
                    {
                        changeText(infoLabel, "Deleting Worlds (" + totalDeletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")");
                        changeValue(progressBar, totalDeletedWorlds);
                        refreshUI();
                    }
                } catch
                {
                    continue;
                } 
            }
            wdCountdownEvent.Signal();
            return;
        }

        private void searchRecordings()
        {
            recordingsToDelete = new List<string>();
            //Gets Variable
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    UpdateScreen = "",
                    RecordingsPath = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false
                };
            }
            string recordingsPath = options.RecordingsPath;
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
                    if(recording.EndsWith(".mp4") || recording.EndsWith(".webm") || recording.EndsWith(".mkv") || recording.EndsWith(".flv") || recording.EndsWith(".vob") || recording.EndsWith(".ogv") || recording.EndsWith(".ogg") || recording.EndsWith(".drc") || recording.EndsWith(".gif") || recording.EndsWith(".gifv") || recording.EndsWith(".mng") || recording.EndsWith(".avi") || recording.EndsWith(".MTS") || recording.EndsWith(".M2TS") || recording.EndsWith("TS") || recording.EndsWith(".mov") || recording.EndsWith(".qt") || recording.EndsWith(".wmv") || recording.EndsWith(".yuv") || recording.EndsWith(".rm") || recording.EndsWith(".rmvb") || recording.EndsWith(".viv") || recording.EndsWith(".asf") || recording.EndsWith(".amv") || recording.EndsWith(".m4p") || recording.EndsWith(".m4v") || recording.EndsWith(".mpg") || recording.EndsWith(".mp2") || recording.EndsWith(".mpeg") || recording.EndsWith(".mpe") || recording.EndsWith(".mpv") || recording.EndsWith(".mpg") || recording.EndsWith(".m2v") || recording.EndsWith(".m4v") || recording.EndsWith(".svi") || recording.EndsWith(".3gp") || recording.EndsWith(".3g2") || recording.EndsWith(".mxf") || recording.EndsWith(".roq") || recording.EndsWith(".nsv") || recording.EndsWith(".flv") || recording.EndsWith(".f4v") || recording.EndsWith(".f4p") || recording.EndsWith(".f4a") || recording.EndsWith(".f4b"))
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

                FileInfo fi = new FileInfo(recording);
                size += fi.Length;

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
            //Get variables
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false
                };
            }
            string[] instancePaths = options.InstancePaths;
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
                        FileInfo fi = new FileInfo(crashReport);
                        size += fi.Length;

                        File.Delete(crashReport);
                        deletedCrashReports++;
                        changeText(infoLabel, "Deleting Crash-reports (" + deletedCrashReports.ToString() + ")");
                        refreshUI();
                    }
                }
                foreach(string file in Directory.GetFiles(minecraftPath))
                {
                    if(file.Substring(minecraftPath.Length + 1).StartsWith("hs_err_pid")) {
                        FileInfo fi = new FileInfo(file);
                        size += fi.Length;

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
            //Get Variables
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false
                };
            }
            string[] instancePaths = options.InstancePaths;
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
                        FileInfo fi = new FileInfo(file);
                        size += fi.Length;

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
            //Get variables
            Options options = new Options();
            if (File.Exists(optionsFile))
            {
                options = JsonSerializer.Deserialize<Options>(File.ReadAllText(optionsFile));
            }
            else
            {
                options = new Options
                {
                    InstancePaths = new string[0],
                    DeleteAllWorlds = false,
                    StartWith = new string[0],
                    Include = new string[0],
                    EndWith = new string[0],
                    DeleteRecordings = false,
                    RecordingsPath = "",
                    UpdateScreen = "",
                    DeleteCrashReports = false,
                    DeleteRawalleLogs = false,
                    DeleteScreenshots = false
                };
            }
            string[] instancePaths = options.InstancePaths;
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
                        FileInfo fi = new FileInfo(screenshot);
                        size += fi.Length;

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
            //Convert file sizes
            string fileSize = "";
            if (size < 1024)
            {
                fileSize = size.ToString() + " Bytes";
            }
            else if (size < 1048576)
            {
                fileSize = Math.Round(Decimal.Divide(size, 1024), 2) + "kB";
            }
            else if (size < 1073741824)
            {
                fileSize = Math.Round(Decimal.Divide(Decimal.Divide(size , 1024) , 1024), 2) + "MB";
            }
            else
            {
                fileSize = Math.Round(Decimal.Divide(Decimal.Divide(Decimal.Divide(size, 1024), 1024), 1024), 2) + "GB";
            }

            changeVisibilaty(progressBar, false);
            if (worldsToDelete.Count == 0)
            {
                changeText(infoLabel, "No Worlds got found! (" + fileSize + ")");
            }
            else if (worldsToDelete.Count == 1)
            {
                changeText(infoLabel, "Deleted 1 World! (" + fileSize + ")");
            }
            else
            {
                changeText(infoLabel, "Deleted " + worldsToDelete.Count.ToString() + " Worlds! (" + fileSize + ")");
            }
            changeText(okButton, "Done");
            changeLocation(infoLabel, new Point(-8, 23));
            changeVisibilaty(okButton, true);
            changeVisibilaty(cancelButton, false);

            if(closeAfterDeletion)
            {
                Application.Exit();
            }
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

        private void calcDirSize(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            DirectoryInfo[] subDirectorys = di.GetDirectories();
            foreach(DirectoryInfo di2 in subDirectorys)
            {
                calcDirSize(di2);
            }
        }
    }
}