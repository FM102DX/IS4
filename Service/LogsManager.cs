using System;
using System.Collections.Generic;
using System.IO;

namespace IS4.Service
{
    public class LogsManager
    {
        public string FullFileName { get; set; } = "Is4Logs.txt";

        public List<string> Lines { get; set; } = new List<string>();

        public LogsManager()
        {

        }

        public void ReadLogs()
        {
            Lines.Clear();
            try
            {
                StreamReader sr = new StreamReader(FullFileName);
                //Read the first line of text
                string line;
                do
                {
                    line = sr.ReadLine();
                    Lines.Add(line);

                } while (line != null);
                sr.Close();
            }
            catch (Exception e)
            {
                Lines.Add($"Exception {e.Message}");
            }
        }



    }
}
