namespace AudioPlayer.Controller
{
    /// <summary>
    /// 播放类型
    /// </summary>
    internal enum CycleType
    {
        /// <summary>
        /// 单曲循环
        /// </summary>
        SingleCycle = 0,

        /// <summary>
        /// 顺序播放
        /// </summary>
        PlayInOrder,

        /// <summary>
        /// 循环播放
        /// </summary>
        LoopPlayback,

        /// <summary>
        /// 随机播放
        /// </summary>
        RandomPlay,

        /// <summary>
        /// 单曲播放
        /// </summary>
        SinglePlay
    }
}
