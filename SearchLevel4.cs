using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using System.Text;
using GenomicSequenceRetrieval;

namespace IndexSequence16s
{
    public class SearchLevel4 : Search
    {
        string 

        public SearchLevel4()
        {
            this.programName = p;
            this.fileName = f;
            this.id = id;
            this.index = index;
            reader = new FastaReader(this.fileName);
        }

    }
}
