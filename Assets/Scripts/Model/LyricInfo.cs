using System.Collections.Generic;

namespace AudioPlayer.Model
{
    /// <summary>
    /// 歌词信息
    /// </summary>
    internal class LyricInfo
    {
        #region 字段
        /// <summary>
        /// 歌手
        /// </summary>
        internal string singerName;

        /// <summary>
        /// 歌名
        /// </summary>
        internal string songName;

        /// <summary>
        /// 专辑
        /// </summary>
        internal string album;

        /// <summary>
        /// 歌曲信息
        /// </summary>
        internal List<Lyric> lyrics;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="singerName">歌手名称</param>
        /// <param name="songName">歌曲名称</param>
        /// <param name="album">专辑</param>
        /// <param name="lyrics">歌词</param>
        internal LyricInfo(string singerName, string songName, string album, List<Lyric> lyrics)
        {
            this.singerName = singerName;
            this.songName = songName;
            this.album = album;
            this.lyrics = lyrics;
        }
        #endregion

        public override string ToString()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(this.singerName))
                stringBuilder.AppendLine("歌手：" + this.singerName);
            if (!string.IsNullOrEmpty(this.songName))
                stringBuilder.AppendLine("歌曲名称：" + this.songName);
            if (!string.IsNullOrEmpty(this.album))
                stringBuilder.AppendLine("专辑：" + this.album);
            for (int i = 0; i < this.lyrics.Count; i++)
                stringBuilder.AppendLine(this.lyrics[i].lyricContent);
            return stringBuilder.ToString();
        }
    }
}