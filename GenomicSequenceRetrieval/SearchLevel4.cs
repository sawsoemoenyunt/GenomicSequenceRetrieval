using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace GenomicSequenceRetrieval
{
    public class SearchLevel4 : Search
    {
        private string indexFileName;
        private string queryFileName;
        private string resultFileName;
        private List<string> resultList;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private List<string> indexList;

        //Search16s -level4 16S.fasta 16S.index query.txt results.txt
        public SearchLevel4(string programName, FlagLevel level, string file, string indexFileName, string query, string result) : base(programName, level, file)
        {
            this.indexFileName = indexFileName;
            this.queryFileName = query;
            this.resultFileName = result;
            LoadIndexFile();
            try
            {
                streamReader = new StreamReader(this.queryFileName);
                streamWriter = new StreamWriter(this.resultFileName);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public override void ShowResult()
        {
            //do nothing
        }

        public void LoadIndexFile()
        {
            try
            {
                StreamReader sr = new StreamReader(this.indexFileName);
                this.indexList = new List<string>();
                while (true)
                {
                    string line = sr.ReadLine();
                    
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    this.indexList.Add(line);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                Console.WriteLine("error here");
            }
        }

        public override void StartSearching()
        {
            try
            {
                resultList = new List<string>();
                while (true)
                {
                    string id = streamReader.ReadLine();
                    if (string.IsNullOrEmpty(id))
                    {
                        break;
                    }

                    bool isFound = false;
                    string result = "";
                    foreach (string index in indexList)
                    {
                        if (index.Contains(id))
                        {
                            isFound = true;
                            string[] idAndIndex = index.Split(null);
                            int indexNumber = int.Parse(idAndIndex[1]);
                            result = base.CurrentReader().DirectAccessByIndex(id, indexNumber);
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        result = string.Format("Error, sequence {0} not found.", id);
                    }
                    this.resultList.Add(result);
                }
                streamReader.Close();
                SaveResult();

            }
            catch (Exception ex)
            {
                base.ShowError(ex.Message);
            }
        }

        public void SaveResult()
        {
            try
            {
                foreach (string result in resultList)
                {
                    if (result.ToLower().Contains("error"))
                    {
                        ///show error on console;
                        base.ShowError(result);
                    }
                    else
                    {
                        ///save data to results.txt
                        streamWriter.WriteLine(result);
                    }
                }
                ///close the writer
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                base.ShowError(ex.Message);
            }
        }
    }
}
