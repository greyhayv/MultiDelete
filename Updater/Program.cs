using System.Diagnostics;
using System.Net;
using System.IO.Compression;

namespace Updater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0) {
                Console.WriteLine("Please enter the version you want to update to as the first arguent.");
                Console.ReadKey();
                return;
            }

            updateMultiDelete(args[0]);
        }

        private static void updateMultiDelete(string version) {
            //Kill MultiDelete processes
            Console.WriteLine("Killing MultiDelete processes");
            foreach(Process process in Process.GetProcessesByName("MultiDelete")) {
                process.Kill();
                process.WaitForExit();
            }

            //Delete current version
            Console.WriteLine("Deleting current version");
            foreach(string file in Directory.GetFiles(Directory.GetCurrentDirectory())) {
                if(file.EndsWith("\\Updater.exe")) {
                    continue;
                }

                try {
                    File.Delete(file);
                    Console.WriteLine("Deleted " + file);
                } catch(Exception e) {
                    Console.WriteLine(e.ToString());
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("Downloading newest version");
            //Download zip of newest version
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://github.com/greyhayv/MultiDelete/releases/download/" + version + "/MultiDelete" + version.Substring(1) + ".zip");
            string zipPath = Directory.GetCurrentDirectory() + @"\MultiDelete" + version.Substring(1) + ".zip";
            HttpResponseMessage respone = client.GetAsync(uri.ToString()).Result;
            if(respone.IsSuccessStatusCode) {
                byte[] zipBytes = respone.Content.ReadAsByteArrayAsync().Result;
                File.WriteAllBytes(zipPath, zipBytes);
            } else {
                Console.WriteLine("There was an error downloading the update");
                Console.ReadKey();
                return;
            }

            //unzip file
            ZipFile.ExtractToDirectory(zipPath, Directory.GetCurrentDirectory());

            //delete zip
            File.Delete(zipPath);

            Console.WriteLine("Update finished! Starting MultiDelete");

            //Start MultiDelete
            Process.Start("MultiDelete.exe");
        }
    }
}