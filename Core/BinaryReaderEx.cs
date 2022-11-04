using System.IO;

namespace WSSE.Core
{
    /// <summary>
    /// Binary reader extension to support specifying an endianness
    /// </summary>
    internal class BinaryReaderEx : BinaryReader
    {
        BinaryReaderEx(Stream strm)
            : base(strm)
        {
        }
    }
}
