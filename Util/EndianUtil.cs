namespace WSSE.Util
{
    internal static class EndianUtil
    {
        /// <summary>
        /// Endianness type
        /// </summary>
        public enum Kind
        {
            Little,
            Big
        };

        /// <summary>
        /// Get endianness of system
        /// </summary>
        /// <returns>EndianKind enum value</returns>
        public static Kind GetSystemEndian()
        {
            return BitConverter.IsLittleEndian
                ? Kind.Little
                : Kind.Big;
        }

        /// <summary>
        /// Convert value to little endian
        /// </summary>
        /// <typeparam name="T">Type of value (must be primitive type)</typeparam>
        /// <param name="val">Original value</param>
        /// <returns>Value represented in little endian</returns>
        public static T ToLittleEndian<T>(T val) where T : unmanaged
        {
            return GetSystemEndian() == Kind.Little ? val : SwapEndian(val);
        }

        /// <summary>
        /// Convert value to big endian
        /// </summary>
        /// <typeparam name="T">Type of value (must be primitive type)</typeparam>
        /// <param name="val">Original value</param>
        /// <returns>Value represented in big endian</returns>
        public static T ToBigEndian<T>(T val) where T : unmanaged
        {
            return GetSystemEndian() == Kind.Big ? val : SwapEndian(val);
        }

        /// <summary>
        /// Swap endianness of data
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="val">value</param>
        /// <returns>Value represented in opposite endian</returns>
        private unsafe static T SwapEndian<T>(T val) where T : unmanaged
        {
            // Only primitive types are supported
            if (!typeof(T).IsPrimitive)
            {
                throw new ArgumentException("Type is not a primitive type");
            }

            // Value is too small to swap
            if (sizeof(T) < 2)
            {
                return val;
            }

            // Swap bytes
            switch (typeof(T).FullName)
            {
                case "System.Int16":
                    return (T)(object)BitConverter.ToInt16(
                        BitConverter.GetBytes((Int16)(object)val).Reverse().ToArray()
                        );
                case "System.UInt16":
                    return (T)(object)BitConverter.ToUInt16(
                        BitConverter.GetBytes((UInt16)(object)val).Reverse().ToArray()
                        );
                case "System.Int32":
                    return (T)(object)BitConverter.ToInt32(
                        BitConverter.GetBytes((Int32)(object)val).Reverse().ToArray()
                        );
                case "System.UInt32":
                    return (T)(object)BitConverter.ToUInt32(
                        BitConverter.GetBytes((UInt32)(object)val).Reverse().ToArray()
                        );
                case "System.Int64":
                    return (T)(object)BitConverter.ToInt64(
                        BitConverter.GetBytes((Int64)(object)val).Reverse().ToArray()
                        );
                case "System.UInt64":
                    return (T)(object)BitConverter.ToUInt64(
                        BitConverter.GetBytes((UInt64)(object)val).Reverse().ToArray()
                        );
                case "System.Single":
                    return (T)(object)BitConverter.ToSingle(
                        BitConverter.GetBytes((Single)(object)val).Reverse().ToArray()
                        );
                case "System.Double":
                    return (T)(object)BitConverter.ToDouble(
                        BitConverter.GetBytes((Double)(object)val).Reverse().ToArray()
                        );
                default:
                    throw new ArgumentException("Primitive type is not supported");
            }
        }
    }
}
