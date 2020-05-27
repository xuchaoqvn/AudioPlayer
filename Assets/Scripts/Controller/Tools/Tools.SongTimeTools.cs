using UnityEngine;
using System.Text;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        /// <summary>
        /// 歌曲时间工具
        /// </summary>
        internal static class SongTimeTools
        {
            private const char ZERO = '0';
            private const char COLON = ':';

            /// <summary>
            /// 检测歌曲歌词播放时间
            /// </summary>
            /// <param name="currentSongTime">当前歌曲播放时间</param>
            /// <param name="nextLyricTime">下一句歌词时间</param>
            /// <returns></returns>
            internal static bool MonitorSongTime(float currentSongTime, float nextLyricTime)
            {
                return currentSongTime >= nextLyricTime;
            }

            /// <summary>
            /// 当前歌曲是否播放完成
            /// </summary>
            /// <param name="currentSongTime">当前歌曲播放时间</param>
            /// <param name="songLength">歌曲总时长</param>
            /// <returns></returns>
            internal static bool IsSongOver(float currentSongTime, float songLength)
            {
                return Mathf.Abs(songLength - currentSongTime) <= Time.fixedDeltaTime;
            }

            /// <summary>
            /// 歌曲时间转分秒
            /// </summary>
            /// <param name="length">歌曲时长</param>
            /// <returns></returns>
            internal static string SongTimeToStringTime(float length)
            {
                int minute = (int)length / 60;
                int seconds = (int)length % 60;

                StringBuilder stringBuilder = new StringBuilder(4);
                if (minute < 10)
                {
                    stringBuilder.Append(ZERO);
                    stringBuilder.Append(minute);
                }
                else
                    stringBuilder.Append(minute);
                stringBuilder.Append(COLON);

                if (seconds < 10)
                {
                    stringBuilder.Append(ZERO);
                    stringBuilder.Append(seconds);
                }
                else
                    stringBuilder.Append(seconds);
                return stringBuilder.ToString();
            }

            /// <summary>
            /// 歌词时间间隔
            /// </summary>
            /// <param name="currentLyricTime">当前歌词时间</param>
            /// <param name="nextLyricTime">下一句歌词时间</param>
            /// <returns></returns>
            internal static float LyricIntervalTime(float currentLyricTime, float nextLyricTime)
            {
                if (nextLyricTime <= currentLyricTime)
                    return 0.0f;
                return nextLyricTime - currentLyricTime;
            }
        }
    }
}
