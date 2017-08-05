using System.IO;

namespace NextHolliday.Models.Repository
{
    public interface IStreamLoader
    {
        Stream GetStreamForFilename(string fileName);
    }
}
