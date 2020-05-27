using UnityEngine;

namespace AudioPlayer.Controller
{
    internal static partial class Tools
    {
        /// <summary>
        /// ��Ƶ����
        /// </summary>
        internal static class AudioSourceData
        {
            /// <summary>
            /// ��ȡ��ǰ��������ʱ��
            /// </summary>
            /// <param name="audioSource">AudioSource����Դ</param>
            /// <returns></returns>
            internal static float GetCurrentSongTime(AudioSource audioSource)
            {
                if (audioSource == null && audioSource.clip == null)
                    return 0.0f;
                return audioSource.time;
            }

            /// <summary>
            /// ��ȡ��ǰ������ʱ��
            /// </summary>
            /// <param name="audioSource">AudioSource����Դ</param>
            /// <returns></returns>
            internal static float GetCurrentSongLength(AudioSource audioSource)
            {
                if (audioSource == null && audioSource.clip == null)
                    return 0.0f;
                return audioSource.clip.length;
            }

            /// <summary>
            /// ��ȡ��Ƶ���ݱ仯
            /// </summary>
            /// <param name="audioSource">AudioSource����Դ</param>
            /// <param name="samples">��Ƶ���ݣ�ֻ����2�ı���</param>
            internal static void GetCurrentSongData(AudioSource audioSource, float[] samples)
            {
                if (audioSource == null && audioSource.clip == null)
                    return;
                audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
            }
        }
    }
}