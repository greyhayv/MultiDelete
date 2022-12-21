using System.Diagnostics;
using System.IO.Compression;

namespace Updater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0) {
                throwError("Please enter the version you want to update to as the first argument.");
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
                throwError(e);
            }

            //Delete current version
            Console.WriteLine("Deleting current version");
            foreach(string file in Directory.GetFiles(Directory.GetCurrentDirectory())) {
                if(file.EndsWith("\\Updater.exe")) {
                    continue;
                }

                try {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    Console.WriteLine("Deleted " + file);
                } catch(Exception e) {
                    throwError(e);
                }
            }

            //Download zip of newest version
            string zipPath = Directory.GetCurrentDirectory() + @"\MultiDelete" + version.Substring(1) + ".zip";
            Console.WriteLine("Downloading newest version");
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + ".zip");
            HttpResponseMessage response = new HttpResponseMessage();
            try {
                response = client.GetAsync(uri.ToString()).Result;
            } catch(Exception e) {
                throwError(e);
            }
            if(response.IsSuccessStatusCode) {
                byte[] zipBytes = response.Content.ReadAsByteArrayAsync().Result;
                File.WriteAllBytes(zipPath, zipBytes);
            } else {
                throwError("There was an error downloading the update:\n" + response.StatusCode);
            }

            //rename current updater
            if(File.Exists("OldUpdater.exe")) {
                File.Delete("OldUpdater.exe");
            }
            File.Move(getProgramFilePath(), "OldUpdater.exe");

            //unzip file
            Console.WriteLine("unzipping zip");
            try {
                using(ZipArchive archive = ZipFile.OpenRead(zipPath)) {
                    foreach(ZipArchiveEntry entry in archive.Entries) {
                        entry.ExtractToFile(Directory.GetCurrentDirectory() + "\\" + entry.ToString());
                        Console.WriteLine("Extracted " + entry.ToString());
                    }
                }
            } catch(Exception e) {
                throwError(e);
            }

            //delete zip
            try {
                File.Delete(zipPath);
            } catch(Exception e) {
                throwError(e);
            }

            //Start MultiDelete
            Console.WriteLine("Update finished! Starting MultiDelete!");
            try {
                Process.Start("MultiDelete.exe");
            } catch(Exception e) {
                throwError(e);
            }

            //Delete current installer
            try {
                Process.Start(new ProcessStartInfo() {
                    Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Directory.GetCurrentDirectory() + "\\OldUpdater.exe" +"\"",
                    WindowStyle = ProcessWindowStyle.Hidden, CreateNoWindow = true, FileName = "cmd.exe"
                });
            } catch(Exception e) {
                throwError(e);
            }
        }

        public static void throwError(Exception e) {
            Console.WriteLine(e.ToString());
            Console.ReadKey();
            System.Environment.Exit(-1);
        }

        public static void throwError(string s) {
            Console.WriteLine(s);
            Console.ReadKey();
            System.Environment.Exit(-1);
        }

        public static string getProgramFilePath() {
            string? path = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;
            if(path != null) {
                return path;
            } else {
                return "";
            }
        }
    }
}