namespace WoWScrnShotSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(".");
            foreach (string file in files)
            {
                if (!file.Contains("WoWScrnShot_"))
                {
                    Console.WriteLine("Skipping {0}", file);
                    continue;
                }
                // Verify that this filename follows the WoWScrnShot_mmddyy_hhmmss*.jpg format
                string filename = file.Substring(file.IndexOf("WoWScrnShot_"));
                if (filename.Substring(0, 12) != "WoWScrnShot_")
                {
                    continue;
                }
                if (filename.Length < 26)
                {
                    continue;
                }
                string ddmmyy = filename.Substring(12, 6);
                string hhmmss = filename.Substring(19, 6);
                string excess = filename.Substring(25);
                try
                {
                    _ = Int32.Parse(ddmmyy);
                    _ = Int32.Parse(hhmmss);
                }
                catch (FormatException)
                {
                    continue;
                }
                Console.WriteLine("Processing {0}", filename);
                string newFilename = "20" + ddmmyy.Substring(4, 2) + ddmmyy.Substring(0, 2) + ddmmyy.Substring(2, 2) + "_" + hhmmss + "_WoWScrnShot";
                if (excess.Length > 4)
                {
                    newFilename += "_" + excess.TrimStart();
                }
                else
                {
                    newFilename += excess.TrimStart();
                }
                // see if newFilename exists
                if (File.Exists(newFilename))
                {
                    Console.WriteLine("Error: {0} exists", newFilename);
                    continue;
                }
                // perform the rename
                Console.WriteLine("> {0}", newFilename);
                File.Move(filename, newFilename);
            }
        }
    }
}