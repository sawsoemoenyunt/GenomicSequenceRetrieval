using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using System.Text;
using GenomicSequenceRetrieval;

namespace IndexSequence16s
{
    class MainClass
    {
        private static string programName, fileName, indexFileName;
        private static List<string> indexList;

        public static void Main(string[] args)
        {
            
            //indexing works here
            bool result = ValidateArguments(args);

            if (result)
            {
                indexList = CreateIndex();

                if (indexList.Count > 0)
                {
                    
                    SaveIndexToFile();
                }
                else
                {
                    ShowError("Index file not created!");
                }
            }
            else
            {
                ShowError("Please enter correct command.");
            }
        }

        public static bool ValidateArguments(string[] args)
        {
            bool validated = false;

            if (args.Length == 3)
            {
                if (args[0].Equals("IndexSequence16s"))
                {
                    programName = args[0];

                    if (args[1].Contains("fasta"))
                    {
                        fileName = args[1];

                        if (args[2].Contains("index"))
                        {
                            indexFileName = args[2];
                            validated = true;
                        }
                        else
                        {
                            ShowError("Third argument must be index file.");
                        }
                    }
                    else
                    {
                        ShowError("Second argument must be fasta file.");
                    }
                }
                else
                {
                    ShowError("Enter correct program name. Program name must be IndexSequence16s");
                }
            }
            else
            {
                ShowError("Please enter 3 arguments for IndexSequence16s program.");
            }
            return validated;
        }

        public static List<string> CreateIndex()
        {
            List<string> idList = new List<string>();

            try
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory.Replace(programName, "GenomicSequenceRetrieval");
                //Console.WriteLine(currentPath);
                string text = File.ReadAllText(currentPath + fileName);
                using (var rearder = new StringReader(text))
                using (var trackingReader = new TextReaderTracker(rearder))
                {
                    string line;
                    while ((line = trackingReader.ReadLine()) != null)
                    {
                        if (line.StartsWith(">", StringComparison.Ordinal))
                        {
                            string[] metadata = line.Split(null);
                            foreach (string data in metadata)
                            {
                                if (data.StartsWith(">", StringComparison.Ordinal))
                                {
                                    int index = trackingReader.Position - (line.Length + 1);
                                    string id = data.Remove(0, 1) + " " + index;
                                    idList.Add(id);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            return idList;
        }

        public static void SaveIndexToFile()
        {
            
            try
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory.Replace(programName, "GenomicSequenceRetrieval");
                //Console.WriteLine(currentPath);
                StreamWriter wr = new StreamWriter(currentPath + indexFileName);
                foreach (string index in indexList)
                {
                    wr.WriteLine(index);
                }
                ///close the writer
                wr.Close();
                Console.WriteLine("Index file successfully created.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public static void ShowError(string error)
        {
            Console.WriteLine(error);
        }
    }
}
