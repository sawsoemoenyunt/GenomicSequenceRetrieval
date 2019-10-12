using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomicSequenceRetrieval
{
    public class SearchLevel5 : Search
    {
        private string dna;
        private List<string> idList;

        public SearchLevel5(string programName, FlagLevel level, string fileName, string _dna) : base(programName, level, fileName)
        {
            this.dna = _dna;
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
            idList = base.CurrentReader().GetIdListBySequence(this.dna);
        }
    }
}
