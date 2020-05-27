using UnityEngine;
using System.Text;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        /// <summary>
        /// ����ʱ�乤��
        /// </summary>
        internal static class SongTimeTools
        {
            private const char ZERO = '0';
            private const char COLON = ':';

            /// <summary>
            /// ��������ʲ���ʱ��
            /// </summary>
            /// <param name="currentSongTime">��ǰ��������ʱ��</param>
            /// <param name="nextLyricTime">��һ����ʱ��</param>
            /// <returns></returns>
            internal static bool MonitorSongTime(float currentSongTime, float nextLyricTime)
            {
                return currentSongTime >= nextLyricTime;
            }

            /// <summary>
            /// ��ǰ�����Ƿ񲥷����
            /// </summary>
            /// <param name="currentSongTime">��ǰ��������ʱ��</param>
            /// <param name="songLength">������ʱ��</param>
            /// <returns></returns>
            internal static bool IsSongOver(float currentSongTime, float songLength)
            {
                return Mathf.Abs(songLength - currentSongTime) <= Time.fixedDeltaTime;
            }

            /// <summary>
            /// ����ʱ��ת����
            /// </summary>
            /// <param name="length">����ʱ��</param>
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
            /// ���ʱ����
            /// </summary>
            /// <param name="currentLyricTime">��ǰ���ʱ��</param>
            /// <param name="nextLyricTime">��һ����ʱ��</param>
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
