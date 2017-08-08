using NextHolliday.Models.Repository;
using System.IO;

namespace NextHolliday.iOS
{
    public class StreamLoader : IStreamLoader
    {
        public Stream GetStreamForFilename(string fileName)
        {
            return File.OpenRead(fileName);
        }
    }
}