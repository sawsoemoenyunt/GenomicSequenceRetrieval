using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomicSequenceRetrieval
{
    public class SearchLevel6 : Search
    {
        private string metaData;
        private List<string> idList;

        public SearchLevel6(string programName, FlagLevel level, string fileName, string _metaData) : base(programName, level, fileName)
        {
            this.metaData = _metaData;
        }

        public override void ShowResult()
        {
            if (idList.Count() == 0)
            {
                Console.WriteLine("IDs not found!");
            }
            else
            {
                foreach (string id in idList)
                {
                    Console.WriteLine(id);
                }
            }
        }

        public override void StartSearching()
        {
            idList = base.CurrentReader().GetIdListByName("Streptomyces");
        }
    }
}
