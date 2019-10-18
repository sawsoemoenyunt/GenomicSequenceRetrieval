using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace IndexSequence16s
{
    public class TextReaderTracker : TextReader
    {
        private TextReader _baseReader;
        private int _position;

        public TextReaderTracker(TextReader baseReader)
        {
            _baseReader = baseReader;
        }

        public override int Read()
        {
            _position++;
            return _baseReader.Read();
        }

        public override int Peek()
        {
            return _baseReader.Peek();
        }

        public int Position
        {
            get { return _position; }
        }
    }
}
