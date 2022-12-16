using System.Diagnostics;
using System.IO.Compression;

namespace Updater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0) {
                Console.WriteLine("Please enter the version you want to update to as the first argument.");
                Console.ReadKey();
                return;
            }

            updateMultiDelete(args[0]);
        }

        private static void updateMultiDelete(string version) {
            //Kill MultiDelete processes
            Console.WriteLine("Killing MultiDelete processes");
            try {
                foreach(Process process in Process.GetProcessesByName("MultiDelete")) {
                    process.Kill();
                    process.WaitForExit();
                }
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }

            //Download zip of newest version
            string zipPath = Directory.GetCurrentDirectory() + @"\MultiDelete" + version.Substring(1) + ".zip";
            if(!File.Exists(zipPath)) {
                Console.WriteLine("Downloading newest version");
                HttpClient client = new HttpClient();
                Uri uri = new Uri("https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + ".zip");
                HttpResponseMessage respone = client.GetAsync(uri.ToString()).Result;
                if(respone.IsSuccessStatusCode) {
                    byte[] zipBytes = respone.Content.ReadAsByteArrayAsync().Result;
                    File.WriteAllBytes(zipPath, zipBytes);
                } else {
                    Console.WriteLine("There was an error downloading the update:");
                    Console.WriteLine(respone.StatusCode);
                    Console.ReadKey();
                    return;
                }
            }

            if(Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName) == "newUpdater") {
                if(File.Exists("Updater.exe")) {
                    File.Delete("Updater.exe");
                }
                File.Move("newUpdater.exe", "Updater.exe");
            } else {
                //update Updater
                Console.WriteLine("Excuting new updater");
                using(ZipArchive archive = ZipFile.OpenRead(zipPath)) {
                    ZipArchiveEntry? entry = archive.GetEntry("Updater.exe");
                    if(entry != null) {
                        string newUpdaterDir = Directory.GetCurrentDirectory() + "\\newUpdater.exe";
                        if(File.Exists(newUpdaterDir)) {
                            File.Delete(newUpdaterDir);
                        }
                        entry.ExtractToFile(newUpdaterDir);
                        Process process = new Process();
                        process.StartInfo.FileName = "newUpdater.exe";
                        process.StartInfo.UseShellExecute = true;
                        process.StartInfo.Verb = "runas";
                        process.StartInfo.Arguments = version;
                        process.Start();
                        return;
                    }
                }
            }

            //Delete current version
            Console.WriteLine("Deleting current version");
            foreach(string file in Directory.GetFiles(Directory.GetCurrentDirectory())) {
                if(file.EndsWith("\\Updater.exe") || file == zipPath) {
                    continue;
                }

                try {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    Console.WriteLine("Deleted " + file);
                } catch(Exception e) {
                    Console.WriteLine(e.ToString());
                    Console.ReadKey();
                    return;
                }
            }

            //unzip file
            try {
                using(ZipArchive archive = ZipFile.OpenRead(zipPath)) {
                    foreach(ZipArchiveEntry entry in archive.Entries) {
                        if(entry.ToString() == "Updater.exe") {
                            continue;
                        }
                        
                        entry.ExtractToFile(Directory.GetCurrentDirectory() + "\\" + entry.ToString());
                        Console.WriteLine("Extracted " + entry.ToString());
                    }
                }
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }

            //delete zip
            try {
                File.Delete(zipPath);
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Update finished! Starting MultiDelete!");

            //Start MultiDelete
            try {
                Process.Start("MultiDelete.exe");
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
                return;
            }
        }
    }
}