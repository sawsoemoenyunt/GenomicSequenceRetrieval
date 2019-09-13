using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomicSequenceRetrieval
{
    class Program
    {
        static void Main(string[] args)
        {
            SequenceRetrieval sqr = new SequenceRetrieval(args);
            sqr.StartValidate();
            Console.ReadLine();
        }
    }
}
