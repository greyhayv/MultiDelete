using System;
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
        static settingsMenu settingsMenu = new settingsMenu();
        string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\MultiDelete";

        public MultiDelete()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deleteWorldsButton.Visible = false;
            settingsButton.Visible = false;
            focusButton.Focus();
            label1.Visible = true;
            deleteWorlds();
        }

        private void deleteWorlds()
        {
            //Gets Variables from TextFiles
            string[] savesPaths = File.ReadAllLines(programsPath + @"\savesPaths.txt");
            string[] startWith = File.ReadAllLines(programsPath + @"\startWith.txt");
            string[] include = File.ReadAllLines(programsPath + @"\include.txt");
            string[] endWith = File.ReadAllLines(programsPath + @"\endWith.txt");
            string deleteAllWorlds = File.ReadAllText(programsPath + @"\deleteAllWorlds.txt");
            //Resets Location and Font of Label
            label1.Location = new Point(-8, 41);
            label1.Font = new Font("Roboto", 16);
            if (savesPaths.Length == 0)
            {
                //Checks if Saves-Paths are configured
                label1.Text = "Please add a Saves-Path in the Settingmenu!";
                label1.Location = new Point(-8, 23);
                button1.Text = "OK";
                button1.Visible = true;
            } else
            {
                if(startWith.Length == 0 && include.Length == 0 && endWith.Length == 0)
                {
                    //Checks if Worlds to delete is configured
                    label1.Text = "Please select what worlds to delete in the Settingsmenu!";
                    label1.Font = new Font("Roboto", 13);
                    label1.Location = new Point(-8, 23);
                    button1.Text = "OK";
                    button1.Visible = true;
                } else
                {
                    //Checks if same Savespath is added twice
                    bool areSamePaths = false;
                    for(int i = 0; i < savesPaths.Length; i++)
                    {
                        for(int i2 = 0; i2 < savesPaths.Length; i2++)
                        {
                            if(savesPaths[i] == savesPaths[i2] && i != i2)
                            {
                                areSamePaths = true;
                            }
                        }
                    }
                    if(areSamePaths == true)
                    {
                        label1.Text = "You cant select the same Saves-Path twice!";
                        label1.Font = new Font("Roboto", 13);
                        label1.Location = new Point(-8, 23);
                        button1.Text = "OK";
                        button1.Visible = true;
                    } else
                    {
                        //Searches all Worlds To delete
                        label1.Text = "Searching Worlds (0)";
                        List<string> worldsToDelete = new List<string>();
                        int deletedWorlds = 0;
                        foreach (string path in savesPaths)
                        {
                            foreach (string world in Directory.GetDirectories(path))
                            {
                                if (deleteAllWorlds == "true")
                                {
                                    worldsToDelete.Add(world);
                                    label1.Text = "Searching Worlds (" + worldsToDelete.Count.ToString() + ")";
                                    this.Refresh();
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
                                                label1.Text = "Searching Worlds (" + worldsToDelete.Count.ToString() + ")";
                                                this.Refresh();
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
                                                label1.Text = "Searching Worlds (" + worldsToDelete.Count.ToString() + ")";
                                                this.Refresh();
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
                                                label1.Text = "Searching Worlds (" + worldsToDelete.Count.ToString() + ")";
                                                this.Refresh();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //deletes All found Worlds
                        label1.Location = new Point(-8, 23);
                        label1.Text = "Deleting Worlds (0/" + worldsToDelete.Count.ToString() + ")";
                        progressBar1.Maximum = worldsToDelete.Count;
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        foreach (string world in worldsToDelete)
                        {
                            Directory.Delete(world, true);
                            deletedWorlds += 1;
                            label1.Text = "Deleting Worlds (" + deletedWorlds.ToString() + "/" + worldsToDelete.Count.ToString() + ")";
                            progressBar1.Value = deletedWorlds;
                            this.Refresh();
                        }
                        progressBar1.Visible = false;
                        if (worldsToDelete.Count == 0)
                        {
                            label1.Text = "No Worlds got found!";
                        }
                        else if (worldsToDelete.Count == 1)
                        {
                            label1.Text = "Deleted 1 World!";
                        }
                        else
                        {
                            label1.Text = "Deleted " + worldsToDelete.Count.ToString() + " Worlds!";
                        }
                        button1.Text = "Done";
                        button1.Visible = true;
                    }
                }
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            //Opens Settingsmenu
            focusButton.Focus();
            settingsMenu.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void focusButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //Opens Mainmenu
            label1.Visible = false;
            button1.Visible = false;
            deleteWorldsButton.Visible = true;
            progressBar1.Visible = false;
            settingsButton.Visible = true;
        }
    }
}