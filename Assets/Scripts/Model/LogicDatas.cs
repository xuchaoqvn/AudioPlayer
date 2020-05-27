using  AudioPlayer.Controller;

namespace AudioPlayer.Model
{
    /// <summary>
    /// 逻辑数据
    /// </summary>
    internal class LogicDatas
    {
        #region 字段
        /// <summary>
        /// 游戏状态
        /// </summary>
        private GameState gameState;

        /// <summary>
        /// 歌单
        /// </summary>
        private string[] songsName;

        /// <summary>
        /// 歌词集合
        /// </summary>
        private LyricInfo lyricInfo;

        /// <summary>
        /// 播放类型
        /// </summary>
        private CycleType cycleType;

        /// <summary>
        /// 当前播放歌词索引
        /// </summary>
        private int index;

        /// <summary>
        /// 计时器
        /// </summary>
        private float timer = 0f;

        /// <summary>
        /// 当前歌曲索引
        /// </summary>
        private int currentSongIndex;
        #endregion

        #region 属性
        /// <summary>
        /// 游戏状态
        /// </summary>
        internal GameState GameState
        {
            set { this.gameState = value; }
            get { return this.gameState; }
        }

        /// <summary>
        /// 歌单
        /// </summary>
        internal string[] SongsName
        {
            set { this.songsName = value; }
            get { return this.songsName; }
        }


        /// <summary>
        /// 歌词集合
        /// </summary>
        internal LyricInfo LyricInfo
        {
            set { this.lyricInfo = value; }
            get { return this.lyricInfo; }
        }

        /// <summary>
        /// 播放类型
        /// </summary>
        internal CycleType CycleType
        {
            set { this.cycleType = value; }
            get { return this.cycleType; }
        }

        /// <summary>
        /// 当前播放歌词索引
        /// </summary>
        internal int Index
        {
            set { this.index = value; }
            get { return this.index; }
        }

        /// <summary>
        /// 计时器
        /// </summary>
        internal float Timer
        {
            set { this.timer = value; }
            get { return this.timer; }
        }

        /// <summary>
        /// 当前歌曲索引
        /// </summary>
        internal int CurrentSongIndex
        {
            set { this.currentSongIndex = value; }
            get { return this.currentSongIndex; }
        }
        #endregion
    }
}
