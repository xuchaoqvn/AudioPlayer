using UnityEngine;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        /// <summary>
        /// 音频数据
        /// </summary>
        internal static class AudioSourceData
        {
            /// <summary>
            /// 获取当前歌曲播放时间
            /// </summary>
            /// <param name="audioSource">AudioSource声音源</param>
            /// <returns></returns>
            internal static float GetCurrentSongTime(AudioSource audioSource)
            {
                if (audioSource == null && audioSource.clip == null)
                    return 0.0f;
                return audioSource.time;
            }

            /// <summary>
            /// 获取当前歌曲总时长
            /// </summary>
            /// <param name="audioSource">AudioSource声音源</param>
            /// <returns></returns>
            internal static float GetCurrentSongLength(AudioSource audioSource)
            {
                if (audioSource == null && audioSource.clip == null)
                    return 0.0f;
                return audioSource.clip.length;
            }

            /// <summary>
            /// 获取音频数据变化
            /// </summary>
            /// <param name="audioSource">AudioSource声音源</param>
            /// <param name="samples">音频数据，只能是2的倍数</param>
            internal static void GetCurrentSongData(AudioSource audioSource, float[] samples)
            {
                if (audioSource == null && audioSource.clip == null)
                    return;
                audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
            }
        }
    }
}