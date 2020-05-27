using System;
using System.Collections.Generic;
using AudioPlayer.Model;

namespace AudioPlayer.Controller
{
    /// <summary>
    /// 解析歌词
    /// </summary>
    internal static class ParseLyric
    {
        private const string AR = "ar";
        private const string TI = "ti";
        private const string AL = "al";
        private const string LEFTSQUAREBRZCKETS = "[";
        private const char RIGHTSQUAREBRZCKETS = ']';
        private const string LINE = "\r\n";

        /// <summary>
        /// 解析函数
        /// </summary>
        /// <param name="content">文件绝对路径</param>
        internal static LyricInfo ParseLyricFunc(string content)
        {
            if (string.IsNullOrEmpty(content))
                return null;
            string[] lyricsTemp = ParseLyric.SplitString(content);
            string singerNameTemp = string.Empty;
            string songNameTemp = string.Empty;
            string albumTemp = string.Empty;
            List<Lyric> lyrics = new List<Lyric>();

            Lyric lyricTemp = new Lyric();
            int leftTag;
            int rightTag;
            string timeTemp;
            string[] arrayTime;
            float temp;
            bool isResult;

            for (int i = 0; i < lyricsTemp.Length; i++)
            {
                //歌手
                if (lyricsTemp[i].Contains(AR))
                    singerNameTemp = lyricsTemp[i].Substring(4, lyricsTemp[i].Length - 5);
                //歌名
                else if (lyricsTemp[i].Contains(TI))
                    songNameTemp = lyricsTemp[i].Substring(4, lyricsTemp[i].Length - 5);
                //专辑
                else if (lyricsTemp[i].Contains(AL))
                    albumTemp = lyricsTemp[i].Substring(4, lyricsTemp[i].Length - 5);
                //歌词
                else if (lyricsTemp[i].Contains(LEFTSQUAREBRZCKETS))
                {
                    leftTag = lyricsTemp[i].IndexOf(LEFTSQUAREBRZCKETS);
                    rightTag = lyricsTemp[i].IndexOf(RIGHTSQUAREBRZCKETS);
                    if (lyricsTemp[i].Length - 1 <= rightTag)
                        continue;

                    timeTemp = lyricsTemp[i].Substring(leftTag + 1, rightTag - (leftTag + 1));
                    arrayTime = new string[3];
                    arrayTime[0] = timeTemp.Substring(0, 2);
                    arrayTime[1] = timeTemp.Substring(3, 2);
                    arrayTime[2] = timeTemp.Substring(6, 2);
                    lyricTemp = new Lyric();

                    //分
                    isResult = float.TryParse(arrayTime[0], out temp);
                    lyricTemp.lyricTime += isResult ? temp * 60f : 0f;

                    //秒
                    isResult = float.TryParse(arrayTime[1], out temp);
                    lyricTemp.lyricTime += isResult ? temp : 0f;

                    //毫秒
                    isResult = float.TryParse(arrayTime[2], out temp);
                    lyricTemp.lyricTime += isResult ? temp * 0.01f : 0f;

                    if (lyricsTemp[i].Length <= rightTag + 1)
                        lyricTemp.lyricContent = string.Empty;
                    else
                        lyricTemp.lyricContent = lyricsTemp[i].Substring(rightTag + 1, lyricsTemp[i].Length - (rightTag + 1));
                    lyrics.Add(lyricTemp);
                }
            }

            LyricInfo lyricInfo = new LyricInfo(singerNameTemp, songNameTemp, albumTemp, lyrics);
            return lyricInfo;
        }

        /// <summary>
        /// 分割字符串（换行符）
        /// </summary>
        /// <param name="content">字符串</param>
        /// <returns></returns>
        private static string[] SplitString(string content)
        {
            return content.Split(Environment.NewLine.ToCharArray());//new string[] { LINE }, StringSplitOptions.None);
        }
    }
}
