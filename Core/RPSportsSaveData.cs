using WSSE.IO;

namespace WSSE.Core
{
    internal class RPSportsSaveData
    {
        // The player list can only hold 100 entries
        public static readonly SByte scPlayerListSize = 100;

        // Common save data
        private RPSportsCommonData m_CmnData;
        // Player list
        private RPSportsPlayerData[] m_PlayerList;

        public RPSportsSaveData()
        {
            m_CmnData = new RPSportsCommonData();
            m_PlayerList = new RPSportsPlayerData[scPlayerListSize];
        }

        /// <summary>
        /// Fill in structure from stream data
        /// </summary>
        /// <param name="strm">Stream</param>
        public void Read(BinaryReaderEx strm)
        {
            // Validate save file
            if (strm.ReadSizeString(8) != "RPSP0000")
            {
                throw new InvalidOperationException("Save file is not valid.");
            }

            m_CmnData.Read(strm);
            for (int i = 0; i < scPlayerListSize; i++)
            {
                m_PlayerList[i].Read(strm);
            }
        }
    }
}
