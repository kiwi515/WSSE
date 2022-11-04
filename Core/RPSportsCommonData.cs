using WSSE.IO;

namespace WSSE.Core
{
    /// <summary>
    /// Common data in the Wii Sports save file
    /// </summary>
    internal class RPSportsCommonData
    {
        // There exists mii history for each possible player count
        public static readonly Int32 scMiiHistorySize = 1 + 2 + 3 + 4;

        // Mii history data
        private MiiHistory[] m_MiiHistory;
        // Total Wii Fitness tests taken across all Miis
        private Byte m_TotalFitnessTests;
        // Last fitness test by any Mii
        private UInt16 m_LastFitnessTestTime;
        //
        private UInt16 SHORT_0x4A;
        //
        private Byte BYTE_0x4C;
        // Common bitfield, primarily used for training game unlocks
        private UInt32 m_Flags;
        //
        private UInt32 WORD_0x54;

        /// <summary>
        /// List of all training games
        /// </summary>
        public enum TrainingGame
        {
            BOX_THROWING_PUNCHES,
            BOX_DODGING,
            BOX_WORKING_THE_BAG,

            GOL_TARGET_PRACTICE,
            GOL_HITTING_THE_GREEN,
            GOL_PUTTING,

            BOW_SPIN_CONTROL,
            BOW_POWER_THROWS,
            BOW_PICKING_UP_SPARES,

            BSB_BATTING_PRACTICE,
            BSB_SWING_CONTROL,
            BSB_HITTING_HOME_RUNS,

            TNS_TARGET_PRACTICE,
            TNS_TIMING_YOUR_SWING,
            TNS_RETURNING_BALLS
        };

        /// <summary>
        /// Mii history structure, used to auto-select the last Miis
        /// you played with (depending on player count)
        /// </summary>
        public struct MiiHistory
        {
            // Size of remote info data
            private static readonly Int32 scRemoteInfoSize = 6;

            // Previous Mii selected (Index into player list)
            // -1 if unused
            private SByte m_PrevMiiIndex;
            // Used to identify the Wii Remote previously used
            private Byte[] m_RemoteInfo;

            /// <summary>
            /// Fill in structure from stream data
            /// </summary>
            /// <param name="strm">Stream</param>
            public void Read(BinaryReaderEx strm)
            {
                m_PrevMiiIndex = strm.ReadS8();
                m_RemoteInfo = strm.ReadBytes(scRemoteInfoSize);
            }

            public SByte GetPrevMiiIndex()
            {
                return m_PrevMiiIndex;
            }

            public void SetPrevMiiIndex(SByte index)
            {
                // Index should not be > 100 (player list size)
                m_PrevMiiIndex = Math.Min(index,
                    RPSportsSaveData.scPlayerListSize);
            }

            public Byte[] GetRemoteInfo()
            {
                return m_RemoteInfo;
            }

            public void SetRemoteInfo(Byte[] info)
            {
                // Only copy the 6 bytes
                Array.Copy(info, m_RemoteInfo, scRemoteInfoSize);
            }
        };

        public RPSportsCommonData()
        {
            m_MiiHistory = new MiiHistory[scMiiHistorySize];
        }

        /// <summary>
        /// Fill in structure from stream data
        /// </summary>
        /// <param name="strm">Stream</param>
        public void Read(BinaryReaderEx strm)
        {
            for (int i = 0; i < scMiiHistorySize; i++)
            {
                m_MiiHistory[i].Read(strm);
            }

            m_TotalFitnessTests = strm.ReadByte();
            m_LastFitnessTestTime = strm.ReadU16();

            SHORT_0x4A = strm.ReadU16();
            BYTE_0x4C = strm.ReadU8();

            m_Flags = strm.ReadU32();
            WORD_0x54 = strm.ReadU32();
        }
    }
}
