using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AudioPlayer.Model
{
    /// <summary>
    /// 场景数据
    /// </summary>
    internal class ScenesDatas
    {
        #region 字段
        /// <summary>
        /// 音源
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// 视频播放器
        /// </summary>
        private VideoPlayer videoPlayer;

        /// <summary>
        /// 音频数据材质球
        /// </summary>
        private Material audioDataMaterial;

        /// <summary>
        /// 音频数据
        /// </summary>
        private Transform[] audioDatas;

        /// <summary>
        /// 鼠标尾迹
        /// </summary>
        private Transform trailRenderer;

        /// <summary>
        /// 歌词文本集合
        /// </summary>
        private Text[] lyricItems;

        /// <summary>
        /// 歌曲名称
        /// </summary>
        private Text songName;

        /// <summary>
        /// 歌曲总时长
        /// </summary>
        private Text songTotalTime;

        /// <summary>
        /// 歌曲播放时长
        /// </summary>
        private Text songPlayingTime;

        /// <summary>
        /// 歌曲时间进度
        /// </summary>
        private Slider songTimeSlide;
        #endregion

        #region 属性
        /// <summary>
        /// 音源
        /// </summary>
        internal AudioSource AudioSource
        {
            set { this.audioSource = value; }
            get { return this.audioSource; }
        }

        /// <summary>
        /// 视频播放器
        /// </summary>
        internal VideoPlayer VideoPlayer
        {
            set { this.videoPlayer = value; }
            get { return this.videoPlayer; }
        }

        /// <summary>
        /// 音频数据材质球
        /// </summary>
        internal Material AudioDataMaterial
        {
            set { this.audioDataMaterial = value; }
            get { return this.audioDataMaterial; }
        }

        /// <summary>
        /// 音频可视化数据
        /// </summary>
        internal Transform[] AudioDatas
        {
            set { this.audioDatas = value; }
            get { return this.audioDatas; }
        }

        /// <summary>
        /// 鼠标尾迹
        /// </summary>
        internal Transform TrailRenderer
        {
            set { this.trailRenderer = value; }
            get { return this.trailRenderer; }
        }

        /// <summary>
        /// 歌词文本
        /// </summary>
        internal Text[] LyricItems
        {
            set { this.lyricItems = value; }
            get { return this.lyricItems; }
        }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        internal Text SongName
        {
            set { this.songName = value; }
            get { return this.songName; }
        }

        /// <summary>
        /// 歌曲总时长
        /// </summary>
        internal Text SongTotalTime
        {
            set { this.songTotalTime = value; }
            get { return this.songTotalTime; }
        }

        /// <summary>
        /// 歌曲播放时长
        /// </summary>
        internal Text SongPlayingTime
        {
            set { this.songPlayingTime = value; }
            get { return this.songPlayingTime; }
        }

        /// <summary>
        /// 歌曲时间进度
        /// </summary>
        internal Slider SongTimeSlide
        {
            set { this.songTimeSlide = value; }
            get { return this.songTimeSlide; }
        }
        #endregion
    }
}