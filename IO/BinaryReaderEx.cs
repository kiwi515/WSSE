using System.Text;
using WSSE.Util;

namespace WSSE.IO
{
    /// <summary>
    /// Binary reader extension to support specifying an endianness
    /// </summary>
    internal class BinaryReaderEx : BinaryReader
    {
        // Reader endianness
        private EndianUtil.Kind m_Endian;

        public BinaryReaderEx(string path, EndianUtil.Kind endian)
            : base(File.Open(path, FileMode.Open))
        {
            m_Endian = endian;
        }

        public BinaryReaderEx(string path, EndianUtil.Kind endian,
                              Encoding encoding)
            : base(File.Open(path, FileMode.Open), encoding)
        {
            m_Endian = endian;
        }

        public BinaryReaderEx(string path, EndianUtil.Kind endian,
                              Encoding encoding, bool leaveOpen)
            : base(File.Open(path, FileMode.Open), encoding, leaveOpen)
        {
            m_Endian = endian;
        }

        /// <summary>
        /// Read signed 8-bit integer from the stream
        /// </summary>
        public sbyte ReadS8()
        {
            return ReadSByte();
        }

        /// <summary>
        /// Read unsigned 8-bit integer from the stream
        /// </summary>
        public byte ReadU8()
        {
            return ReadByte();
        }

        /// <summary>
        /// Read signed 16-bit integer from the stream
        /// </summary>
        public short ReadS16()
        {
            return ToStreamEndian(ReadInt16());
        }

        /// <summary>
        /// Read unsigned 16-bit integer from the stream
        /// </summary>
        public ushort ReadU16()
        {
            return ToStreamEndian(ReadUInt16());
        }

        /// <summary>
        /// Read signed 32-bit integer from the stream
        /// </summary>
        public int ReadS32()
        {
            return ToStreamEndian(ReadInt32());
        }

        /// <summary>
        /// Read unsigned 32-bit integer from the stream
        /// </summary>
        public uint ReadU32()
        {
            return ToStreamEndian(ReadUInt32());
        }

        /// <summary>
        /// Read signed 64-bit integer from the stream
        /// </summary>
        public long ReadS64()
        {
            return ToStreamEndian(ReadInt64());
        }

        /// <summary>
        /// Read unsigned 64-bit integer from the stream
        /// </summary>
        public ulong ReadU64()
        {
            return ToStreamEndian(ReadUInt64());
        }

        /// <summary>
        /// Read float value from the stream
        /// </summary>
        public float ReadFloat()
        {
            return ToStreamEndian(ReadSingle());
        }

        /// <summary>
        /// Read double value from the stream
        /// </summary>
        public override double ReadDouble()
        {
            return ToStreamEndian(base.ReadDouble());
        }

        /// <summary>
        /// Read string of specified size from stream
        /// </summary>
        /// <param name="sz">String size</param>
        public string ReadSizeString(int sz)
        {
            return new string(ReadChars(sz));
        }

        /// <summary>
        /// Convert value to the endianness of the stream
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="val">Value</param>
        /// <returns>Value represented in stream's endianness</returns>
        private T ToStreamEndian<T>(T val) where T : unmanaged
        {
            return m_Endian == EndianUtil.Kind.Big
                ? EndianUtil.ToBigEndian(val)
                : EndianUtil.ToLittleEndian(val);
        }
    }
}
