namespace AudioPlayer.Controller
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    internal enum GameState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 0,

        /// <summary>
        /// 待机
        /// </summary>
        StandBy,

        /// <summary>
        /// 播放中
        /// </summary>
        Playing,

        /// <summary>
        /// 暂停
        /// </summary>
        Pause,

        /// <summary>
        /// 加载资源中
        /// </summary>
        Loading
    }
}
