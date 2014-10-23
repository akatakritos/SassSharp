using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SassSharp
{
    public class FileEnumerator
    {
        private string filename;
        private char[] buffer;

        public FileEnumerator(string filename, int bufferSize = 1024)
        {
            this.filename = filename;
            this.buffer = new char[1024];
        }

        public IEnumerable<char> GetChars()
        {
            int charsRead = 0;
            using (var reader = new StreamReader(this.filename))
            {
                while ((charsRead = reader.ReadBlock(this.buffer, 0, buffer.Length)) > 0)
                    for (var i = 0; i < charsRead; i++)
                        yield return this.buffer[i];
            }
        }
    }
}
